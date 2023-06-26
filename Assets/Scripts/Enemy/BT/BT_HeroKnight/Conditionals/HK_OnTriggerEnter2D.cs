using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class HK_OnTriggerEnter2D : Conditional
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

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(tag))
        {
            isEntered = true;
        }
    }

    public override void OnEnd()
    {
        isEntered = false;
    }
}
