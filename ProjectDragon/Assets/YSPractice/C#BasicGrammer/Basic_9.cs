//// IF ���ǹ�
//if (����)
//{ ����; }
//else
//{ ����; }

//// ����
//int a = -11;

//if(a>=0)
//{
//    val = a;
//}
//else
//{
//    val = -a;
//}

//Console.Write(val); // ����ȭ

//// Switch ���ǹ�
//switch (x��)
//{
//    case a (x Ư����):
//        ���๮
//        break; �ش�Ǵ� case ���๮ ���� �� switch�� Ż��
//    default: � case���� �ش����� �ʴ� ���
//        ���๮
//        break;
//}

//// ����
//switch (category)
//{
//    case "���":
//        price = 1000;
//        break;
//    case "����":
//        price = 1100; 
//        break;
//    case "����":
//        price = 900;
//        break;
//    default:
//        price = 0;
//        break;
//}

//// ����
//using System;

//namespace MySystem
//{
//    class Program
//    {
//        static bool verbose = false;
//        static bool continueOnError = false;
//        static bool logging=false;

//        static void Main(string[] args)
//        {
//            if(args.Length !=1)
//            {
//                Console.WriteLine("Usage: MyApp.exe option");
//                return;
//            }

//            string option = args[0];
//            switch (option.ToLower())
//            {
//                case "/v":
//                case "/verbose":
//                    verbose=true;
//                    break;
//                case "/c"
//                    continueOnError = true;
//                    break;
//                case "/l":
//                    logging = true;
//                    break;
//                default:
//                    Console.WriteLine("Unknown argument: {0}", option);
//                    break;
//            }
//        }
//    }
//}
