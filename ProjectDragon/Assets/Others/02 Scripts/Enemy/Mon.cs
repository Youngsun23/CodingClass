using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.UI.Image;




/////////////////////////////////////////////////////////////////////////////////////////////////////////


public class Mon : MonoBehaviour
{
    private Ray2D ray;            // ���� ���ð�(���� ��) �� �����ϱ� ���� ���� 
    private RaycastHit2D rayHitP; // ���̿� �÷����� �浹 ������ �����ϱ� ���� ����
    private RaycastHit2D rayHitT; // ���̿� Ÿ���� �浹 ������ �����ϱ� ���� ����
    private float rayRange = 2f;  // ���� �߻�Ÿ� (�÷��� üũ��)

    private string colTag;        // �ݶ��̴��� �浹�� ����� �±װ��� �����ϱ� ���� ����
    private Vector3 targetPos;    // PC�� ��ǥ���� �����ϱ� ���� ����
    private float targetDist;     // PC�� ������ �Ÿ��� �����ϱ� ���� ����
    private Vector3 targetDir;    // PC ������ �����ϱ� ���� ����
    private SpriteRenderer spriteRenderer;
    

    private float patrolRadius = 11.2f; // ���� ���� ������
    private float patrolAngle = 0f; // ���� ���� ���� ����

    // PC�� ������ ������ �ִ���, ������ �ִ����� ���ϴ� ���� ���� ������
    private Vector3 tt;
    private Vector3 cc;
    private float dot;

    public float size;
    public GoTB goTB;                 // ������ ���� ��/�� ������ �����ϱ� ���� ����
    public GoLR goLR;                 // ������ ���� ��/�� ������ �����ϱ� ���� ����

    public float patrolSpeed;         // ���� ���� �̵� �ӵ�
    public float chaseSpeed;          // ���� �߰� �̵� �ӵ�

    public float chaseRange;          // ���� �߰� �Ÿ� 
    public float attackRange;         // ���� ���� �Ÿ� (��Ÿ�)
    public float attackCoolTime;      // ���� ���� ��Ÿ��
    private float currentCoolTime;    // ������ ���� ���� ��Ÿ���� �����ϱ� ���� ����

    public GameObject skill;          // ������ ��ų �������� �����ϱ� ���� ����

    public State state = State.Idle;  // ������ ���¸� �Ǻ��ϱ� ���� ���� (�ʱⰪ = Idle)                       
    public MoveType moveType;         // ������ �̵� Ÿ���� �����ϱ� ���� ����      
    public PatrolType patrolType;     // ������ ���� ���� & Ÿ���� �����ϱ� ���� ����
    public ChaseType chaseType;       // ������ �߰� ���� & Ÿ���� �����ϱ� ���� ����


    public enum GoTB       // ������ ���� ��/�� ������ �����ϱ� ���� ������ ���
    {
        Top,       // ���
        Bottom     // �ϴ�
    }

    public enum GoLR       // ������ ���� ��/�� ������ �����ϱ� ���� ������ ���
    {
        Left,      // ����
        Right      // ����
    }

    public enum State      // ������ ���¸� �Ǻ��ϱ� ���� ������ ��� (������ ���� �ڵ� ��ȯ)
    {
        Idle,     // ����_���
        Chase,    // ����_�߰�
        Attack    // ����_����
    }

    public enum MoveType   // ������ �̵� Ÿ���� �����ϱ� ���� ������ ���
    {
        Ground,   // �̵�_���� [���� �̵��� ����]
        Air       // �̵�_���� [���� �̵��� ����]
    }

    public enum PatrolType // ������ ���� ���� & Ÿ���� �����ϱ� ���� ������ ���
    {
        None,      // ����_X
        Patrol_G1, // ����_����_1 [���� (�ݽð���� ȸ��)]
        Patrol_G2, // ����_����_2 [���� (�ð���� ȸ��)]
        Patrol_A1, // ����_����_1 [����_����_�¿� (ī�޶� ���� �ȿ����� ����)]
        Patrol_A2  // ����_����_2 [����_����_���� (ī�޶� ���� �ȿ����� ����)]
    }

    public enum ChaseType  // ������ �߰� ���� & Ÿ���� �����ϱ� ���� ������ ���
    {
        None,      // �߰�_X
        Chase_G1,  // �߰�_����_1 [���� (�÷��� ���� �ȿ����� �߰�)]
        Chase_A1,  // �߰�_����_1 [���� (ī�޶� ���� �ȿ����� �߰� / Ž������ ����� ���)]
        Chase_A2,  // �߰�_����_2 [���� (ī�޶� ���� �ȿ����� �߰� / Ž������ ����� �߰�)]
    }




    /////////////////////////////////////////////////////////////////////////////////////////////////////////




    private void Start()
    {
        transform.localScale *= size;
        LookLeft();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Vector3 lookDir = transform.position - transform.parent.position;
        patrolAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
    }


    private void Update()
    {
        // moveType�� Ground�� (������ ����)
        if (moveType == MoveType.Ground)
        {
            // ������ ���Ϳ��� �ʿ��� �Լ� ����
            Check_Flatform();
            Check_PC_Dist();
            Check_PC_Ray();
            CoolTimeManager();
            StateManager_G();
        }

        // moveType�� Air�� (������ ����)
        if (moveType == MoveType.Air)
        {
            // ������ ���Ϳ��� �ʿ��� �Լ� ����
            Check_PC_Dist();
            CoolTimeManager();
            StateManager_A();
        }
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////////////




    ///// �� �� �ΰ� �Լ� ///// -------------------------------------------------------------------------


    // �浹 üũ �Լ� (�ڵ� ȣ��)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹 ����� �±װ� Floor��
        if (collision.CompareTag("Floor"))
        {
            // �浹 ����� �±װ��� ������ ����
            colTag = collision.gameObject.tag;
        }
    }

    // �÷��� üũ �Լ� (���̿� �÷����� �浹���� �˻� / ������ ���Ͱ� �÷��� �������� �̵��ϵ��� �ϴ� �뵵)
    private void Check_Flatform()
    {
        // ���� �߻� ���� ����
        {
            // ������ ���� �ִ� ���
            if (goLR == GoLR.Left)
            {
                // ���� ��ü�� ������ ���ɴϴ�.
                Vector2 objectDirection = transform.right.normalized;

                // 45�� �Ʒ� ���� ���͸� ����մϴ�.
                float angleInDegrees = -135f;
                Vector2 rayDirection = Quaternion.Euler(0f, 0f, angleInDegrees) * objectDirection;

                // ������ �����մϴ�.
                ray.direction = rayDirection;
            }

            // ������ ���� �ִ� ���
            else
            {
                // ���� ��ü�� ������ ���ɴϴ�.
                Vector2 objectDirection = transform.right.normalized;

                // 135�� �Ʒ� ���� ���͸� ����մϴ�.
                float angleInDegrees = -45f;
                Vector2 rayDirection = Quaternion.Euler(0f, 0f, angleInDegrees) * objectDirection;

                // ������ �����մϴ�.
                ray.direction = rayDirection;
            }
        }

        // ���� �߻� (������ǥ����, ������ ��������, ������ �Ÿ���ŭ) / ���̾ "Platform"�� ��ü�� üũ
        rayHitP = Physics2D.Raycast(transform.position, ray.direction, rayRange, LayerMask.GetMask("Platform"));
        Debug.DrawRay(transform.position, ray.direction * rayRange, Color.red);
    }

    // PC üũ �Լ� (PC�� ��ǥ/�Ÿ�/���� ���� �˻�) (������&������ ���� ��ο��� �ʿ�)
    private void Check_PC_Dist()
    {
        // PC�� ��ǥ ����
        targetPos = GameObject.FindWithTag("Player").transform.position;
        // PC�� ������ �Ÿ� ����  
        targetDist = Vector3.Distance(targetPos, transform.position);
        // PC�� ��ǥ ���� ����
        targetDir = (targetPos - transform.position).normalized;

        // PC�� ������ ������ �ִ���, ������ �ִ����� ���ϴ� ���� (dot > 0 �� ����, dot < 0 �� ����) 
        tt = targetPos - transform.position;
        cc = Vector3.Cross(tt, transform.forward);
        dot = Vector3.Dot(cc, Vector3.up);
    }

    // PC üũ �Լ� (���̿� PC�� �浹���� �˻� / ������ ������ �߰ݰŸ� ���� PC�� �ִ��� �Ǻ��ϴ� �뵵) (������ ���͸� �ʿ�)
    private void Check_PC_Ray()
    {
        // ���� �߻� ���� ����
        {
            // ������ ���� �ִ� ���
            if (goLR == GoLR.Left)
            {
                // ���� �߻� ������ �������� ����
                ray.direction = new Vector2(-1f, 0f);
            }

            // ������ ���� �ִ� ���
            else
            {
                // ���� �߻� ������ �������� ����
                ray.direction = new Vector2(1f, 0f);
            }
        }

        // ���� �߻� (������ǥ����, ������ ��������, ������ �Ÿ���ŭ) / ���̾ "Player"�� ��ü�� üũ
        rayHitT = Physics2D.Raycast(transform.position, ray.direction, chaseRange, LayerMask.GetMask("Player"));
        //Debug.DrawRay(transform.position, ray.direction * chaseRange, Color.red);
    }

    // ���� ��/�� ������ �����̸� ������ ������ ���ִ� �Լ�
    private void LookLeft()
    {
        // ������ ���� ��/�� ������ �����̸�
        if (goLR == GoLR.Left)
        {

            
            // ������ ������ ���� (���������� ��������Ʈ�� ������ �ٶ󺸰� �����Ƿ�)
            transform.localScale = new Vector2(-size, size);
        }
    }

    // ���� ��Ÿ�� �Ŵ��� �Լ�
    private void CoolTimeManager()
    {
        if (currentCoolTime > 0f)
        {
            currentCoolTime -= Time.deltaTime;
           // Debug.Log(currentCoolTime);
        }
    }



    
    ///// ���� �Ŵ��� �Լ� ///// ------------------------------------------------------------------------
    

    // ���� �Ŵ���_G �Լ�
    private void StateManager_G()
    {
        // PC���� �Ÿ��� ���ݰŸ� ���� & ���� ���� ��Ÿ�Ӱ��� 0���ϸ�
        if (targetDist <= attackRange && currentCoolTime <= 0)
        {
            // skill�� null�� �ƴϸ�
            if (skill != null)
            {
                // ���� ���·� ��ȯ
                state = State.Attack;

                // Attack �Լ� ����
                Attack();
            }
        }

        // ���̿� PC�� �浹�ϸ�
        else if (rayHitT)
        {
            //chaseType�� None�� �ƴϸ�
            if (chaseType != ChaseType.None)
            {
                // �߰� ���·� ��ȯ
                state = State.Chase;

                // Chase_G �Լ� ����
                Chase_G();
            }
        }

        // PC���� �Ÿ��� ���ݰŸ� �ʰ� & ���̿� PC�� �浹���� ������
        else
        {
            // ��� ���·� ��ȯ
            state = State.Idle;

            // Idle_G �Լ� ����
            Idle_G();
        }
    }

    // ���� �Ŵ���_A �Լ� (������ ����)
    private void StateManager_A()
    {
        // PC���� �Ÿ��� ���ݰŸ� ���� & ���� ���� ��Ÿ�Ӱ��� 0���ϸ�
        if (targetDist <= attackRange && currentCoolTime <= 0)
        {
            // skill�� null�� �ƴϸ�
            if (skill != null)
            {
                // ���� ���·� ��ȯ
                state = State.Attack;

                // Attack �Լ� ����
                Attack();
            }
        }

        // PC���� �Ÿ��� �߰ݰŸ� ���ϸ�
        else if (targetDist <= chaseRange)
        {
            // chaseType�� None�� �ƴϸ�
            if (chaseType != ChaseType.None)
            {
                // �߰� ���·� ��ȯ
                state = State.Chase;
               
                // Chase_A �Լ� ����
                Chase_A();
            }
        }

        // PC���� �Ÿ��� ���ݰŸ� �ʰ� & �߰ݰŸ� �ʰ���
        else
        {
            // ��� ���·� ��ȯ
            state = State.Idle;

            // Idle_A �Լ� ����
            Idle_A();
        }
    }




    ///// ����_��� �Լ� ///// --------------------------------------------------------------------------


    // ����_���_G �Լ�
    private void Idle_G()
    {
        switch (patrolType)
        {
            // PatrolType�� None��
            case PatrolType.None:
                // �ƹ��͵� �������� ���� (����X)
                break;

            // PatrolType�� Patrol_G1��
            case PatrolType.Patrol_G1:               
                // Patrol_G1 �Լ� ����
                Patrol_G1();
                break;
            case PatrolType.Patrol_G2:
                // Patrol_G1 �Լ� ����
                Patrol_G2();
                break;
        }
    }

    // ����_���_A �Լ�
    private void Idle_A()
    {
        switch (patrolType)
        {
            // PatrolType�� None��
            case PatrolType.None:
                // �ƹ��͵� �������� ���� (����X)
                break;

            // PatrolType�� Patrol_A1��
            case PatrolType.Patrol_A1:
                // Patrol_A1 �Լ� ����
                Patrol_A1();
                break;

            // PatrolType�� Patrol_A2��
            case PatrolType.Patrol_A2:
                // Patrol_A2 �Լ� ����
                Patrol_A2();
                break;
        }
    }




    ///// ����_�߰� �Լ� ///// ---------------------------------------------------------------------------


    // ����_�߰�_G �Լ�
    private void Chase_G()
    {
        switch (chaseType)
        {
            // ChaseType�� Chase_G1��
            case ChaseType.Chase_G1:
                // Chase_G1 �Լ� ����
                Chase_G1();
                break;
        }
    }

    // ����_�߰�_A �Լ�
    private void Chase_A()
    {
        switch (chaseType)
        {
            // ChaseType�� Chase_A1��
            case ChaseType.Chase_A1:
                // Chase_A1 �Լ� ����
                Chase_A1();
                break;

            // ChaseType�� Chase_A2��
            case ChaseType.Chase_A2:
                // Chase_A2 �Լ� ����
                Chase_A2();
                break;
        }
    }




    ///// ����_���� �Լ� ///// ---------------------------------------------------------------------------


    // ���� �Լ��� ��ų ��ü �����ϴ� �Լ� 1���� ������ �� �� (��ų ������ ��ų ��ü���� �ϴ� ���)
    private void Attack()
    {
        // ��ų ������ ����
        Instantiate(skill, transform.position, Quaternion.identity, transform.parent.parent);

        //GameObject newSkill = Instantiate(skill, transform.position, Quaternion.identity);

        // ������ newSkill�� ���� ���� ������Ʈ�� �ڽ����� �����մϴ�.
        //newSkill.transform.parent = transform;
        // ���� ���� ��Ÿ�Ӱ��� ���� ���� ��Ÿ�Ӱ��� ����
        currentCoolTime = attackCoolTime;
    }




    ///// ��Ʈ�� �Լ� ///// ------------------------------------------------------------------------------


    // Patrol_G1 �Լ�
    private void Patrol_G1()
    {
        // ���� ���� ������ ������Ŵ
        patrolAngle +=  patrolSpeed * Time.deltaTime;

        // ȸ���� �����ϴ� ������ �ڱ� ��ġ�� �������� ���
        float posX = Mathf.Cos(patrolAngle * Mathf.Deg2Rad) * patrolRadius;
        float posY = Mathf.Sin(patrolAngle * Mathf.Deg2Rad) * patrolRadius;

        // �ڱ� ��ġ�� �������� ���� ����
        transform.localPosition = new Vector3(posX, posY, 0f);

        // ���� ���� �ٶ󺸱�
        Vector3 parentCenter = transform.parent.position;
        Vector3 lookDir = parentCenter - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, lookDir);
    }




    private void Patrol_G2()
    {
        // ���� ���� ������ ������Ŵ
        patrolAngle -= patrolSpeed * Time.deltaTime;

        // ȸ���� �����ϴ� ������ �ڱ� ��ġ�� �������� ���
        float posX = Mathf.Cos(patrolAngle * Mathf.Deg2Rad) * patrolRadius;
        float posY = Mathf.Sin(patrolAngle * Mathf.Deg2Rad) * patrolRadius;

        // �ڱ� ��ġ�� �������� ���� ����
        transform.localPosition = new Vector3(posX, posY, 0f);

        // ���� ���� �ٶ󺸱�
        Vector3 parentCenter = transform.parent.position;
        Vector3 lookDir = parentCenter - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, lookDir);
    }

    private void Patrol_A1()
    {
        // ������ ī�޶� ���� ���� ��ǥ�� ������ ����
        Vector2 currentPos = Camera.main.WorldToViewportPoint(transform.position);
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        // Raycast�� ���� Floor �±׸� �ν��ϴ� �������� ȸ���ϱ�
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.1f, LayerMask.GetMask("Floor"));
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.1f, LayerMask.GetMask("Floor"));

        // ������ ���� ������
        if (goLR == GoLR.Left)
        {

            // �������� �̵�
            transform.Translate(Vector2.left * patrolSpeed * Time.deltaTime);

            // ī�޶� ���� ���� ���� �����ϰų� Floor�� ������
            if (hitLeft.collider != null)
            {
                // ������ ������ ����
                transform.localScale = new Vector2(size, size);
                goLR = GoLR.Right;
            }
        }

        // ������ ���� ������
        else
        {
            // �������� �̵�
            transform.Translate(Vector2.right * patrolSpeed * Time.deltaTime);

            // ī�޶� ���� ���� ���� �����ϰų� Floor�� ������
            if (hitRight.collider != null)
            {
                // ������ ������ ����
                transform.localScale = new Vector2(-size, size);
                goLR = GoLR.Left;
            }
        }
    }

    ////Patrol_A1 �Լ�
    //private void Patrol_A1()
    //{
    //    // ������ ī�޶� ���� ���� ��ǥ�� ������ ����
    //    Vector2 currentPos = Camera.main.WorldToViewportPoint(transform.position);
    //    transform.eulerAngles = new Vector3(0f, 0f, 0f);

    //    // ������ ���� ������
    //    if (goLR == GoLR.Left)
    //    {
    //        // �������� �̵�
    //        transform.Translate(Vector2.left * patrolSpeed * Time.deltaTime);

    //        // ī�޶� ���� ���� ���� �����ϸ�
    //        if (currentPos.x < 0.01f)
    //        {
    //            // ������ ������ ����
    //            transform.localScale = new Vector2(1, 1);
    //            goLR = GoLR.Right;
    //        }
    //    }

    //    // ������ ���� ������
    //    else
    //    {
    //        // �������� �̵�
    //        transform.Translate(Vector2.right * patrolSpeed * Time.deltaTime);

    //        // ī�޶� ���� ���� ���� �����ϸ�
    //        if (currentPos.x > 0.99f)
    //        {
    //            // ������ ������ ����
    //            transform.localScale = new Vector2(-1, 1);
    //            goLR = GoLR.Left;
    //        }
    //    }
    //}

    // Patrol_A2 �Լ�
    private void Patrol_A2()
    {
        // ������ ī�޶� ���� ���� ��ǥ�� ������ ����
        Vector2 currentPos = Camera.main.WorldToViewportPoint(transform.position);
        transform.eulerAngles = new Vector3(0f, 0f, 0f);

        // ������� �����Ǿ� ������
        if (goTB == GoTB.Top)
        {
            // ������� �̵�
            transform.Translate(Vector2.up * patrolSpeed * Time.deltaTime);

            // ī�޶� ���� ��� ���� �����ϸ�
            if (currentPos.y > 0.99f)
            {
                goTB = GoTB.Bottom;
            }
        }

        // �ϴ����� �����Ǿ� ������
        else
        {
            // �ϴ����� �̵�
            transform.Translate(Vector2.down * patrolSpeed * Time.deltaTime);

            // �ϴ� �ٴڰ� �浹�ϸ� (�浹 ����� �±װ��� Floor��)
            if (colTag == "Floor")
            {
                goTB = GoTB.Top;
                colTag = null;
            }
        }
    }




    ///// �߰� �Լ� ///// --------------------------------------------------------------------------------


    // Chase_G1 �Լ�
    private void Chase_G1()
    {
        // Check_Flatform �Լ� & Check_PC_Ray �Լ� ����
        Check_Flatform();
        Check_PC_Ray();


        // [���̿� �÷����� �浹 && ���̿� �÷��̾ �浹] �̸� 
        if (rayHitP.transform.CompareTag("Platform") && rayHitT.transform.CompareTag("Player"))
        {
            // ������ ���� ������
            if (dot > 0)
            {
                // �������� �̵�
                transform.Translate(Vector2.left * chaseSpeed * Time.deltaTime);
            }

            // ������ ���� ������
            else
            {
                // �������� �̵�
                transform.Translate(Vector2.right * chaseSpeed * Time.deltaTime);
            }
        }
    }

    // Chase_A1 �Լ�
    private void Chase_A1()
    {
        // PC �������� �̵�
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        transform.position += targetDir * chaseSpeed * Time.deltaTime;
    }

    // Chase_A2 �Լ�
    private void Chase_A2()
    {
        //// �÷��̾� ������ �ٶ󺸵��� ȸ��
        //float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        // PC �������� �̵�
        transform.position += targetDir * chaseSpeed * Time.deltaTime;
        // ������ �߰��ϱ� ���� �߰ݰŸ����� ũ�� ����
        chaseRange = 100f;
        if (targetDir.x < 0f) // �÷��̾ ������ ���ʿ� �ִ� ���
        {
            spriteRenderer.flipX = true; // X�� �������� ��������Ʈ ����
        }
        else // �÷��̾ ������ �����ʿ� �ִ� ���
        {
            spriteRenderer.flipX = false; // ��������Ʈ ���� ����
        }

    }
}

