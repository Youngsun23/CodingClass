using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using Unity.VisualScripting;
using System;

public class PlayerHealthPlusUI : MonoBehaviour
{
    private Animator playerAnimator;
    public Animator PlayerHitEffect;
    public GameObject PHitEffectPrefab;
    
    // 오디오 소스 컴포넌트를 인스펙터에서 연결해줍니다.
    public AudioSource MenuOpenSound;
    public AudioSource MenuCloseSound;
    public AudioSource PierceSound;
    public AudioSource ThrowSound;
    public AudioSource DamagedSound;

    private WaveManager waveManager; // WaveManager 인스턴스를 저장할 변수
    public PlayerManager playermanager;
    public Spear spear;

    public int maxHP = 5;
    public int currentHP;
    public int monsterCount;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private bool isInvincible = false;
    private float invincibleTimer = 0f;
    public float invincibleDuration = 1f;
    private SpriteRenderer spriteRenderer;

    public GameObject PauseMenuUI;
    public GameObject ClearMenuUI;
    public GameObject OverMenuUI;
    public TMP_Text monsterCountTxt;
    public TMP_Text StageTxt;
    public TMP_Text ScoreTxt;
    public TMP_Text CurrentScoreTxt;

    private bool GameIsPaused = false;
    private bool isGameEnd = false;
    private bool isDieTriggered = false;



    private void Start()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        // WaveManager의 인스턴스를 가져옴
        waveManager = FindObjectOfType<WaveManager>();

        PauseMenuUI.gameObject.SetActive(false);
        ClearMenuUI.gameObject.SetActive(false);
        OverMenuUI.gameObject.SetActive(false);

        // 몬스터 수 0이 아닌 값으로 초기화 -> 시작 시 game clear 방지
        monsterCount = 12;
        Time.timeScale = 1f;
        GameIsPaused = false;

        EnablePlayerInput();
        EnableSpearInput();

        playerAnimator = GetComponent<Animator>();
        //spear = GetComponentInChildren<Spear>();
        PlayerHitEffect = PHitEffectPrefab.GetComponent<Animator>();

        spear.OnSpearStateChangedCallback += OnChangedSpearState;
    }

    private void OnChangedSpearState(Spear.SpearState state)
    {
        switch (state)
        {
            case Spear.SpearState.Idle:
                break;
            case Spear.SpearState.Pierce:
                {
                    playerAnimator.SetTrigger("isPiercing");
                    PierceSound.Play();
                }
                break;
            case Spear.SpearState.Throw:
                {
                    playerAnimator.SetTrigger("isThrowing");
                    ThrowSound.Play();
                }
                break;
        }
    }

    private void LateUpdate()
    {
        if (waveManager == null) return; // Null 체크 추가
        monsterCount = waveManager.GetMonsterCount(); // 몬스터 수 할당
        monsterCountTxt.text = monsterCount.ToString(); // 몬스터 수 텍스트 업데이트
        CurrentScoreTxt.text = "Score: " + waveManager.score.ToString(); // 게임 플레이 중 스코어 업데이트
        StageTxt.text = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name; // 씬 이름 텍스트 업데이트
    }

    private void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;

            if (invincibleTimer <= 0f)
            {
                isInvincible = false;
                spriteRenderer.enabled = true;
            }
            else if (!isGameEnd)
            {
                // 점멸 로직
                
                
                    spriteRenderer.enabled = !spriteRenderer.enabled;
                

            }

            UpdateHealthUI();
        }

        // ESC 키 입력 처리
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

       

        // 몬스터 카운트가 0 이하인 경우 처리
        
        if (monsterCount <= 0)
        {
           GameClear();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage(1);
            StartInvincibility();
        }
        if (collision.CompareTag("Bullet_enemy") && !isInvincible)
        {
            TakeDamage(1);
            StartInvincibility();
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        // PlayerHit 이펙트
        Vector3 offset = new Vector3(0f, 1.0f, 0f); // Y 축으로 1.0만큼 이동
        GameObject PHItEffect = Instantiate(PHitEffectPrefab, transform.position + offset, Quaternion.identity);
        PlayerHitEffect.SetTrigger("PlayerHit");
        StartCoroutine(DestroyPHitEffect(PHItEffect));
        DamagedSound.Play();

        currentHP -= damage;
        // HP가 0 이하로 떨어진 경우 처리 (예: 게임 오버)
        if (currentHP <= 0 && !isDieTriggered)
        {
            isDieTriggered = true;
            playerAnimator.SetTrigger("Die");
            // 사망 애니메이션 종료 후 게임 오버 UI 뜨게 추가 처리 필요
            StartCoroutine(GameOverCoroutine());
        }
        else
        {
            playerAnimator.SetTrigger("Damaged");
        }
    }

    IEnumerator DestroyPHitEffect(GameObject PHitEffect)
    {
        // 애니메이션 재생 시간 만큼 대기
        yield return new WaitForSeconds(0.417f);

        // 애니메이션 및 콜라이더 파괴
        Destroy(PHitEffect);
    }


    private IEnumerator GameOverCoroutine()
    {
        // 애니메이션 길이만큼 대기
        yield return new WaitForSeconds(1.2f);

        // 게임 오버 처리
        GameOver();
    } // 여기에 나중에 그 입력 안 받는 거 넣으면 될 듯? 게임 오버 쪽에서 빼고 (죽은 상태에서 키입력->이동 막게)

    private void StartInvincibility()
    {
        isInvincible = true;
        
        invincibleTimer = invincibleDuration;
        spriteRenderer.enabled = false;
    }

    private void GameOver()
    {
        OverMenuUI.gameObject.SetActive(true);
        MenuOpenSound.Play();
        Time.timeScale = 0f;
        GameIsPaused = true;
        isGameEnd = true;
        spriteRenderer.enabled = true;
        DisablePlayerInput();
        DisableSpearInput();

    }

    private void GameClear()
    {
        if (isGameEnd)
        {
            return;
        }
        ClearMenuUI.gameObject.SetActive(true);
        MenuOpenSound.Play();
        Time.timeScale = 0f;
        GameIsPaused = true;
        isGameEnd = true;
        // spriteRenderer.enabled = true;
        DisablePlayerInput();
        DisableSpearInput();

        ScoreTxt.text = "Score: " + waveManager.score.ToString();
    }

    private void PauseGame()
    {
        PauseMenuUI.SetActive(true);
        MenuOpenSound.Play();
        Time.timeScale = 0f;
        GameIsPaused = true;

        DisablePlayerInput();
        DisableSpearInput();
    }

    private void DisablePlayerInput()
    {
        //// Rigidbody2D를 비활성화하여 물리 움직임을 중지
        //movejump.rb.isKinematic = true;

        // 플레이어 컨트롤 스크립트의 스프라이트 좌우전환 막음
        playermanager.canMove = false;
    }

    private void DisableSpearInput()
    {
        spear.canInput = false;
    }

    public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        MenuCloseSound.Play();
        Time.timeScale = 1f;
        GameIsPaused = false;

        // 입력 활성화
        EnablePlayerInput();
        EnableSpearInput();
    }

    private void EnablePlayerInput()
    {
        //// Rigidbody2D를 다시 활성화하여 물리 움직임을 재개
        //movejump.rb.isKinematic = false;

        playermanager.canMove = true;
    }

    private void EnableSpearInput()
    {
        spear.canInput = true;
    }

    public void RestartGame()
    {
        // GameReset(); // 게임 초기화 함수 정의 필요?
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PauseMenuUI.SetActive(false);
        ClearMenuUI.SetActive(false);
        OverMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        EnablePlayerInput();
        EnableSpearInput();
    }

    public void LoadLobby()
    {
        // GameReset(); // 게임 초기화 함수 정의 필요?
        SceneManager.LoadScene("LobbyScene");
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHP)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < maxHP)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }




}