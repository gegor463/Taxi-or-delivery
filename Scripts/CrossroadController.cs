using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossroadController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _wayPoints;

    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            _wayPoints[i] = gameObject.transform.GetChild(i).gameObject;
        }

    }

    private Transform GetRandomWayPoint()
    {
        if (_wayPoints.Count <= 0)
            return null;
        else
            return _wayPoints[Random.Range(0,_wayPoints.Count)].transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        foreach (var wayPoint in _wayPoints)
        {
            if (wayPoint != null)
            {
                Gizmos.DrawSphere(wayPoint.transform.position, 0.5f);
                Gizmos.DrawRay(wayPoint.transform.position, wayPoint.transform.forward * 3);
            }
        }
    }
}
