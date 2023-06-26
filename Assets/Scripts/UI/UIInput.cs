using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class UIInput : Singleton<UIInput>
{
    private InputSystemUIInputModule inputSystemUIInputModule;
    protected override void Awake()
    {
        base.Awake();
        inputSystemUIInputModule = GetComponent<InputSystemUIInputModule>();
        inputSystemUIInputModule.enabled = false;
    }

    // have a choose when enter the UI, suitable for GamePad
    public void SelectUI(Selectable UIObject)
    {
        UIObject.Select();
        UIObject.OnSelect(null);
        //GameManager.GameState = GameState.Playing;
        inputSystemUIInputModule.enabled = true;
    }

    public void DisableAllUIInputs()
    {
        //GameManager.GameState = GameState.Playing;
        inputSystemUIInputModule.enabled = false;
    }
}