using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    private List<GameObject> _trafficLights = new List<GameObject>();
    private float timeCounter = 0.0f;
    [SerializeField] private float _changeMainSignalsTime = 10.0f;
    [SerializeField] private float _changeYellowSignalTime = 2.0f;

    [SerializeField]private bool _lastColorRed = true;
    [SerializeField]private bool _lastColorGreen = false;
    private bool _newApproach = false;
    public bool isGreenActive = false;
    void Start()
    {
        timeCounter = Time.realtimeSinceStartup;
        foreach (Transform child in transform)
        {
            _trafficLights.Add(child.gameObject);
        }
    }

    void Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter > _changeMainSignalsTime && _lastColorRed && !_lastColorGreen)
        {
            _trafficLights[2].GetComponent<Light>().enabled = false;
            _trafficLights[1].GetComponent<Light>().enabled = true;
            _lastColorRed = false;
            timeCounter = 0.0f;
        }
        else if (timeCounter > _changeYellowSignalTime && !_lastColorRed && !_lastColorGreen)
        {
            _trafficLights[1].GetComponent<Light>().enabled = false;
            _trafficLights[0].GetComponent<Light>().enabled = true;
            isGreenActive = true;
            _lastColorGreen = true;
            timeCounter = 0.0f;
        }
        else if (timeCounter > _changeMainSignalsTime && !_lastColorRed && _lastColorGreen)
        {
            _trafficLights[0].GetComponent<Light>().enabled = false;
            isGreenActive = false;
            _trafficLights[1].GetComponent<Light>().enabled = true;
            _lastColorGreen = false;
            _lastColorRed = true;
            timeCounter = 0.0f;
            _newApproach = true;
        }
        else if (timeCounter > _changeYellowSignalTime && _newApproach)
        {
            _trafficLights[1].GetComponent<Light>().enabled = false;
            _trafficLights[2].GetComponent<Light>().enabled = true;
            _newApproach = false;
        }

    }
}
