using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : IState
{
    private FSM manager;
    private Parameter parameter;

    public FallState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    
    public void OnEnter()
    {
        parameter.animator.Play("Jump");
        parameter.isFall = true;
    }

    public void OnUpdate()
    {
        // 掉落时
        if (parameter.rb.velocity.y < 0)
        {
            parameter.rb.velocity += Vector2.up * Physics2D.gravity.y * parameter.fallMultiplier * Time.deltaTime;
            //anim.ResetTrigger("Jump");
        }

        if (parameter.isFall && InputManager.Instance.JumpKeyDown)
        {
            manager.TransitionState(StateType.Jump);
            // TODO play SFX

            parameter.rb.velocity = new Vector2(parameter.rb.velocity.x, parameter.jumpForce);
        }

        if (parameter.isFall && parameter.isGround)
        {
            manager.TransitionState(StateType.Idle);
        }

        // 掉落地面时
        // if (manager.player.GetComponent<Collider2D>().IsTouchingLayers(parameter.groundLayer))
        // {
        //     // TODO 检测落地时条件
        //     if (parameter.rb.velocity.y > 0f)
        //     {
        //         manager.TransitionState(StateType.Idle);
        //     }
        // }
    }

    public void OnExit()
    {
        parameter.isFall = false;
        parameter.isGround = true;
    }
}
