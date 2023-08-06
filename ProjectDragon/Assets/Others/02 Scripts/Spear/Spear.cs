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
    // ������ ���� ���θ� ��Ÿ���� ����
    private bool isAttackEffectCreated = false;
    public Transform effectSpawnPoint; // Ư�� ��ġ�� ����Ʈ�� ������ Transform ����

    private WaveManager waveManager; // ���� ��� ó��, ����ó����

    public int spearslot = 0;  // ������ �󸶳� ä���� �ִ��� �Ǻ��ϱ� ���� ����
    public bool hasSpearSlot1; // ����_1 ä��/��� �Ǻ��ϱ� ���� ����
    public bool hasSpearSlot2; // ����_2 ä��/��� �Ǻ��ϱ� ���� ����
    public bool hasSpearSlot3; // ����_3 ä��/��� �Ǻ��ϱ� ���� ����

    public GameObject spearSlot1; // Spear_Slot_1 ������Ʈ�� �Ҵ��ϱ� ���� ����
    public GameObject spearSlot2; // Spear_Slot_2 ������Ʈ�� �Ҵ��ϱ� ���� ����
    public GameObject spearSlot3; // Spear_Slot_3 ������Ʈ�� �Ҵ��ϱ� ���� ����

    private SpriteRenderer spriteRenderer1; // spearSlot1�� SpriteRenderer ������Ʈ
    private SpriteRenderer spriteRenderer2; // spearSlot2�� SpriteRenderer ������Ʈ
    private SpriteRenderer spriteRenderer3; // spearSlot3�� SpriteRenderer ������Ʈ

    private Vector3 targetPosition;     // Ÿ�� ��ǥ (���콺 ��ǥ)
    private Vector3 directionToTarget;  // Ÿ�� ���� (���콺 ��ǥ ����)
    public Vector3 directionToTargetOfProjectile; // ����ü�� Ÿ�� ���� (���콺 ��ǥ ����)
    private Transform parentTransform;  // �θ�(PC) �� Transform    
    private Transform parentTransform2;  // �θ�(PC) �� Transform   

    private BulletList bullet;
    private GameObject Parent;
    private GameObject Parent2;

    public int monType1;
    public int monType2;
    public int monType3;

    //public GameObject bulletNormal; // ���ҽ� ������ ����� ������
    //public GameObject bulletAOE;   // ���ҽ� ������ ����� ������

    public float moveSpeed = 5f;     // â_�̵� �ӵ�
    public float maxDistance = 10f;  // â_�̵� �Ÿ�

    public enum SpearState { Idle, Pierce, Throw } // â�� ���� (���/���/������)
    SpearState spearstate = SpearState.Idle;

    enum attackState { Forward, Reverse } // ��� �� ���� (����/����)
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



    // â ���� �Ŵ���
    void StateManager()
    {
        if (spearstate == SpearState.Idle) { Idle(); }
        if (spearstate == SpearState.Pierce) { Pierce(); }
        if (spearstate == SpearState.Throw) { Throw(); }
    }



    // â ��� ����
    void Idle()
    {
        // â�� �׻� ���콺 Ŀ�� ���� �ٶ󺸵��� ó��
        Vector3 mousePositionOnScreen = Input.mousePosition;
        mousePositionOnScreen.z = transform.position.z - Camera.main.transform.position.z;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
        Vector3 directionToMouse = mousePositionInWorld - transform.position;

        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        Quaternion desiredRotation = Quaternion.Euler(0f, 0f, angle - 90f);

        transform.rotation = desiredRotation;



        // ���콺 [��]Ŭ���ϸ�
        if (Input.GetMouseButtonDown(0) && spearslot < 3 && spearstate == SpearState.Idle)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = transform.position.z - Camera.main.transform.position.z;
            targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            directionToTarget = (targetPosition - transform.position).normalized;

            spearstate = SpearState.Pierce;
            OnSpearStateChangedCallback?.Invoke(spearstate);
        }

        // ���콺 [��]Ŭ���ϸ�
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


    // â ���� ����
    void Pierce()
    {
        // �̹� �������� �����Ǿ��ٸ� �� �̻� �������� ����
        if (!isAttackEffectCreated)
        {// â ���� ����Ʈ
            Vector3 offset = new Vector3(0f, 0f, 0f); // Y ������ 1.0��ŭ �̵�
            GameObject SAttackEffect = Instantiate(SAttackEffectPrefab, effectSpawnPoint.position + offset, 
                transform.rotation * Quaternion.Euler(0, 0, 90));

            StartCoroutine(DestroySAttackEffect(SAttackEffect));
            isAttackEffectCreated = true;
        }

        float distanceToParent = Vector3.Distance(transform.position, parentTransform.position);

        if (attackstate == attackState.Forward)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;

            // Ÿ�� �������� �̵�
            transform.position += directionToTarget * moveSpeed * Time.deltaTime;

            // ��Ÿ� ��ŭ �̵��ϸ�
            if (distanceToParent >= maxDistance)
            {
                attackstate = attackState.Reverse;
            }
        }

        if (attackstate == attackState.Reverse)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            // PC �������� �̵�
            transform.position = Vector3.MoveTowards(transform.position, parentTransform.position, moveSpeed * Time.deltaTime);
            Vector3 directionToParent = (parentTransform.position - transform.position).normalized;
            transform.position += directionToParent * moveSpeed * Time.deltaTime;

            // PC ��ǥ�� �����ϸ�
            if (distanceToParent <= 0.1f)
            {
                spearstate = SpearState.Idle;
                OnSpearStateChangedCallback?.Invoke(spearstate);
                attackstate = attackState.Forward;
            }
        }
    }


    // â ������ ����
    void Throw()
    {
        // �̹� �������� �����Ǿ��ٸ� �� �̻� �������� ����
        if (!isAttackEffectCreated)
        {// â ���� ����Ʈ
            Vector3 offset = new Vector3(0f, 0f, 0f); // Y ������ 1.0��ŭ �̵�
            GameObject SAttackEffect = Instantiate(SAttackEffectPrefab, effectSpawnPoint.position + offset,
                Parent2.transform.rotation, Parent2.transform);
            StartCoroutine(DestroySAttackEffect(SAttackEffect));
            isAttackEffectCreated = true;

        }

        // ���� --
        if (spearslot > 0)
        {
            spearslot--;
        }

        // ���� ���� (���)
        {
            spearstate = SpearState.Idle;
            OnSpearStateChangedCallback?.Invoke(spearstate);

            // ����ü ������ ����
            if (bullet != null && bullet.Bullet.Length > 0)
                Instantiate(bullet.Bullet[monType1], spearSlot1.transform.position, Quaternion.identity, Parent.transform.parent);

            // ����_1&2&3 �� �ִ� ���
            if (spriteRenderer3.sprite != null)
            {
                // ����_1�� ����_2 ��������Ʈ ��� & ���� 2�� ����_3 ��������Ʈ ���               
                spriteRenderer1.sprite = spriteRenderer2.sprite;
                spriteRenderer2.sprite = spriteRenderer3.sprite;

                monType1 = monType2;
                monType2 = monType3;

                // ����_3 ��������Ʈ ����
                spriteRenderer3.sprite = null;
                hasSpearSlot3 = false;
            }

            // ����_1&2 �� �ִ� ���
            else if (spriteRenderer2.sprite != null)
            {
                // ����_1�� ����_2 ��������Ʈ ���
                spriteRenderer1.sprite = spriteRenderer2.sprite;

                monType1 = monType2;

                // ����_2 ��������Ʈ ����
                spriteRenderer2.sprite = null;
                hasSpearSlot2 = false;
            }

            // ����_1 �� �ִ� ���
            else if (spriteRenderer1.sprite != null)
            {
                // ����_1 ��������Ʈ ����
                spriteRenderer1.sprite = null;
                hasSpearSlot1 = false;
            }
        }



        // ����ü���� Ÿ�� ������ �����ϱ� ���� �۾�
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = transform.position.z - Camera.main.transform.position.z;
        targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        directionToTargetOfProjectile = (targetPosition - transform.position).normalized;
    }



    // â�� ���� �浹�� ó�� (ȣ������ �ʾƵ� ������ ȣ��Ǵ� �Լ�)
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // â�� �浹�� ���� �ǰ� ����Ʈ
            Vector3 offset = new Vector3(0f, 0f, 0f);
            GameObject MHitEffect = Instantiate(MHitEffectPrefab, collision.transform.position + offset, Quaternion.identity, Parent.transform.parent);
            StartCoroutine(DestroyMHitEffect(MHitEffect));

            // â�� �浹�� ���� ����
            Destroy(collision.gameObject);

            // ���� ++
            if (spearslot < 3)
            {
                spearslot++;
            }

            // ���� ���� (ä���)
            {
                // ����_1&2 �� �ִ� ���
                if (spriteRenderer2.sprite != null)
                {
                    // ����_3�� ����_2 ��������Ʈ ��� & ���� 2�� ����_1 ��������Ʈ ���               
                    spriteRenderer3.sprite = spriteRenderer2.sprite;
                    spriteRenderer2.sprite = spriteRenderer1.sprite;
                    hasSpearSlot3 = true;

                    monType3 = monType2;
                    monType2 = monType1;
                }

                // ����_1 �� �ִ� ���
                else if (spriteRenderer1.sprite != null)
                {
                    // ����_2�� ����_1 ��������Ʈ ���
                    spriteRenderer2.sprite = spriteRenderer1.sprite;
                    hasSpearSlot2 = true;

                    monType2 = monType1;
                }

                // â�� �浹�� ������ ��������Ʈ ����


                //â�� �浹�� ������ Ÿ�� ����
                if (spriteRenderer3.sprite == null)
                {
                    Sprite destroyedSprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;

                    EnemyType enemy = collision.gameObject.GetComponent<EnemyType>();
                    if (enemy != null)
                    {
                        monType1 = (int)enemy.monType;
                    }

                    // ����_1�� ���� ��������Ʈ ���
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
        // �ִϸ��̼� ��� �ð� ��ŭ ���
        yield return new WaitForSeconds(0.417f);

        // �ִϸ��̼� �� �ݶ��̴� �ı�
        Destroy(MHitEffect);
    }

    IEnumerator DestroySAttackEffect(GameObject SAttackEffect)
    {
        // �ִϸ��̼� ��� �ð� ��ŭ ���
        yield return new WaitForSeconds(0.333f);

        // �ִϸ��̼� �� �ݶ��̴� �ı�
        Destroy(SAttackEffect);

        isAttackEffectCreated = false;
    }
}

