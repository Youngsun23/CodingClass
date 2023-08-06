using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;



/////////////////////////////////////////////////////////////////////////////////////////////////////////



public class Spear : MonoBehaviour
{

    private Animator playerAnimator;
    public Animator MonHitEffect;
    public GameObject MHitEffectPrefab;
    public GameObject SAttackEffectPrefab;
    // 프리팹 생성 여부를 나타내는 변수
    private bool isAttackEffectCreated = false;
    public Transform effectSpawnPoint; // 특정 위치로 이펙트를 생성할 Transform 변수

    private WaveManager waveManager; // 몬스터 사망 처리, 점수처리용

    public int spearslot = 0;  // 슬롯이 얼마나 채워져 있는지 판별하기 위한 변수
    public bool hasSpearSlot1; // 슬롯_1 채움/비움 판별하기 위한 변수
    public bool hasSpearSlot2; // 슬롯_2 채움/비움 판별하기 위한 변수
    public bool hasSpearSlot3; // 슬롯_3 채움/비움 판별하기 위한 변수

    public GameObject spearSlot1; // Spear_Slot_1 오브젝트를 할당하기 위한 변수
    public GameObject spearSlot2; // Spear_Slot_2 오브젝트를 할당하기 위한 변수
    public GameObject spearSlot3; // Spear_Slot_3 오브젝트를 할당하기 위한 변수

    private SpriteRenderer spriteRenderer1; // spearSlot1의 SpriteRenderer 컴포넌트
    private SpriteRenderer spriteRenderer2; // spearSlot2의 SpriteRenderer 컴포넌트
    private SpriteRenderer spriteRenderer3; // spearSlot3의 SpriteRenderer 컴포넌트

    private Vector3 targetPosition;     // 타겟 좌표 (마우스 좌표)
    private Vector3 directionToTarget;  // 타겟 방향 (마우스 좌표 방향)
    public Vector3 directionToTargetOfProjectile; // 투사체의 타겟 방향 (마우스 좌표 방향)
    private Transform parentTransform;  // 부모(PC) 의 Transform    
    private Transform parentTransform2;  // 부모(PC) 의 Transform   

    private BulletList bullet;
    private GameObject Parent;
    private GameObject Parent2;

    public int monType1;
    public int monType2;
    public int monType3;

    //public GameObject bulletNormal; // 리소스 폴더에 저장된 프리팹
    //public GameObject bulletAOE;   // 리소스 폴더에 저장된 프리팹

    public float moveSpeed = 5f;     // 창_이동 속도
    public float maxDistance = 10f;  // 창_이동 거리

    public enum SpearState { Idle, Pierce, Throw } // 창의 상태 (대기/찌르기/던지기)
    SpearState spearstate = SpearState.Idle;

    enum attackState { Forward, Reverse } // 찌르기 시 상태 (전진/후진)
    attackState attackstate = attackState.Forward;

    public bool canInput = true;

    /////////////////////////////////////////////////////////////////////////////////////////////////////

    public event Action<SpearState> OnSpearStateChangedCallback;


    private void Start()
    {
        bullet = FindObjectOfType<BulletList>();
        waveManager = FindObjectOfType<WaveManager>();
        Parent = GameObject.Find("WaveManager");
        Parent2 = GameObject.Find("Spear_Point");

        parentTransform = transform.parent;
        parentTransform2 = transform.parent;

        spriteRenderer1 = spearSlot1.GetComponent<SpriteRenderer>();
        spriteRenderer2 = spearSlot2.GetComponent<SpriteRenderer>();
        spriteRenderer3 = spearSlot3.GetComponent<SpriteRenderer>();

        playerAnimator = GetComponent<Animator>();
        MonHitEffect = MHitEffectPrefab.GetComponent<Animator>();
        isAttackEffectCreated = false;
    }


    private void Update()
    {
        if (canInput)
        {
            StateManager();
        }

    }



    /////////////////////////////////////////////////////////////////////////////////////////////////////



    // 창 상태 매니저
    void StateManager()
    {
        if (spearstate == SpearState.Idle) { Idle(); }
        if (spearstate == SpearState.Pierce) { Pierce(); }
        if (spearstate == SpearState.Throw) { Throw(); }
    }



    // 창 대기 상태
    void Idle()
    {
        // 창이 항상 마우스 커서 쪽을 바라보도록 처리
        Vector3 mousePositionOnScreen = Input.mousePosition;
        mousePositionOnScreen.z = transform.position.z - Camera.main.transform.position.z;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
        Vector3 directionToMouse = mousePositionInWorld - transform.position;

        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        Quaternion desiredRotation = Quaternion.Euler(0f, 0f, angle - 90f);

        transform.rotation = desiredRotation;



        // 마우스 [좌]클릭하면
        if (Input.GetMouseButtonDown(0) && spearslot < 3 && spearstate == SpearState.Idle)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = transform.position.z - Camera.main.transform.position.z;
            targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            directionToTarget = (targetPosition - transform.position).normalized;

            spearstate = SpearState.Pierce;
            OnSpearStateChangedCallback?.Invoke(spearstate);
        }

        // 마우스 [우]클릭하면
        if (Input.GetMouseButtonDown(1) && spearslot != 0 && spearstate == SpearState.Idle)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = transform.position.z - Camera.main.transform.position.z;
            targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            directionToTarget = (targetPosition - transform.position).normalized;

            spearstate = SpearState.Throw;
            OnSpearStateChangedCallback?.Invoke(spearstate);
        }
    }


    // 창 공격 상태
    void Pierce()
    {
        // 이미 프리팹이 생성되었다면 더 이상 생성하지 않음
        if (!isAttackEffectCreated)
        {// 창 공격 이펙트
            Vector3 offset = new Vector3(0f, 0f, 0f); // Y 축으로 1.0만큼 이동
            GameObject SAttackEffect = Instantiate(SAttackEffectPrefab, effectSpawnPoint.position + offset, 
                transform.rotation * Quaternion.Euler(0, 0, 90));

            StartCoroutine(DestroySAttackEffect(SAttackEffect));
            isAttackEffectCreated = true;
        }

        float distanceToParent = Vector3.Distance(transform.position, parentTransform.position);

        if (attackstate == attackState.Forward)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;

            // 타겟 방향으로 이동
            transform.position += directionToTarget * moveSpeed * Time.deltaTime;

            // 사거리 만큼 이동하면
            if (distanceToParent >= maxDistance)
            {
                attackstate = attackState.Reverse;
            }
        }

        if (attackstate == attackState.Reverse)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            // PC 방향으로 이동
            transform.position = Vector3.MoveTowards(transform.position, parentTransform.position, moveSpeed * Time.deltaTime);
            Vector3 directionToParent = (parentTransform.position - transform.position).normalized;
            transform.position += directionToParent * moveSpeed * Time.deltaTime;

            // PC 좌표에 도착하면
            if (distanceToParent <= 0.1f)
            {
                spearstate = SpearState.Idle;
                OnSpearStateChangedCallback?.Invoke(spearstate);
                attackstate = attackState.Forward;
            }
        }
    }


    // 창 던지기 상태
    void Throw()
    {
        // 이미 프리팹이 생성되었다면 더 이상 생성하지 않음
        if (!isAttackEffectCreated)
        {// 창 공격 이펙트
            Vector3 offset = new Vector3(0f, 0f, 0f); // Y 축으로 1.0만큼 이동
            GameObject SAttackEffect = Instantiate(SAttackEffectPrefab, effectSpawnPoint.position + offset,
                Parent2.transform.rotation, Parent2.transform);
            StartCoroutine(DestroySAttackEffect(SAttackEffect));
            isAttackEffectCreated = true;

        }

        // 슬롯 --
        if (spearslot > 0)
        {
            spearslot--;
        }

        // 슬롯 정리 (사용)
        {
            spearstate = SpearState.Idle;
            OnSpearStateChangedCallback?.Invoke(spearstate);

            // 투사체 프리팹 생성
            if (bullet != null && bullet.Bullet.Length > 0)
                Instantiate(bullet.Bullet[monType1], spearSlot1.transform.position, Quaternion.identity, Parent.transform.parent);

            // 슬롯_1&2&3 차 있는 경우
            if (spriteRenderer3.sprite != null)
            {
                // 슬롯_1에 슬롯_2 스프라이트 출력 & 슬롯 2에 슬롯_3 스프라이트 출력               
                spriteRenderer1.sprite = spriteRenderer2.sprite;
                spriteRenderer2.sprite = spriteRenderer3.sprite;

                monType1 = monType2;
                monType2 = monType3;

                // 슬롯_3 스프라이트 제거
                spriteRenderer3.sprite = null;
                hasSpearSlot3 = false;
            }

            // 슬롯_1&2 차 있는 경우
            else if (spriteRenderer2.sprite != null)
            {
                // 슬롯_1에 슬롯_2 스프라이트 출력
                spriteRenderer1.sprite = spriteRenderer2.sprite;

                monType1 = monType2;

                // 슬롯_2 스프라이트 제거
                spriteRenderer2.sprite = null;
                hasSpearSlot2 = false;
            }

            // 슬롯_1 차 있는 경우
            else if (spriteRenderer1.sprite != null)
            {
                // 슬롯_1 스프라이트 제거
                spriteRenderer1.sprite = null;
                hasSpearSlot1 = false;
            }
        }



        // 투사체에게 타겟 방향을 리턴하기 위한 작업
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = transform.position.z - Camera.main.transform.position.z;
        targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        directionToTargetOfProjectile = (targetPosition - transform.position).normalized;
    }



    // 창과 몬스터 충돌시 처리 (호출하지 않아도 무조건 호출되는 함수)
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // 창과 충돌한 몬스터 피격 이펙트
            Vector3 offset = new Vector3(0f, 0f, 0f);
            GameObject MHitEffect = Instantiate(MHitEffectPrefab, collision.transform.position + offset, Quaternion.identity, Parent.transform.parent);
            StartCoroutine(DestroyMHitEffect(MHitEffect));

            // 창과 충돌한 몬스터 제거
            Destroy(collision.gameObject);

            // 슬롯 ++
            if (spearslot < 3)
            {
                spearslot++;
            }

            // 슬롯 정리 (채우기)
            {
                // 슬롯_1&2 차 있는 경우
                if (spriteRenderer2.sprite != null)
                {
                    // 슬롯_3에 슬롯_2 스프라이트 출력 & 슬롯 2에 슬롯_1 스프라이트 출력               
                    spriteRenderer3.sprite = spriteRenderer2.sprite;
                    spriteRenderer2.sprite = spriteRenderer1.sprite;
                    hasSpearSlot3 = true;

                    monType3 = monType2;
                    monType2 = monType1;
                }

                // 슬롯_1 차 있는 경우
                else if (spriteRenderer1.sprite != null)
                {
                    // 슬롯_2에 슬롯_1 스프라이트 출력
                    spriteRenderer2.sprite = spriteRenderer1.sprite;
                    hasSpearSlot2 = true;

                    monType2 = monType1;
                }

                // 창과 충돌한 몬스터의 스프라이트 저장


                //창과 충돌한 몬스터의 타입 저장
                if (spriteRenderer3.sprite == null)
                {
                    Sprite destroyedSprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;

                    EnemyType enemy = collision.gameObject.GetComponent<EnemyType>();
                    if (enemy != null)
                    {
                        monType1 = (int)enemy.monType;
                    }

                    // 슬롯_1에 저장 스프라이트 출력
                    spriteRenderer1.sprite = destroyedSprite;
                    hasSpearSlot1 = true;
                }
            }
            waveManager.UpdateWaveStatus();
            waveManager.IncreaseScore(100);
        }
    }
    IEnumerator DestroyMHitEffect(GameObject MHitEffect)
    {
        // 애니메이션 재생 시간 만큼 대기
        yield return new WaitForSeconds(0.417f);

        // 애니메이션 및 콜라이더 파괴
        Destroy(MHitEffect);
    }

    IEnumerator DestroySAttackEffect(GameObject SAttackEffect)
    {
        // 애니메이션 재생 시간 만큼 대기
        yield return new WaitForSeconds(0.333f);

        // 애니메이션 및 콜라이더 파괴
        Destroy(SAttackEffect);

        isAttackEffectCreated = false;
    }
}

