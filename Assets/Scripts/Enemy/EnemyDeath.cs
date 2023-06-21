using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyMain enemyMain;
        [SerializeField] private EnemyAnimator enemyAnimator;
        [SerializeField] private EnemyLoot enemyLoot;
        [SerializeField] private ParticleSystem dieParticle;

        private const float TimeDelayAfterDeath = 3f;
        private const float DieParticleOffsetY = 0.5f;
        private const float AttackTime = 2f;

        public void EnemyDeathProp()
        {
            enemyMain.isDead();
        }

        public void EnemyDeathEffect()
        {
            Instantiate(dieParticle, new Vector3(transform.position.x, transform.position.y + DieParticleOffsetY, transform.position.z), Quaternion.identity);
            GameManager.Instance.EnemyWasDestroy();
            Destroy(gameObject);
        }

        public IEnumerator EnemyReachedCastleCoroutine()
        {
            EnemyDeathProp();
            enemyAnimator.AttackAnimation(true);
            AudioManager.Instance.PlaySfx("castleHit");
            yield return new WaitForSeconds(AttackTime);

            enemyAnimator.DeathAnimation(true);
            yield return new WaitForSeconds(TimeDelayAfterDeath);

            EnemyDeathEffect();
        }
        public IEnumerator EnemyDeathCoroutine()
        {
            EnemyDeathProp();
            enemyAnimator.DeathAnimation(true);
            yield return new WaitForSeconds(TimeDelayAfterDeath);

            enemyLoot.Loot();
            EnemyDeathEffect();
        }

    }
}


