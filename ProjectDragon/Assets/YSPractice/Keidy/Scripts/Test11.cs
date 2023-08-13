using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 11. 구조체

// struct가 예전 기능, namespace가 새 기능
public struct Youtube // 클래스와의 차이: 값 할당 불가, 상속(: MonoBehavior) 불가, 그릇 역할만 가능
{
    // int a = 5; // 오류

    public int a;
    public int b;
    public int c;
    public int d;
    public void GetA(int value)
    {
        a=value;
    }

    public Youtube(int _a, int _b, int _c, int _d) // 생성자 - 변수와 파라미터 매칭
    {
        a=_a;
        b=_b;
        c=_c;
        d=_d;
    }
}

// enum - 목록 생성

public enum Item 
{
    Weapon,
    Shield,
    Potion,
}

public class Test11 : MonoBehaviour
{
    Youtube youtube;
    // 차이점 1
    // Struct가 아니라 Class였다면 Null 오류 발생 - =new ~ 로 실제 공간 만들어주지 않고 만들거다 말만 해도 Construct는 오류 X
    // (내부적으로 알아서 처리, int a; 하면 int a=new int(); 알아서 내부 처리되듯이)

    // 차이점 2
    // Struct - 값 타입 (a의 값 접근), Class - 주소 타입 (a의 주소 속 값 접근)

    Youtube youtube2=new Youtube(1,2,3,4); // 생성자 이용해 선언, 생성, 값 할당

    //   //   //   //   //   //   

    Item item;

    void Start()
    {
        youtube.GetA(1);
        print(youtube.a);

        //   //   //   //   //   //  

        item = Item.Weapon; // 목록 중 선택
        item= Item.Shield;
        print(item);

    }

}
