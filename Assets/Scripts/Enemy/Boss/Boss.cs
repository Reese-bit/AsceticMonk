using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    //[SerializeField] private BossHealthBar bossHealthBar;
    [SerializeField] public float speed = 4.0f;
    [SerializeField] private float jumpForce = 7.5f;
    [SerializeField] private float rollForce = 6.0f;
    [SerializeField] private bool noBlood = false;
    [SerializeField] private GameObject slideDust;
    
    private Canvas healthBarCanvas;
    private Animator anim;
    private Rigidbody2D rb;
    private Sensor_HeroKnight groundSensor;
    private Sensor_HeroKnight wallSensorR1;
    private Sensor_HeroKnight wallSensorR2;
    private Sensor_HeroKnight wallSensorL1;
    private Sensor_HeroKnight wallSensorL2;
    private bool WallSliding = false;
    private bool grounded = false;
    private bool rolling = false;
    private int facingDirection = 1;
    private int currentAttack = 0;
    private float timeSinceAttack = 0.0f;
    private float delayToIdle = 0.0f;
    private float rollDuration = 8.0f / 14.0f;
    private float rollCurrentTime;
    
    protected override void Awake()
    {
        //bossHealthBar = FindObjectOfType<EnemyStateBar_HUD>();
        //healthBarCanvas = bossHealthBar.GetComponentInChildren<Canvas>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        //bossHealthBar.Initialize(health,maxHealth);
        //healthBarCanvas.enabled = true;
    }

    private void Update()
    {
        // Increase timer that controls attack combo
        timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if(rolling)
            rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if(rollCurrentTime > rollDuration)
            rolling = false;

        //Check if character just landed on the ground
        if (!grounded && groundSensor.State())
        {
            grounded = true;
            anim.SetBool("Grounded", grounded);
        }

        //Check if character just started falling
        if (grounded && !groundSensor.State())
        {
            grounded = false;
            anim.SetBool("Grounded", grounded);
        }

        if (health == 0)
        {
            isStageTwo = !isStageTwo;
        }
    }

    public override void Die()
    {
        //healthBarCanvas.enabled = false;
        base.Die();

        if (isStageTwo)
        {
            // DeathNoBlood
            
        }
        else
        {
            // Stage Two
            // Death
            
        }

        
        // anim.SetBool("noBlood", noBlood);
        // anim.SetTrigger("Death");
    }

    public override void TakeDamage(float damage)
    {
        if (isInvulnerable)
        {
            return;
        }
        base.TakeDamage(damage);

        
        //bossHealthBar.UpdateState(health,maxHealth);

        if (health <= criticalValue)
        {
            // enter the second stage
            // boss body scale * 2 by animator
            // change the audio
        }

        if (health <= 0f)
        {
            Die();
        }
    }
}