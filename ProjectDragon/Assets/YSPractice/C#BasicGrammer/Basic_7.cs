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
//            //?// enum Ÿ�� City�� myCity��� �̸����� �θ��ڴٴ� ��? ���� ��?
        
//        // enum Ÿ�Կ� ���� �����ϴ� ���
//        myCity=City.Seoul;
//            // '='�� �� �����̴�, myCity=0�� �Ǵ� �ǰ�?

//        // enum�� int�� ��ȯ(Casting)�ϴ� ���.
//        // (int)�� �տ� ����.
//        int cityValue=(int)myCity;
//            //?// (int) City.Seoul �̶�� ���� �� �Ǵ� �ǰ�?
//            //?// �� ������ �����̰�, int�� ��ȯ�� ���� ����� �ϴ� ��?

//        if (myCity == City.Seoul) // enum ���� ���ϴ� ���
//        {
//            Console.WriteLine("Welcome to Seoul");
//        }
//            // if(int 0 == enum City.Seoul(=0)) -> True�� �Ǵ� �� �³�?
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
//    // �� ���� 1,2,4,8... ��Ʈ ������?

//static void Main(string[] args)
//{
//    // OR �����ڷ� ���� �÷��� �Ҵ�
//    Border b = Border.Top | Border.Bottom;
//        //?// ���� Ÿ�� ���� b�� ���� 1 Ȥ�� 4?

//    // & �����ڷ� �÷��� üũ
//    if ((b & Border.Top) != 0)
//        //?// b�� 1 Ȥ�� 4�̰� Top�� 1�̴� �� �� 1 ����, ����� 1, !=0�̶� True �³�?
//    {
//        //HasFlag() �̿� �÷��� üũ
//        if (b.HasFlag(Border.Bottom))
//        {
//            // "Top, Borrom" ���
//            Console.WriteLine(b.ToString());
//        }
//    }
//}