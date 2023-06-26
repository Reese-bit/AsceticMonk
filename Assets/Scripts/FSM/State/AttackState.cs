using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private FSM manager;
    private Parameter parameter;

    public AttackState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    
    public void OnEnter()
    {
        parameter.animator.Play("Attack");
    }

    public void OnUpdate()
    {
        parameter.rb.velocity = new Vector2(-manager.player.transform.localScale.x * parameter.attackSpeed, parameter.rb.velocity.y);
        Debug.Log("Attack Successful");
        
        if (parameter.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= parameter.animator.GetCurrentAnimatorStateInfo(0).length)
        {
            manager.TransitionState(StateType.Idle);
        }
    }

    public void OnExit()
    {
        parameter.timeSinceAttack = 0f;
    }
}
