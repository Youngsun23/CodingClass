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
        // virtual Ű���� => "����"
        // �Լ� �տ� ������ => "���� �Լ�"
        // ���� �տ� ������ => "���� ����"
        [field: SerializeField] public virtual InteractionObjectType InteractionObjectType { get; protected set; }

        public virtual void OnInteraction() { }
    }
}
