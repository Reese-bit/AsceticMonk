using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class Shoot : BT_EvilWizard
{
    public List<EvilWeapon> weapons;
    public bool shakeCamera;

    public override TaskStatus OnUpdate()
    {
        foreach (var weapon in weapons)
        {
            var projectile = Object.Instantiate(weapon.projectilePrefab, weapon.weaponTransform.position,
                Quaternion.identity);
            //projectile.Shooter = gameObject;

            var force = new Vector2(weapon.horizontalForce * transform.position.x, weapon.verticalForce);

            if (shakeCamera)
            {
                
            }
        }
        return TaskStatus.Success;
    }
}
