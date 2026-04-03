using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField] private float _dayCycle = 30.0f;
    private Light _light;
    private Color _dayColor = new Color(1.0f, 0.95f, 0.84f);
    private Color _nightColor = new Color(0.1f, 0.1f, 0.3f);
    private AnimationCurve _animationCurve;
    void Start()
    {
        if (_animationCurve == null || _animationCurve.length == 0)
        {
            _animationCurve = new AnimationCurve(
                new Keyframe(0, 0),
                new Keyframe(0.25f, 0.25f),
                new Keyframe(0.3f, 0.3f),
                new Keyframe(0.7f, 0.7f),
                new Keyframe(0.75f,0.75f),
                new Keyframe(1.0f, 1.0f)
                );

            _animationCurve.SmoothTangents(2, 0.5f);
        }
        _light = gameObject.GetComponent<Light>();  
        
    }

    void Update()
    {
        float lightProgress = Mathf.PingPong(Time.time / _dayCycle, 1);
        _light.intensity = Mathf.SmoothStep(0, 1.5f, lightProgress);
        //_light.color = Mathf.SmoothStep()
        float colorIntensity = _animationCurve.Evaluate(lightProgress);
        _light.color = Color.Lerp(_nightColor, _dayColor, colorIntensity);
    }

    public float DayCycle
    {
        get
        {
            return _dayCycle;
        }
    }
}
