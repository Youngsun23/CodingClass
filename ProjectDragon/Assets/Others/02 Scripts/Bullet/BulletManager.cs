using System.Collections;
using TreeEditor;
using UnityEngine;

/////////////////////////////////////////////////////////////////////////////////////////////////////////

public class BulletManager : MonoBehaviour
{
    private int PlayerHP;
    public Transform player;
    private WaveManager waveManager; // 몬스터 사망 처리, 점수처리용
    private Vector3 directionToTarget;   // 타겟 방향 (스피어 객체 변수 참조)
    public float moveSpeed; // 투사체_이동 속도
    private GameObject Parent;
    public GameObject MHitEffectPrefab;
    public enum SkillType // 투사체_타입
    {
        Normal,
        AOE,
        Fly
    }
    public SkillType type;

    public GameObject explosionPrefab; // 폭발 애니메이션 프리팹
    private Vector3 explosionPosition; // 폭발 위치

    /////////////////////////////////////////////////////////////////////////////////////////////////////



    private void Start()
    {
        Parent = GameObject.Find("WaveManager");
        waveManager = FindObjectOfType<WaveManager>();
        directionToTarget = GameObject.Find("spear").GetComponent<Spear>().directionToTargetOfProjectile;
    }

    private void Update()
    {
        float rotationSpeed = 2160f;

        // z축 기준으로 회전각을 시간에 따라 증가시킴
        transform.rotation *= Quaternion.Euler(0f, 0f, rotationSpeed * Time.deltaTime);


        transform.position += directionToTarget * moveSpeed * Time.deltaTime;
    }



    /////////////////////////////////////////////////////////////////////////////////////////////////////


    void OnTriggerEnter2D(Collider2D collider)
    {


        switch (type)
        {
            case SkillType.Normal:
                if (collider.CompareTag("Enemy") && !gameObject.CompareTag("Bullet_enemy"))
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    // 창 투사체에 맞은 몬스터 피격 이펙트
                    Vector3 offset = new Vector3(0f, 0f, 0f);
                    GameObject MHitEffect = Instantiate(MHitEffectPrefab, transform.position + offset, Quaternion.identity, Parent.transform.parent);
                    StartCoroutine(DestroyMHitEffect(MHitEffect));

                    // 투사체와 충돌한 몬스터 제거
                    Destroy(collider.gameObject);
                   

                    waveManager.UpdateWaveStatus();
                    waveManager.IncreaseScore(100);
                }
                else if (collider.CompareTag("Floor") && !gameObject.CompareTag("Bullet_enemy"))
                {
                    Destroy(gameObject);
                }

                else if (collider.CompareTag("Player") && gameObject.CompareTag("Bullet_enemy"))
                {
                    GetComponent<PlayerHealthPlusUI>().TakeDamage(1);
                }

                break;

            case SkillType.AOE:
                if (collider.CompareTag("Enemy") || collider.CompareTag("Floor") && !gameObject.CompareTag("Bullet_enemy"))
                {

                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    explosionPosition = transform.position; // 충돌 지점의 위치 기록

                    // 폭발 애니메이션 생성
                    GameObject explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity, Parent.transform.parent);

                    // 폭발 애니메이션 재생
                    Animator animator = explosion.GetComponent<Animator>();
                    animator.SetBool("isExplosion", true);

                    // 폭발 애니메이션 및 콜라이더 파괴를 위한 코루틴 실행
                    StartCoroutine(DestroyExplosion(explosion));
                }

                else if (collider.CompareTag("Player") && gameObject.CompareTag("Bullet_enemy"))
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    explosionPosition = transform.position; // 충돌 지점의 위치 기록

                    // 폭발 애니메이션 생성
                    GameObject explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);

                    // 폭발 애니메이션 재생
                    Animator animator = explosion.GetComponent<Animator>();
                    animator.SetBool("isExplosion", true);

                    // 폭발 애니메이션 및 콜라이더 파괴를 위한 코루틴 실행
                    StartCoroutine(DestroyExplosion(explosion));

                }
                break;

            case SkillType.Fly:
                if (collider.CompareTag("Enemy") && !gameObject.CompareTag("Bullet_enemy"))
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    // 창 투사체에 맞은 몬스터 피격 이펙트
                    Vector3 offset = new Vector3(0f, 0f, 0f);
                    GameObject MHitEffect = Instantiate(MHitEffectPrefab, transform.position + offset, Quaternion.identity, Parent.transform.parent);
                    StartCoroutine(DestroyMHitEffect(MHitEffect));

                    // 투사체와 충돌한 몬스터 제거
                    Destroy(collider.gameObject);

                    waveManager.UpdateWaveStatus();
                    waveManager.IncreaseScore(100);
                }
                else if (collider.CompareTag("Floor") && !gameObject.CompareTag("Bullet_enemy"))
                {
                    Destroy(gameObject);
                }

                else if (collider.CompareTag("Player") && gameObject.CompareTag("Bullet_enemy"))
                {
                    GetComponent<PlayerHealthPlusUI>().TakeDamage(1);
                }

                break;
        }

    }

    IEnumerator DestroyExplosion(GameObject explosion)
    {
        // 폭발 애니메이션 재생 시간 만큼 대기
        yield return new WaitForSeconds(2.75f);

        // 폭발 애니메이션 및 콜라이더 파괴
        Destroy(explosion);
    }

    IEnumerator DestroyMHitEffect(GameObject MHitEffect)
    {
        // 애니메이션 재생 시간 만큼 대기
        yield return new WaitForSeconds(0.417f);

        // 애니메이션 및 콜라이더 파괴
        Destroy(MHitEffect);
    }
}

