using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

// 12. ��������Ʈ

public class Test12 : MonoBehaviour
{
    public delegate void ChainFunction(int value); // ����� �ټ��� �Լ� �������� �Լ� ��� ����
    ChainFunction chain;

    public static event ChainFunction OnStart; // ���� chain ��ſ� OnStart ��� -> �ٸ� ��ũ��Ʈ���� ����

    // delegate�� �� Ŭ���� ���� �Լ��� ����
    // event�� delegate�� �޾Ƽ� ���, Ÿ Ŭ������ �Լ����� ����

    int power;
    int defence;

    public void SetPower(int value) // �Լ� ��� ���ڿ� �Լ��� �Ķ���� ��ġ �ʿ�
    {
        power += value;
        print($"Power�� ���� {value}��ŭ �����߽��ϴ�. �� power�� �� = {power}");
    }

    public void SetDefence(int value)
    {
        defence += value;
        print($"Defence�� ���� {value}��ŭ �����߽��ϴ�. �� Defense�� �� = {defence}");
    }

    void Start()
    {
        chain += SetPower; // �Լ� �߰�
        chain += SetDefence;

        chain(5);
        // �Լ� 2�� �� �� �� �Ͱ� ����
        // SetPower(5);
        // SetDefence(5); 

        chain -= SetPower; // �Լ� ����
        chain -= SetDefence; 

        if(chain != null) // ���� ����
        {
            chain(5);
        }

    }

    private void OnDisable() // ������Ʈ ��Ȱ��ȭ, ���� ���� �� ȣ��
    {
        OnStart(5);
    }

}
