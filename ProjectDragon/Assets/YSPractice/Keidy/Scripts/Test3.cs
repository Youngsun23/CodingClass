using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 3. 함수

public class Test3 : MonoBehaviour
{
    int intValue;

    float floatValue = 10.5f;
    float floatValue2 = 20.5f;

    int result;

    void FloatToInt(float parameter, float parameter2, string parameter3="배고파") // 인수만 바꿔서 동일함수 여러번 쓰고 싶을 때
        // 뒤쪽에는 디폴트값 미리 넣어놓을 수 있음
    {
        intValue= (int) (parameter + parameter2);
        print(intValue);
        print(parameter3);
    }

    int Four()
    {
        return 4; // return값은 함수의 결과값 (이후의 코드는 무시됨)
    }


    void Start()
    {
        FloatToInt(floatValue, floatValue2); // 세 번째 인수 입력 없이도 "배고파"로 처리
        FloatToInt(floatValue, floatValue2, "졸려");

        result = Four(); // 함수 return값을 변수에 넣기
        print(result);

        print(FloatToInt2(floatValue, floatValue2));

    }

    // 함수 안에 함수

    int FloatToInt2(float par, float par2)
    {
        return Multiply((int)(par + par2));
    }    

    int Multiply(int par)
    {
        return (int)(par * par);
    }

}
