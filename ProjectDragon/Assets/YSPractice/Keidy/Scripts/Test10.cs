using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YS; // 네임스페이스 가져다 사용
// 하위 네임스페이스에 속한 class 쓸 것이기 때문에 필요 X
// YS의 class와 YS.Studio의 clss의 이름이 동일한 경우, 모호한 참조 오류 발생
using YS.Studio; // 하위 네임스페이스

// 10. 네임스페이스

namespace YS // 클래스, 변수, 함수 등이 담긴 세트메뉴
// 협업, 대형 프로젝트, 외부 라이브러리 활용 위한 namespace 사용 - 각자의 세트메뉴, 중복 시 명확한 소속 밝혀 해결, 변경도 편리 등
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
    YS.Studio.Youtube youtube=new YS.Studio.Youtube(); // 소속 명확히 밝혀주면 해결

    void Start()
    {
        youtube.SetLike(5);

        print(youtube.IsLike());
    }


}
