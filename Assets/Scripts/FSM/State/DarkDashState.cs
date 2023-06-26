using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkDashState : IState
{
    private FSM manager;
    private Parameter parameter;

    public DarkDashState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    
    public void OnEnter()
    {
        parameter.animator.Play("DarkDash");
        manager.player.GetComponent<Collider2D>().isTrigger = true;
    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        parameter.timeSinceDarkDash = 0f;
        manager.player.GetComponent<Collider2D>().isTrigger = false;
    }
}
