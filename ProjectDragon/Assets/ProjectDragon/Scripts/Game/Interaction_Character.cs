using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragon
{
    public class Interaction_Character : InteractionBase
    {
        public CharacterBase linkedCharacter;

        // override => "재정의"
        // override 키워드를 사용해서 virtual 함수/변수를  재.정.의 해서 구현 할 수 있다.
        public override InteractionObjectType InteractionObjectType
        {
            get => InteractionObjectType.Character;
        }

        public override void OnInteraction()
        {
            linkedCharacter.OnInteracted();
        }
    }
}

