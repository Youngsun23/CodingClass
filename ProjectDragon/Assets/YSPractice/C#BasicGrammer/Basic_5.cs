//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Basic_5 : MonoBehaviour
//{
//    // �迭 : ���� Ÿ�� �����͵��� ����
//    // 1�� ~ 32��

//    // 1�� �迭
//    string[] players = new string[10];
//    string[] Regions = { "����", "���", "�λ�" };

//    // 2�� �迭
//    string[,] Depts = { { "�����", "�渮��" }, { "�̰���", "�ѹ���" } };

//    // 3�� �迭
//    string[,,] Cubes;

//    // ���� �迭 : ù ������ Ȯ��, �� �̻��� ��Ÿ�ӽ� �������� ���� �ٸ� ũ���� �迭 ����
//    int[][] A = new int[3][]; // �迭 3����¥�� �迭
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

//    // �迭 - Reference Ÿ�� (��� ������ x ������ Reference pointer���� ����)
//    static void Main(string[] args)
//    {
//        int[] scores = { 80, 78, 60, 90, 100 };
//        int sum = CalculateSum(scores); // �迭 ����: �迭�� ��� (�ش� �迭�� �μ��� �޴� �Լ�)
//        Console.WriteLine(sum);
//    }

//    static int CalculateSum(int[] scoresArray) // �迭 �޴� �� (�迭 ������ Ÿ��[] �迭 �Ķ���͸�)
//    {
//        int sum = 0;
//        for (int i=0; i<scoresArray.Length; i++)
//        {
//            sum += scoresArray[i];
//        }
//        return sum;
//    }


