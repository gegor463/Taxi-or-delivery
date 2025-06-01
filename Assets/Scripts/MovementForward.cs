using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementForward : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    private CrossroadController _crossroadController;
    private bool _isTouchedWithCrossroad = false;
    private GameObject _wayPoint;
    void Start()
    {
        _crossroadController = GameObject.FindGameObjectWithTag("Crossroad").GetComponent<CrossroadController>();     
    }

    void Update()
    {
        if (_isTouchedWithCrossroad == false)
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
        else
        {
            Vector3.MoveTowards(transform.position, _wayPoint.transform.position, _speed);
        } 
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject touchedGameObject = collision.gameObject;
        if (touchedGameObject.CompareTag("Crossroad"))
        {
            _wayPoint = touchedGameObject.transform.Find($"WayPoint ({Random.Range(1,3)})").gameObject;
            Debug.Log(_wayPoint.transform.rotation);
            _isTouchedWithCrossroad = true;
        }

        else if (touchedGameObject.CompareTag("WayPoint"))
        {
            //Debug.Log(touchedGameObject.transform.rotation.y);
            transform.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y + touchedGameObject.transform.rotation.y, transform.rotation.z);
        }
       
        

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Crossroad"))
        {
            _isTouchedWithCrossroad = false;
        }
    }
}

