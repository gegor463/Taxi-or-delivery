using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _gameMenu;
    private Button _newGameButton;
    private Button _continueButton;
    private Button _exitButton;
    private Button _homeButton;

    private void OnEnable()
    {
        ButtonSearch();    
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_gameMenu.activeInHierarchy)
            {      
                UnityEngine.Cursor.lockState = CursorLockMode.Confined;
                Time.timeScale = 0.0f;
                _gameMenu.SetActive(true);
                ButtonSearch();
            }
            else
            {
                Time.timeScale = 1.0f;
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                _gameMenu.SetActive(false);
            }

        }
    }

    void OnDisable()
    {
        if (_newGameButton != null) _continueButton.clicked -= OnNewGamePressed;
        if (_continueButton != null) _continueButton.clicked -= OnContinuePressed;
        if (_exitButton != null) _exitButton.clicked -= OnExitPressed;
        if (_homeButton != null) _homeButton.clicked -= OnHomePressed;
    }


    private void OnNewGamePressed()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnContinuePressed()
    {
        _gameMenu.SetActive(false);
        Time.timeScale = 1.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnExitPressed()
    {
        Application.Quit();
    }

    private void OnHomePressed()
    {
        SceneManager.LoadScene("StartScene");
    }

    private void ButtonSearch()
    {
        var root = _gameMenu.GetComponent<UIDocument>().rootVisualElement;
        _newGameButton = root.Q<Button>("new_game_button");
        _continueButton = root.Q<Button>("continue_button");
        _exitButton = root.Q<Button>("exit_button");
        _homeButton = root.Q<Button>("home_button");

        _newGameButton.clicked += OnNewGamePressed;
        _continueButton.clicked += OnContinuePressed;
        _exitButton.clicked += OnExitPressed;
        _homeButton.clicked += OnHomePressed;
    }

}
