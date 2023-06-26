using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

public class HK_Attack3 : BT_HeroKnight
{
    public string attack3TriggerName;
    public float attack3Time;

    private bool isAttack3;

    private Tween attack3Tween;
    
    public override void OnStart()
    {
        attack3Tween = DOVirtual.DelayedCall(attack3Time, StartAttack3, false);
        anim.SetTrigger(attack3TriggerName);
    }

    public override TaskStatus OnUpdate()
    {
        return isAttack3 ? TaskStatus.Success : TaskStatus.Running;
    }

    public void StartAttack3()
    {
        isAttack3 = true;
    }

    public override void OnEnd()
    {
        attack3Tween?.Kill();
        isAttack3 = false;
    }
}