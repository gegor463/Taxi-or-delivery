using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossroadController : MonoBehaviour
{
    private Vector3 _wayPointPosition;

    private void Start()
    {
        Debug.Log(transform.childCount);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Traffic"))
        {
            _wayPointPosition = GameObject.Find($"WayPoint ({FindRandomWayPoint(transform.childCount + 1)})").transform.position;
        }     
    }

    private int FindRandomWayPoint(int maxWayPointsQuantity)
    {
        return Random.Range(1, maxWayPointsQuantity);
    }

    public Vector3 WayPointPosition
    {
        get
        {
            return _wayPointPosition;
        }
    }
}
