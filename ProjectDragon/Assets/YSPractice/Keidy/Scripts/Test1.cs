using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 1. 변수

// class == 코드 담는 그릇
public class Test1 : MonoBehaviour // 카메라에 넣어도 로그 출력 확인 가능이네
{
    // 1 (상수) + x (변수/숫자 담는 공간) = y
    int x=100; 
    int y = 100;
    int sum;
    

    void Start()
    {
        print(x);  // 아니 뭐야 여기도 print가 되네

        x = -500;

        print(x);

        x = x - 500; // '=' 우항값을 좌항에 집어넣어라

        print(x);

        sum = x + y;

        print(sum);

        print(-sum);

    }


}
