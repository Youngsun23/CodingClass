//// For 반복문
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;

//for (범위 조건겸연산)
//{ 실행문; }

//// 예제
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

//// Foreach 반복문
//foreach (범위조건) 범위(배열, 컬렉션) 내 요소 하나씩 실행
//{ 실행문; }  

//// 예제
//static void Main(string[] args)
//{
//    string[] array = new string[] { "AB", "CD", "EF" };

//    foreach(string s in array)
//    {
//        Console.WriteLine(s);
//    }
//}

//// for vs foreach
//성능은 유사하거나 for가 약간 빠르나 foreach 코드가 더 간결

//// 예제
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

//// While 반복문
//while (조건식)
//{실행문;}

//// 예제
//static void Main(string[] args)
//{
//    int i = 1;

//    while (i<=10)
//    {
//        Console.WriteLine(i);
//        i++;
//    }
//}

//// Do While 반복문
//do
//{ 실행문; } 한 번 실행 후,
//while (조건) 조건문 체크하고 만족하면 계속

//// 예제
//static void Main(string[] args)
//{ int i = 1;

//    do
//    {
//        Console.WriteLine(i);
//        i++;
//    }
//    while (i<10);
//}

//// 예제
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