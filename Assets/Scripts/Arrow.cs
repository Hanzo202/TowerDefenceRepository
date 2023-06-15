using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    public override void HitEnemy(Transform enemy)
    {
        if (enemy.GetComponent<Enemy>() != null) 
        {
            Instantiate(hitEffect, transform.position, Quaternion.FromToRotation(Vector3.back, transform.forward));
            enemy.GetComponent<Enemy>().HitEnemy(damage);
            Destroy(gameObject);
        }         
    }
}
