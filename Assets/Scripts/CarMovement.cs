using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

    private float _delayBeforeRespawn = 3.0f;
    private float _timeOfLastPressing = 0.0f;
    private bool _isPressing = false;
    private bool _isPressingCondition = false;


    private bool _isTouchWithDynamicMesh = false;

    private GameObject _dynamicMesh;

    void Update()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");

      

        for (int i = 0; i < 4; i++)
        {
            _wheelColliders[i].motorTorque = _verticalInput * _power;
        }

        float steerAngle = _horizontalInput * _maxAngleToTurn;

        
        for (int t = 0;t < 2;t++) 
        {
            _wheelColliders[t].steerAngle = steerAngle;
        }

        for (int f = 0; f < 4; f++)
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            _timeOfLastPressing = Time.time;
            _isPressing = true;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            _isPressing = false;
        }

        if (_isPressing && Time.time > _timeOfLastPressing + _delayBeforeRespawn)
        {
            _isPressingCondition = true;
            //gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            _isPressing = false;

            if (_isTouchWithDynamicMesh)
            {
                StartCoroutine(DelayBeforeDestroy());
            }
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DynamicMesh") && _isPressingCondition)
        {
            _dynamicMesh = collision.gameObject;
            _isTouchWithDynamicMesh = true;
        }   

        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("DynamicMesh") && !_isPressingCondition)
        {
            _isTouchWithDynamicMesh = false;
        }
    }



    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("PropsTraffic"))
        {
            collider.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }

    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("PropsTraffic"))
        {
            collider.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }

    }

    private IEnumerator DelayBeforeDestroy()
    {
        Vector3 startScale = _dynamicMesh.transform.localScale;
        float timer = 0.0f;
        float delay = 2.0f;

        while (timer < delay)
        {
            timer += Time.deltaTime;
            float progress = timer / delay;
            _dynamicMesh.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, progress);
        }
        //Destroy(_dynamicMesh);
        yield return null;
    }
}
