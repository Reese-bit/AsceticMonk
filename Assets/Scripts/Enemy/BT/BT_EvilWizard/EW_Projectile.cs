using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EW_Projectile : BT_EvilWizard
{
    private EnemyProjectile enemyProj;
    
    protected GameObject _hitVFX;
    protected AudioData[] _hitSFX;
    protected float _damage;
    protected float _moveSpeed;
    protected Vector2 _direction;

    public override void OnAwake()
    {
        enemyProj = Object.FindObjectOfType<EnemyProjectile>().GetComponent<EnemyProjectile>();
        _hitVFX = enemyProj.hitVFX;
        _hitSFX = enemyProj.hitSFX;
        _damage = enemyProj.damage;
        _moveSpeed = enemyProj.moveSpeed;
        _direction = enemyProj.direction;
    }
}
