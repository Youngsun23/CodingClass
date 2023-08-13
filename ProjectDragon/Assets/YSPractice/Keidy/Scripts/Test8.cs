using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 8. 배열

public class Test8 : MonoBehaviour
{
    int[] exp = { 50, 100, 150, 200, 250 }; // 변수의 집합체

    int[] array = new int[10]; // 크기만 지정

    int[] array2; // 그릇만 생성

    // 2차원 배열: 1차원 배열 2개
    int[,] array3 = { { 1, 2, 3, 4, 5 }, { 10, 20, 30, 40, 50 } };

    // 3차원 배열: 2차원 배열 2개
    int[,,] array4 = { { { 1, 2, 3, 4, 5 }, { 10, 20, 30, 40, 50 } }, { { 15, 25, 35, 45, 55 }, { 15, 25, 35, 45, 55 } } };

    void Start()
    {
        exp[4] = 500; // 변경 O
        print(exp[4]);

        // exp[5] = 600; // 오류: 배열의 크기 선언부 밖에서 바꿀 수 없음 (추가, 제거)
        // print(exp[5]);

        print(exp[0]); // 배열명[인덱스]

        print(exp.Length);

        for(int i=0; i<exp.Length; i++) // 배열 크기(인덱스 개수) .Length
        {
            print(exp[i]);
        }

        array[0] = 1;

        array2=new int[exp.Length]; // 생성한 배열 그릇에 크기 지정

        print(array3[1,3]); // 1(두번째 중괄호)의 3(네번째 인덱스)

        print(array4[1, 0, 1]); // 1(두번째 2차원 배열)의 1(첫번째 중괄호)의 1(두번째 인덱스)
    }

}
