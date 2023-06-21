using UnityEngine;


public abstract class Projectile : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField] private string shotSound;

    [SerializeField] protected float damage;
    [SerializeField] protected ParticleSystem hitEffect;

    private void Start() => StartMethod();

    private void OnTriggerEnter(Collider other) => HitEnemy(other.transform);

    public abstract void HitEnemy(Transform enemy);

    public virtual void StartMethod()
    {
        AudioManager.Instance.PlaySfx(shotSound);
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Acceleration);
        Destroy(gameObject, 3);
    }

}
