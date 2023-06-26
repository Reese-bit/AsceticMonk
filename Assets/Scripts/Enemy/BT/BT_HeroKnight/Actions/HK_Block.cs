using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

public class HK_Block : BT_HeroKnight
{
    public string blockTriggerName;

    private bool isBLock;

    private Tween blockTween;

    public override void OnStart()
    {
        blockTween = DOVirtual.DelayedCall(0f, StartBlock, false);
        anim.SetTrigger(blockTriggerName);
    }

    public override TaskStatus OnUpdate()
    {
        return isBLock ? TaskStatus.Success : TaskStatus.Running;
    }

    public void StartBlock()
    {
        isBLock = true;
    }

    public override void OnEnd()
    {
        blockTween?.Kill();
        isBLock = false;
    }
}
