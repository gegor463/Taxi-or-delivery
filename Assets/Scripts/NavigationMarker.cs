using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationMarker : MonoBehaviour
{
    private Transform _deliveryPoint;
    void Start()
    {
        _deliveryPoint = GameObject.FindGameObjectWithTag("Point").GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 direction = _deliveryPoint.position - gameObject.transform.position;
        direction.y = 0;
        
        if (direction != Vector3.zero)
        {
            Quaternion deliveryPointRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, deliveryPointRotation, 5.0f * Time.deltaTime);
        }
        
        
    }
}
