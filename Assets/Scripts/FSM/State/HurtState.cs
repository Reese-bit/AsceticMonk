using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : IState
{
    private FSM manager;
    private Parameter parameter;

    public HurtState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    
    public void OnEnter()
    {
        parameter.animator.Play("Hurt");
        //parameter.isHurt = true;
    }

    public void OnUpdate()
    {
        if (manager.bossWeapon.isBeingAttacked || parameter.isHurt)
        {
            manager.TransitionState(StateType.Hurt);
            //manager.player.TakeDamage(parameter.damageFromEnemy);
        }
    }

    public void OnExit()
    {
        parameter.isHurt = false;
    }
}
