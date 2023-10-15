using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragon
{
    public enum IteractionObjectType
    {
        None = 0,
        Tree,
        Rock,

        Character,
    }

    public class InteractionBase : MonoBehaviour
    {
        // virtual 키워드 => "가상"
        // 함수 앞에 붙으면 => "가상 함수"
        // 변수 앞에 붙으면 => "가상 변수"
        [field: SerializeField] public virtual IteractionObjectType InteractionObjectType { get; protected set; }

        public virtual void OnInteraction() { }
    }
}
