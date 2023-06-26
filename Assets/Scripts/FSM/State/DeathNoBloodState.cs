using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathNoBloodState : IState
{
    private int d;
    private FSM manager;
    private Parameter parameter;
    
    public DeathNoBloodState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("DeathNoBlood");
        parameter.isDeathNoBlood = true;
    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        parameter.isDeathNoBlood = false;
    }
}
