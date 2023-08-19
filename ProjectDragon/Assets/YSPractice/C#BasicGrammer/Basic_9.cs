//// IF 조건문
//if (조건)
//{ 실행; }
//else
//{ 실행; }

//// 예제
//int a = -11;

//if(a>=0)
//{
//    val = a;
//}
//else
//{
//    val = -a;
//}

//Console.Write(val); // 절댓값화

//// Switch 조건문
//switch (x값)
//{
//    case a (x 특정값):
//        실행문
//        break; 해당되는 case 실행문 실행 후 switch문 탈출
//    default: 어떤 case에도 해당하지 않는 경우
//        실행문
//        break;
//}

//// 예제
//switch (category)
//{
//    case "사과":
//        price = 1000;
//        break;
//    case "딸기":
//        price = 1100; 
//        break;
//    case "포도":
//        price = 900;
//        break;
//    default:
//        price = 0;
//        break;
//}

//// 예제
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
