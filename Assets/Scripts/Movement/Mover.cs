using RPG.Core;
using UnityEngine;
using UnityEngine.AI;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        Animator characterAnimator;
        NavMeshAgent nmAgent;
        Health health;

        private void Awake()
        {
            health = GetComponent<Health>();
            characterAnimator = GetComponent<Animator>();
            nmAgent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {
            nmAgent.enabled = !health.IsDead;
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
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
