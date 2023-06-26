using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

public class HK_Attack2 : BT_HeroKnight
{
    public string attack2TriggerName;
    private float attack2Time;

    private bool isAttack2;

    private Tween attack2Tween;
    
    public override void OnStart()
    {
        attack2Tween = DOVirtual.DelayedCall(attack2Time, StartAttack2, false);
        anim.SetTrigger(attack2TriggerName);
    }

    public override TaskStatus OnUpdate()
    {
        return isAttack2 ? TaskStatus.Success : TaskStatus.Running;
    }

    public void StartAttack2()
    {
        isAttack2 = true;
    }

    public override void OnEnd()
    {
        attack2Tween?.Kill();
        isAttack2 = false;
    }
}