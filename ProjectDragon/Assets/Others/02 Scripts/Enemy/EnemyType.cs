using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonType //Ÿ��
{
    Normal,
    AOE,
    Fly
        // �ٸ� ���� Ÿ�� �߰�
}

public class EnemyType : MonoBehaviour
{

    public MonType monType;
}
