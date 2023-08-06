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
    
    // ����� �ҽ� ������Ʈ�� �ν����Ϳ��� �������ݴϴ�.
    public AudioSource MenuOpenSound;
    public AudioSource MenuCloseSound;
    public AudioSource PierceSound;
    public AudioSource ThrowSound;
    public AudioSource DamagedSound;

    private WaveManager waveManager; // WaveManager �ν��Ͻ��� ������ ����
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
        // WaveManager�� �ν��Ͻ��� ������
        waveManager = FindObjectOfType<WaveManager>();

        PauseMenuUI.gameObject.SetActive(false);
        ClearMenuUI.gameObject.SetActive(false);
        OverMenuUI.gameObject.SetActive(false);

        // ���� �� 0�� �ƴ� ������ �ʱ�ȭ -> ���� �� game clear ����
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
        if (waveManager == null) return; // Null üũ �߰�
        monsterCount = waveManager.GetMonsterCount(); // ���� �� �Ҵ�
        monsterCountTxt.text = monsterCount.ToString(); // ���� �� �ؽ�Ʈ ������Ʈ
        CurrentScoreTxt.text = "Score: " + waveManager.score.ToString(); // ���� �÷��� �� ���ھ� ������Ʈ
        StageTxt.text = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name; // �� �̸� �ؽ�Ʈ ������Ʈ
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
                // ���� ����
                
                
                    spriteRenderer.enabled = !spriteRenderer.enabled;
                

            }

            UpdateHealthUI();
        }

        // ESC Ű �Է� ó��
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

       

        // ���� ī��Ʈ�� 0 ������ ��� ó��
        
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
        // PlayerHit ����Ʈ
        Vector3 offset = new Vector3(0f, 1.0f, 0f); // Y ������ 1.0��ŭ �̵�
        GameObject PHItEffect = Instantiate(PHitEffectPrefab, transform.position + offset, Quaternion.identity);
        PlayerHitEffect.SetTrigger("PlayerHit");
        StartCoroutine(DestroyPHitEffect(PHItEffect));
        DamagedSound.Play();

        currentHP -= damage;
        // HP�� 0 ���Ϸ� ������ ��� ó�� (��: ���� ����)
        if (currentHP <= 0 && !isDieTriggered)
        {
            isDieTriggered = true;
            playerAnimator.SetTrigger("Die");
            // ��� �ִϸ��̼� ���� �� ���� ���� UI �߰� �߰� ó�� �ʿ�
            StartCoroutine(GameOverCoroutine());
        }
        else
        {
            playerAnimator.SetTrigger("Damaged");
        }
    }

    IEnumerator DestroyPHitEffect(GameObject PHitEffect)
    {
        // �ִϸ��̼� ��� �ð� ��ŭ ���
        yield return new WaitForSeconds(0.417f);

        // �ִϸ��̼� �� �ݶ��̴� �ı�
        Destroy(PHitEffect);
    }


    private IEnumerator GameOverCoroutine()
    {
        // �ִϸ��̼� ���̸�ŭ ���
        yield return new WaitForSeconds(1.2f);

        // ���� ���� ó��
        GameOver();
    } // ���⿡ ���߿� �� �Է� �� �޴� �� ������ �� ��? ���� ���� �ʿ��� ���� (���� ���¿��� Ű�Է�->�̵� ����)

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
        //// Rigidbody2D�� ��Ȱ��ȭ�Ͽ� ���� �������� ����
        //movejump.rb.isKinematic = true;

        // �÷��̾� ��Ʈ�� ��ũ��Ʈ�� ��������Ʈ �¿���ȯ ����
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

        // �Է� Ȱ��ȭ
        EnablePlayerInput();
        EnableSpearInput();
    }

    private void EnablePlayerInput()
    {
        //// Rigidbody2D�� �ٽ� Ȱ��ȭ�Ͽ� ���� �������� �簳
        //movejump.rb.isKinematic = false;

        playermanager.canMove = true;
    }

    private void EnableSpearInput()
    {
        spear.canInput = true;
    }

    public void RestartGame()
    {
        // GameReset(); // ���� �ʱ�ȭ �Լ� ���� �ʿ�?
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
        // GameReset(); // ���� �ʱ�ȭ �Լ� ���� �ʿ�?
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