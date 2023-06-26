using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

public class HK_DeathNoBlood : BT_HeroKnight
{
    public string deathnobloodTriggerName;
    public SharedBool isDeathNo;

    private bool isDeathNoBlood;

    private Tween deathnobloodTween;

    public override void OnStart()
    {
        deathnobloodTween = DOVirtual.DelayedCall(0f, StartDeathNoBlood, false);
        anim.SetBool(deathnobloodTriggerName,true);
        boss.Die();
    }

    public override TaskStatus OnUpdate()
    {
        //GlobalVariables.Instance.SetVariable("isStageTwo",isDeathNo);
        isDeathNo = isDeathNoBlood;
        return isDeathNoBlood ? TaskStatus.Success : TaskStatus.Failure;
    }

    public void StartDeathNoBlood()
    {
        isDeathNoBlood = true;
    }

    public override void OnEnd()
    {
        deathnobloodTween?.Kill();
        isDeathNoBlood = false;
        
        //TODO 对于在此死亡的时间，会有一个延迟
        anim.SetBool(deathnobloodTriggerName,false);
    }
}
