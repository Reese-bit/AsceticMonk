using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class EW_AttackByNormalShoot : EW_Projectile
{
    public float attackTime;
    
    private bool isOver;

    private Tween startTween;
    private Tween overTween;

    public override void OnStart()
    {
        startTween = DOVirtual.DelayedCall(0f, StartNormalShoot, false);
    }

    public override TaskStatus OnUpdate()
    {
        return isOver ? TaskStatus.Success : TaskStatus.Running;
    }

    void StartNormalShoot()
    {
        PoolManager.Release(_hitVFX, transform.position,
            quaternion.LookRotation(player.transform.position, Vector3.up));

        overTween = DOVirtual.DelayedCall(attackTime, () => isOver = true, false);
    }

    public override void OnEnd()
    {
        isOver = false;
        startTween?.Kill();
        overTween?.Kill();
    }
}
