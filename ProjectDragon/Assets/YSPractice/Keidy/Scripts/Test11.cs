using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 11. ����ü

// struct�� ���� ���, namespace�� �� ���
public struct Youtube // Ŭ�������� ����: �� �Ҵ� �Ұ�, ���(: MonoBehavior) �Ұ�, �׸� ���Ҹ� ����
{
    // int a = 5; // ����

    public int a;
    public int b;
    public int c;
    public int d;
    public void GetA(int value)
    {
        a=value;
    }

    public Youtube(int _a, int _b, int _c, int _d) // ������ - ������ �Ķ���� ��Ī
    {
        a=_a;
        b=_b;
        c=_c;
        d=_d;
    }
}

// enum - ��� ����

public enum Item 
{
    Weapon,
    Shield,
    Potion,
}

public class Test11 : MonoBehaviour
{
    Youtube youtube;
    // ������ 1
    // Struct�� �ƴ϶� Class���ٸ� Null ���� �߻� - =new ~ �� ���� ���� ��������� �ʰ� ����Ŵ� ���� �ص� Construct�� ���� X
    // (���������� �˾Ƽ� ó��, int a; �ϸ� int a=new int(); �˾Ƽ� ���� ó���ǵ���)

    // ������ 2
    // Struct - �� Ÿ�� (a�� �� ����), Class - �ּ� Ÿ�� (a�� �ּ� �� �� ����)

    Youtube youtube2=new Youtube(1,2,3,4); // ������ �̿��� ����, ����, �� �Ҵ�

    //   //   //   //   //   //   

    Item item;

    void Start()
    {
        youtube.GetA(1);
        print(youtube.a);

        //   //   //   //   //   //  

        item = Item.Weapon; // ��� �� ����
        item= Item.Shield;
        print(item);

    }

}
