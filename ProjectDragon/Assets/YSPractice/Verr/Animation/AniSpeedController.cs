using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniSpeedController : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator=GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // 애니메이터의 모든 애니메이션 속도 조절
            animator.speed += 0.2f; 
            // 특정 애니메이션의 속도만 독립적으로 조절
            // 애니메이터에서 int/float parameter 만들고
            // 원하는 애니메이션의 speed의 multiplier로 설정 (곱연산)
            animator.SetFloat("speed",animator.GetFloat("speed")+0.2f);
        }
    }


}
