using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Threading.Tasks;
using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    private int _nowCount = 0;
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private TextMeshProUGUI _moneyText;
    private CarMovement _carMovement;
    async Task Start()
    {
        await Task.Delay(100);
        _carMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CarMovement>();
        MovementToStartPosition();
        _moneyText.text = "" + PlayerPrefs.GetInt("Money", 0);
    }

    async Task Update()
    {
        transform.Rotate(0, 0.5f, 0);
        await Task.Delay(100);
        _countText.text = "Pizza: " + _nowCount + "/" + _carMovement.MaxCount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Building"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (_nowCount > 0)
                {
                    int baseIndex = 10;
                    int totalCost = 5;
                    if (Time.time - _carMovement.LastTimeDelivery < _carMovement.Timer)
                    {
                        totalCost = (int)(baseIndex * 1.2f);
                        Debug.Log(totalCost);
                    }
                    else
                    {
                        totalCost = baseIndex;
                    }
                    _carMovement.Money += totalCost;
                    _moneyText.text = "" + _carMovement.Money;
                    _nowCount--;
                    if (_nowCount <= 0)
                    {
                        MovementToStartPosition();
                    }
                    else
                    {
                        MovementToRandomPosition();
                    }
                }
                else if (_nowCount == 0)
                {
                    MovementToRandomPosition();
                    _nowCount = _carMovement.MaxCount;
                    _carMovement.LastTimeDelivery = Time.time;
                }
            }
            if (collision.gameObject.CompareTag("Building"))
            {
                MovementToRandomPosition();
            }

        }
    }

    private void MovementToRandomPosition()
    {
        float randomPositionX = Random.Range(-8.5f, 248.0f);
        float randomPositionZ = Random.Range(-7.5f, 222.0f);

        transform.position = new Vector3(randomPositionX, transform.position.y, randomPositionZ);


    }

    private void MovementToStartPosition()
    {
        transform.position = _gameObject.transform.position;
    }
}