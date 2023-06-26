using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class EW_AttackByStorm : EW_Projectile
{
    public float stormTime;
    
    private bool isOver;

    private Tween stormTween;
    private Tween overTween;

    public override void OnStart()
    {
        stormTween = DOVirtual.DelayedCall(0f, StartStorm, false);
    }

    public override TaskStatus OnUpdate()
    {
        return isOver ? TaskStatus.Success : TaskStatus.Running;
    }

    void StartStorm()
    {
        PoolManager.Release(_hitVFX, transform.position, Quaternion.LookRotation(Vector3.up, Vector3.up));
        PoolManager.Release(_hitVFX, transform.position,
            quaternion.LookRotation(new Vector3(0.5f, 0.5f, 0.5f), Vector3.up));
        PoolManager.Release(_hitVFX, transform.position,
            quaternion.LookRotation(new Vector3(-0.5f, 0.5f, 0f), Vector3.up));

        overTween = DOVirtual.DelayedCall(stormTime, () => isOver = true, false);
    }

    public override void OnEnd()
    {
        isOver = false;
        stormTween?.Kill();
        overTween?.Kill();
    }
}
