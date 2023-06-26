using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideState : IState
{
    private int d;
    private FSM manager;
    private Parameter parameter;
    
    private int[] arr = { };
    
    public WallSlideState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    
    public void OnEnter()
    {
        parameter.animator.Play("Wall Slide");
    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        //manager.getRandomTime = 0f;
    }
}
