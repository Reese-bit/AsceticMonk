using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBlockState : IState
{
    private FSM manager;
    private Parameter parameter;

    private float idleBlockTime;

    public IdleBlockState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Idle Block");
        parameter.isGround = true;
        parameter.isIdleBlock = true;
        idleBlockTime = parameter.idleblockTime;
    }

    public void OnUpdate()
    {
        if (InputManager.Instance.IdleBlock)
        {
            manager.TransitionState(StateType.IdleBlock);
            idleBlockTime -= Time.deltaTime;
            parameter.runSpeed *= 0.5f;
            parameter.damageFromEnemy = 0f;
            if(manager.bossWeapon.isBeingAttacked || manager.enemy.isBeingTouched)
            {
                manager.TransitionState(StateType.Block);
                parameter.isBlock = true;
                idleBlockTime = 0.02f;
            }
        }
        
        if (idleBlockTime <= 0f)
        {
            manager.TransitionState(StateType.Idle);
        }
    }

    public void OnExit()
    {
        parameter.isIdleBlock = false;
    }
}
