using UnityEngine;

namespace Enemy
{
    public class EnemyLoot : MonoBehaviour
    {
        [SerializeField] private Coin coin;

        private const float _lootCreateOffsetY = 1f;
        public void Loot()
        {
            Instantiate(coin, new Vector3(transform.position.x, transform.position.y + _lootCreateOffsetY, transform.position.z), Quaternion.identity);
        }
    }
}


