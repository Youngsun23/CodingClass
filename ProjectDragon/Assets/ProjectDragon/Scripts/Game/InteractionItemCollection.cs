using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragon
{
    [System.Serializable]
    public class InteractionItemData
    {
        public InteractionObjectType TargetObjectType;
        public GameObject Prefab;
    }

    [CreateAssetMenu(fileName = "New Interaction Item Collection", menuName = "Dragon/Interaction Item Collection")]
    public class InteractionItemCollection : ScriptableObject
    {
        [field: SerializeField] public List<InteractionItemData> ItemCollection { get; private set; }
    }
}

