using System;
using RPG.Control;
using RPG.Movement;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool cinematicPlayed;

        private void OnTriggerEnter(Collider other)
        {
            if (!cinematicPlayed && other.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
                cinematicPlayed = true;
            }
        }
    }
}
