using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 1. ����

// class == �ڵ� ��� �׸�
public class Test1 : MonoBehaviour // ī�޶� �־ �α� ��� Ȯ�� �����̳�
{
    // 1 (���) + x (����/���� ��� ����) = y
    int x=100; 
    int y = 100;
    int sum;
    

    void Start()
    {
        print(x);  // �ƴ� ���� ���⵵ print�� �ǳ�

        x = -500;

        print(x);

        x = x - 500; // '=' ���װ��� ���׿� ����־��

        print(x);

        sum = x + y;

        print(sum);

        print(-sum);

    }


}
