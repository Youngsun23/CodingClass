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

        private List<InteractionBase> currentInteractableObjects = new List<InteractionBase>();
        private bool isInteractable_Tree = false;
        private bool isInteractable_Rock = false;

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

            if (Input.GetKey(KeyCode.F))
            {
                OnInputInteraction();
            }
            else
            {
                characterController.SetActionAnimation(0);
            }
        }

        void OnInputMovement()
        {
            characterController.SetMovementAnimation(movement.magnitude);
            characterController.SetMovementTransform(new Vector3(movement.x, 0, movement.y));
            characterController.SetRotate(transform.rotation.eulerAngles.y + (mouseChangeDelta.x * Time.deltaTime * cameraRotateXSpeed));
        }


        void OnInputInteraction()
        {
            if (!isInteractable_Tree && !isInteractable_Rock)
            {
                characterController.SetActionAnimation(0);
                return;
            }

            if (isInteractable_Tree)
            {
                characterController.SetActionAnimation(3);
            }
            else if (isInteractable_Rock)
            {
                characterController.SetActionAnimation(2);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent<InteractionBase>(out var interactionItem))
            {
                switch (interactionItem.InteractionObjectType)
                {
                    case IteractionObjectType.Tree:
                        isInteractable_Tree = true;
                        break;
                    case IteractionObjectType.Rock:
                        isInteractable_Rock = true;
                        break;
                }

                currentInteractableObjects.Add(interactionItem);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.TryGetComponent<InteractionBase>(out var interactionItem))
            {
                currentInteractableObjects.Remove(interactionItem);

                switch (interactionItem.InteractionObjectType)
                {
                    case IteractionObjectType.Tree:
                        {
                            if (false == currentInteractableObjects.Exists(x => x.InteractionObjectType == IteractionObjectType.Tree))
                            {
                                isInteractable_Tree = false;
                            }
                        }
                        break;
                    case IteractionObjectType.Rock:
                        {
                            if (false == currentInteractableObjects.Exists(x => x.InteractionObjectType == IteractionObjectType.Rock))
                            {
                                isInteractable_Rock = false;
                            }
                        }
                        break;
                }
            }
        }
    }
}

