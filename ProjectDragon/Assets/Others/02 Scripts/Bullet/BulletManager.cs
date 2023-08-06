using System.Collections;
using TreeEditor;
using UnityEngine;

/////////////////////////////////////////////////////////////////////////////////////////////////////////

public class BulletManager : MonoBehaviour
{
    private int PlayerHP;
    public Transform player;
    private WaveManager waveManager; // ���� ��� ó��, ����ó����
    private Vector3 directionToTarget;   // Ÿ�� ���� (���Ǿ� ��ü ���� ����)
    public float moveSpeed; // ����ü_�̵� �ӵ�
    private GameObject Parent;
    public GameObject MHitEffectPrefab;
    public enum SkillType // ����ü_Ÿ��
    {
        Normal,
        AOE,
        Fly
    }
    public SkillType type;

    public GameObject explosionPrefab; // ���� �ִϸ��̼� ������
    private Vector3 explosionPosition; // ���� ��ġ

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

        // z�� �������� ȸ������ �ð��� ���� ������Ŵ
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

                    // â ����ü�� ���� ���� �ǰ� ����Ʈ
                    Vector3 offset = new Vector3(0f, 0f, 0f);
                    GameObject MHitEffect = Instantiate(MHitEffectPrefab, transform.position + offset, Quaternion.identity, Parent.transform.parent);
                    StartCoroutine(DestroyMHitEffect(MHitEffect));

                    // ����ü�� �浹�� ���� ����
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
                    explosionPosition = transform.position; // �浹 ������ ��ġ ���

                    // ���� �ִϸ��̼� ����
                    GameObject explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity, Parent.transform.parent);

                    // ���� �ִϸ��̼� ���
                    Animator animator = explosion.GetComponent<Animator>();
                    animator.SetBool("isExplosion", true);

                    // ���� �ִϸ��̼� �� �ݶ��̴� �ı��� ���� �ڷ�ƾ ����
                    StartCoroutine(DestroyExplosion(explosion));
                }

                else if (collider.CompareTag("Player") && gameObject.CompareTag("Bullet_enemy"))
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    explosionPosition = transform.position; // �浹 ������ ��ġ ���

                    // ���� �ִϸ��̼� ����
                    GameObject explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);

                    // ���� �ִϸ��̼� ���
                    Animator animator = explosion.GetComponent<Animator>();
                    animator.SetBool("isExplosion", true);

                    // ���� �ִϸ��̼� �� �ݶ��̴� �ı��� ���� �ڷ�ƾ ����
                    StartCoroutine(DestroyExplosion(explosion));

                }
                break;

            case SkillType.Fly:
                if (collider.CompareTag("Enemy") && !gameObject.CompareTag("Bullet_enemy"))
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    // â ����ü�� ���� ���� �ǰ� ����Ʈ
                    Vector3 offset = new Vector3(0f, 0f, 0f);
                    GameObject MHitEffect = Instantiate(MHitEffectPrefab, transform.position + offset, Quaternion.identity, Parent.transform.parent);
                    StartCoroutine(DestroyMHitEffect(MHitEffect));

                    // ����ü�� �浹�� ���� ����
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
        // ���� �ִϸ��̼� ��� �ð� ��ŭ ���
        yield return new WaitForSeconds(2.75f);

        // ���� �ִϸ��̼� �� �ݶ��̴� �ı�
        Destroy(explosion);
    }

    IEnumerator DestroyMHitEffect(GameObject MHitEffect)
    {
        // �ִϸ��̼� ��� �ð� ��ŭ ���
        yield return new WaitForSeconds(0.417f);

        // �ִϸ��̼� �� �ݶ��̴� �ı�
        Destroy(MHitEffect);
    }
}

