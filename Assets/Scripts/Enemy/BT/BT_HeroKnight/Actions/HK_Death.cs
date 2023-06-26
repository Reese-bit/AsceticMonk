using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class HK_Death : BT_HeroKnight
{
    public string deathTriggerName;

    public override void OnStart()
    {
        anim.SetTrigger(deathTriggerName);
        boss.Die();
    }

    public override TaskStatus OnUpdate()
    {
        return base.OnUpdate();
    }

    public void StartDeath()
    {
        
    }

    public override void OnEnd()
    {
        
    }
}
