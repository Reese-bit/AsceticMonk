using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] protected EnemyStateBar_HUD bossHealthBar;
    [SerializeField] protected GameObject bossCanvasBar;
    //[SerializeField] protected AudioData audioInStageTwo;
    [SerializeField] protected EnemyType enemyType;
    
    [SerializeField] protected float jumpForce = 7.5f;
    [SerializeField] protected float rollForce = 6.0f;
    [SerializeField] protected float attackMoveDistance = 1.0f;
    [SerializeField] protected bool noBlood = false;
    [SerializeField] protected float waitForRestoreTime = 1.0f;
    //[SerializeField] private GameObject slideDust;
    
    // [SerializeField] private Canvas HUD_EnemyBlood;
    public float runSpeed = 4.0f;
    [SerializeField] protected float damageByPlayer;  // 玩家所造成伤害
    [SerializeField] protected float damage;  // 碰撞后玩家所受伤害
    [SerializeField] protected float distacne;  // 碰撞后玩家所退距离
    [SerializeField] protected float damageSelf;  // 碰撞后敌人所受伤害
    [SerializeField] private Transform playerTrans;
    [SerializeField] private bool isFlipped = false;
    [SerializeField] protected float criticalValue;
    
    protected Transform bossWeapon;
    protected WaitForSeconds waitForRestore;
    protected int ad;

    private Vector3 flipped;
    protected bool isInvulnerable = false;
    protected bool isStageTwo = false;

    public bool isBeingTouched = false;

    protected override void Awake()
    {
        base.Awake();
        bossHealthBar = FindObjectOfType<EnemyStateBar_HUD>();
        //bossCanvasBar = bossHealthBar.GetComponentInChildren<Canvas>();
        waitForRestore = new WaitForSeconds(waitForRestoreTime);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        bossHealthBar.Initialize(health,maxHealth);
        bossCanvasBar.SetActive(true);
        bossWeapon = transform.Find("BossWeapon");
        enemyType = EnemyType.HeroKnight;
    }
    
    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage(damage);
            isBeingTouched = true;
            // player所退距离
            player.BackUp(distacne);
            
            TakeDamage(damageSelf);
        }
    }

    public void LookAtPlayer()
    {
        flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > playerTrans.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = true;
        }
        else if (transform.position.x < playerTrans.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = false;
        }
    }
    
    public void LookAt(Transform target)
    {
        if (target != null)
        {
            if (transform.position.x > target.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public override void TakeDamage(float damage)
    {
        if (isInvulnerable)
        {
            return;
        }
        base.TakeDamage(damage);
        if (gameObject.activeSelf)
        {
            // Update UI
            bossHealthBar.UpdateState(health,maxHealth);
        }
    }

    public override void Die()
    {
        // ScoreManager.Instance.AddScore(scorePoint);
        // PlayerEnergy.instance.Obtain(deathEnergyBouns);
        // EnemyManager.instance.RemoveFromList(gameObject);
        base.Die();
        // if (isStageTwo == false)
        // {
        //     isStageTwo = true;
        //     bossHealthBar.UpdateState(maxHealth,maxHealth);
        // }
        // else
        // {
        //     gameObject.SetActive(false);
        //     bossCanvasBar.gameObject.SetActive(false);
        //     GameManager.GameState = GameState.GameOver;
        // }
    }
}

public enum EnemyType
{
    HeroKnight,
    EvilWizard,
}
