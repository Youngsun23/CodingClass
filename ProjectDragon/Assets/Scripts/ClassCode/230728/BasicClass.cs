using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicClass : MonoBehaviour
{
    // Unity Event Life Cycle
    // -> Awake -> OnEnable -> Start -> FixedUpdate -> Update -> LateUpdate -> Rendering! (Cycle�� �Լ� �� ���)
    // -> Awake -> OnEnable -> Start -> FixedUpdate -> Update -> LateUpdate -> Rendering! -> ����? -> OnDisable -> OnDestroy

    // bool     System.Boolean      True or False
    // byte     System.Byte         8��Ʈ unsigned integer
    // sbyte    System.SByte        8��Ʈ signed integer
    // short    System.Int16        16��Ʈ signed integer
    // int      System.Int32        32��Ʈ signed integer
    // long     System.Int64        64��Ʈ signed integer
    // ushort   System.UInt16       16��Ʈ unsigned integer
    // uint     System.UInt32       32��Ʈ unsigned integer
    // ulong    System.UInt64       64��Ʈ unsigned integer
    // float    System.Single       32��Ʈ single precision �ε��Ҽ��� ����
    // double   System.Double       64��Ʈ double precision �ε��Ҽ��� ����
    // decimal  System.Decimal      128��Ʈ Decimal
    // char     System.Char         16��Ʈ �����ڵ� ����
    // string   System.String       �����ڵ� ���ڿ�
    // System.DateTime              ��¥�� �ð�, ������ C# Ű���尡 ����
    // object   System.Object       ��� Ÿ���� �⺻ Ŭ������ ��� ������ ������ �� ����

    // ���� => Variables
    // ������ �����Ѵ�.
    // int (��� �κ��� ������ Ÿ��:Type)
    // HP (��� �κ��� ������ �̸�:Name)
    // 100 (��� �κ��� ������ ��:Value)
    int HP = 100;

    // ���� �ִ´�? "=" 1���� ǥ��
    // ���� ��� ���� equal �̶�� ���� => "==" 2���� ǥ��

    bool        isFlag;      // false == 0, true == 1 (:0 �� �ƴϸ� ���δ� true)
    int         intValue;    // ����(����, 0, ���) ǥ���� ������ ~43�ﰳ(-21�� ~ 21��)
    long        longValue;   // ����(����, 0, ���) ǥ���� ������...�ѻ纮
    float       floatValue;  // �Ҽ��� ���� 0.001f, -10.998f, (�Ҽ��� 5�ڸ������� ��)
    double      doubleValue; // �Ҽ��� ���� 3242342340.00000000000000000001 �ǰ� ū �Ҽ����� ����� ���
    string      stringValue; // ���ڸ� ǥ���� �� ���� ��
    uint        uintValue;   // (0, ����� => �ڿ����� ���� => ��ȣ�� ���� => ������ ����) (0 ~ 42��?)
    
    void Start()
    {
        LogGenerator();

        isFlag = true;
        isFlag = false;
        intValue = -10000;
        intValue = 10000;
        floatValue = 0.00001f;
        doubleValue = 10.999999999999999;
        stringValue = "Hello World";
        uintValue = 875938475;

        Debug.Log("Hello World!!!!!!");

        Debug.Log($"A Type => isFlag : {isFlag}");
        // Debug.LogFormat("B Type => isFlag:{0}", isFlag);

        int resultValue = SumResult(1, 2);
        Debug.Log($"Result Value : {resultValue}");

    }

    void LogGenerator()
    {
        Debug.Log("Hello World 1");
        Debug.Log("Hello World 2");
        Debug.Log("Hello World 3");
        Debug.Log("Hello World 4");
        Debug.Log("Hello World 5");
        Debug.Log("Hello World 6");

        InternalLogGenerator();
    }

    void InternalLogGenerator()
    {
        Debug.Log("Deep 1");
        Debug.Log("Deep 2");
        Debug.Log("Deep 3");
    }

    // �޼����� ���� : ���� �ϴ� Ÿ��, �Լ� �̸�, �Ķ����
    void MyMethod(int a, int b, int c, string name)
    {

    }

    int SumResult(int a, int b)
    {
        int result = a + b;
        return result;
    }
}
