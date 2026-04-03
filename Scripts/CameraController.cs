using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _carTransform;

    [SerializeField] private float _distance = 10.0f;
    [SerializeField] private float _speedByX = 250.0f;
    [SerializeField] private float _speedByY = 120.0f;

    [SerializeField] private float _borderYMin = 0.0f;
    [SerializeField] private float _borderYMax = 80.0f;

    [SerializeField] private float _angleX = 0.0f;
    [SerializeField] private float _angleY = 0.0f;

    [SerializeField] private GameObject _frontCamera;
    async void Start()
    {
        Vector3 angles = transform.eulerAngles;
        _angleX = angles.x;
        _angleY = angles.y;

        _carTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        _angleX += Input.GetAxis("Mouse X") * _speedByX * Time.deltaTime;
        _angleY -= Input.GetAxis("Mouse Y") * _speedByY * Time.deltaTime;

        _angleY = Mathf.Clamp(_angleY, _borderYMin, _borderYMax);

        Quaternion rotation = Quaternion.Euler(_angleY, _angleX, 0.0f);
        Vector3 deltaPositionCamera = new Vector3(_carTransform.position.x, _carTransform.position.y + 2.0f, _carTransform.position.z);
        Vector3 position = deltaPositionCamera - (rotation * Vector3.forward * _distance);

        transform.position = position;
        transform.rotation = rotation;

        
    }


}
