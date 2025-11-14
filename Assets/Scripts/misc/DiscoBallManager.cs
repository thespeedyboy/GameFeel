using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DiscoBallManager : MonoBehaviour
{
    [SerializeField] private float _discoBallPartyTime = 2f;
    [SerializeField] private Light2D _globalLight;
    [SerializeField] private float _globalLightIntensity = 0.2f;


    private float _originalGlobalLightIntensity;
    private Coroutine _DiscoCorotine;
    private static Action OnDiscoBallHitEvent;

    private ColorSpotLight[] _colorSpotLights;
    private void Awake()
    {
        _originalGlobalLightIntensity = _globalLight.intensity;
    }
    private void Start()
    {
       _colorSpotLights = FindObjectsByType<ColorSpotLight>(FindObjectsSortMode.None);
    }
    private void OnEnable()
    {
        OnDiscoBallHitEvent += DimTheLights;
    }
    public void DiscoBallParty()
    {
        if(_DiscoCorotine != null) { return; }
        OnDiscoBallHitEvent?.Invoke();
    }
    private void OnDisable()
    {
        OnDiscoBallHitEvent -= DimTheLights;
    }
    private void DimTheLights()
    {
        foreach(ColorSpotLight spotLight in _colorSpotLights)
        {
           StartCoroutine(spotLight.SpotLightDiscoParty(_discoBallPartyTime));
        }
        _DiscoCorotine = StartCoroutine(GlobalLightResetRoutine());
    }
    private IEnumerator GlobalLightResetRoutine()
    {
        _globalLight.intensity = _globalLightIntensity;
        yield return new WaitForSeconds(_discoBallPartyTime);
        _globalLight.intensity = _originalGlobalLightIntensity;
        _DiscoCorotine = null;
    }
}
