using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Assertions;

public class TeleportWithFade : TeleportationProvider
{
    public ScreenFader screenFader = null;
    public float fadeDuration = 0.5f;

    protected override void Update()
    {
        //Debug.Log("Running the override teleport with fade update method! And validRequest book is currently " + validRequest);

        if (!validRequest || !BeginLocomotion())
            return;

        //Debug.Log("Got through the first if statement.");

        var xrRig = system.xrRig;
        if (xrRig != null)
        {
            //Debug.Log("Starting the fade sequence coroutine. Prepare thyself.");
            //startCoroutine
            StartCoroutine(FadeSequence(xrRig));
        }

        EndLocomotion();

        validRequest = false;
    }

    IEnumerator FadeSequence(XRRig xrRig)
    {
        //Debug.Log("Starting fade sequence now.");
        // Fade to black
        screenFader.StartFadeIn();

        // Wait, then do the teleport stuff
        yield return new WaitForSeconds(fadeDuration);
        
        switch (currentRequest.matchOrientation)
        {
            case MatchOrientation.WorldSpaceUp:
                xrRig.MatchRigUp(Vector3.up);
                break;
            case MatchOrientation.TargetUp:
                xrRig.MatchRigUp(currentRequest.destinationRotation * Vector3.up);
                break;
            case MatchOrientation.TargetUpAndForward:
                xrRig.MatchRigUpCameraForward(currentRequest.destinationRotation * Vector3.up, currentRequest.destinationRotation * Vector3.forward);
                break;
            case MatchOrientation.None:
                // Change nothing. Maintain current rig rotation.
                break;
            default:
                Assert.IsTrue(false, $"Unhandled {nameof(MatchOrientation)}={currentRequest.matchOrientation}.");
                break;
        }

        var heightAdjustment = xrRig.rig.transform.up * xrRig.cameraInRigSpaceHeight;

        var cameraDestination = currentRequest.destinationPosition + heightAdjustment;

        xrRig.MoveCameraToWorldLocation(cameraDestination);

        // Fade to clear
        screenFader.StartFadeOut();
        //Debug.Log("done with fade sequence.");
    }
}
