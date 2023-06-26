using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class IdleState : IState
{
    private FSM manager;
    private Parameter parameter;


    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    
    public void OnEnter()
    {
        parameter.animator.Play("Idle");
        parameter.isGround = true;
        Debug.Log("Idle OnEnter");
    }

    public void OnUpdate()
    {
        if (!parameter.inputEnable)
        {
            return;
        }
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        if (Mathf.Abs(inputX) > 0.0f || Mathf.Abs(inputY) > 0.0f)
        {
            manager.TransitionState(StateType.Run);
        }
    }

    public void OnExit()
    {
        
    }
}
