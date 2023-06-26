using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject hitVFX;
    [SerializeField] protected AudioData hitSFX;
    public float hitDamage;
    [SerializeField] protected float hitRadius;
    [SerializeField] protected LayerMask hitLayer;

    [Header("Delivery Animation Event")] 
    public GameObject animaGo;
    public bool isParentDelivery = true;
    
    protected Type t;
    protected GameObject target;
    protected Collider2D colliders;
    protected NewPlayer player;
    protected Enemy enemy;

    public virtual void WeaponAttack()
    {
        if (isParentDelivery)
        {
            var recieve = gameObject.GetComponentInParent<IAnimaEvent>();

            if (recieve != null)
            {
                colliders = Physics2D.OverlapCircle(transform.position, hitRadius, hitLayer);
            }
        }
        else if (animaGo != null)
        {
            var e = animaGo.GetComponent<IAnimaEvent>();
            
            if (e == null)
            {
                Debug.Log("MonoBehaviour not implement IAnimaEvent");
            }
            else
            {
                e.OnAnimaEvent();
            }
        }
    }

    protected void SetTarget(GameObject _target)
    {
        target = _target;
    }
    
}

public interface IAnimaEvent
{
    void OnAnimaEvent();
    void OnEndAnimaEvent();
    void OnEnemyAttackEvent();
    void OnEndAttackEvent();
}