using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;


public class MenuSampleScene : MonoBehaviour
{
        [SerializeField] private UnityEngine.UI.Button _newGameButton;
        [SerializeField] private UnityEngine.UI.Button _continueButton;
        [SerializeField] private UnityEngine.UI.Button _exitButton;
        [SerializeField] private UnityEngine.UI.Button _startSceneButton;
        private Canvas _canvas;
        [SerializeField] private GameObject _backgroundForMenuButtons;
        [SerializeField] private GameObject _gameMenu;
        
        void Start()
        {
            _canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();

            UnityEngine.UI.Button newGameButton = _newGameButton.GetComponent<UnityEngine.UI.Button>();
            newGameButton.onClick.AddListener(NewGameButton);
            UnityEngine.UI.Button continueButton = _continueButton.GetComponent<UnityEngine.UI.Button>();
            continueButton.onClick.AddListener(ContinueButton);
            UnityEngine.UI.Button exitButton = _exitButton.GetComponent<UnityEngine.UI.Button>();
            exitButton.onClick.AddListener(ExitButton);
            UnityEngine.UI.Button startSceneButton = _startSceneButton.GetComponent<UnityEngine.UI.Button>();
            startSceneButton.onClick.AddListener(StartSceneButton);
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
            }
            else
            {
                Time.timeScale = 1.0f;
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                _gameMenu.SetActive(false);
            }

        }
    }

    private void NewGameButton()
        {
            SceneManager.LoadScene("SampleScene");
            Time.timeScale = 1.0f;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
        private void ContinueButton()
        {
            _backgroundForMenuButtons.SetActive(false);
            Time.timeScale = 1.0f;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
        private void ExitButton()
        {
            Application.Quit();
        }

        private void StartSceneButton()
        {
            SceneManager.LoadScene("StartScene");
        }
}
