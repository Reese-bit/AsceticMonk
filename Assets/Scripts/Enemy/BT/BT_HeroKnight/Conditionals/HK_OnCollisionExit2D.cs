using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class HK_OnCollisionExit2D : Conditional
{
    public string tag;

    private bool isExited;

    public override void OnStart()
    {
        isExited = false;
    }

    public override TaskStatus OnUpdate()
    {
        return isExited ? TaskStatus.Success : TaskStatus.Failure;
    }

    public override void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            isExited = true;
        }
    }

    public override void OnEnd()
    {
        isExited = false;
    }
}
