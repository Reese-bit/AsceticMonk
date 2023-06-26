using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    private FSM manager;
    private Parameter parameter;

    public RunState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Run");
    }

    public void OnUpdate()
    {
        //newBoss.LookAtPlayer();

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        float yVelocity = Input.GetAxisRaw("Horizontal");

        Vector2 playerVel = new Vector2(inputX, inputY).normalized;
        parameter.rb.velocity = playerVel * parameter.runSpeed;
        if (yVelocity != 0)
        {
            manager.player.transform.localScale = new Vector3(-yVelocity, 1, 1);
        }

        if (parameter.rb.velocity == Vector2.zero)
        {
            manager.TransitionState(StateType.Idle);
        }

    }

    public void OnExit()
    {
        parameter.rb.velocity = Vector2.zero;
    }
}
