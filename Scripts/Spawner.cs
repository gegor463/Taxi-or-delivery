using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _deliveryPoint;
    void Start()
    {
        Instantiate(_deliveryPoint,transform.position, transform.rotation);      
    }

    void Update()
    {
        
    }
}
