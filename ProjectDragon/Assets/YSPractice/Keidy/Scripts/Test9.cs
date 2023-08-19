using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 9. 컬렉션

public class Test9 : MonoBehaviour
{
    int[] exp = new int[5] { 1, 2, 3, 4, 5 }; // 배열 크기 정하면서 인덱스도 할당

    // 컬렉션: 리스트, 큐, 스택, 해시테이블, 딕셔너리, 어레이리스트 ..
    
    // ArrayList
    ArrayList arrayList=new ArrayList(); // 하늘색: 클래스 (using), 변수타입x = 뭐든지 가능 (연산량 크다)
    // 추가, 제거, 변경 가능

    // List
    List<int> list= new List<int>(); // <자료형> 명시

    // Hashtable
    Hashtable hashtable = new Hashtable();

    // Dictionary
    Dictionary<string,int> dictionary = new Dictionary<string,int>(); // <자료형, 자료형> 명시

    // Queue - 선입선출, FIFO
    Queue<int> queue = new Queue<int>(); // 자료형 있어도 없어도
    // 박스에 순서대로 넣고 빼는데, 빼면 그게 사라지고 하나씩 앞당겨짐 (참조 불가, 빼기만 가능)

    // Stack - 후입선출, LIFO
    Stack<int> stack = new Stack<int>(); // 자료형 있어도 없어도
    // 컵에 넣고 push 꺼내는데 pop, 빼면 그게 사라짐 (참조 불가, 빼기만 가능)


    void Start()
    {
        arrayList.Add(1); // 클래스의 Add라는 함수 이용 - 원소 추가
        arrayList.Add("가나다라");
        arrayList.Add(1.5);
        arrayList.Add("먀");

        print(arrayList.Count); // .Length와 동일

        arrayList[3] = "마"; // 값 변경

        arrayList.RemoveRange(2, 1); // 인덱스2부터 1개 제거
        // 29가 아닌 32에 위치할 경우, 위에서 이미 제거했기 때문에 인덱스가 0(1.5),1("마")만 남게 되어, 오류 출력
        arrayList.Remove("가나다라"); // 직접 지정해 제거
        arrayList.RemoveAt(0); // 인덱스 이용해 제거

        arrayList.Insert(0, 2); // 인덱스0에 1.5를 끼워넣기 - 기존 0이었던 "마"가 1로 밀려나고, 0에 2가 들어감

        for (int i = 0; i < arrayList.Count; i++)
        {
            print(arrayList[i]);
        }

        arrayList.Clear(); // 초기화

        print(arrayList.Contains("가나다라")); // 있는지 없는지 확인해 T/F로 반환

        //   //   //   //   //   //   //   

        // list.Add("가"); // 자료형과 맞지 않으면 불가 - ArrayList와의 유일한 차이점
        list.Add(3);

        hashtable.Add("백", 100); // 키, 값
        hashtable.Add("천", 1000);
        hashtable.Add(50, "1억");

        print(hashtable[0]); // NULL - 인덱스 통한 접근 불가
        print(hashtable["만"]); // 키를 통해 값을 찾음
        print(hashtable[50]);
        // Add처럼 그 외 함수 사용도 동일

        // ArrayList와 List의 관계 == Hashtable과 Dictionary의 관계

        //   //   //   //   //   //   //  

        queue.Enqueue(100); // 집어넣고
        queue.Enqueue(200);

        if(queue.Count !=0)
        {
            print(queue.Dequeue()); // 빼고
        }

        // 순서대로 빼고
        print(queue.Dequeue()); // 100 나오고
        print(queue.Dequeue()); // 200 나오고
        print(queue.Dequeue()); // 더이상 뺄 게 없으니 오류

        stack.Push(1); // 집어넣고
        stack.Push(2);

        if (stack.Count != 0)
        {
            print(stack.Pop()); // 빼고
        }

        print(stack.Pop()); // 2부터 나오고
        print(stack.Pop()); // 1 나오고
        print(stack.Pop()); // 더이상 뺄 게 없으니 오류

    }




}
