using UnityEngine;


namespace Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator enemyAnimator;
        [SerializeField] private float speed;
        [SerializeField] private float speedRot;
        private Transform[] path;
        private int indexPath = 0;
        private const float TransitionBetweenWaypoints = 0.5f;

        private void Start()
        {
            path = WayPoints.points;
            transform.LookAt(path[indexPath].position);
        }

        public void Move()
        {
            if (indexPath > path.Length)
            {
                return;
            }

            Quaternion lookRotation = Quaternion.LookRotation(path[indexPath].position - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speedRot * Time.deltaTime);
            if (Vector3.Distance(transform.position, path[indexPath].position) < TransitionBetweenWaypoints)
            {
                indexPath++;
            }
            transform.position = Vector3.MoveTowards(transform.position, path[indexPath].position, Time.deltaTime * speed);
        }

        public void Idle()
        {
            speed = 0;
            enemyAnimator.IdleAnimation(true);
        }
    }
}

