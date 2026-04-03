using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : MonoBehaviour
{
    private GameObject _point;
    void Start()
    {
        _point = GameObject.FindGameObjectWithTag("Point");
    }
    void Update()
    {
        if (gameObject.transform.position.x != _point.transform.position.x || gameObject.transform.position.z != _point.transform.position.z)
        {
            gameObject.transform.position = new Vector3(_point.transform.position.x,gameObject.transform.position.y, _point.transform.position.z);
        }  
    }


}
