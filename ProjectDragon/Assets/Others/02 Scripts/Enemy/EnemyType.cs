using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonType //타입
{
    Normal,
    AOE,
    Fly
        // 다른 몬스터 타입 추가
}

public class EnemyType : MonoBehaviour
{

    public MonType monType;
}
