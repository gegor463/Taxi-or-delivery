using UnityEngine;

public class MovementForward : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _rotationSpeed = 5.0f;
    private bool _isTouchedWithCrossroad = false;
    private Transform _targetWaypoint;
    private Rigidbody _rb;
    private Quaternion _targetRotation;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _targetRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        GameObject traffic = GameObject.FindGameObjectWithTag("Traffic");

        if (!_isTouchedWithCrossroad /* && Vector3.Distance(transform.position, traffic.transform.position) > 20.0f*/)
        {
            _rb.MovePosition(transform.position + transform.forward * _speed * Time.fixedDeltaTime);
            _rb.MoveRotation(_targetRotation);
        }
        else if (_targetWaypoint != null)
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
}

