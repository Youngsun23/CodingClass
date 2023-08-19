using UnityEngine;

public class For0804 : MonoBehaviour
{
    void Start()
    {
        // if문 이용해 분기 태워서 Log문 출력
        int a = 4902;
        int b = 12;
        Debug.Log($"정수 a: {a} 정수 b: {b}");
        if (a % b == 0)
        {
            Debug.Log("a는 b의 배수가 맞습니다.");
        }
        else
        {
            Debug.Log("a는 b의 배수가 아닙니다.");
        }

        // for문 이용해 구구단 출력
        for (int i=2; i<10; i++)
        {
            Debug.Log("구구단 " + i + "단");
            for (int j=1; j<10; j++)
            {
                Debug.Log(i+" x "+j+" = "+ (i*j));
            }
        }

    }

}
