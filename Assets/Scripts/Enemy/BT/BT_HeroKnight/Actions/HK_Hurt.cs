using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

public class HK_Hurt : BT_HeroKnight
{
    public string hurtTriggerName;
    public float hurtTime;
    public SharedBool isHurt;
    
    private bool isHurting;

    private Tween hurtTween;

    public override void OnStart()
    {
        hurtTween = DOVirtual.DelayedCall(hurtTime, StartHurt, false);
        anim.SetTrigger(hurtTriggerName);
        boss.TakeDamage(playerWeapon.hitDamage);
        Debug.Log("HK_Hurt OnStart");
    }

    public override TaskStatus OnUpdate()
    {
        isHurt = isHurting;
        return isHurting ? TaskStatus.Success : TaskStatus.Running;
    }

    public void StartHurt()
    {
        isHurting = true;
    }

    public override void OnEnd()
    {
        hurtTween?.Kill();
        isHurting = false;
        isHurt = false;
    }
}
