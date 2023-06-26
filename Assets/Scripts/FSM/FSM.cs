using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public Enemy enemy;
    public BossWeapon bossWeapon;
    public NewPlayer player;
    [SerializeField] private bool isFlipped = false;

    public Parameter parameter;
    
    private IState currentState;
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();

    private int jumpRemainNum;

    private void Awake()
    {
        parameter.animator = GetComponent<Animator>();
        parameter.rb = GetComponent<Rigidbody2D>();
        player = GetComponent<NewPlayer>();

        enemy = FindObjectOfType<Enemy>().GetComponent<Enemy>();
        bossWeapon = FindObjectOfType<BossWeapon>().GetComponent<BossWeapon>();
    }

    private void Start()
    {
        states.Add(StateType.Idle,new IdleState(this));
        states.Add(StateType.CombatIdle,new CombatIdleState(this));
        states.Add(StateType.Run,new RunState(this));
        states.Add(StateType.Jump,new JumpState(this));
        states.Add(StateType.Fall,new FallState(this));
        states.Add(StateType.Dash,new DashState(this));
        states.Add(StateType.DarkDash,new DarkDashState(this));
        states.Add(StateType.Block,new BlockState(this));
        states.Add(StateType.IdleBlock,new IdleBlockState(this));
        states.Add(StateType.Attack,new AttackState(this));
        states.Add(StateType.Hurt,new HurtState(this));
        states.Add(StateType.Death,new DeathState(this));
        states.Add(StateType.DeathNoBlood,new DeathNoBloodState(this));

        //Debug.Log(parameter.isGround);
        TransitionState(StateType.Idle);
        parameter.currentGravity = parameter.rb.gravityScale;
    }

    private void Update()
    {
        if (parameter.isDash)
        {
            return;
        }
        PhysicsCheck();
        CheckGravityEnable();

        CheckJumpState();
        CheckAttackState();
        CheckDashState();
        
        currentState.OnUpdate();
        Debug.Log(currentState.ToString());
    }

    public void CheckJumpState()
    {
        if (!parameter.inputEnable)
        {
            return;
        }
        if (parameter.isGround)
        {
            jumpRemainNum = parameter.jumpNum;
        }
        if (InputManager.Instance.JumpKeyDown && jumpRemainNum-- >= 0)
        {
            TransitionState(StateType.Jump);
            // TODO play SFX

            parameter.rb.velocity = new Vector2(parameter.rb.velocity.x, parameter.jumpForce);
        }
    }

    public void CheckAttackState()
    {
        if (!parameter.inputEnable)
        {
            return;
        }
        parameter.timeSinceAttack += Time.deltaTime;
        if (InputManager.Instance.Attack && parameter.timeSinceAttack > 0.05f)
        {
            //rb.velocity = Vector2.zero;
            TransitionState(StateType.Attack);
        }
    }

    public void CheckDashState()
    {
        var dashTmpTime = parameter.dashTime;
        
        if (!parameter.inputEnable)
        {
            return;
        }
        parameter.timeSinceDash += Time.deltaTime;
        parameter.timeSinceDarkDash += Time.deltaTime;
        
        if (InputManager.Instance.DashDown && parameter.isCanDash && parameter.timeSinceDash > 0.5f)
        {
            if (parameter.isCanDarkDash && parameter.timeSinceDarkDash > 1f)
            {
                TransitionState(StateType.DarkDash);
            }
            else
            {
                TransitionState(StateType.Dash);
            }

            StartCoroutine(DashMove(parameter.dashTime));
            dashTmpTime -= Time.deltaTime;

            if (dashTmpTime < 0f)
            {
                TransitionState(StateType.Idle);
                player.GetComponent<Collider2D>().isTrigger = false;
                StopCoroutine(DashMove(parameter.dashTime));
            }
        }
    }
    
    IEnumerator DashMove(float time)
    {
        var yVelocity = Input.GetAxisRaw("Horizontal");
        parameter.isCanDash = false;
        parameter.isDash = true;
        parameter.inputEnable = false;
        parameter.gravityEnable = false;

        //parameter.rb.velocity = new Vector2(transform.localScale.x * parameter.dashPower, 0f);
        parameter.rb.AddForce(new Vector2(parameter.dashPower * yVelocity,0f),ForceMode2D.Impulse);
        yield return new WaitForSeconds(time);
        
        parameter.inputEnable = true;
        parameter.gravityEnable = true;
        parameter.isDash = false;
        yield return new WaitForSeconds(parameter.timeSinceDash);
        parameter.isCanDash = true;
    }

    public void CheckGravityEnable()
    {
        if (parameter.gravityEnable)
        {
            parameter.rb.gravityScale = parameter.currentGravity;
        }
        else if (!parameter.gravityEnable)
        {
            parameter.rb.gravityScale = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            parameter.isHurt = true;
        }
    }

    public void TransitionState(StateType stateType)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = states[stateType];
        currentState.OnEnter();
    }
    
    public void PhysicsCheck()
    {
        RaycastHit2D leftCheck = Raycast(new Vector2(-parameter.footOffset, parameter.rayPositionY), Vector2.down, 
            parameter.groundDistance, parameter.groundLayer);
        RaycastHit2D rightCheck = Raycast(new Vector2(parameter.footOffset, parameter.rayPositionY), Vector2.down, 
            parameter.groundDistance, parameter.groundLayer);

        if (leftCheck || rightCheck)
        {
            parameter.isGround = true;
        }
        else
        {
            parameter.isGround = false;
        }

        if (parameter.isGround)
        {
            parameter.isCanDash = true;
            
        }
    }
    
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length,LayerMask layer)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, layer);

        Color color = hit ? Color.red : Color.green;
        
        Debug.DrawRay(pos + offset,rayDirection * length,color);
        return hit;
    }
}

[Serializable]
public class Parameter
{
    public Animator animator;
    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public float runSpeed = 5.2f;  // 运动速度
    public float jumpForce = 2.0f;  // 跳跃力
    public float lowJumpMultiplier = 5f;  // 弱跳所乘数
    public float fallMultiplier = 3.2f;  // 掉落所乘数
    public float attackSpeed = 0.5f;  // 攻击补偿速度
    public float timeSinceAttack;  // 攻击冷却时间
    public float damageFromEnemy = 10f;  // 伤害来自敌人
    public float idleblockTime = 1.0f;  // 站立防御时间
    
    public float groundDistance = 0.2f;  // 射线的长度
    public float footOffset = 0.4f;  // 两条射线的距离
    public float rayPositionY = -0.5f;  // 射线的Y轴

    public float currentGravity;  // 当前重力
    public float dashDistance;  // 冲刺距离 与暗影冲刺相同
    public float dashPower;  // 冲刺力量 与暗影冲刺相同
    public float dashTime;  // 冲刺时间 与暗影冲刺相同
    public float timeSinceDash;  // 冲刺冷却时间
    public float timeSinceDarkDash;  // 暗影冲刺冷却时间
    
    public int jumpNum = 2;  // 跳跃次数

    public bool gravityEnable = true;
    public bool inputEnable;
    public bool isGround;
    public bool isAttack;
    public bool isJump;
    public bool isFall;
    public bool isBlock;
    public bool isIdleBlock;
    public bool isCanDash;  // 是否能够冲刺
    public bool isCanDarkDash;  // 是否能够暗影冲刺
    public bool isDash;  // 正在冲刺
    public bool isHurt;
    public bool isDeath;
    public bool isDeathNoBlood;
}

public enum StateType
{
    Idle,
    CombatIdle,
    Run,
    Jump,
    Dash,
    DarkDash,  // AudioManager Collider2D Sprite
    Fall,
    Block,
    IdleBlock,
    Attack,
    Roll,
    WallSlide,
    Hurt,
    Death,
    DeathNoBlood,
}