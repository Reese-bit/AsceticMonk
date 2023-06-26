using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public GameObject[] projectiles;

    public int number;

    // Update is called once per frame
    void Update()
    {
        InitializeProj();
    }

    void InitializeProj()
    {
        for (int i = 0; i < number; i++)
        {
            var projectile = Instantiate(projectiles[Random.Range(0, projectiles.Length)]);
            projectile.transform.localPosition = Random.insideUnitSphere;  // 将位置设置在随机的球形范围内
            projectile.AddComponent<Projectile>().destroyEvent.AddListener(() =>
            {
                Destroy(projectile);
            });
        }
    }
}
