using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportAreaWithFadeUpdate : TeleportationArea
{
    
    public ScreenFader screenFader = null;

    public float fadeDuration = 0.5f;
    
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (teleportTrigger == TeleportTrigger.OnSelectEntered)
            StartCoroutine(FadeSequence(base.OnSelectEntered, args));
    }
    
    
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (teleportTrigger == TeleportTrigger.OnSelectExited)
            StartCoroutine(FadeSequence(base.OnSelectExited, args));
    }
    
    protected override void OnActivated(ActivateEventArgs args)
    {
        if (teleportTrigger == TeleportTrigger.OnActivated)
            StartCoroutine(FadeSequence(base.OnActivated, args));
    }
    
    
    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        if (teleportTrigger == TeleportTrigger.OnDeactivated)
            StartCoroutine(FadeSequence(base.OnDeactivated, args));
    }
    
    private IEnumerator FadeSequence<T>(UnityAction<T> action, T args)
        where T : BaseInteractionEventArgs
    {
        // Fade to black
        screenFader.StartFadeIn();
        //float duration = screenFader.StartFadeIn();

        // Wait, then do the teleport stuff
        yield return new WaitForSeconds(fadeDuration);
        action.Invoke(args);

        // Fade to clear
        screenFader.StartFadeOut();
    }

    private void OnValidate()
    {
        // Uhhh, let's get the screen fader like this
        if (!screenFader)
            screenFader = FindObjectOfType<ScreenFader>();
    }
    
}
