using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

public class HK_Attack1 : BT_HeroKnight
{
    public string attack1TriggerName;
    public float attack1Time;

    private bool isAttack1;

    private Tween attack1Tween;
    
    public override void OnStart()
    {
        attack1Tween = DOVirtual.DelayedCall(attack1Time, StartAttack1, false);
        anim.SetTrigger(attack1TriggerName);
    }

    public override TaskStatus OnUpdate()
    {
        return isAttack1 ? TaskStatus.Success : TaskStatus.Running;
    }

    public void StartAttack1()
    {
        isAttack1 = true;
    }

    public override void OnEnd()
    {
        attack1Tween?.Kill();
        isAttack1 = false;
    }
}
