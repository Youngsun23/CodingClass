//// For �ݺ���
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;

//for (���� ���ǰ⿬��)
//{ ���๮; }

//// ����
//class Program
//{
//    static void Main(string[] args)
//    {
//        for (int i = 0; i <10;i++)
//        {
//            Console.WriteLine("Loop{0}", i);
//        }
//    }
//}

//// Foreach �ݺ���
//foreach (��������) ����(�迭, �÷���) �� ��� �ϳ��� ����
//{ ���๮; }  

//// ����
//static void Main(string[] args)
//{
//    string[] array = new string[] { "AB", "CD", "EF" };

//    foreach(string s in array)
//    {
//        Console.WriteLine(s);
//    }
//}

//// for vs foreach
//������ �����ϰų� for�� �ణ ������ foreach �ڵ尡 �� ����

//// ����
//static void Main(string[] args)
//{
//    string[,,] arr = new string[,,]
//    {
//        {{"1","2"},{ "11","22"}},
//        {{"3","4"},{"33","44"}}
//    };

//    for (int i = 0;i < arr.GetLength(0);i++)
//    {
//        for(int j = 0;j < arr.GetLength(1);j++)
//        {
//            for(int k=0;k < arr.GetLength(2);k++)
//            {
//                Debug.WriteLine(arr[i,j,k]);
//            }
//        }
//    }

//    foreach(var s in arr)
//    {
//        Debug.WriteLine(s);
//    }    
//}

//// While �ݺ���
//while (���ǽ�)
//{���๮;}

//// ����
//static void Main(string[] args)
//{
//    int i = 1;

//    while (i<=10)
//    {
//        Console.WriteLine(i);
//        i++;
//    }
//}

//// Do While �ݺ���
//do
//{ ���๮; } �� �� ���� ��,
//while (����) ���ǹ� üũ�ϰ� �����ϸ� ���

//// ����
//static void Main(string[] args)
//{ int i = 1;

//    do
//    {
//        Console.WriteLine(i);
//        i++;
//    }
//    while (i<10);
//}

//// ����
//using System;
//using System.Collections.Generic;

//namespace MySystem
//{
//    class Program
//    { static void Main(string[] args)
//        {
//            List<char> keyList = new List<char>();
//            ConsoleKeyInfo key;
//            do
//            {
//                key = Console.ReadKey();
//                keyList.Add(key.KeyChar);
//            } while (key.Key != ConsoleKey.Q);

//            Console.WriteLine();
//            foreach(char ch in keyList)
//            {
//                Console.Write(ch);
//            }
//        }
        
//}