//    //?// s="C#", s="F#" -> �� ���� < ���ο� ��ü ����, ������ �ʱ�ȭ, ������ s�� �Ҵ� (s�� �� �� ����� ��?)
//    //?// ���������� ���� �ٸ� �޸𸮸� ���� ��ü -> ���� �ǹ�?

//using System;
//using System.Text;

//namespace MySystem
//{
//    class Program
//    {
//        static void Main(string[] args)
//       {
//            // ���ڿ�(string) ����
//            string s1 = "C#";
//            string s2 = "Programming";

//            // ����(char) ����
//            char c1 = 'A';
//            char c2 = 'B';

//            // ���ڿ� ����
//            string s3 = s1 + " " + s2;
//            Console.WriteLine("String: {0}",s3);

//            // �κй��ڿ� ����
//            string s3substring = s3.Substring(1, 5);
//            Console.WriteLine("Substring: {0}",s3substring);
//            //?// Substring(1,5)�� 1~5���� �κ� �����ΰ�?
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

//            // ���ڿ��� �迭�ε����� �ѹ��� �׼���
//            for(int i=0; i<s.Length; i++)
//            {
//                Console.WriteLine("{0}:{1}", i, s[i]);
//            }

//            // ���ڿ��� ���ڹ迭�� ��ȯ
//            string str = "Hello";
//            char[] charArray=str.ToCharArray();
//                //?// ToCharArray �Լ� ����?

//            for(int i=0;i<charArray.Length;i++)
//            {
//                Console.WriteLine(charArray[i]);
//            }

//            // ���ڹ迭�� ���ڿ��� ��ȯ
//            char[] charArray2 = { 'A', 'B', 'C', 'D' };
//            s=new string(charArray2);

//            Console.WriteLine(s);

//            // ���� ����
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
//            //?// �� �κ� ��ü �𸣰���
//    }
//}