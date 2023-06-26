using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUIController : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas optionsCanvas;
    
    [Header("Button")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button quitButton;

    private bool escPressed;
    
    private void Start()
    {
        optionsCanvas.enabled = false;
    }

    private void Update()
    {
        escPressed = Input.GetKeyDown(KeyCode.Escape);
        //PauseButtonClick();
        //BackButtonClick();
        Test();
    }

    void Test()
    {
        if (escPressed)
        {
            if (!optionsCanvas.enabled)
            {
                OnPauseButtonClick();
            }
            else if (optionsCanvas.enabled)
            {
                OnBackButtonClick();
            }
        }
    }

    public void PauseButtonClick()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPauseButtonClick();
        }
    }

    void OnPauseButtonClick()
    {
        Character.PauseGame();
        optionsCanvas.enabled = true;
        GameManager.GameState = GameState.Menu;
        Debug.Log("PauseButton is pressed");
        
        //TODO timeController
    }

    public void BackButtonClick()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && optionsCanvas.enabled)
        {
            OnBackButtonClick();
        }
    }

    void OnBackButtonClick()
    {
        Character.ResumeGame();
        optionsCanvas.enabled = false;
        GameManager.GameState = GameState.Playing;
        Debug.Log("BackButton is pressed");
    }

    public void OnQuitButtonClick()
    {
        SceneLoader.Instance.LoadMainMenu();
    }
}
