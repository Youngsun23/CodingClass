//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//// 4. ������

//public class Test4_1
//{ 
//    // ���� ������ (����, �Լ� �� ��� ����)
//    private int b; // Ŭ���� �������� ���
//    public int c; // ��� Ŭ�������� ��� - ���� ������
//    public static int d; // ���� �ڿ�
//}


//public class Test4 : MonoBehaviour
//{
//    int a = 5; // ���� ����

//    Test4_1 a1 = new Test4_1(); // �ٸ� Ŭ���� ����� �غ� (�׸�)
//    Test4_1 a2 = new Test4_1(); // ���� �ϰ� =new ������ Null ����
//    Test4_1 a3 = new Test4_1();


//    void Aaa()
//    {
//        // aaa.b = 5; // ����: ��ȣ ���� ������ �׼����� �� �����ϴ�.
//        a1.c = 5;
//        a2.c = 10;
//        a3.c = 15;

//        print(a1.c);
//        print(a2.c);
//        print(a3.c);

//        Test4_1.d = 100; // ���� �ڿ�: Ŭ���� ��ü�� ���� -> a1, a2, a3 ��ü �����ϰ� ���� (���� ����)
//        print(Test4_1.d); 
//    }

//    void Abc() // ���� ����: ���� �ÿ��� ���� (����-�ı�)
//    {
//        int a = 5; // �̸� ���� ���, ���� ���� ���õǰ� ���� ���� a�� ���� �ڵ� ���� (6 �Ҵ�)
//        a = 6; // �������� ��� ����

//        int b = 5; // ���� �ٸ� ���� ����
//        print(b);
//    }

//    void Abc2()
//    {
//        int b = 6; // ���� �ٸ� ���� ����
//        print(b);
//    }

//    void Abc3(int parameter) // �Ű� ����
//    {

//    }

//    void Start()
//    {
//        // print(b); // ����: ���� ���ؽ�Ʈ�� �����ϴ�.
//        Aaa();
//    }


//}
