//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//// 2. �ڷ���

//public class Test2 : MonoBehaviour
//{
//    // byte(255) sbyte(+-128) ushort(+6��) short(+-3��) integer(+-20��) long ���� �ڷ���
//    // short a = 40000; ����

//    float f = 4.001f;
//    double d = 4.001;
//    decimal m = 4.001m;
//    // float double decimal (���� ���� ���� �۾���) �Ǽ� �ڷ��� 

//    string s = "�����u";
//    char c = 'a';
//    // char(�����ڵ� ���) string(��) ���� �ڷ���

//    bool aa = true;
//    bool bb = false;

//    void Start()
//    {
//        // ĳ��Ʈ�� �ִ��� Ȯ���ϼ��� (int<float) (int<long)
//        int q = 100;
//        float w = 100.15f;
//        int sum1;
//        // sum1 = q + w; 
//        sum1 = (int)(q + w); // Console: 200
//        // ĳ��Ʈ: ���� �ڷ��� ��ȯ (�ٲ� �ڷ����� ���� �ʴ� �κ��� �߷�����)

//        int e = 100;
//        long r = 100;
//        int sum2;
//        // sum2 = e + r;
//        sum2 = (int)(e + r);

//        print(sum1);

//        int z = 100;
//        string v;
//        v = z.ToString(); // ���ڸ� ���ڿ��� ��ȯ
//        print(v);

//        int za;
//        string va = "100";
//        za = int.Parse(va); // (����) ���ڿ��� ���ڷ� ��ȯ
//        print(za);
//    }

//}
