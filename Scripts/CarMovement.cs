using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CarMovement : MonoBehaviour
{
    private MenuSampleScene _mScene;
    [SerializeField] private List<WheelCollider> _wheelColliders;

    [SerializeField] private float _power = 10.0f;
    [SerializeField] private float _verticalInput = 0.0f;
    [SerializeField] private float _horizontalInput = 0.0f;
    [SerializeField] private float _maxAngleToTurn = 45.0f;
    private int _money = 0;
    private int _maxCount = 4;

    private GameObject _frontCamera;
    private GameObject _mainCamera;

    private float _delayBeforeRespawn = 3.0f;
    private float _timeOfLastPressing = 0.0f;
    private bool _isPressing = false;
    private bool _isPressingCondition = false;


    private bool _isTouchWithDynamicMesh = false;

    private GameObject _dynamicMesh;

    private GameObject _backgroundForMenuButtons;

    private float _timer = 60.0f;
    private float _lastTimeDelivery = 0.0f;

    private Canvas _canvasRenderer;
    private async Task Start()
    {
        _money = PlayerPrefs.GetInt("Money", 0);
        _maxCount = PlayerPrefs.GetInt("MaxCount", 4);



        //_frontCamera = GameObject.FindGameObjectWithTag("FrontCamera");
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //await Task.Delay(100);
        _backgroundForMenuButtons = GameObject.FindGameObjectWithTag("BackgroundForMenuButtons");
        _backgroundForMenuButtons.SetActive(false);
    }
    void Update()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");
        try
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!_backgroundForMenuButtons.activeInHierarchy)
                {

                    Cursor.lockState = CursorLockMode.Confined;
                    Time.timeScale = 0.0f;
                    _backgroundForMenuButtons.SetActive(true);
                }
                else
                {
                    Time.timeScale = 1.0f;
                    Cursor.lockState = CursorLockMode.Locked;
                    _backgroundForMenuButtons.SetActive(false);
                }

            }
        }
        catch(System.Exception ex)
        {
            Debug.Log("Ошибка при попытке вывода _backgroundForMenuButtons: " + ex);
        }



        for (int i = 0; i < _wheelColliders.Count; i++)
        {
            _wheelColliders[i].motorTorque = _verticalInput * _power;
        }

        float steerAngle = _horizontalInput * _maxAngleToTurn;


        for (int t = 0; t < _wheelColliders.Count / 2; t++)
        {
            _wheelColliders[t].steerAngle = steerAngle;
        }

        for (int f = 0; f < _wheelColliders.Count; f++)
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

    public int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            PlayerPrefs.SetInt("Money", _money);
            PlayerPrefs.Save();
        }
    }

    public int MaxCount
    {
        get
        {
            return _maxCount;
        }
        set
        {
            _maxCount = value;
            PlayerPrefs.SetInt("MaxCount", _maxCount);
            PlayerPrefs.Save();
        }
    }

    public float Timer
    {
        get
        {
            return _timer;
        }

        set
        {
            _timer = value;
        }
    }

    public float LastTimeDelivery
    {
        get
        {
            return _lastTimeDelivery;
        }

        set
        {
            _lastTimeDelivery = value;
        }
    }
}

