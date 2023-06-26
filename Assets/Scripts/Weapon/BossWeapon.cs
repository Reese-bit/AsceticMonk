using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : Weapon
{
    private bool m_iscollidersNotNull;
    public bool isBeingAttacked = false;

    private void Awake()
    {
        m_iscollidersNotNull = colliders != null;

        enemy = GetComponentInParent<NewBoss>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //boss.TakeDamage(hitDamage);
            player = FindObjectOfType<NewPlayer>();
            player.TakeDamage(hitDamage);
            isBeingAttacked = true;
            AudioManager.Instance.PlaySFX(hitSFX);
        }
    }

    public override void WeaponAttack()
    {
        base.WeaponAttack();
        
        if (m_iscollidersNotNull)
        {
            //enemyCharacter.TakeDamage(explosionDamage);
            colliders.GetComponent<Player>().TakeDamage(hitDamage);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;;
        Gizmos.DrawWireSphere(transform.position,hitRadius);
    }

    // * AOE Damage Implementation 2
    // * 范围伤害实现方法2
    // !Disadvantages: To detect all enemies in the scene, slightly lower efficiency 
    // !缺点：检测场景中所有的敌人，效率稍低
    // void DistanceDetection()
    // {
    //     // Loop detection all enemies in current scene
    //     // 遍历当前场景中所有的敌人
    //     foreach (var enemyInRange in EnemyManager.Instance.Enemies)
    //     {
    //         // If the distance between the enemy and the missile is within the explosion radius (3f)
    //         // 如果敌人和导弹的距离在爆炸半径(3f)内
    //         if (Vector2.Distance(transform.position, enemyInRange.transform.position) <= 3f)
    //         {
    //             if (enemyInRange.TryGetComponent<Enemy>(out Enemy enemy))
    //             {
    //                 // enemy take 100 damage
    //                 // 则敌人受到100点伤害
    //                 enemy.TakeDamage(100f);
    //             }
    //         }
    //     }
    // }
    
    // * AOE Damage Implementation 3
    // * 范围伤害实现方法3
    // [SerializeField] LayerMask enemyLayerMask = default;
    // [SerializeField] float explosionRadius = 3f;
    // [SerializeField] float explosionDamage = 100f;

    // void PhysicsOverlapDetection()
    // {
    //     // Enemies within explosion radius take AOE damage
    //     var colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayerMask);

    //     foreach (var collider in colliders)
    //     {
    //         if (collider.TryGetComponent<Enemy>(out Enemy enemy))
    //         {
    //             enemy.TakeDamage(explosionDamage);
    //         }
    //     }
    // }

    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireSphere(transform.position, explosionRadius);
    // }
}
