using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MenuSampleScene : MonoBehaviour
{
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _startSceneButton;
        private Canvas _canvas;
        [SerializeField] private GameObject _backgroundForMenuButtons;

        void Start()
        {
            _canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();

            Button newGameButton = _newGameButton.GetComponent<Button>();
            newGameButton.onClick.AddListener(NewGameButton);
            Button continueButton = _continueButton.GetComponent<Button>();
            continueButton.onClick.AddListener(ContinueButton);
            Button exitButton = _exitButton.GetComponent<Button>();
            exitButton.onClick.AddListener(ExitButton);
            Button startSceneButton = _startSceneButton.GetComponent<Button>();
            startSceneButton.onClick.AddListener(StartSceneButton);
        }   

        private void NewGameButton()
        {
            SceneManager.LoadScene("SampleScene");
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
        }
        private void ContinueButton()
        {
            _backgroundForMenuButtons.SetActive(false);
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
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
