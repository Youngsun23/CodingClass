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
    private Ray2D ray;            // 레이 관련값(방향 등) 을 저장하기 위한 변수 
    private RaycastHit2D rayHitP; // 레이와 플랫폼의 충돌 정보를 저장하기 위한 변수
    private RaycastHit2D rayHitT; // 레이와 타겟의 충돌 정보를 저장하기 위한 변수
    private float rayRange = 2f;  // 레이 발사거리 (플랫폼 체크용)

    private string colTag;        // 콜라이더와 충돌한 대상의 태그값을 저장하기 위한 변수
    private Vector3 targetPos;    // PC의 좌표값을 저장하기 위한 변수
    private float targetDist;     // PC와 몬스터의 거리를 저장하기 위한 변수
    private Vector3 targetDir;    // PC 방향을 저장하기 위한 변수
    private SpriteRenderer spriteRenderer;
    

    private float patrolRadius = 11.2f; // 원형 돌기 반지름
    private float patrolAngle = 0f; // 현재 원형 돌기 각도

    // PC가 몬스터의 좌측에 있는지, 우측에 있는지를 구하는 연산 관련 변수들
    private Vector3 tt;
    private Vector3 cc;
    private float dot;

    public float size;
    public GoTB goTB;                 // 몬스터의 최초 상/하 방향을 설정하기 위한 변수
    public GoLR goLR;                 // 몬스터의 최초 좌/우 방향을 설정하기 위한 변수

    public float patrolSpeed;         // 몬스터 순찰 이동 속도
    public float chaseSpeed;          // 몬스터 추격 이동 속도

    public float chaseRange;          // 몬스터 추격 거리 
    public float attackRange;         // 몬스터 공격 거리 (사거리)
    public float attackCoolTime;      // 몬스터 공격 쿨타임
    private float currentCoolTime;    // 몬스터의 현재 공격 쿨타임을 저장하기 위한 변수

    public GameObject skill;          // 몬스터의 스킬 프리팹을 저장하기 위한 변수

    public State state = State.Idle;  // 몬스터의 상태를 판별하기 위한 변수 (초기값 = Idle)                       
    public MoveType moveType;         // 몬스터의 이동 타입을 설정하기 위한 변수      
    public PatrolType patrolType;     // 몬스터의 순찰 여부 & 타입을 설정하기 위한 변수
    public ChaseType chaseType;       // 몬스터의 추격 여부 & 타입을 설정하기 위한 변수


    public enum GoTB       // 몬스터의 최초 상/하 방향을 설정하기 위한 변수값 멤버
    {
        Top,       // 상단
        Bottom     // 하단
    }

    public enum GoLR       // 몬스터의 최초 좌/우 방향을 설정하기 위한 변수값 멤버
    {
        Left,      // 좌측
        Right      // 우측
    }

    public enum State      // 몬스터의 상태를 판별하기 위한 변수값 멤버 (연산을 통해 자동 변환)
    {
        Idle,     // 상태_대기
        Chase,    // 상태_추격
        Attack    // 상태_공격
    }

    public enum MoveType   // 몬스터의 이동 타입을 설정하기 위한 변수값 멤버
    {
        Ground,   // 이동_지상 [지상 이동형 몬스터]
        Air       // 이동_공중 [공중 이동형 몬스터]
    }

    public enum PatrolType // 몬스터의 순찰 여부 & 타입을 설정하기 위한 변수값 멤버
    {
        None,      // 순찰_X
        Patrol_G1, // 순찰_지상_1 [지상 (반시계방향 회전)]
        Patrol_G2, // 순찰_지상_2 [지상 (시계방향 회전)]
        Patrol_A1, // 순찰_공중_1 [공중_직선_좌우 (카메라 영역 안에서만 순찰)]
        Patrol_A2  // 순찰_공중_2 [공중_직선_상하 (카메라 영역 안에서만 순찰)]
    }

    public enum ChaseType  // 몬스터의 추격 여부 & 타입을 설정하기 위한 변수값 멤버
    {
        None,      // 추격_X
        Chase_G1,  // 추격_지상_1 [지상 (플랫폼 영역 안에서만 추격)]
        Chase_A1,  // 추격_공중_1 [공중 (카메라 영역 안에서만 추격 / 탐지범위 벗어나면 대기)]
        Chase_A2,  // 추격_공중_2 [공중 (카메라 영역 안에서만 추격 / 탐지범위 벗어나도 추격)]
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
        // moveType이 Ground면 (지상형 몬스터)
        if (moveType == MoveType.Ground)
        {
            // 지상형 몬스터에게 필요한 함수 실행
            Check_Flatform();
            Check_PC_Dist();
            Check_PC_Ray();
            CoolTimeManager();
            StateManager_G();
        }

        // moveType이 Air면 (공중형 몬스터)
        if (moveType == MoveType.Air)
        {
            // 공중형 몬스터에게 필요한 함수 실행
            Check_PC_Dist();
            CoolTimeManager();
            StateManager_A();
        }
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////////////




    ///// 각 종 부가 함수 ///// -------------------------------------------------------------------------


    // 충돌 체크 함수 (자동 호출)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌 대상의 태그가 Floor면
        if (collision.CompareTag("Floor"))
        {
            // 충돌 대상의 태그값을 변수에 저장
            colTag = collision.gameObject.tag;
        }
    }

    // 플랫폼 체크 함수 (레이와 플랫폼의 충돌여부 검사 / 지상형 몬스터가 플랫폼 위에서만 이동하도록 하는 용도)
    private void Check_Flatform()
    {
        // 레이 발사 방향 설정
        {
            // 좌측을 보고 있는 경우
            if (goLR == GoLR.Left)
            {
                // 현재 개체의 방향을 얻어옵니다.
                Vector2 objectDirection = transform.right.normalized;

                // 45도 아래 방향 벡터를 계산합니다.
                float angleInDegrees = -135f;
                Vector2 rayDirection = Quaternion.Euler(0f, 0f, angleInDegrees) * objectDirection;

                // 방향을 설정합니다.
                ray.direction = rayDirection;
            }

            // 우측을 보고 있는 경우
            else
            {
                // 현재 개체의 방향을 얻어옵니다.
                Vector2 objectDirection = transform.right.normalized;

                // 135도 아래 방향 벡터를 계산합니다.
                float angleInDegrees = -45f;
                Vector2 rayDirection = Quaternion.Euler(0f, 0f, angleInDegrees) * objectDirection;

                // 방향을 설정합니다.
                ray.direction = rayDirection;
            }
        }

        // 레이 발사 (현재좌표에서, 설정된 방향으로, 설정된 거리만큼) / 레이어가 "Platform"인 객체만 체크
        rayHitP = Physics2D.Raycast(transform.position, ray.direction, rayRange, LayerMask.GetMask("Platform"));
        Debug.DrawRay(transform.position, ray.direction * rayRange, Color.red);
    }

    // PC 체크 함수 (PC의 좌표/거리/방향 등을 검사) (지상형&공중형 몬스터 모두에게 필요)
    private void Check_PC_Dist()
    {
        // PC의 좌표 저장
        targetPos = GameObject.FindWithTag("Player").transform.position;
        // PC와 몬스터의 거리 저장  
        targetDist = Vector3.Distance(targetPos, transform.position);
        // PC의 좌표 방향 저장
        targetDir = (targetPos - transform.position).normalized;

        // PC가 몬스터의 좌측에 있는지, 우측에 있는지를 구하는 연산 (dot > 0 면 좌측, dot < 0 면 우측) 
        tt = targetPos - transform.position;
        cc = Vector3.Cross(tt, transform.forward);
        dot = Vector3.Dot(cc, Vector3.up);
    }

    // PC 체크 함수 (레이와 PC의 충돌여부 검사 / 지상형 몬스터의 추격거리 내에 PC가 있는지 판별하는 용도) (지상형 몬스터만 필요)
    private void Check_PC_Ray()
    {
        // 레이 발사 방향 설정
        {
            // 좌측을 보고 있는 경우
            if (goLR == GoLR.Left)
            {
                // 레이 발사 방향을 좌측으로 설정
                ray.direction = new Vector2(-1f, 0f);
            }

            // 우측을 보고 있는 경우
            else
            {
                // 레이 발사 방향을 우측으로 설정
                ray.direction = new Vector2(1f, 0f);
            }
        }

        // 레이 발사 (현재좌표에서, 설정된 방향으로, 설정된 거리만큼) / 레이어가 "Player"인 객체만 체크
        rayHitT = Physics2D.Raycast(transform.position, ray.direction, chaseRange, LayerMask.GetMask("Player"));
        //Debug.DrawRay(transform.position, ray.direction * chaseRange, Color.red);
    }

    // 최초 좌/우 방향이 좌측이면 좌측을 보도록 해주는 함수
    private void LookLeft()
    {
        // 몬스터의 최초 좌/우 방향이 좌측이면
        if (goLR == GoLR.Left)
        {

            
            // 좌측을 보도록 변경 (보편적으로 스프라이트가 우측을 바라보고 있으므로)
            transform.localScale = new Vector2(-size, size);
        }
    }

    // 공격 쿨타임 매니저 함수
    private void CoolTimeManager()
    {
        if (currentCoolTime > 0f)
        {
            currentCoolTime -= Time.deltaTime;
           // Debug.Log(currentCoolTime);
        }
    }



    
    ///// 상태 매니저 함수 ///// ------------------------------------------------------------------------
    

    // 상태 매니저_G 함수
    private void StateManager_G()
    {
        // PC와의 거리가 공격거리 이하 & 현재 공격 쿨타임값이 0이하면
        if (targetDist <= attackRange && currentCoolTime <= 0)
        {
            // skill이 null이 아니면
            if (skill != null)
            {
                // 공격 상태로 전환
                state = State.Attack;

                // Attack 함수 실행
                Attack();
            }
        }

        // 레이와 PC가 충돌하면
        else if (rayHitT)
        {
            //chaseType이 None이 아니면
            if (chaseType != ChaseType.None)
            {
                // 추격 상태로 전환
                state = State.Chase;

                // Chase_G 함수 실행
                Chase_G();
            }
        }

        // PC와의 거리가 공격거리 초과 & 레이와 PC가 충돌하지 않으면
        else
        {
            // 대기 상태로 전환
            state = State.Idle;

            // Idle_G 함수 실행
            Idle_G();
        }
    }

    // 상태 매니저_A 함수 (공중형 몬스터)
    private void StateManager_A()
    {
        // PC와의 거리가 공격거리 이하 & 현재 공격 쿨타임값이 0이하면
        if (targetDist <= attackRange && currentCoolTime <= 0)
        {
            // skill이 null이 아니면
            if (skill != null)
            {
                // 공격 상태로 전환
                state = State.Attack;

                // Attack 함수 실행
                Attack();
            }
        }

        // PC와의 거리가 추격거리 이하면
        else if (targetDist <= chaseRange)
        {
            // chaseType이 None이 아니면
            if (chaseType != ChaseType.None)
            {
                // 추격 상태로 전환
                state = State.Chase;
               
                // Chase_A 함수 실행
                Chase_A();
            }
        }

        // PC와의 거리가 공격거리 초과 & 추격거리 초과면
        else
        {
            // 대기 상태로 전환
            state = State.Idle;

            // Idle_A 함수 실행
            Idle_A();
        }
    }




    ///// 상태_대기 함수 ///// --------------------------------------------------------------------------


    // 상태_대기_G 함수
    private void Idle_G()
    {
        switch (patrolType)
        {
            // PatrolType이 None면
            case PatrolType.None:
                // 아무것도 실행하지 않음 (순찰X)
                break;

            // PatrolType이 Patrol_G1면
            case PatrolType.Patrol_G1:               
                // Patrol_G1 함수 실행
                Patrol_G1();
                break;
            case PatrolType.Patrol_G2:
                // Patrol_G1 함수 실행
                Patrol_G2();
                break;
        }
    }

    // 상태_대기_A 함수
    private void Idle_A()
    {
        switch (patrolType)
        {
            // PatrolType이 None면
            case PatrolType.None:
                // 아무것도 실행하지 않음 (순찰X)
                break;

            // PatrolType이 Patrol_A1면
            case PatrolType.Patrol_A1:
                // Patrol_A1 함수 실행
                Patrol_A1();
                break;

            // PatrolType이 Patrol_A2면
            case PatrolType.Patrol_A2:
                // Patrol_A2 함수 실행
                Patrol_A2();
                break;
        }
    }




    ///// 상태_추격 함수 ///// ---------------------------------------------------------------------------


    // 상태_추격_G 함수
    private void Chase_G()
    {
        switch (chaseType)
        {
            // ChaseType이 Chase_G1면
            case ChaseType.Chase_G1:
                // Chase_G1 함수 실행
                Chase_G1();
                break;
        }
    }

    // 상태_추격_A 함수
    private void Chase_A()
    {
        switch (chaseType)
        {
            // ChaseType이 Chase_A1면
            case ChaseType.Chase_A1:
                // Chase_A1 함수 실행
                Chase_A1();
                break;

            // ChaseType이 Chase_A2면
            case ChaseType.Chase_A2:
                // Chase_A2 함수 실행
                Chase_A2();
                break;
        }
    }




    ///// 상태_공격 함수 ///// ---------------------------------------------------------------------------


    // 공격 함수는 스킬 객체 생성하는 함수 1개만 있으면 될 듯 (스킬 설정은 스킬 객체에서 하는 방식)
    private void Attack()
    {
        // 스킬 프리팹 생성
        Instantiate(skill, transform.position, Quaternion.identity, transform.parent.parent);

        //GameObject newSkill = Instantiate(skill, transform.position, Quaternion.identity);

        // 생성된 newSkill을 현재 게임 오브젝트의 자식으로 설정합니다.
        //newSkill.transform.parent = transform;
        // 현재 공격 쿨타임값에 원래 공격 쿨타임값을 저장
        currentCoolTime = attackCoolTime;
    }




    ///// 패트롤 함수 ///// ------------------------------------------------------------------------------


    // Patrol_G1 함수
    private void Patrol_G1()
    {
        // 원형 돌기 각도를 증가시킴
        patrolAngle +=  patrolSpeed * Time.deltaTime;

        // 회전을 시작하는 지점을 자기 위치를 기준으로 계산
        float posX = Mathf.Cos(patrolAngle * Mathf.Deg2Rad) * patrolRadius;
        float posY = Mathf.Sin(patrolAngle * Mathf.Deg2Rad) * patrolRadius;

        // 자기 위치를 기준으로 원형 돌기
        transform.localPosition = new Vector3(posX, posY, 0f);

        // 같은 방향 바라보기
        Vector3 parentCenter = transform.parent.position;
        Vector3 lookDir = parentCenter - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, lookDir);
    }




    private void Patrol_G2()
    {
        // 원형 돌기 각도를 증가시킴
        patrolAngle -= patrolSpeed * Time.deltaTime;

        // 회전을 시작하는 지점을 자기 위치를 기준으로 계산
        float posX = Mathf.Cos(patrolAngle * Mathf.Deg2Rad) * patrolRadius;
        float posY = Mathf.Sin(patrolAngle * Mathf.Deg2Rad) * patrolRadius;

        // 자기 위치를 기준으로 원형 돌기
        transform.localPosition = new Vector3(posX, posY, 0f);

        // 같은 방향 바라보기
        Vector3 parentCenter = transform.parent.position;
        Vector3 lookDir = parentCenter - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, lookDir);
    }

    private void Patrol_A1()
    {
        // 몬스터의 카메라 기준 현재 좌표를 변수에 저장
        Vector2 currentPos = Camera.main.WorldToViewportPoint(transform.position);
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        // Raycast를 통해 Floor 태그를 인식하는 방향으로 회전하기
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.1f, LayerMask.GetMask("Floor"));
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.1f, LayerMask.GetMask("Floor"));

        // 좌측을 보고 있으면
        if (goLR == GoLR.Left)
        {

            // 좌측으로 이동
            transform.Translate(Vector2.left * patrolSpeed * Time.deltaTime);

            // 카메라 영역 좌측 끝에 도착하거나 Floor를 만나면
            if (hitLeft.collider != null)
            {
                // 우측을 보도록 변경
                transform.localScale = new Vector2(size, size);
                goLR = GoLR.Right;
            }
        }

        // 우측을 보고 있으면
        else
        {
            // 우측으로 이동
            transform.Translate(Vector2.right * patrolSpeed * Time.deltaTime);

            // 카메라 영역 우측 끝에 도착하거나 Floor를 만나면
            if (hitRight.collider != null)
            {
                // 좌측을 보도록 변경
                transform.localScale = new Vector2(-size, size);
                goLR = GoLR.Left;
            }
        }
    }

    ////Patrol_A1 함수
    //private void Patrol_A1()
    //{
    //    // 몬스터의 카메라 기준 현재 좌표를 변수에 저장
    //    Vector2 currentPos = Camera.main.WorldToViewportPoint(transform.position);
    //    transform.eulerAngles = new Vector3(0f, 0f, 0f);

    //    // 좌측을 보고 있으면
    //    if (goLR == GoLR.Left)
    //    {
    //        // 좌측으로 이동
    //        transform.Translate(Vector2.left * patrolSpeed * Time.deltaTime);

    //        // 카메라 영역 좌측 끝에 도착하면
    //        if (currentPos.x < 0.01f)
    //        {
    //            // 우측을 보도록 변경
    //            transform.localScale = new Vector2(1, 1);
    //            goLR = GoLR.Right;
    //        }
    //    }

    //    // 우측을 보고 있으면
    //    else
    //    {
    //        // 우측으로 이동
    //        transform.Translate(Vector2.right * patrolSpeed * Time.deltaTime);

    //        // 카메라 영역 우측 끝에 도착하면
    //        if (currentPos.x > 0.99f)
    //        {
    //            // 좌측을 보도록 변경
    //            transform.localScale = new Vector2(-1, 1);
    //            goLR = GoLR.Left;
    //        }
    //    }
    //}

    // Patrol_A2 함수
    private void Patrol_A2()
    {
        // 몬스터의 카메라 기준 현재 좌표를 변수에 저장
        Vector2 currentPos = Camera.main.WorldToViewportPoint(transform.position);
        transform.eulerAngles = new Vector3(0f, 0f, 0f);

        // 상단으로 설정되어 있으면
        if (goTB == GoTB.Top)
        {
            // 상단으로 이동
            transform.Translate(Vector2.up * patrolSpeed * Time.deltaTime);

            // 카메라 영역 상단 끝에 도착하면
            if (currentPos.y > 0.99f)
            {
                goTB = GoTB.Bottom;
            }
        }

        // 하단으로 설정되어 있으면
        else
        {
            // 하단으로 이동
            transform.Translate(Vector2.down * patrolSpeed * Time.deltaTime);

            // 하단 바닥과 충돌하면 (충돌 대상의 태그값이 Floor면)
            if (colTag == "Floor")
            {
                goTB = GoTB.Top;
                colTag = null;
            }
        }
    }




    ///// 추격 함수 ///// --------------------------------------------------------------------------------


    // Chase_G1 함수
    private void Chase_G1()
    {
        // Check_Flatform 함수 & Check_PC_Ray 함수 실행
        Check_Flatform();
        Check_PC_Ray();


        // [레이와 플랫폼이 충돌 && 레이와 플레이어가 충돌] 이면 
        if (rayHitP.transform.CompareTag("Platform") && rayHitT.transform.CompareTag("Player"))
        {
            // 좌측을 보고 있으면
            if (dot > 0)
            {
                // 좌측으로 이동
                transform.Translate(Vector2.left * chaseSpeed * Time.deltaTime);
            }

            // 우측을 보고 있으면
            else
            {
                // 우측으로 이동
                transform.Translate(Vector2.right * chaseSpeed * Time.deltaTime);
            }
        }
    }

    // Chase_A1 함수
    private void Chase_A1()
    {
        // PC 방향으로 이동
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        transform.position += targetDir * chaseSpeed * Time.deltaTime;
    }

    // Chase_A2 함수
    private void Chase_A2()
    {
        //// 플레이어 방향을 바라보도록 회전
        //float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        // PC 방향으로 이동
        transform.position += targetDir * chaseSpeed * Time.deltaTime;
        // 무조건 추격하기 위해 추격거리값을 크게 변경
        chaseRange = 100f;
        if (targetDir.x < 0f) // 플레이어가 몬스터의 왼쪽에 있는 경우
        {
            spriteRenderer.flipX = true; // X축 방향으로 스프라이트 반전
        }
        else // 플레이어가 몬스터의 오른쪽에 있는 경우
        {
            spriteRenderer.flipX = false; // 스프라이트 반전 해제
        }

    }
}

