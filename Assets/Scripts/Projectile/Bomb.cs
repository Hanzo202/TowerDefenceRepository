using UnityEngine;
using Enemy;

public class Bomb : Projectile
{
    [SerializeField]private int radiusExplosion;
    [SerializeField] private ParticleSystem startExplosion;
    public override void HitEnemy(Transform enemy) => BombExplosion();      


    private void BombExplosion()
    {
        AudioManager.Instance.PlaySfx("explision");
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radiusExplosion);
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<EnemyMain>() != null)
            {
                collider.GetComponent<EnemyMain>().HitEnemy(damage);
            }
        }
        Destroy(gameObject);
    }

    public override void StartMethod()
    {
        base.StartMethod();
        Instantiate(startExplosion, transform.position, Quaternion.identity);
    }
}
