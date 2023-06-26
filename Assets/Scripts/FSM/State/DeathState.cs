using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IState
{
    private int d;
    private FSM manager;
    private Parameter parameter;
    
    private int[] arr = { };
    
    public DeathState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Death");
        parameter.isDeath = true;
    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        parameter.isDeath = false;
    }
}
