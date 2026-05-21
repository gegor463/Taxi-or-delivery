using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    private int _nowCount = 0;
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private GameObject _earningTextGO;
    [SerializeField] private GameObject _earningTextContainer;
    [SerializeField] private GameObject _earningTextDetails;
    [SerializeField] private GameObject _earningDetail;
    private CarMovement _carMovement;
    private bool _exactTime = false;
    private int _total = 0;
    async Task Start()
    {
        await Task.Delay(100);
        _carMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CarMovement>();
        MovementToStartPosition();
        _moneyText.text = "" + PlayerPrefs.GetInt("Money", 0);
    }

    async Task Update()
    {
        if (Input.GetKey(KeyCode.Tab) && _exactTime && !_earningDetail.activeInHierarchy)
        {
            ShowDetails(10, _total);
        }

        else if (Input.GetKey(KeyCode.Tab) && _earningDetail.activeInHierarchy)
        {
            _earningDetail.SetActive(false);
        }
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
                    Debug.Log(_carMovement.BonusTime);
                    int baseIndex = 10;
                    int totalCost = 10;

                    if (_carMovement.LeftTime > 0.0f && _carMovement.BonusTime)
                    {
                        totalCost = (int)(baseIndex * 1.2f);
                        Debug.Log(totalCost);
                    }
                    else if(_carMovement.LeftTime > 30.0f && !_carMovement.BonusTime)
                    {
                        totalCost = (int)(baseIndex * 0.8f);
                        Debug.Log(totalCost);
                    }
                    _carMovement.Money += totalCost;
                    _total = totalCost;
                    StartCoroutine(TextCoroutine(totalCost));
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


    
    IEnumerator TextCoroutine(int totalCost)
    {
        _earningTextContainer.SetActive(true);
        _exactTime = true;
        _earningTextGO.GetComponent<TextMeshProUGUI>().text = "+" + totalCost;
        yield return new WaitForSeconds(3.0f);
        _exactTime = false;
        _earningTextContainer.SetActive(false);
    }

    private void ShowDetails(int baseIndex, int totalCost)
    {
        _earningDetail.SetActive(true);
        string firstPart = "baseIndex: " + "+" + baseIndex;
        firstPart = "<color=red>" + firstPart + "</color>";
        if (totalCost > baseIndex)
        {
            string secondPart = "bonus: " + "+ " + (totalCost - baseIndex);
            secondPart = "<color=blue>" + secondPart + "</color>";
            string thirdPart = "total: " + "+ " + totalCost;
            thirdPart = "<color=orange>" + thirdPart + "</color>";
            _earningTextDetails.GetComponent<TextMeshProUGUI>().text = firstPart + "                      " + secondPart + "                          " + thirdPart;
        }
        else if (totalCost == baseIndex)
        {
            string secondPart = "total: " + "+ " + totalCost;
            secondPart = "<color=orange>" + secondPart + "</color>";
            _earningTextDetails.GetComponent<TextMeshProUGUI>().text = firstPart + "                      " + secondPart;
        }
        else
        {
            string secondPart = "Fine: " + "- " + (baseIndex - totalCost);
            secondPart = "<color=blue>" + secondPart + "</color>";
            string thirdPart = "total: " + "+ " + totalCost;
            thirdPart = "<color=orange>" + thirdPart + "</color>";
            _earningTextDetails.GetComponent<TextMeshProUGUI>().text = firstPart + "                      " + secondPart + "                        " + thirdPart;
        }

    }
}