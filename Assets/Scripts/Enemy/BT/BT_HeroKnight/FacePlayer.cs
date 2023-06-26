using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class FacePlayer : BT_HeroKnight
{
    private float baseScaleX;

    public override void OnAwake()
    {
        base.OnAwake();
        baseScaleX = transform.localScale.x;
    }

    public override TaskStatus OnUpdate()
    {
        var scale = transform.localScale;
        scale.x = transform.position.x > player.transform.position.x ? -baseScaleX : baseScaleX;
        transform.localScale = scale;
        return TaskStatus.Success;
    }
}
