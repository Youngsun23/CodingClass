//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//// 4. 지정자

//public class Test4_1
//{ 
//    // 접근 지정자 (변수, 함수 등 모두 적용)
//    private int b; // 클래스 내에서만 사용
//    public int c; // 모든 클래스에서 사용 - 보안 떨어짐
//    public static int d; // 공유 자원
//}


//public class Test4 : MonoBehaviour
//{
//    int a = 5; // 전역 변수

//    Test4_1 a1 = new Test4_1(); // 다른 클래스 사용할 준비 (그릇)
//    Test4_1 a2 = new Test4_1(); // 선언만 하고 =new 없으면 Null 오류
//    Test4_1 a3 = new Test4_1();


//    void Aaa()
//    {
//        // aaa.b = 5; // 오류: 보호 수준 때문에 액세스할 수 없습니다.
//        a1.c = 5;
//        a2.c = 10;
//        a3.c = 15;

//        print(a1.c);
//        print(a2.c);
//        print(a3.c);

//        Test4_1.d = 100; // 공유 자원: 클래스 자체로 접근 -> a1, a2, a3 전체 동일하게 적용 (정적 변수)
//        print(Test4_1.d); 
//    }

//    void Abc() // 지역 변수: 실행 시에만 존재 (생성-파괴)
//    {
//        int a = 5; // 이름 같을 경우, 전역 변수 무시되고 지역 변수 a에 이후 코드 실행 (6 할당)
//        a = 6; // 전역에서 사용 가능

//        int b = 5; // 서로 다른 지역 변수
//        print(b);
//    }

//    void Abc2()
//    {
//        int b = 6; // 서로 다른 지역 변수
//        print(b);
//    }

//    void Abc3(int parameter) // 매개 변수
//    {

//    }

//    void Start()
//    {
//        // print(b); // 오류: 현재 컨텍스트에 없습니다.
//        Aaa();
//    }


//}
