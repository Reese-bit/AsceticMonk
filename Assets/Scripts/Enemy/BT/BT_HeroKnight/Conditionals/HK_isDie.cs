using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class HK_isDie : BT_Conditionals
{
    private bool isDeath;

    public override void OnStart()
    {
        isDeath = false;
    }

    public override TaskStatus OnUpdate()
    {
        isDeath = StartDeath();
        return isDeath ? TaskStatus.Success : TaskStatus.Failure;
    }

    public bool StartDeath()
    {
        bool isTrue = boss.health <= 0f;

        return isTrue;
    }

    public override void OnEnd()
    {
        isDeath = false;
    }
}
