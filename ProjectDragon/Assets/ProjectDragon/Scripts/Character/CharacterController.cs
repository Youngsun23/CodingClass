using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragon
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private Animator characterAnimator;
        [SerializeField] private float movementSpeed = 1f;


        public void SetMovementTransform(Vector3 movement)
        {
            transform.Translate(movement * movementSpeed * Time.deltaTime);
        }

        public void SetRotate(float angle)
        {
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        public void SetMovementAnimation(float movement)
        {
            characterAnimator.SetFloat("Movement", movement);
        }
    }
}

