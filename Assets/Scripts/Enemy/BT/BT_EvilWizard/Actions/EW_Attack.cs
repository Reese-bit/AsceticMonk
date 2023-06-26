using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

public class EW_Attack : BT_EvilWizard
{
    public string attackName;
    public float attackTime;

    private bool isAttacking;

    private Tween attackTween;
    private Tween attackingTween;

    public override void OnStart()
    {
        attackTween = DOVirtual.DelayedCall(0f, StartAttack, false);
        anim.SetTrigger(attackName);
    }

    public override TaskStatus OnUpdate()
    {
        return isAttacking ? TaskStatus.Success : TaskStatus.Running;
    }

    void StartAttack()
    {
        attackingTween = DOVirtual.DelayedCall(attackTime, () => isAttacking = true, false);
    }

    public override void OnEnd()
    {
        isAttacking = false;
        attackTween?.Kill();
        attackingTween?.Kill();
    }
}
