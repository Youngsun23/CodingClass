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
            // �ִϸ������� ��� �ִϸ��̼� �ӵ� ����
            animator.speed += 0.2f; 
            // Ư�� �ִϸ��̼��� �ӵ��� ���������� ����
            // �ִϸ����Ϳ��� int/float parameter �����
            // ���ϴ� �ִϸ��̼��� speed�� multiplier�� ���� (������)
            animator.SetFloat("speed",animator.GetFloat("speed")+0.2f);
        }
    }


}
