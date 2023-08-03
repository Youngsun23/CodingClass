//public enum Category
//{
//    Cake,
//    IceCream,
//    Bread
//}

//class Program
//{
//    enum City
//    {
//        Seoul,
//        Daejun,
//        Busan=5,
//        Jeju=10
//    }
    
//    static void Main(string[] args)
//    {
//        City myCity;
//            //?// enum 타입 City를 myCity라는 이름으로 부르겠다는 뜻? 무슨 뜻?
        
//        // enum 타입에 값을 대입하는 방법
//        myCity=City.Seoul;
//            // '='은 값 대입이니, myCity=0이 되는 건가?

//        // enum을 int로 변환(Casting)하는 방법.
//        // (int)를 앞에 지정.
//        int cityValue=(int)myCity;
//            //?// (int) City.Seoul 이라고 쓰면 안 되는 건가?
//            //?// 값 대입은 대입이고, int로 변환은 따로 해줘야 하는 것?

//        if (myCity == City.Seoul) // enum 값을 비교하는 방법
//        {
//            Console.WriteLine("Welcome to Seoul");
//        }
//            // if(int 0 == enum City.Seoul(=0)) -> True가 되는 것 맞나?
//    }

//}

//[Flags]
//enum Border
//{
//    None=0,
//    Top=1,
//    Right=2,
//    Bottom=4,
//    Left=8
//}
//    // 왜 굳이 1,2,4,8... 비트 값이지?

//static void Main(string[] args)
//{
//    // OR 연산자로 다중 플래그 할당
//    Border b = Border.Top | Border.Bottom;
//        //?// 보더 타입 변수 b의 값은 1 혹은 4?

//    // & 연산자로 플래그 체크
//    if ((b & Border.Top) != 0)
//        //?// b는 1 혹은 4이고 Top은 1이니 둘 다 1 만족, 결과값 1, !=0이라 True 맞나?
//    {
//        //HasFlag() 이용 플래그 체크
//        if (b.HasFlag(Border.Bottom))
//        {
//            // "Top, Borrom" 출력
//            Console.WriteLine(b.ToString());
//        }
//    }
//}