//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Basic_5 : MonoBehaviour
//{
//    // 배열 : 동일 타입 데이터들의 집합
//    // 1차 ~ 32차

//    // 1차 배열
//    string[] players = new string[10];
//    string[] Regions = { "서울", "경기", "부산" };

//    // 2차 배열
//    string[,] Depts = { { "김과장", "경리부" }, { "이과장", "총무부" } };

//    // 3차 배열
//    string[,,] Cubes;

//    // 가변 배열 : 첫 차원은 확정, 그 이상은 런타임시 동적으로 서로 다른 크기의 배열 지정
//    int[][] A = new int[3][]; // 배열 3묶음짜리 배열
//    A[0] = new int[2];
//    A[1] = new int[] {1,2,3};
//    A[2] = new int[] { 1, 2, 3, 4 };
//}

//    static void Main(string[] args)
//    {
//        int sum = 0;
//        int[] scores = { 80, 78, 60, 90, 100 };
//        for (int i=0; i<scores.Length; i++)
//        {
//            sum += scores[i];
//        }
//        Console.WriteLine(sum);

//    }

//    // 배열 - Reference 타입 (모든 데이터 x 참조값 Reference pointer만을 전달)
//    static void Main(string[] args)
//    {
//        int[] scores = { 80, 78, 60, 90, 100 };
//        int sum = CalculateSum(scores); // 배열 전달: 배열명 사용 (해당 배열을 인수로 받는 함수)
//        Console.WriteLine(sum);
//    }

//    static int CalculateSum(int[] scoresArray) // 배열 받는 쪽 (배열 데이터 타입[] 배열 파라미터명)
//    {
//        int sum = 0;
//        for (int i=0; i<scoresArray.Length; i++)
//        {
//            sum += scoresArray[i];
//        }
//        return sum;
//    }


