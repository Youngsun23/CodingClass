using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragon
{
    public enum InteractionObjectType
    {
        None = 0,
        Tree,
        Rock,
        Earth,

        Character,
    }

    public class InteractionBase : MonoBehaviour
    {
        // virtual 키워드 => "가상"
        // 함수 앞에 붙으면 => "가상 함수"
        // 변수 앞에 붙으면 => "가상 변수"
        [field: SerializeField] public virtual InteractionObjectType InteractionObjectType { get; protected set; }

        public virtual void OnInteraction() { }
    }
}
