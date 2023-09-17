//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//// 2. 자료형

//public class Test2 : MonoBehaviour
//{
//    // byte(255) sbyte(+-128) ushort(+6만) short(+-3만) integer(+-20억) long 정수 자료형
//    // short a = 40000; 오류

//    float f = 4.001f;
//    double d = 4.001;
//    decimal m = 4.001m;
//    // float double decimal (오차 범위 점점 작아짐) 실수 자료형 

//    string s = "배고파u";
//    char c = 'a';
//    // char(유니코드 기억) string(열) 문자 자료형

//    bool aa = true;
//    bool bb = false;

//    void Start()
//    {
//        // 캐스트가 있는지 확인하세요 (int<float) (int<long)
//        int q = 100;
//        float w = 100.15f;
//        int sum1;
//        // sum1 = q + w; 
//        sum1 = (int)(q + w); // Console: 200
//        // 캐스트: 숫자 자료형 변환 (바뀐 자료형에 맞지 않는 부분은 잘려나감)

//        int e = 100;
//        long r = 100;
//        int sum2;
//        // sum2 = e + r;
//        sum2 = (int)(e + r);

//        print(sum1);

//        int z = 100;
//        string v;
//        v = z.ToString(); // 숫자를 문자열로 변환
//        print(v);

//        int za;
//        string va = "100";
//        za = int.Parse(va); // (숫자) 문자열을 숫자로 변환
//        print(za);
//    }

//}
