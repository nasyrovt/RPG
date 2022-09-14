using UnityEngine;
using UnityEngine.AI;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        bool isDead;
        public bool IsDead { get { return isDead; } }


        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0 && !isDead)
            {
                Die();
            }

        }

        private void Die()
        {
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<Collider>().enabled = false;
            GetComponent<CombatTarget>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            isDead = true;
        }
    }
}
