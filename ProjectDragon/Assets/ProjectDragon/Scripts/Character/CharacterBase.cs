using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragon
{
    public class CharacterBase : MonoBehaviour
    {
        public bool IsRunning { get; set; } = false;

        /// <summary>
        /// public float HealthPoint 
        /// { 
        ///     get
        ///     {
        ///         return healthPoint;
        ///     } 
        /// }
        /// </summary>
        public float HealthPoint => healthPoint;
        public float StaminaPoint => staminaPoint;


        [SerializeField] private float healthPoint;
        [SerializeField] private float staminaPoint;
        [SerializeField] private float staminaRecoveryPoint;


        [SerializeField] private Animator characterAnimator;
        [SerializeField] private float walkSpeed = 1f;
        [SerializeField] private float runSpeed = 2f;

        [SerializeField] private Transform rightHandItemRoot;
        [SerializeField] private InteractionItemCollection itemCollection;

        private GameObject currentHandleItem;

        private void Update()
        {
            if (staminaPoint < 100f && !IsRunning)
            {
                staminaPoint += staminaRecoveryPoint * Time.deltaTime;
                staminaPoint = Mathf.Clamp(staminaPoint, 0, 100);
            }
        }

        public void SetMovementTransform(Vector3 movement)
        {
            float speed = IsRunning ? runSpeed : walkSpeed;
            if (IsRunning)
            {
                staminaPoint -= 1 * Time.deltaTime;
                if (staminaPoint <= 0)
                {
                    speed = walkSpeed;
                }
            }

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

        public void OnInteracted()
        {
            characterAnimator.SetTrigger("Trigger_EmotionGreeting");
        }
    }
}

