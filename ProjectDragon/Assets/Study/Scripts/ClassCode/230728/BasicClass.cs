using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicClass : MonoBehaviour
{
    // Unity Event Life Cycle
    // -> Awake -> OnEnable -> Start -> FixedUpdate -> Update -> LateUpdate -> Rendering! (Cycle이 게속 돌 경우)
    // -> Awake -> OnEnable -> Start -> FixedUpdate -> Update -> LateUpdate -> Rendering! -> 종료? -> OnDisable -> OnDestroy

    // bool     System.Boolean      True or False
    // byte     System.Byte         8비트 unsigned integer
    // sbyte    System.SByte        8비트 signed integer
    // short    System.Int16        16비트 signed integer
    // int      System.Int32        32비트 signed integer
    // long     System.Int64        64비트 signed integer
    // ushort   System.UInt16       16비트 unsigned integer
    // uint     System.UInt32       32비트 unsigned integer
    // ulong    System.UInt64       64비트 unsigned integer
    // float    System.Single       32비트 single precision 부동소수점 숫자
    // double   System.Double       64비트 double precision 부동소수점 숫자
    // decimal  System.Decimal      128비트 Decimal
    // char     System.Char         16비트 유니코드 문자
    // string   System.String       유니코드 문자열
    // System.DateTime              날짜와 시간, 별도의 C# 키워드가 없음
    // object   System.Object       모든 타입의 기본 클래스로 모든 유형을 포함할 수 있음

    // 변수 => Variables
    // 변수를 선언한다.
    // int (라는 부분은 변수의 타입:Type)
    // HP (라는 부분은 변수의 이름:Name)
    // 100 (라는 부분은 변수의 값:Value)
    int HP = 100;

    // 값을 넣는다? "=" 1개로 표기
    // 같다 라는 말은 equal 이라고 읽음 => "==" 2개로 표기

    bool        isFlag;      // false == 0, true == 1 (:0 이 아니면 전부다 true)
    int         intValue;    // 정수(음수, 0, 양수) 표현의 범위가 ~43억개(-21억 ~ 21억)
    long        longValue;   // 정수(음수, 0, 양수) 표현의 범위가...넘사벽
    float       floatValue;  // 소숫점 포함 0.001f, -10.998f, (소수점 5자리까지만 됨)
    double      doubleValue; // 소숫점 포함 3242342340.00000000000000000001 되게 큰 소수점을 사용할 경우
    string      stringValue; // 문자를 표현할 때 많이 씀
    uint        uintValue;   // (0, 양수만 => 자연수만 가능 => 부호가 없다 => 음수가 없다) (0 ~ 42억?)
    
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

    // 메서드의 구조 : 리턴 하는 타입, 함수 이름, 파라미터
    void MyMethod(int a, int b, int c, string name)
    {

    }

    int SumResult(int a, int b)
    {
        int result = a + b;
        return result;
    }
}
