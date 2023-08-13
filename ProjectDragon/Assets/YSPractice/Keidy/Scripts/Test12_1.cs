using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test12_1 : MonoBehaviour
{
    public void Abc(int value) // 파라미터 동일하게
    {
        print(value + "값이 증가했습니다.");
    }

    // Start is called before the first frame update
    void Start()
    {
        Test12.OnStart += Abc; // Test12 스크립트의 event OnStart에 함수 추가
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
