using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class HK_OnCollisionEnter2D : Conditional
{
    public string tag;
    public SharedBool isHurt;
    
    private bool isEntered;

    public override void OnStart()
    {
        isEntered = false;
    }

    public override TaskStatus OnUpdate()
    {
        isHurt = isEntered;
        return isEntered ? TaskStatus.Success : TaskStatus.Failure;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        // 不是Layer的问题，是Tag的问题
        if (collision.gameObject.CompareTag(tag))
        {
            isEntered = true;
        }
    }

    // OnEnd只会在返回Success或Failure时才会被调用
    // 在返回Running是不会被调用
    public override void OnEnd()
    {
        isEntered = false;
    }
}
