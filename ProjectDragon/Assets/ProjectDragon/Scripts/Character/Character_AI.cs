using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Dragon
{
    public class Character_AI : MonoBehaviour
    {
        [field: Header("Character Components")]
        [field: SerializeField] public NavMeshAgent NavAgent { get; private set; }
        [field: SerializeField] public CharacterBase LinkedCharacter { get; private set; }


        [Header("Character Settings")]
        public float brainFrequency;
        public float lastBrainTime = 0;

        private void Update()
        {
            if (lastBrainTime + brainFrequency <= Time.time)
            {
                var playerCharacter = CharacterBase.SpawnedCharacters.Find(x => x.CharacterType == CharacterType.Player);
                SetDestination(playerCharacter.transform.position);
                lastBrainTime = Time.time;
            }

            LinkedCharacter.SetMovementAnimation(NavAgent.velocity.magnitude * 0.5f);

            //Debug.Log($"NavAgent.velocity.magnitude : {NavAgent.velocity.magnitude}");
            //Debug.Log($"Time.time : {Time.time}");
            //Debug.Log($"NavAgent.velocity : {NavAgent.velocity}");
        }

        /// <summary> Nav Agent 를 목표 지점으로 이동명령을 내리는 함수 </summary>
        public void SetDestination(Vector3 position)
        {
            float distance = Vector3.Distance(transform.position, position);
            if (distance >= 2f)
            {
                NavAgent.SetDestination(position);
            }
        }
    }
}


