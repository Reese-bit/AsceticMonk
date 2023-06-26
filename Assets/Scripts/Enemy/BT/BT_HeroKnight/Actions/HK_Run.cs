using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

public class HK_Run : BT_HeroKnight
{
    public string runTriggerName;
    public float speed;
    public float runTime;
    
    private Vector2 target;
    private Vector2 newPos;
    private bool isRun;

    private Tween runTween;
    private Tween runningTween;

    public override void OnStart()
    {
        runTween = DOVirtual.DelayedCall(0f, StartRun, false);
        anim.SetInteger(runTriggerName,1);
    }

    public override TaskStatus OnUpdate()
    {
        return isRun ? TaskStatus.Success : TaskStatus.Running;
    }

    public void StartRun()
    {
        // var position = rb.position;
        // target = new Vector2(player.position.x, position.y);
        // newPos = Vector2.MoveTowards(position, target, speed * Time.deltaTime);
        // rb.MovePosition(newPos);

        var dir = player.transform.position.x > transform.position.x ? 1 : -1;
        //rb.AddForce(new Vector2(speed * dir,transform.position.y),ForceMode2D.Force);
        var velocity = rb.velocity;
        velocity = new Vector2((velocity.x + speed) * dir, velocity.y);
        rb.velocity = velocity;

        runningTween = DOVirtual.DelayedCall(runTime, () => { isRun = true; }, false);
    }

    public override void OnEnd()
    {
        runTween?.Kill();
        runningTween?.Kill();
        isRun = false;
    }
}
