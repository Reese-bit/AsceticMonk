using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class HK_isDeathNoBlood : BT_Conditionals
{
    public SharedBool isDeathNoBlood;  // 此变量值为GlobalValuable: isStageTwo
                                       // 当isDeathNoBlood(Action)被使用时，isStageTwo = true;

    private bool isDeath;

    public override TaskStatus OnUpdate()
    {
        isDeath = isDeathNoBlood.Value;
        return isDeath ? TaskStatus.Success : TaskStatus.Failure;
    }
}
