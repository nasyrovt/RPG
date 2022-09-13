using UnityEngine;
using RPG.Movement;
using RPG.Combat;


namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Fighter fighter;

        private void Awake()
        {
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
        }

        void Update()
        {
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

                if (Input.GetMouseButtonDown(1))
                {
                    fighter.Attack(target);
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
                    mover.StartMoveAction(hit.point);
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
