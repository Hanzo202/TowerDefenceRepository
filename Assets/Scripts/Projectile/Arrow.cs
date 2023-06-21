using UnityEngine;
using Enemy;

public class Arrow : Projectile
{
    public override void HitEnemy(Transform enemy)
    {
        if (enemy.GetComponent<EnemyMain>() != null) 
        {
            Instantiate(hitEffect, transform.position, Quaternion.FromToRotation(Vector3.back, transform.forward));
            enemy.GetComponent<EnemyMain>().HitEnemy(damage);
            Destroy(gameObject);
        }         
    }
}
