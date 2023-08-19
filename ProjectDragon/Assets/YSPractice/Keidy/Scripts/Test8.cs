using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 8. �迭

public class Test8 : MonoBehaviour
{
    int[] exp = { 50, 100, 150, 200, 250 }; // ������ ����ü

    int[] array = new int[10]; // ũ�⸸ ����

    int[] array2; // �׸��� ����

    // 2���� �迭: 1���� �迭 2��
    int[,] array3 = { { 1, 2, 3, 4, 5 }, { 10, 20, 30, 40, 50 } };

    // 3���� �迭: 2���� �迭 2��
    int[,,] array4 = { { { 1, 2, 3, 4, 5 }, { 10, 20, 30, 40, 50 } }, { { 15, 25, 35, 45, 55 }, { 15, 25, 35, 45, 55 } } };

    void Start()
    {
        exp[4] = 500; // ���� O
        print(exp[4]);

        // exp[5] = 600; // ����: �迭�� ũ�� ����� �ۿ��� �ٲ� �� ���� (�߰�, ����)
        // print(exp[5]);

        print(exp[0]); // �迭��[�ε���]

        print(exp.Length);

        for(int i=0; i<exp.Length; i++) // �迭 ũ��(�ε��� ����) .Length
        {
            print(exp[i]);
        }

        array[0] = 1;

        array2=new int[exp.Length]; // ������ �迭 �׸��� ũ�� ����

        print(array3[1,3]); // 1(�ι�° �߰�ȣ)�� 3(�׹�° �ε���)

        print(array4[1, 0, 1]); // 1(�ι�° 2���� �迭)�� 1(ù��° �߰�ȣ)�� 1(�ι�° �ε���)
    }

}
