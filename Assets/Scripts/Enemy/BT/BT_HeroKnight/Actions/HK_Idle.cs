using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

public class HK_Idle : BT_HeroKnight
{
    public string idleTriggerName;
    public float idleTime;

    private bool isIdle;

    private Tween idleTween;
    
    public override void OnStart()
    {
        idleTween = DOVirtual.DelayedCall(idleTime, () => isIdle = true, false);
        anim.SetInteger(idleTriggerName,0);
    }

    public override TaskStatus OnUpdate()
    {
        return isIdle ? TaskStatus.Success : TaskStatus.Running;
    }
    
    
}
