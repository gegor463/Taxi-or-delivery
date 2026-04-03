using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _exitButton;
    private Canvas _canvas;
    void Start()
    {

        _canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();

        Button newGameButton = _newGameButton.GetComponent<Button>();
        newGameButton.onClick.AddListener(NewGameButton);
        Button exitButton = _exitButton.GetComponent<Button>();
        exitButton.onClick.AddListener(ExitButton);
        Button shopButton = _shopButton.GetComponent<Button>();
        shopButton.onClick.AddListener(ShopButton);

    }

    private void NewGameButton()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1.0f;
    }

    private void ExitButton()
    {
        Application.Quit();
    }

    private void ShopButton()
    {
        SceneManager.LoadScene("ShopScene");
    }

}
