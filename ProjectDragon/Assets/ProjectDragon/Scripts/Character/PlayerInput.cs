using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dragon
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector2 InputMovement => movement;

        [SerializeField] private bool visibleMouseCorsor = true;
        [SerializeField] private float cameraRotateXSpeed = 1f;

        private CharacterController characterController;
        private Vector2 movement;
        private Vector3 prevMousePosition;
        private Vector2 mouseChangeDelta;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            prevMousePosition = Input.mousePosition;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                visibleMouseCorsor = true;
            }
            if (Input.GetMouseButtonDown(0))
            {
                visibleMouseCorsor = false;
            }
            UnityEngine.Cursor.visible = visibleMouseCorsor;
            UnityEngine.Cursor.lockState = visibleMouseCorsor ? CursorLockMode.Locked : CursorLockMode.None;

            movement = Vector2.zero;
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            movement.x = x;
            movement.y = y;

            mouseChangeDelta = Vector2.zero;
            Vector3 mousePositionDelta = prevMousePosition - Input.mousePosition;
            float mouseChangeX = mousePositionDelta.x;
            float mouseChangeY = mousePositionDelta.y;
            mouseChangeDelta.x = mouseChangeX;
            mouseChangeDelta.y = mouseChangeY;
            prevMousePosition = Input.mousePosition;

            OnInputMovement();
        }

        void OnInputMovement()
        {
            characterController.SetMovementAnimation(movement.magnitude);
            characterController.SetMovementTransform(new Vector3(movement.x, 0, movement.y));
            characterController.SetRotate(transform.rotation.eulerAngles.y + (mouseChangeDelta.x * Time.deltaTime * cameraRotateXSpeed));
        }
    }
}

