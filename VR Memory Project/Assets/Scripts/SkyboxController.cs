using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public Material BlackSkybox;
    public Material StarfieldSkybox;
    public Material DefaultSkybox;

    [SerializeField] private Skybox _skybox;

    private void Start()
    {
        _skybox = GetComponent<Skybox>();
    }

    public void SkyboxStars()
    {
        _skybox.material = StarfieldSkybox;
    }

    public void SkyboxBlack()
    {
        _skybox.material = BlackSkybox;
    }

    public void SkyboxDefault()
    {
        _skybox.material = DefaultSkybox;
    }
}
