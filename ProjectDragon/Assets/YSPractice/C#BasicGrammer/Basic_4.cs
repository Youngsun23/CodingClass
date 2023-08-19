////?// '로컬변수가 함수 호출 끝난 후 소멸된다'의 의미?
//// static은 또 뭔데. 왜 따로 존재하는 건데. 클래스의 객체가 살아있는 한 존속vs프로그램 종료까지 존속

//using System;

//namespace ConsoleApplication1
//    //?// namespace 키워드의 의미?
//{
//    class CSVar
//    {
//        // 필드 (클래스 내에서 공통적으로 사용되는 전역 변수)
//        int globalVar;
//        const int MAX = 1024;
//        // const int MAX는 왜 쓴 거지?


//        public void Method1()
//        {
//            // 로컬변수
//            int localVar;

//            // 아래 할당이 없으면 에러 발생
//            localVar = 100;

//            Console.WriteLine(globalVar);
//            Console.WriteLine(localVar);
//        }
//    }

//    class Program 
//        //?// 한 스크립트 안에 Class 여럿 있기도 하나? Class의 개념이 확실치 않음
//    {
//        // 모든 프로그램에는 Main()이 있어야 함
//        static void Main(string[] args) 
//        {
//            // 테스트
//            CSVar obj=new CSVar();
//            obj.Method1();
//        }
//    }
//}

//using System;

//namespace ConsoleApplication1
//{
//    class CSVar
//    {
//        // 상수
//        const int MAX = 1024;

//        // readonly 필드
//        readonly int Max;
//            //?// 용도가 무엇?
//        public CSVar() 
//        {
//            Max = 1;
//        }
//            //?// 얘는 왜 쓰는 거?

//        //?// 결국 이거 출력시키면 어떻게 되는 거지?
//    }
//}

