using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuShopScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _trunkCapacityText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _costOfCarText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Button _improveButton;
    private int _attempt = 2;
    private int _cost = 20;
    private int _costOfCar = 400;
    private CarMovement _carMovement;
    private int _money;
    private int _maxCount;

    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _selectButton;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _startSceneButton;


    [SerializeField] private List<GameObject> _cars;
    private int _carIndex = 0;
    private Transform _spawnManager;
    private GameObject _currentCar;
    private DBConnection _dbConnection;
    
    private CarsModel _carsModel;
    private string _nameOfCar;
    private int _selectIndexCar;

    void Start()
    {

        _money = PlayerPrefs.GetInt("Money", 0);
        _money = 1000000;
        _maxCount = PlayerPrefs.GetInt("MaxCount", 4);
        TextsUpdate();
        Button improveButton = _improveButton.GetComponent<Button>();
        improveButton.onClick.AddListener(ImproveButton);
        Button leftButton = _leftButton.GetComponent<Button>();
        leftButton.onClick.AddListener(LeftButton);
        Button rightButton = _rightButton.GetComponent<Button>();
        rightButton.onClick.AddListener(RightButton);
        Button buyButton = _buyButton.GetComponent<Button>();
        buyButton.onClick.AddListener(BuyButton);
        Button selectButton = _selectButton.GetComponent<Button>();
        selectButton.onClick.AddListener(SelectButton);
        Button startSceneButton = _startSceneButton.GetComponent<Button>();
        startSceneButton.onClick.AddListener(StartSceneButton);
        Debug.Log(PlayerPrefs.GetInt("SelectCar", 5));
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<Transform>();
        _currentCar = _cars[0];
        //_selectIndexCar = PlayerPrefs.GetInt("SelectCar", 0);
        Instantiate(_currentCar, _spawnManager.position, _spawnManager.rotation, _spawnManager);
        
        _dbConnection = new DBConnection();
        _dbConnection.ExecuteQueryWithoutAnswer("");

        _costOfCarText.enabled = false;
        _costText.enabled = true;

        _cost = _dbConnection.GetDataFromCars("CostOfUpgrade",gameObject.name);
    }
  
    private void ImproveButton()
    {
        if (_money >= _cost)
        { 
            _money -= _cost;
            PlayerPrefs.SetInt("Money", _money);
            _maxCount += 1;
            PlayerPrefs.SetInt("MaxCount", _maxCount);
            _cost *= _attempt;
            PlayerPrefs.SetInt("Cost", _cost);
            PlayerPrefs.Save();
            TextsUpdate();
        }   
    }

    private void BuyButton()
    {
        if ( _money >= _costOfCar)
        {
            try
            {
                string query = $"UPDATE Cars SET IsPurchasedCar = 1 WHERE NameOfCar = '{_nameOfCar}'";
                _dbConnection.ExecuteQueryWithoutAnswer(query);
                _money -= _costOfCar;
                PlayerPrefs.SetInt("Money", _money);
                PlayerPrefs.Save();
                TextsUpdate();
                _selectButton.gameObject.SetActive(true);
                _buyButton.gameObject.SetActive(false);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);     
            }
        }
    }

    private void SelectButton()
    {
        _selectButton.interactable = false;
        PlayerPrefs.SetInt("SelectCar", _carIndex);
        PlayerPrefs.SetString("SelectCarName", _nameOfCar);
        Debug.Log(PlayerPrefs.GetString("SelectCarName"));
        Debug.Log(PlayerPrefs.GetInt("SelectCar", 0));
        PlayerPrefs.Save();
    }

    private void TextsUpdate()
    {
        _moneyText.text = "" + _money;
        _costText.text = "Cost: " + _cost;
        _trunkCapacityText.text = "Trunk capacity: " + _maxCount;
    }

    private void LeftButton()
    {

        GameObject spawnedCar = GameObject.FindGameObjectWithTag("Car");
        Destroy(spawnedCar);
        if (_carIndex -  1 >= 0)
        {
            _carIndex--;
            ReplaceMenu();          
        }

        else 
        {
            _carIndex = 2;
            ReplaceMenu();
        }
    }

    private void RightButton()
    {
        GameObject spawnedCar = GameObject.FindGameObjectWithTag("Car");
        Destroy(spawnedCar);
        
        if (_carIndex + 1 < _cars.Count)
        {  
            _carIndex++;
            ReplaceMenu();
        }
        else
        {
            _carIndex = 0;
            ReplaceMenu();
        }
    }

    private void Text()
    {
        if (_carIndex == 0)
        {
            _nameText.text = "Car: Pizza Car";
            _improveButton.enabled = true;
            _trunkCapacityText.text = "Trunk capacity: " + _maxCount;
        }
        if (_carIndex == 1)
        {
            _nameText.text = "Car: Taxi Car";
            _trunkCapacityText.text = "Trunk capacity: " + 3;
        }
        if (_carIndex == 2)
        {
            _nameText.text = "Car: HotDog Car";
            _trunkCapacityText.text = "Trunk capacity: " + 8;
        }

    }

    private void ReplaceMenu()
    {
        _currentCar = _cars[_carIndex];
        Instantiate(_currentCar, _spawnManager.position, _spawnManager.rotation, _spawnManager);
        string nameOfCar = _currentCar.name;
        nameOfCar = nameOfCar.Replace("(Clone)", "");
        _nameOfCar = nameOfCar;
        int isPurchasedCar = _dbConnection.GetDataFromCars("IsPurchasedCar", nameOfCar);
        _cost = _dbConnection.GetDataFromCars("CostOfUpgrade", nameOfCar);
        Text();

        if (isPurchasedCar == 0)
        {
            _costOfCarText.enabled = true;
            _costText.enabled = false;
            _costOfCar = _dbConnection.GetDataFromCars("CostOfCar", nameOfCar);
            _costOfCarText.text = "Cost of car: " + _costOfCar;
            _selectButton.gameObject.SetActive(false);
            _buyButton.gameObject.SetActive(true);
        }

        else if (isPurchasedCar == 1)
        {
            _costOfCarText.enabled = false;
            _costText.enabled = true;
            _costText.text = "Cost: " + _cost;
            _improveButton.enabled = true;
            _buyButton.gameObject.SetActive(false);
            Debug.Log(_carIndex);
            Debug.Log(PlayerPrefs.GetInt("SelectCar"));
            if (_carIndex == PlayerPrefs.GetInt("SelectCar", 0))
            {
                _selectButton.interactable = false;
            }
            else
            {
                _selectButton.interactable = true;
            }

        }
    }

    private void StartSceneButton()
    {
        SceneManager.LoadScene("StartScene");
    }



}
