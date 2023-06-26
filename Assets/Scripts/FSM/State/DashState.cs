using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : IState
{
    private FSM manager;
    private Parameter parameter;

    public DashState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    
    public void OnEnter()
    {
        parameter.animator.Play("Dash");
    }

    public void OnUpdate()
    {
        // float yVelocity = Input.GetAxisRaw("Horizontal");
        // var transform = manager.player.transform;
        // var position = transform.position;
        // Vector2.Lerp(position,
        //     new Vector2(position.x + parameter.dashDistance,
        //         position.y), parameter.dashTime);
    }

    public void OnExit()
    {
        parameter.timeSinceDash = 0f;
    }
}
