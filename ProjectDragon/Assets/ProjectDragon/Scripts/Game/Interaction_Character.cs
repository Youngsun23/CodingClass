using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragon
{
    public class Interaction_Character : InteractionBase
    {
        public CharacterBase linkedCharacter;

        // override => "������"
        // override Ű���带 ����ؼ� virtual �Լ�/������  ��.��.�� �ؼ� ���� �� �� �ִ�.
        public override IteractionObjectType InteractionObjectType
        {
            get => IteractionObjectType.Character;
        }

        public override void OnInteraction()
        {
            linkedCharacter.OnInteracted();
        }
    }
}

