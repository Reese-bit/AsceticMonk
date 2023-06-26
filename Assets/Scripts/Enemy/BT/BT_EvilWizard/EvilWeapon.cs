using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EvilWeapon
{
    public Transform weaponTransform;
    public GameObject projectilePrefab;
    public float horizontalForce = 5f;
    public float verticalForce = 4f;
}
