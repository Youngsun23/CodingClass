using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 15f;    // �̵� �ӵ�
    public float jumpForce = 15f;    // ���� ��
    private bool isJumping = false; // ���� ������ ���θ� �����ϴ� ����
    public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private GameObject Parent;
    public GameObject JumpEffectPrefab;
    public Animator playerEffect;

    public bool canMove = true;

    public Animator playerAnimator;



    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerEffect = JumpEffectPrefab.GetComponent<Animator>();
        Parent = GameObject.Find("WaveManager");
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float moveDirection = Input.GetAxis("Horizontal"); // ���� �̵� �Է��� �޽��ϴ�.
        //rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y); // ���� �������� �̵��մϴ�.

        bool isMoving = Mathf.Abs(moveDirection) > 0;

        playerAnimator.SetBool("isMoving", isMoving);

        if (Input.GetButtonDown("Jump") && !isJumping) // ���� ��ư�� ������ ���� ���� ���� �ƴ� ���
        {
            playerAnimator.SetBool("isJumping",true);
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); // ���� ���� ���մϴ�.
            isJumping = true; // ���� ������ ǥ���մϴ�.

            GameObject JumpEffect = Instantiate(JumpEffectPrefab, transform.position, Quaternion.identity, Parent.transform.parent);

            playerEffect.SetTrigger("Jump");

            StartCoroutine(DestroyJumpEffect(JumpEffect));


        }

        if(canMove)
        {
            UpdateFacingDirection(); // ĳ������ ���� ������ ������Ʈ�մϴ�.
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || (collision.gameObject.CompareTag("Platform"))) // "Ground" �±׸� ���� ���� ������Ʈ�� ���� ���
        {
            playerAnimator.SetBool("isJumping",false);
            isJumping = false; // ���� ���¸� false�� �����մϴ�.

        }
    }

    private void UpdateFacingDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float horizontalDistance = mousePosition.x - transform.position.x;

        if (horizontalDistance > 0) // ���콺 Ŀ���� ĳ������ �����ʿ� �ִ� ���
        {
            spriteRenderer.flipX = false; // �¿� ������ �����մϴ�.
        }
        else if (horizontalDistance < 0) // ���콺 Ŀ���� ĳ������ ���ʿ� �ִ� ���
        {
            spriteRenderer.flipX = true; // �¿� ������ �����մϴ�.
        }
    }

    IEnumerator DestroyJumpEffect(GameObject JumpEffect)
    {
        // �ִϸ��̼� ��� �ð� ��ŭ ���
        yield return new WaitForSeconds(0.333f);

        // �ִϸ��̼� �� �ݶ��̴� �ı�
        Destroy(JumpEffect);
    }

}