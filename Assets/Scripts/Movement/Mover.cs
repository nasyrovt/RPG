using RPG.Core;
using UnityEngine;
using UnityEngine.AI;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] float maxSpeed = 6f;
        Animator characterAnimator;
        NavMeshAgent nmAgent;
        Health health;
        Collider thisCollider;


        private void Awake()
        {
            thisCollider = GetComponent<Collider>();
            health = GetComponent<Health>();
            characterAnimator = GetComponent<Animator>();
            nmAgent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {
            if (thisCollider != null)
                thisCollider.enabled = !health.IsDead;
            nmAgent.enabled = !health.IsDead;
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            nmAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            nmAgent.destination = destination;
            nmAgent.isStopped = false;
        }

        public void Cancel()
        {
            nmAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(nmAgent.velocity);
            float speed = localVelocity.z;
            characterAnimator.SetFloat("forwardSpeed", speed);
        }
    }

}
