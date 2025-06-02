using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTrafficCar : MonoBehaviour
{
    [SerializeField] private List<GameObject> _cars;
    private int _carNumber = 0;
    void Start()
    {
        InvokeRepeating("SpawnCar", 10.0f, 10.0f);    
    }

    void Update()
    {
        
    }

    private void SpawnCar()
    {
        _carNumber = Random.Range(0, _cars.Count);
        Instantiate(_cars[_carNumber], transform.position, transform.rotation);
    }
}
