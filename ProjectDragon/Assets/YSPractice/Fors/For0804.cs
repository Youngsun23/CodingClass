using UnityEngine;

public class For0804 : MonoBehaviour
{
    void Start()
    {
        // if�� �̿��� �б� �¿��� Log�� ���
        int a = 4902;
        int b = 12;
        Debug.Log($"���� a: {a} ���� b: {b}");
        if (a % b == 0)
        {
            Debug.Log("a�� b�� ����� �½��ϴ�.");
        }
        else
        {
            Debug.Log("a�� b�� ����� �ƴմϴ�.");
        }

        // for�� �̿��� ������ ���
        for (int i=2; i<10; i++)
        {
            Debug.Log("������ " + i + "��");
            for (int j=1; j<10; j++)
            {
                Debug.Log(i+" x "+j+" = "+ (i*j));
            }
        }

    }

}
