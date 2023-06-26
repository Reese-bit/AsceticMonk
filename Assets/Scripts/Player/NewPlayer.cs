using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : Character,IAnimaEvent
{
    public float speed = 5.6f;
    public float xVelocity;
    public float yVelocity;
    public float upVelocity;
    public float jumpForce;
    public float jumpTime = 1.0f;  // 跳跃蓄力时间
    public int jumpNum = 2;
    public int jumpRemainNum;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    public float dashLength;
    public float dashTime;
    public float attackSpeed = 0.5f;  // 攻击补偿速度
    public float groundDistance = 0.2f;  // 射线的长度
    public float footOffset = 0.4f;  // 两条射线的距离
    public float rayPositionY = -0.5f;  // 射线的Y轴
    public float playerAttackMoveDistance = 1.0f;
    public float lookAtDir;

    public bool pressJump = false;
    public bool isGround = true;
    public bool isJump = false;
    public bool isCanDash = true;
    public bool isAlive = true;

    public StateBar_HUD playerStateBarHUD;
    public EnergyBar playerEnergyBar;

    private float timeJump;  // 当前跳跃蓄力时间
    private float timeSinceAttack;

    public Rigidbody2D rb;
    public Animator anim;
    private Transform playerTrans;
    private Transform playerWeapon;
    private AnimatorStateInfo stateInfo;
    public LayerMask groundLayer;

    protected override void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        playerTrans = gameObject.GetComponent<Transform>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        playerStateBarHUD.Initialize(health,maxHealth);
        playerWeapon = transform.Find("PlayerWeapon");
    }

    private void Update()
    {
        if (!isPaused)
        {
            if (!isAlive)
            {
                return; // 死亡不进行任何操作
            }

            pressJump = Input.GetButtonDown("Jump");
            xVelocity = Input.GetAxis("Horizontal");
            yVelocity = Input.GetAxisRaw("Horizontal");
            lookAtDir = gameObject.transform.localScale.x;

            anim.SetBool("Grounded", isGround);
        }
    }

    private void FixedUpdate()
    {
        if (!isPaused)
        {
            //PhysicsCheck();
            GroundMovement();

            JumpUpdate();
        }
    }

    public void GroundMovement()
    {
        if (xVelocity != 0)  // pressed movement
        {
            rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);
            anim.SetInteger("AnimState",2);
        }
        // else if(xVelocity == 0)  // 
        // {
        //     anim.SetInteger("AnimState",0);
        // }

        if (yVelocity != 0)
        {
            transform.localScale = new Vector3(-yVelocity, 1, 1);
        }
    }

    void PhysicsCheck()
    {
        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset, rayPositionY), Vector2.down, 
            groundDistance, groundLayer);
        RaycastHit2D rightCheck = Raycast(new Vector2(footOffset, rayPositionY), Vector2.down, 
            groundDistance, groundLayer);

        if (leftCheck || rightCheck)
        {
            isGround = true;
            isCanDash = true;
            //anim.ResetTrigger("Jump");
            //anim.SetInteger("AnimState", 0);
        }
        else
        {
            isGround = false;
        }

        if (isGround)
        {
            anim.SetInteger("AnimState", 0);
        }
    }

    void JumpUpdate(float time = 0)
    {
        upVelocity += time;
    }

    /// <summary>
    /// 跳跃
    /// </summary>
    void Jump()
    {
        if (pressJump)
        {
            anim.SetTrigger("Jump");
            //isGround = false;
            //isJump = true;
            // TODO play SFX
            
            if (isGround)
            {
                jumpRemainNum = jumpNum;
            }

            if (pressJump && jumpRemainNum-- > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isGround = false;

            }

            // 掉落时
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
                //anim.ResetTrigger("Jump");
            }
            // 跳跃时松开跳跃键
            else if (rb.velocity.y > 0 && !pressJump)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// 冲刺
    /// </summary>
    void Dash()
    {
        if (Input.GetButtonDown("Dash") && isCanDash)
        {
            StartCoroutine(DashMove(dashTime));
            anim.SetTrigger("Dash");
            isCanDash = false;
        }
    }

    IEnumerator DashMove(float time)
    {
        rb.gravityScale = 0;
        xVelocity = 0;
        if (upVelocity != 0)
        {
            xVelocity = 15 * upVelocity;
        }

        yield return new WaitForSeconds(time);
        rb.gravityScale = 1;
    }

    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length,LayerMask layer)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, layer);

        Color color = hit ? Color.red : Color.green;
        
        Debug.DrawRay(pos + offset,rayDirection * length,color);
        return hit;
    }
    
    /// <summary>
    /// 攻击
    /// </summary>
    void Attack()
    {
        timeSinceAttack += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.J) && timeSinceAttack > 0.05f)
        {
            //rb.velocity = Vector2.zero;
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f)
            {
                anim.SetTrigger("Attack");
                rb.velocity = new Vector2(-playerTrans.localScale.x * attackSpeed, rb.velocity.y);
                //Debug.Log("Attack Seccessful");
            }
            

            timeSinceAttack = 0.0f;
        }
        else
        {
            anim.ResetTrigger("Attack");
        }
    }
    
    /// <summary>
    /// 受伤
    /// </summary>
    /// <param name="damage"></param>
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        // Update UI
        playerStateBarHUD.UpdateState(health,maxHealth);

        if (gameObject.activeSelf)
        {
            StartCoroutine(nameof(InvincibleCoroutine));
        }

        if (health < 0f)
        {
            isAlive = false;
            Die();
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

    /// <summary>
    /// 死亡
    /// </summary>
    public override void Die()
    {
        if (!isAlive)
        {
            playerStateBarHUD.UpdateState(0f,maxHealth);
            base.Die();
            anim.SetTrigger("Death");
            GameManager.GameState = GameState.Lose;
        }

        // isAlive = !isAlive;
    }

    /// <summary>
    /// 恢复(自动)
    /// </summary>
    /// <param name="恢复量"></param>
    public override void Restore(float value)
    {
        base.Restore(value);
        if (Input.anyKeyDown)
        {
            anim.SetTrigger("Recover");
            // Update UI
            //stateBarHUD.UpdateState(health,maxHealth);
        }
        else if (Input.GetKeyUp(KeyCode.L))
        {
            anim.ResetTrigger("Recover");
            // Update UI
        }
    }

    /// <summary>
    /// 后退
    /// </summary>
    public override void BackUp(float distance)
    {
        rb.velocity = new Vector2(distance * 5 * lookAtDir / Mathf.Abs(lookAtDir), 0);
    }

    public void OnAnimaEvent()
    {
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        float lookDir = gameObject.transform.localScale.x;

        // 攻击移动
        // if (yVelocity == 0)
        // {
            if (stateInfo.IsName("Attack"))
            {
                // rb.velocity = new Vector3(playerAttackMoveDistance * 5 * lookDir / Mathf.Abs(lookDir), 0, 0);
                playerWeapon.gameObject.SetActive(true);
            }
            // rb.velocity = new Vector3(attackMoveDistance * 10 * dir, 0, 0);
        // }
        // else
        // {
        //     rb.velocity = new Vector3(playerAttackMoveDistance * 10 * yVelocity, 0, 0);
        // }

        Invoke(nameof(OnEndAnimaEvent),0.05f);
    }

    public void OnEndAnimaEvent()
    {
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Attack"))
        {
            playerWeapon.gameObject.SetActive(false);
        }
    }

    public void OnEnemyAttackEvent()
    {
        
    }

    public void OnEndAttackEvent()
    {
        
    }
}
