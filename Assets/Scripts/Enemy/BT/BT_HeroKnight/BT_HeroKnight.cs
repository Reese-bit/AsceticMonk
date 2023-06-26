using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using UnityEngine;

public class BT_HeroKnight : Action
{
    protected Rigidbody2D rb;
    protected Animator anim;
    protected Transform player;
    
    protected NewBoss boss;
    protected PlayerWeapon playerWeapon;

    public override void OnAwake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        boss = GetComponent<NewBoss>();
        playerWeapon = GameObject.FindObjectOfType<PlayerWeapon>().GetComponent<PlayerWeapon>();
    }
}
