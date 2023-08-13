using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 9. �÷���

public class Test9 : MonoBehaviour
{
    int[] exp = new int[5] { 1, 2, 3, 4, 5 }; // �迭 ũ�� ���ϸ鼭 �ε����� �Ҵ�

    // �÷���: ����Ʈ, ť, ����, �ؽ����̺�, ��ųʸ�, ��̸���Ʈ ..
    
    // ArrayList
    ArrayList arrayList=new ArrayList(); // �ϴû�: Ŭ���� (using), ����Ÿ��x = ������ ���� (���귮 ũ��)
    // �߰�, ����, ���� ����

    // List
    List<int> list= new List<int>(); // <�ڷ���> ���

    // Hashtable
    Hashtable hashtable = new Hashtable();

    // Dictionary
    Dictionary<string,int> dictionary = new Dictionary<string,int>(); // <�ڷ���, �ڷ���> ���

    // Queue - ���Լ���, FIFO
    Queue<int> queue = new Queue<int>(); // �ڷ��� �־ ���
    // �ڽ��� ������� �ְ� ���µ�, ���� �װ� ������� �ϳ��� �մ���� (���� �Ұ�, ���⸸ ����)

    // Stack - ���Լ���, LIFO
    Stack<int> stack = new Stack<int>(); // �ڷ��� �־ ���
    // �ſ� �ְ� push �����µ� pop, ���� �װ� ����� (���� �Ұ�, ���⸸ ����)


    void Start()
    {
        arrayList.Add(1); // Ŭ������ Add��� �Լ� �̿� - ���� �߰�
        arrayList.Add("�����ٶ�");
        arrayList.Add(1.5);
        arrayList.Add("��");

        print(arrayList.Count); // .Length�� ����

        arrayList[3] = "��"; // �� ����

        arrayList.RemoveRange(2, 1); // �ε���2���� 1�� ����
        // 29�� �ƴ� 32�� ��ġ�� ���, ������ �̹� �����߱� ������ �ε����� 0(1.5),1("��")�� ���� �Ǿ�, ���� ���
        arrayList.Remove("�����ٶ�"); // ���� ������ ����
        arrayList.RemoveAt(0); // �ε��� �̿��� ����

        arrayList.Insert(0, 2); // �ε���0�� 1.5�� �����ֱ� - ���� 0�̾��� "��"�� 1�� �з�����, 0�� 2�� ��

        for (int i = 0; i < arrayList.Count; i++)
        {
            print(arrayList[i]);
        }

        arrayList.Clear(); // �ʱ�ȭ

        print(arrayList.Contains("�����ٶ�")); // �ִ��� ������ Ȯ���� T/F�� ��ȯ

        //   //   //   //   //   //   //   

        // list.Add("��"); // �ڷ����� ���� ������ �Ұ� - ArrayList���� ������ ������
        list.Add(3);

        hashtable.Add("��", 100); // Ű, ��
        hashtable.Add("õ", 1000);
        hashtable.Add(50, "1��");

        print(hashtable[0]); // NULL - �ε��� ���� ���� �Ұ�
        print(hashtable["��"]); // Ű�� ���� ���� ã��
        print(hashtable[50]);
        // Addó�� �� �� �Լ� ��뵵 ����

        // ArrayList�� List�� ���� == Hashtable�� Dictionary�� ����

        //   //   //   //   //   //   //  

        queue.Enqueue(100); // ����ְ�
        queue.Enqueue(200);

        if(queue.Count !=0)
        {
            print(queue.Dequeue()); // ����
        }

        // ������� ����
        print(queue.Dequeue()); // 100 ������
        print(queue.Dequeue()); // 200 ������
        print(queue.Dequeue()); // ���̻� �� �� ������ ����

        stack.Push(1); // ����ְ�
        stack.Push(2);

        if (stack.Count != 0)
        {
            print(stack.Pop()); // ����
        }

        print(stack.Pop()); // 2���� ������
        print(stack.Pop()); // 1 ������
        print(stack.Pop()); // ���̻� �� �� ������ ����

    }




}
