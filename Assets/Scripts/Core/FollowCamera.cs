using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;

        void LateUpdate()
        {
            transform.position = playerTransform.position;
        }
    }

}
