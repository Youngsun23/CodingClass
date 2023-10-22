using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragon
{
    public enum CharacterType
    {
        None = 0,
        Player = 1,
        NPC = 2,
    }

    public class CharacterBase : MonoBehaviour
    {
        public static List<CharacterBase> SpawnedCharacters = new List<CharacterBase>();

        public bool IsRunning { get; set; } = false;
        public float HealthPoint => healthPoint;
        public float StaminaPoint => staminaPoint;
        public CharacterType CharacterType => characterType;


        [SerializeField] private float healthPoint;
        [SerializeField] private float staminaPoint;
        [SerializeField] private float staminaRecoveryPoint;

        [SerializeField] private Animator characterAnimator;
        [SerializeField] private float walkSpeed = 1f;
        [SerializeField] private float runSpeed = 2f;

        [SerializeField] private Transform rightHandItemRoot;
        [SerializeField] private InteractionItemCollection itemCollection;
        [SerializeField] private CharacterType characterType;

        private GameObject currentHandleItem;

        private void Awake()
        {
            SpawnedCharacters.Add(this);
        }

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
                        ResetHandleItem();
                    }
                    break;
                case 1: // »ðÁú
                    {
                        SetHandleItem(InteractionObjectType.Earth);
                    }
                    break;
                case 2: // °î±ªÀÌ
                    {
                        SetHandleItem(InteractionObjectType.Rock);
                    }
                    break;
                case 3: // µµ³¢
                    {
                        SetHandleItem(InteractionObjectType.Tree);
                    }
                    break;

            }
        }

        private void ResetHandleItem()
        {
            if (currentHandleItem)
            {
                Destroy(currentHandleItem);
                currentHandleItem = null;
            }
        }

        private void SetHandleItem(InteractionObjectType type)
        {
            var itemData = itemCollection.ItemCollection.Find(x => x.TargetObjectType == type);
            var newHandItem = Instantiate(itemData.Prefab, rightHandItemRoot);
            if (currentHandleItem != newHandItem)
            {
                Destroy(currentHandleItem);
                currentHandleItem = null;
            }
            currentHandleItem = newHandItem;
        }

        public void OnInteracted()
        {
            characterAnimator.SetTrigger("Trigger_EmotionGreeting");
        }
    }
}

