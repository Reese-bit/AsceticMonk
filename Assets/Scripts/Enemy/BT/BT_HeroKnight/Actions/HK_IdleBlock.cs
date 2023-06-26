using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

public class HK_IdleBlock : BT_HeroKnight
{
    public string idleblockTriggerName;
    public float idleblockTime;

    private bool isIdleBlock;

    private Tween idleblockTween;

    public override void OnStart()
    {
        idleblockTween = DOVirtual.DelayedCall(idleblockTime, StartIdleBlock, false);
        anim.SetBool(idleblockTriggerName,true);
    }

    public override TaskStatus OnUpdate()
    {
        return isIdleBlock ? TaskStatus.Success : TaskStatus.Running;
    }

    public void StartIdleBlock()
    {
        isIdleBlock = true;
    }

    public override void OnEnd()
    {
        idleblockTween?.Kill();
        isIdleBlock = false;
    }
}
