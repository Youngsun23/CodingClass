using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test12_1 : MonoBehaviour
{
    public void Abc(int value) // �Ķ���� �����ϰ�
    {
        print(value + "���� �����߽��ϴ�.");
    }

    // Start is called before the first frame update
    void Start()
    {
        Test12.OnStart += Abc; // Test12 ��ũ��Ʈ�� event OnStart�� �Լ� �߰�
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
