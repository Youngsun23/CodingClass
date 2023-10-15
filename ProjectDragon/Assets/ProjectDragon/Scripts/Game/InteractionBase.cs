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
        [field: SerializeField] public virtual IteractionObjectType InteractionObjectType { get; protected set; }

        public virtual void OnInteraction() { }
    }
}
