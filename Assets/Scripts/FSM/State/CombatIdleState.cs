using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatIdleState : IState
{
    private FSM manager;
    private Parameter parameter;
    
    public CombatIdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    
    public void OnEnter()
    {
        parameter.animator.Play("Combat Idle");
    }

    public void OnUpdate()
    {
        if (InputManager.Instance.CombatIdle)
        {
            manager.TransitionState(StateType.CombatIdle);
        }
    }

    public void OnExit()
    {
        
    }
}
