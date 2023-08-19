using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YS; // ���ӽ����̽� ������ ���
// ���� ���ӽ����̽��� ���� class �� ���̱� ������ �ʿ� X
// YS�� class�� YS.Studio�� clss�� �̸��� ������ ���, ��ȣ�� ���� ���� �߻�
using YS.Studio; // ���� ���ӽ����̽�

// 10. ���ӽ����̽�

namespace YS // Ŭ����, ����, �Լ� ���� ��� ��Ʈ�޴�
// ����, ���� ������Ʈ, �ܺ� ���̺귯�� Ȱ�� ���� namespace ��� - ������ ��Ʈ�޴�, �ߺ� �� ��Ȯ�� �Ҽ� ���� �ذ�, ���浵 �� ��
{
    public class Youtube
    {
        public int subscribe;
    }

    namespace Studio
    {
        public class Youtube
        {
            int like;

            public void SetLike(int value)
            {
                like = value;
            }

            public bool IsLike()
            {
                return like != 0;
            }
        }
    }
}


public class Test10 : MonoBehaviour
{
    YS.Studio.Youtube youtube=new YS.Studio.Youtube(); // �Ҽ� ��Ȯ�� �����ָ� �ذ�

    void Start()
    {
        youtube.SetLike(5);

        print(youtube.IsLike());
    }


}
