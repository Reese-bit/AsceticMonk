using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBoss : Enemy,IAnimaEvent
{
    //private Canvas healthBarCanvas;
    private Animator anim;
    private Rigidbody2D rb;
    private AnimatorStateInfo stateInfo;
    
    [HideInInspector]
    public EnemyStateBar_HUD enemyStateBarHUD;
    
    protected override void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        enemyStateBarHUD = FindObjectOfType<EnemyStateBar_HUD>();
    }
    
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void Start()
    {
        enemyStateBarHUD.Initialize(maxHealth,maxHealth);
    }

    public override void Die()
    {
        //bossCanvasBar.SetActive(false);
        base.Die();

        if (isStageTwo == false)
        {
            // DeathNoBlood
            isStageTwo = true;
            StartCoroutine(nameof(ChangeInStageTwo));
        }
        else
        {
            // Stage Two
            // Death
            StopCoroutine(nameof(ChangeInStageTwo));  // !!!!!!!!!!!!!! Disable()
            GameManager.GameState = GameState.Win;
        }
    }

    public override void TakeDamage(float _damage)
    {
        base.TakeDamage(_damage);
    }

    IEnumerator ChangeInStageTwo()
    {
        // enter the second stage
        // boss body scale * 2 by animator
        // change the audio
        // need to put canvas again
        bossCanvasBar.SetActive(true);
        
        enemyStateBarHUD.Initialize(maxHealth * 2,maxHealth * 2);
        //bossHealthBar.UpdateState(maxHealth, maxHealth);

        yield return waitForRestore;
    }

    public void OnAnimaEvent()
    {
        
    }
    
    public void OnEndAnimaEvent()
    {
        
    }

    public void OnEnemyAttackEvent()
    {
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        float lookDir = gameObject.transform.localScale.x;
        
            if (stateInfo.IsName("Attack1") || stateInfo.IsName("Attack2") || stateInfo.IsName("Attack3"))
            {
                rb.velocity = new Vector3(attackMoveDistance * 5 * lookDir / Mathf.Abs(lookDir), 0, 0);
                bossWeapon.gameObject.SetActive(true);
            }
            // rb.velocity = new Vector3(attackMoveDistance * 10 * dir, 0, 0);

            Invoke(nameof(OnEndAttackEvent),0.05f);
    }

    public void OnEndAttackEvent()
    {
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Attack1") || stateInfo.IsName("Attack2") || stateInfo.IsName("Attack3"))
        {
            bossWeapon.gameObject.SetActive(false);
        }
    }
}
