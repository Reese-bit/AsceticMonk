using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character,IAnimaEvent
{
    [SerializeField] private float speed;
    [SerializeField] private int extraJump;
    [SerializeField] private float jumpForce;

    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundCheck;

    private Rigidbody2D rb;
    private Collider2D col;
    private Animator anim;
    private Sensor_Bandit m_sensorBandit;
    private bool isGround;
    private bool isCombatIdle;
    private bool isDead;
    private float timeSinceAttack;
    
    private const float recoverTime = 0.3f;
    
    private WaitForSeconds waitForRecoverTime;
    
    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        m_sensorBandit = transform.Find("GroundCheck").GetComponent<Sensor_Bandit>();
        waitForRecoverTime = new WaitForSeconds(recoverTime);
    }

    private void Start()
    {
        //stateBarHUD.Initialize(health,maxHealth);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void Update()
    {
        //Check if character just landed on the ground
        if (!isGround && m_sensorBandit.State()) {
            isGround = true;
            anim.SetBool("Grounded", isGround);
        }

        //Check if character just started falling
        if(isGround && !m_sensorBandit.State()) {
            isGround = false;
            anim.SetBool("Grounded", isGround);
        }
        
        //Set AirSpeed in animator
        anim.SetFloat("AirSpeed", rb.velocity.y);

        newJump();
        
        Attack();
    }

    private void FixedUpdate()
    {
        Movement();

        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
    }

    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal"); //Horizontal包含(-1,0)和(0,1)之间的小数
        float faceDirection = Input.GetAxisRaw("Horizontal"); //Horizontal只有三个值:-1,0,1
        if (horizontalmove != 0) //角色移动
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.fixedDeltaTime, rb.velocity.y);
            anim.SetInteger("AnimState", 2); //避免使用Abs
        }
        else
        {
            anim.SetInteger("AnimState", 0);
            if (Input.GetKeyDown(KeyCode.C))
            {
                isCombatIdle = !isCombatIdle;
                if (isCombatIdle)
                {
                    anim.SetInteger("AnimState", 1);
                }
                else
                {
                    anim.SetInteger("AnimState", 0);
                }
            }
        }

        if (faceDirection != 0)
        {
            transform.localScale = new Vector3(-faceDirection, 1, 1);
        }
    }

    void newJump()  //二段跳
    {
        if (isGround)
        {
            extraJump = 1;
        }
        if (Input.GetButtonDown("Jump") && extraJump > 0)
        {
            anim.SetTrigger("Jump");
            isGround = false;
            anim.SetBool("Grounded",isGround);
            rb.velocity = new Vector2(rb.velocity.x,jumpForce); //Vector2.up = new Vector2(0,1)
            extraJump--;
            //jumpSource.Play();
            m_sensorBandit.Disable(0.2f);
        }
    }

    void Attack()
    {
        timeSinceAttack += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.J) && timeSinceAttack > 0.25f)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
            {
                anim.SetTrigger("Attack");
            }

            timeSinceAttack = 0.0f;
        }
    }

    // //playAnimationName 将要播放动画的名字    animationTime 播放到某一时刻（0 - 1播放完）   action回掉//enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime 当返回的值大于1的时候该动画已经播放完毕
    // public IEnumerator PlayAnimation(string playAnimationName, float animationTime, Action action)
    // {
    //     //设置要播放的动画名称
    //     anim.SetTrigger(playAnimationName);
    //
    //     //(增加1个判断如果这个是动画融合的情况！)为什么我会先判断animator是否进入我们想要播放的动画  比如我们想播放attack这个动画  但是当它在    idle转向attack的时候 animator会有一个动画融合
    //     //当开始播放attack动画的时候他不会立即进入而是回在idle -> attack的过程中 而返回的名字仍然是idle  所以要判断是否进入attack动画
    //     while (!anim.GetCurrentAnimatorStateInfo(0).IsName(playAnimationName))
    //     {
    //         yield return null;
    //     }
    //     while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < animationTime)
    //     {
    //         yield return null;
    //     }
    //     //Debug.Log("播放完毕");
    //     anim.ResetTrigger(playAnimationName);
    //     action?.Invoke();
    // }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        //stateBarHUD.UpdateState(health,maxHealth);

        // Update UI

        if (gameObject.activeSelf)
        {
            StartCoroutine(nameof(InvincibleCoroutine));
        }
    }

    IEnumerator InvincibleCoroutine()
    {
        anim.SetTrigger("Hurt");
        Physics2D.IgnoreLayerCollision(7, 8, true);

        yield return waitForInvincibleTime;

        Physics2D.IgnoreLayerCollision(7, 8, false);
        anim.ResetTrigger("Hurt");
    }

    public override void Die()
    {
        if (!isDead)
        {
            //stateBarHUD.UpdateState(0f,maxHealth);
            base.Die();
            anim.SetTrigger("Death");
            GameManager.GameState = GameState.GameOver;
        }

        isDead = !isDead;
    }

    public override void Restore(float value)
    {
        base.Restore(value);
        if (Input.GetKeyDown(KeyCode.L))
        {
            anim.SetTrigger("Recover");
            // Update UI
            //stateBarHUD.UpdateState(health,maxHealth);
        }
        else
        {
            anim.ResetTrigger("Recover");
            // Update UI
        }
    }

    public void OnAnimaEvent()
    {
        Attack();
    }
    
    public void OnEndAnimaEvent()
    {
        
    }

    public void OnEnemyAttackEvent()
    {
        
    }

    public void OnEndAttackEvent()
    {
        
    }
}
