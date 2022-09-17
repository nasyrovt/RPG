using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Fighter fighter;
        Health health;

        private void Start()
        {
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
        }

        void Update()
        {
            if (health.IsDead) return;
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hitResults = Physics.RaycastAll(GetMousePositionRay());
            foreach (RaycastHit hit in hitResults)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButton(1))
                {
                    fighter.Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            bool hasHit = Physics.Raycast(GetMousePositionRay(), out RaycastHit hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(1))
                    mover.StartMoveAction(hit.point, 1f);
                return true;
            }
            return false;
        }



        private static Ray GetMousePositionRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}
