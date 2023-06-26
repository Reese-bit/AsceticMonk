using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class BT_Conditionals : Conditional
{
    protected bool isGround;
    protected NewBoss boss;

    public override void OnAwake()
    {
        boss = GetComponent<NewBoss>();
    }
}
