using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

public class EW_Idle : BT_EvilWizard
{
    public string idleName;
    public float idleTime;

    private bool isIdle;

    private Tween idleTween;
    
    public override void OnStart()
    {
        idleTween = DOVirtual.DelayedCall(idleTime, () => isIdle = true, false);
        anim.SetInteger(idleName,0);
    }

    public override TaskStatus OnUpdate()
    {
        return isIdle ? TaskStatus.Success : TaskStatus.Running;
    }

    public override void OnEnd()
    {
        isIdle = false;
        idleTween?.Kill();
    }
}
