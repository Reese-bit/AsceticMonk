using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

[CreateAssetMenu(menuName = "PlayerInput")]
public class PlayerInput : ScriptableObject,InputActions.IGamePlayActions
{
    private InputActions inputActions;
    public event UnityAction<Vector2> onMove = delegate {};
    public event UnityAction onStopMove = delegate {}; 
    public event UnityAction onAttack = delegate {};
    public event UnityAction onJump = delegate {};
    
    private void OnEnable() 
    {
        inputActions = new InputActions();

        inputActions.GamePlay.SetCallbacks(this);
    }

    private void OnDisable()
    {
        DisableAllInput();
    }
    
    void SwitchActionMap(InputActionMap inputActionMap,bool isUIInput)
    {
        inputActions.Disable();
        inputActionMap.Enable();

        if (isUIInput)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void SwitchDynamicUpdateMode()
    {
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
    }

    public void SwitchFixedUpdateMode()
    {
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
    }

    //diaable all input when play some animations which is unstoppable
    public void DisableAllInput() => inputActions.Disable();

    public void EnableGamePlayInput() => SwitchActionMap(inputActions.GamePlay,false);

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onMove.Invoke(context.ReadValue<Vector2>());
        }

        if (context.canceled)
        {
            onStopMove.Invoke();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onAttack.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onJump.Invoke();
        }
    }
}
