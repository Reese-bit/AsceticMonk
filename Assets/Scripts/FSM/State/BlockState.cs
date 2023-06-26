using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : IState
{
    private FSM manager;
    private Parameter parameter;

    public BlockState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Block");
    }

    public void OnUpdate()
    {
        if (parameter.isBlock)
        {
            manager.TransitionState(StateType.Idle);
        }
    }

    public void OnExit()
    {
        parameter.isBlock = false;
    }
}
