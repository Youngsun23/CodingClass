//    //?// s="C#", s="F#" -> 값 변경 < 새로운 객체 생성, 데이터 초기화, 변수명 s에 할당 (s가 두 개 생기는 거?)
//    //?// 내부적으로 전혀 다른 메모리를 갖는 객체 -> 무슨 의미?

//using System;
//using System.Text;

//namespace MySystem
//{
//    class Program
//    {
//        static void Main(string[] args)
//       {
//            // 문자열(string) 변수
//            string s1 = "C#";
//            string s2 = "Programming";

//            // 문자(char) 변수
//            char c1 = 'A';
//            char c2 = 'B';

//            // 문자열 결합
//            string s3 = s1 + " " + s2;
//            Console.WriteLine("String: {0}",s3);

//            // 부분문자열 발췌
//            string s3substring = s3.Substring(1, 5);
//            Console.WriteLine("Substring: {0}",s3substring);
//            //?// Substring(1,5)는 1~5까지 부분 발췌인가?
//        }

//    }
//}

//using System;

//namespace MySystem
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string s = "C# studies";

//            // 문자열을 배열인덱스로 한문자 액세스
//            for(int i=0; i<s.Length; i++)
//            {
//                Console.WriteLine("{0}:{1}", i, s[i]);
//            }

//            // 문자열을 문자배열로 변환
//            string str = "Hello";
//            char[] charArray=str.ToCharArray();
//                //?// ToCharArray 함수 무엇?

//            for(int i=0;i<charArray.Length;i++)
//            {
//                Console.WriteLine(charArray[i]);
//            }

//            // 문자배열을 문자열로 변환
//            char[] charArray2 = { 'A', 'B', 'C', 'D' };
//            s=new string(charArray2);

//            Console.WriteLine(s);

//            // 문자 연산
//            char c1 = 'A';
//            char c2 = (char)(c1 + 3);
//            Console.WriteLine(c2);

//        }
//    }
//}

//using System;
//using System.Text;

//namespace MySystem
//{
//    class Program
//        static void Main(string[] args)
//    {
//        StringBuilder sb = new StringBuilder();
//        for(int i=1;i<=26;i++)
//        {
//            sb.Append(i.ToString());
//            sb.Append(System.Environment.NewLine);
//        }
//        string s = sb.ToString();

//        Console.WriteLine(s);
//            //?// 이 부분 전체 모르겠음
//    }
//}