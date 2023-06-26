using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

public class HK_Roll : BT_HeroKnight
{
    public string rollTriggerName;
    public float rollTime;
    public float rollSpeed;

    private bool isRoll;

    private Tween rollTween;
    private Tween rollingTween;
    
    public override void OnStart()
    {
        rollTween = DOVirtual.DelayedCall(0f, StartRoll, false);
        anim.SetTrigger(rollTriggerName);
    }

    public override TaskStatus OnUpdate()
    {
        return isRoll ? TaskStatus.Success : TaskStatus.Running;
    }

    public void StartRoll()
    {
        var dir = player.transform.position.x < transform.position.x ? 1 : -1;
        rb.AddForce(new Vector2(rollSpeed * dir,transform.position.y),ForceMode2D.Force);
        
        isRoll = true;
    }

    public override void OnEnd()
    {
        rollTween?.Kill();
        isRoll = false;
    }
}
