using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 0.001f;
    [SerializeField] private GameObject _focusForSun;
    private GameMode _gameMode;
    private float _localDayCycle = 0.0f;
    void Start()
    {
        _gameMode = GameObject.FindGameObjectWithTag("DirectionalLight").GetComponent<GameMode>();  
        _localDayCycle = _gameMode.DayCycle;
        _rotationSpeed = 360 / _localDayCycle;
    }

    void Update()
    {
        transform.RotateAround(_focusForSun.transform.position, Vector3.right, _rotationSpeed * Time.deltaTime);
    }
}
