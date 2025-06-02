using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    void Start()
    {
        MovementToRandomPosition();
    }

    void Update()
    {
        transform.Rotate(0,0.5f,0);   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Building"))
        {
            MovementToRandomPosition();    
        }

        

    }

    private void MovementToRandomPosition() 
    {
        float randomPositionX = Random.Range(-8.5f, 248.0f);
        float randomPositionZ = Random.Range(-7.5f, 222.0f);

        transform.position = new Vector3(randomPositionX, transform.position.y, randomPositionZ);
    
    
    }
}
