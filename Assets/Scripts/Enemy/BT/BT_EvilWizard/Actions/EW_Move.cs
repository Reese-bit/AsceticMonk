using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

public class EW_Move : BT_EvilWizard
{
    public string moveName;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("将速度增大，变成 dash")]
    public float speed;  // 将速度增大，变成 dash
    public float moveTime;

    private Vector2 target;
    private Vector2 newPos;
    private bool isMove;

    private Tween runTween;
    private Tween runningTween;

    public override void OnStart()
    {
        runTween = DOVirtual.DelayedCall(0f, StartMove, false);
        anim.SetInteger(moveName,1);
    }

    public override TaskStatus OnUpdate()
    {
        return isMove ? TaskStatus.Success : TaskStatus.Running;
    }

    void StartMove()
    {
        var dir = player.transform.position.x > transform.position.x ? 1 : -1;
        //rb.AddForce(new Vector2(speed * dir,transform.position.y),ForceMode2D.Force);
        var velocity = rb.velocity;
        velocity = new Vector2((velocity.x + speed) * dir, velocity.y);
        rb.velocity = velocity;

        runningTween = DOVirtual.DelayedCall(moveTime, () => { isMove = true; }, false);
    }

    public override void OnEnd()
    {
        isMove = false;
        runTween?.Kill();
        runningTween?.Kill();
    }
}
