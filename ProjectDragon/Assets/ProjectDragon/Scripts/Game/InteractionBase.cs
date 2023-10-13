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
    }

    public class InteractionBase : MonoBehaviour
    {
        [field: SerializeField] public IteractionObjectType InteractionObjectType { get; private set; }


    }
}
