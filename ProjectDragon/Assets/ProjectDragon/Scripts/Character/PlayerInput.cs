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

        private CharacterBase linkedCharacter;
        private Vector2 movement;
        private Vector3 prevMousePosition;
        private Vector2 mouseChangeDelta;
        private bool isRun = false;

        private List<InteractionBase> currentInteractableObjects = new List<InteractionBase>();
        private bool isInteractable_Tree = false;
        private bool isInteractable_Rock = false;
        private bool isInteractable_Earth = false;

        private float diggingTime=3f;
        private float curdiggingTime;
        public GameObject Cube;

        private void Start()
        {
            linkedCharacter = GetComponent<CharacterBase>();
            prevMousePosition = Input.mousePosition;

            var ingameUI = UIManager.Singleton.GetUI<IngameUI>(UIList.IngameUI);
            ingameUI.SetHealth(100, 100);
            ingameUI.SetStamina(100, 100);

            curdiggingTime = diggingTime;
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

            isRun = Input.GetKey(KeyCode.LeftShift);
            linkedCharacter.IsRunning = isRun;

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
                linkedCharacter.SetActionAnimation(0);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                linkedCharacter.SetTriggerEmotion();
            }
        }

        private void LateUpdate()
        {
            float currentStamina = linkedCharacter.StaminaPoint;
            var ingameUI = UIManager.Singleton.GetUI<IngameUI>(UIList.IngameUI);
            ingameUI.SetStamina(currentStamina, 100f);
        }

        void OnInputMovement()
        {
            float magnitude = movement.magnitude * (isRun ? 1 : 0.5f);
            linkedCharacter.SetMovementAnimation(magnitude);
            linkedCharacter.SetMovementTransform(new Vector3(movement.x, 0, movement.y));

            if (magnitude > 0)
            {
                linkedCharacter.SetRotate(transform.rotation.eulerAngles.y + (mouseChangeDelta.x * Time.deltaTime * cameraRotateXSpeed));
                CameraController.Instance.SetCameraActive(IngameCameraType.FollowCamera);
            }
            else
            {
                //CameraController.Instance.SetCameraActive(IngameCameraType.FreelookCamera);
            }
        }


        void OnInputInteraction()
        {
            if (!isInteractable_Tree && !isInteractable_Rock && !isInteractable_Earth)
            {
                linkedCharacter.SetActionAnimation(0);
                return;
            }
            if (isInteractable_Tree)
            {
                linkedCharacter.SetActionAnimation(3);
            }
            else if (isInteractable_Rock)
            {
                linkedCharacter.SetActionAnimation(2);
            }
            else if(isInteractable_Earth)
            {
                linkedCharacter.SetActionAnimation(1);
                curdiggingTime -= 1f * Time.deltaTime;
                if (curdiggingTime <= 0)
                {
                    Instantiate(Cube, transform.position, transform.rotation);
                    curdiggingTime = diggingTime;
                }
                //문제// 큐브를 Rock으로 하면, f키 입력이 끝나도 삽이 안 사라짐
            }
            if(!isInteractable_Earth)
            {
                curdiggingTime = diggingTime;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent<InteractionBase>(out var interactionItem))
            {
                switch (interactionItem.InteractionObjectType)
                {
                    case InteractionObjectType.Tree:
                        isInteractable_Tree = true;
                        break;
                    case InteractionObjectType.Rock:
                        isInteractable_Rock = true;
                        break;
                    case InteractionObjectType.Character:
                        interactionItem.OnInteraction();
                        break;
                    case InteractionObjectType.Earth:
                        isInteractable_Earth = true;
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
                    case InteractionObjectType.Tree:
                        {
                            if (false == currentInteractableObjects.Exists(x => x.InteractionObjectType == InteractionObjectType.Tree))
                            {
                                isInteractable_Tree = false;
                            }
                        }
                        break;
                    case InteractionObjectType.Rock:
                        {
                            if (false == currentInteractableObjects.Exists(x => x.InteractionObjectType == InteractionObjectType.Rock))
                            {
                                isInteractable_Rock = false;
                            }
                        }
                        break;
                    case InteractionObjectType.Earth:
                        {
                            if (false == currentInteractableObjects.Exists(x => x.InteractionObjectType == InteractionObjectType.Earth))
                            {
                                isInteractable_Earth = false;
                            }
                        }
                        break;
                }
            }
        }
    }
}

