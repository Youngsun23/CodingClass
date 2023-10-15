using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragon
{
    public class CharacterController : MonoBehaviour
    {
        public bool IsRunning { get; set; } = false;

        [SerializeField] private Animator characterAnimator;
        [SerializeField] private float walkSpeed = 1f;
        [SerializeField] private float runSpeed = 2f;

        [SerializeField] private Transform rightHandItemRoot;
        [SerializeField] private InteractionItemCollection itemCollection;

        private GameObject currentHandleItem;

        public void SetMovementTransform(Vector3 movement)
        {
            float speed = IsRunning ? runSpeed : walkSpeed;
            transform.Translate(movement * speed * Time.deltaTime);
        }

        public void SetRotate(float angle)
        {
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        public void SetMovementAnimation(float movement)
        {
            characterAnimator.SetFloat("Movement", movement);
        }

        public void SetActionAnimation(int actionType)
        {
            int currentAnimActionType = characterAnimator.GetInteger("ActionType");
            if (currentAnimActionType == actionType)
            {
                return;
            }

            characterAnimator.SetInteger("ActionType", actionType);
            switch (actionType)
            {
                case 0:
                    {
                        if (currentHandleItem)
                        {
                            Destroy(currentHandleItem);
                            currentHandleItem = null;
                        }
                    }
                    break;
                case 1: // ����
                    {

                    }
                    break;
                case 2: // ���
                    {
                        var itemData = itemCollection.ItemCollection.Find(x => x.TargetObjectType == IteractionObjectType.Rock);
                        var newHandItem = Instantiate(itemData.Prefab, rightHandItemRoot);
                        currentHandleItem = newHandItem;
                    }
                    break;
                case 3: // ����
                    {
                        var itemData = itemCollection.ItemCollection.Find(x => x.TargetObjectType == IteractionObjectType.Tree);
                        var newHandItem = Instantiate(itemData.Prefab, rightHandItemRoot);
                        currentHandleItem = newHandItem;
                    }
                    break;

            }
        }
    }
}

