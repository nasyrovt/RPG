using RPG.Core;
using RPG.Movement;
using UnityEngine;


namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 0.5f;

        private Transform target;
        private Mover mover;
        private Animator animator;
        private float timeSinceLastAttack;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (!GetIsInRange())
                mover.MoveTo(target.position);
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }

        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0f;
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }

        //Animation event
        void Hit()
        {

        }
    }

}