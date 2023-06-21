using UnityEngine;

namespace Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        private const string IdleAnim = "Idle";
        private const string AttackAnim = "Attack";
        private const string DeathAnim = "Death";

        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void IdleAnimation(bool state)
        {
            animator.SetBool(IdleAnim, state);
        }
        public void DeathAnimation(bool state)
        {
            animator.SetBool(DeathAnim, state);
        }
        public void AttackAnimation(bool state)
        {
            animator.SetBool(AttackAnim, state);
        }
    }
}

