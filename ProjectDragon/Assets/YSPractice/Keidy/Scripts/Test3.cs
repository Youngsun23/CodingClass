using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 3. �Լ�

public class Test3 : MonoBehaviour
{
    int intValue;

    float floatValue = 10.5f;
    float floatValue2 = 20.5f;

    int result;

    void FloatToInt(float parameter, float parameter2, string parameter3="�����") // �μ��� �ٲ㼭 �����Լ� ������ ���� ���� ��
        // ���ʿ��� ����Ʈ�� �̸� �־���� �� ����
    {
        intValue= (int) (parameter + parameter2);
        print(intValue);
        print(parameter3);
    }

    int Four()
    {
        return 4; // return���� �Լ��� ����� (������ �ڵ�� ���õ�)
    }


    void Start()
    {
        FloatToInt(floatValue, floatValue2); // �� ��° �μ� �Է� ���̵� "�����"�� ó��
        FloatToInt(floatValue, floatValue2, "����");

        result = Four(); // �Լ� return���� ������ �ֱ�
        print(result);

        print(FloatToInt2(floatValue, floatValue2));

    }

    // �Լ� �ȿ� �Լ�

    int FloatToInt2(float par, float par2)
    {
        return Multiply((int)(par + par2));
    }    

    int Multiply(int par)
    {
        return (int)(par * par);
    }

}
