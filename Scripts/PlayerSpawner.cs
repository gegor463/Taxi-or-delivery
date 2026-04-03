using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{  
    async Task Start()
    {
        GameObject _playerCar = Resources.Load<GameObject>(PlayerPrefs.GetString("SelectCarName"));
        Instantiate(_playerCar, transform.position, transform.rotation);      
    }
}
