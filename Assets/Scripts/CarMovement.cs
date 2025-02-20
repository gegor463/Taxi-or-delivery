using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private List<WheelCollider> _wheelColliders;

    [SerializeField] private float _power = 10.0f;
    [SerializeField] private float _verticalInput = 0.0f;
    [SerializeField] private float _horizontalInput = 0.0f;
    [SerializeField] private float _maxAngleToTurn = 45.0f;

    private int _money = 0;

    [SerializeField] private GameObject _frontCamera;
    [SerializeField] private GameObject _mainCamera;

    
    void Update()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");

      

        for (int i = 0; i < 3; i++)
        {
            _wheelColliders[i].motorTorque = _verticalInput * _power;
        }

        float steerAngle = _horizontalInput * _maxAngleToTurn;

        
        for (int t = 0;t < 2;t++) 
        {
            _wheelColliders[t].steerAngle = steerAngle;
        }

        for (int f = 0; f < 3; f++)
        {
            UpdateWheelRotation(_wheelColliders[f], _wheelColliders[f].transform);
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            _mainCamera.SetActive(false);
            _frontCamera.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            _frontCamera.SetActive(false);
            _mainCamera.SetActive(true);
        }

    }

    private void UpdateWheelRotation(WheelCollider wheelCollider, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation);

        transform.position = position;
        transform.rotation = rotation;

    }
}
