using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using System.Threading;
using System.Threading.Tasks;
//[RequireComponent (typeof(NavMeshAgent))]
public class MovementForward : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _rotationSpeed = 5.0f;
    private bool _isTouchedWithCrossroad = false;
    private Transform _targetWaypoint;
    private Rigidbody _rb;
    private Quaternion _targetRotation;
    private float _raycastDistance = 10.0f;
    private Color _rayColor = Color.red;
    [SerializeField] private float _minRaycastDistance = 5.0f;
    [SerializeField] private float _maxRaycastDistance = 12.0f;
    private TrafficLightController _trafficLightController;
    private bool _canGo = true;
    private bool _isAbleToMove = true;
    private bool _needToDestroy = false;
    private GameObject _player;
    [SerializeField] private float _maxDistance = 70.0f;
    async Task Start()
    {
        _raycastDistance = Random.Range(_minRaycastDistance, _maxRaycastDistance);

        _rb = GetComponent<Rigidbody>();
        _targetRotation = transform.rotation;
        await Task.Delay(100);
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (_needToDestroy)
        {
            float distanceBetweenPlayerAndTraffic = Vector3.Distance(transform.position, _player.transform.position);
            if (this != null || this)
            {
                if (distanceBetweenPlayerAndTraffic > _maxDistance)
                {
                    Destroy(gameObject);
                }
            }

        }
        bool isTouchedTraffic = false;
        GameObject traffic = GameObject.FindGameObjectWithTag("Traffic");
        Vector3 rayStartPosition = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
        Ray ray = new Ray(rayStartPosition, transform.forward);
        Debug.DrawRay(rayStartPosition, transform.forward * _raycastDistance, _rayColor);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _raycastDistance))
        {
            if (hit.collider.CompareTag("Traffic") || hit.collider.CompareTag("Player"))
            {
                isTouchedTraffic = true; 
            }
        }
        if (!_isTouchedWithCrossroad && !isTouchedTraffic && _isAbleToMove && _canGo)
        {
            _rb.MovePosition(transform.position + transform.forward * _speed * Time.fixedDeltaTime);
            _rb.MoveRotation(_targetRotation);
            if (_targetWaypoint != null)
            {
                //_navMeshAgent.SetDestination();
            }
        }
        else if (_targetWaypoint != null && !isTouchedTraffic && _isAbleToMove)
        { 
            Vector3 direction = (_targetWaypoint.position - transform.position).normalized;
            _rb.MovePosition(transform.position + direction * _speed * Time.fixedDeltaTime);

            Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            _rb.MoveRotation(Quaternion.Slerp(transform.rotation, lookRotation, _rotationSpeed * Time.fixedDeltaTime));
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crossroad") && !_isTouchedWithCrossroad)
        {
            Transform crossroad = other.transform;
            if (crossroad.childCount > 0)
            {
                int randomIndex = Random.Range(0, crossroad.childCount);
                _targetWaypoint = crossroad.GetChild(randomIndex);
                _isTouchedWithCrossroad = true;
            }
        }
        else if (other.CompareTag("WayPoint") && _isTouchedWithCrossroad)
        {
            
            _isTouchedWithCrossroad = false;
            _targetWaypoint = null;
            
            Vector3 euler = other.transform.rotation.eulerAngles;
            _targetRotation = Quaternion.Euler(0, euler.y, 0);

            transform.position = new Vector3(other.transform.position.x,transform.position.y, other.transform.position.z);
        }

        else if (other.CompareTag("Deleter"))
        {
            Destroy(gameObject);
        }


        

        // else if (other.CompareTag("WaypointDouble") && !other.CompareTag("Crossroad"))
        // {
        //     _targetRotation = other.gameObject.transform.rotation;
        //     //_rb.MovePosition(transform.position + transform.forward * _speed * Time.fixedDeltaTime);
        //     //_rb.MoveRotation(_targetRotation);
        // }
    }
    private async void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Traffic") || collision.gameObject.CompareTag("Player"))
        {
            _isAbleToMove = false;
            await Task.Delay(5000);
            _needToDestroy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WayPoint") && _isTouchedWithCrossroad)
        {
            
            _isTouchedWithCrossroad = false;
            _targetWaypoint = null;
            
            Vector3 euler = other.transform.rotation.eulerAngles;
            _targetRotation = Quaternion.Euler(0, euler.y, 0);
        }    
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("TrafficSignalReader"))
        {
            _trafficLightController = other.gameObject.GetComponentInParent<TrafficLightController>();
            if (_trafficLightController.isGreenActive)
            {
                _canGo = true;
            }
            else
            {
                _canGo = false;
            }
        }
        else
        {
            _canGo = true;
        }
    }
}

