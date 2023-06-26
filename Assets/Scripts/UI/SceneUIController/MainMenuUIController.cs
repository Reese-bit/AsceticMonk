using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
//using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [Header("----CANVAS----")] 
    [SerializeField] private Canvas mainMenuCanvs;
    [SerializeField] private Canvas optionsCanvas;
    [SerializeField] private Canvas saveCanvas;
    
    [Header("----BUTTON----")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    private void OnEnable()
    {
        PlayButtonPressedBehaviour.playButtonFunctionTable.Add(playButton.gameObject.name,OnPlayButtonClick);
        PlayButtonPressedBehaviour.playButtonFunctionTable.Add(continueButton.gameObject.name,OnContinueButtonClick);
        PlayButtonPressedBehaviour.playButtonFunctionTable.Add(optionsButton.gameObject.name,OnOptionsButtonClick);
        PlayButtonPressedBehaviour.playButtonFunctionTable.Add(quitButton.gameObject.name,OnExitButtonClick);
    }

    private void OnDisable()
    {
        PlayButtonPressedBehaviour.playButtonFunctionTable.Clear();
        // ContinueButtonPressedBehaviour.continueButtonFunctionTable.Clear();
        // OptionsButtonPressedBehaviour.optionsButtonFunctionTable.Clear();
        // QuitButtonPressedBehaviour.quitButtonFunctionTable.Clear();
    }

    private void Start()
    {
        Time.timeScale = 1f;
        GameManager.GameState = GameState.Menu;
        //UIInput.Instance.SelectUI(playButton);
    }

    void OnPlayButtonClick()
    {
        // mainMenuCanvs.enabled = false;
        // SceneLoader.Instance.LoadRoom();
        optionsCanvas.enabled = false;
        saveCanvas.enabled = true;
    }

    void OnContinueButtonClick()
    {
        // read the file
        UIInput.Instance.SelectUI(continueButton);
    }

    void OnOptionsButtonClick()
    {
        //UIInput.instance.SelectUI(optionsButton);
        SceneLoader.Instance.LoadMenu();
    }

    void OnExitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}