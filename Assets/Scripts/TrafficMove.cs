using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TrafficMove : MonoBehaviour
{
    [SerializeField] private List<GameObject> _wayPoints;
    private int _currentWayPoint = 0;
    [SerializeField] private float _speed = 10.0f;
    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Debug.Log(_wayPoints.Count);    
    }

    void Update()
    {
        if(Vector3.Distance(this.transform.position, _wayPoints[_currentWayPoint].transform.position) < 3.0f)
        {
            if(_currentWayPoint < _wayPoints.Count - 1)
            {
                _currentWayPoint++;
            }
            else
            {
                _currentWayPoint = 0;
            }
            Debug.Log(_currentWayPoint);
            
        }
   

        transform.LookAt(_wayPoints[_currentWayPoint].transform);
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
