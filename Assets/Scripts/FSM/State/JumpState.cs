using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IState
{
    private FSM manager;
    private Parameter parameter;

    public JumpState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    
    public void OnEnter()
    {
        parameter.animator.Play("Jump");
        parameter.isGround = false;
    }

    public void OnUpdate()
    {
        //isGround = false;
        //isJump = true;
        //Debug.Log("Jump OnUpdate");

        // 跳跃时松开跳跃键
        if (parameter.rb.velocity.y > 0 && !InputManager.Instance.Jump)
        {
            parameter.rb.velocity += Vector2.up * Physics2D.gravity.y * parameter.lowJumpMultiplier * Time.deltaTime;
        }

        // 跳到最高处速度为 0时
        if (parameter.rb.velocity.y < 0.0f)
        {
            manager.TransitionState(StateType.Fall);
        }
    }


    public void OnExit()
    {
        
    }
}
