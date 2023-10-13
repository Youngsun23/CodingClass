using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragon
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private Animator characterAnimator;
        [SerializeField] private float movementSpeed = 1f;

        [SerializeField] private Transform rightHandItemRoot;
        [SerializeField] private InteractionItemCollection itemCollection;

        private GameObject currentHandleItem;

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
                case 1: // »ðÁú
                    {

                    }
                    break;
                case 2: // °î±ªÀÌ
                    {
                        var itemData = itemCollection.ItemCollection.Find(x => x.TargetObjectType == IteractionObjectType.Rock);
                        var newHandItem = Instantiate(itemData.Prefab, rightHandItemRoot);
                        currentHandleItem = newHandItem;
                    }
                    break;
                case 3: // µµ³¢
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

