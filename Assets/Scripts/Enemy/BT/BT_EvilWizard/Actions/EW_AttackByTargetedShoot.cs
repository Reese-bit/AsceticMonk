using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EW_AttackByTargetedShoot : EW_Projectile
{
    private bool isOver;

    public override TaskStatus OnUpdate()
    {
        return isOver ? TaskStatus.Success : TaskStatus.Running;
    }
}
