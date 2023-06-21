using UnityEngine;
using UnityEngine.UI;

namespace Enemy 
{
    public class EnemyMain : MonoBehaviour
    {
        [SerializeField] private EnemyDeath enemyDeath;
        [SerializeField] private EnemyMove enemyMove;
        [SerializeField] private Slider hpBar;
        [SerializeField] private float hp;
        [SerializeField] private int damage;

        private BoxCollider boxCollider;
        private bool isAlive = true;

        public bool IsAlive => isAlive;

        private void Start()
        {
            hpBar.maxValue = hp;
            hpBar.value = hp;
            boxCollider = GetComponent<BoxCollider>();
        }

        private void Update()
        {
            if (!isAlive)
            {
                return;
            }
            enemyMove.Move();
        }

        public void HitEnemy(float damage)
        {
            hp -= damage;
            hpBar.value = hp;
            if (hp <= 0 && isAlive)
            {
                StartCoroutine(enemyDeath.EnemyDeathCoroutine());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Castle"))
            {
                Castle castle = other.GetComponent<Castle>();
                castle.EnemyDamage(damage);
                StartCoroutine(enemyDeath.EnemyReachedCastleCoroutine());
            }
        }

        public void isDead()
        {
            AudioManager.Instance.PlaySfx("enemyDeath");
            isAlive = false;
            boxCollider.enabled = false;
            hpBar.gameObject.SetActive(false);
        }     
    }
}


