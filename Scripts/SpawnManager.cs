using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float _speed = 0.1f;
    void Start()
    {
        
    }
    void Update()
    {
        transform.Rotate(0, _speed, 0);   
    }
}
