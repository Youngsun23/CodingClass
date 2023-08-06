using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 15f;    // 이동 속도
    public float jumpForce = 15f;    // 점프 힘
    private bool isJumping = false; // 점프 중인지 여부를 저장하는 변수
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
        float moveDirection = Input.GetAxis("Horizontal"); // 수평 이동 입력을 받습니다.
        //rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y); // 수평 방향으로 이동합니다.

        bool isMoving = Mathf.Abs(moveDirection) > 0;

        playerAnimator.SetBool("isMoving", isMoving);

        if (Input.GetButtonDown("Jump") && !isJumping) // 점프 버튼을 누르고 아직 점프 중이 아닌 경우
        {
            playerAnimator.SetBool("isJumping",true);
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); // 점프 힘을 가합니다.
            isJumping = true; // 점프 중임을 표시합니다.

            GameObject JumpEffect = Instantiate(JumpEffectPrefab, transform.position, Quaternion.identity, Parent.transform.parent);

            playerEffect.SetTrigger("Jump");

            StartCoroutine(DestroyJumpEffect(JumpEffect));


        }

        if(canMove)
        {
            UpdateFacingDirection(); // 캐릭터의 보는 방향을 업데이트합니다.
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || (collision.gameObject.CompareTag("Platform"))) // "Ground" 태그를 가진 게임 오브젝트에 닿은 경우
        {
            playerAnimator.SetBool("isJumping",false);
            isJumping = false; // 점프 상태를 false로 변경합니다.

        }
    }

    private void UpdateFacingDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float horizontalDistance = mousePosition.x - transform.position.x;

        if (horizontalDistance > 0) // 마우스 커서가 캐릭터의 오른쪽에 있는 경우
        {
            spriteRenderer.flipX = false; // 좌우 반전을 해제합니다.
        }
        else if (horizontalDistance < 0) // 마우스 커서가 캐릭터의 왼쪽에 있는 경우
        {
            spriteRenderer.flipX = true; // 좌우 반전을 적용합니다.
        }
    }

    IEnumerator DestroyJumpEffect(GameObject JumpEffect)
    {
        // 애니메이션 재생 시간 만큼 대기
        yield return new WaitForSeconds(0.333f);

        // 애니메이션 및 콜라이더 파괴
        Destroy(JumpEffect);
    }

}