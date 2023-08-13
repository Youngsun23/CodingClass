using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

// 12. 델리게이트

public class Test12 : MonoBehaviour
{
    public delegate void ChainFunction(int value); // 비슷한 다수의 함수 관리위해 함수 담는 상자
    ChainFunction chain;

    public static event ChainFunction OnStart; // 위의 chain 대신에 OnStart 사용 -> 다른 스크립트에서 접근

    // delegate는 한 클래스 내의 함수들 관리
    // event는 delegate를 받아서 사용, 타 클래스의 함수까지 관리

    int power;
    int defence;

    public void SetPower(int value) // 함수 담는 상자와 함수의 파라미터 일치 필요
    {
        power += value;
        print($"Power의 값이 {value}만큼 증가했습니다. 총 power의 값 = {power}");
    }

    public void SetDefence(int value)
    {
        defence += value;
        print($"Defence의 값이 {value}만큼 증가했습니다. 총 Defense의 값 = {defence}");
    }

    void Start()
    {
        chain += SetPower; // 함수 추가
        chain += SetDefence;

        chain(5);
        // 함수 2개 둘 다 한 것과 동일
        // SetPower(5);
        // SetDefence(5); 

        chain -= SetPower; // 함수 제거
        chain -= SetDefence; 

        if(chain != null) // 오류 방지
        {
            chain(5);
        }

    }

    private void OnDisable() // 오브젝트 비활성화, 게임 종료 시 호출
    {
        OnStart(5);
    }

}
