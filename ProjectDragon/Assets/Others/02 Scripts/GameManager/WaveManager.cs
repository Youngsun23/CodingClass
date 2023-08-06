
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject wave1; // Wave_1 ������Ʈ
    public GameObject wave2; // Wave_2 ������Ʈ
    public GameObject wave3; // Wave_3 ������Ʈ
    public GameObject wave4; // Wave_4 ������Ʈ
    public GameObject wave5; // Wave_5 ������Ʈ
    public int score = 0;
    public float WaitTime = 2f;
    private bool isGameStopped = false; // ���� ���� ���¸� ��Ÿ���� �÷���
    private float timeSinceLastUpdate = 0f;
    private float updateInterval = 1f; // score ���� ����

    // UI��
    private int wave1ChildCount;
    private int wave2ChildCount;
    private int wave3ChildCount;
    private int wave4ChildCount;
    private int wave5ChildCount;
    // �ӽ� combo��
    private ComboManager comboManager;
    private int totalMonsterCount = 0;


    private void Start()
    {
        // Wave_1 ������Ʈ Ȱ��ȭ
        Invoke("Wave1Start", 1f);
        // Wave_2 ������Ʈ ��Ȱ��ȭ
        wave2.SetActive(false);
        wave3.SetActive(false);
        wave4.SetActive(false);
        wave5.SetActive(false);

        // �ӽ� Combo��
        comboManager = GetComponent<ComboManager>();
    }

    private void Update()
    {
        if (!isGameStopped)
        {
            // deltaTime���� score ����
            timeSinceLastUpdate += Time.deltaTime;
            if (timeSinceLastUpdate >= updateInterval)
            {
                timeSinceLastUpdate = 0f;
                DecreaseScore(1); // 1�� ����
            }
        }

        // �ڽ� ��ü ���� �� �����Ӹ��� �Ǵ�
        UpdateWaveStatus();

        // �ӽ� Combo��
        int currentTotalMonsterCount = GetMonsterCount();
        if(currentTotalMonsterCount != totalMonsterCount)
        {
            totalMonsterCount=currentTotalMonsterCount;
            comboManager.IncreaseCombo();
        }
    }

    public void UpdateWaveStatus()
    {

        // Wave_1 �ڽ� ��ü ���� Ȯ��
        wave1ChildCount = wave1.transform.childCount;
        wave2ChildCount = wave2.transform.childCount;
        wave3ChildCount = wave3.transform.childCount;
        wave4ChildCount = wave4.transform.childCount;
        wave5ChildCount = wave5.transform.childCount;

        // Wave_1 �ڽ� ��ü�� ��� ������� ��
        if (wave1ChildCount == 0)
        {
            // Wave_2 ������Ʈ Ȱ��ȭ
            Invoke("Wave2Start", WaitTime);
        }

        // Wave_2 �ڽ� ��ü�� ��� ������� ��
        if (wave2ChildCount == 0)
        {
            Invoke("Wave3Start", WaitTime);
        }

        // ��� Wave ��ü�� �ڽ� ��ü�� ������� ��
        if (wave3ChildCount == 0)
        {
            Invoke("Wave4Start", WaitTime);
        }

        if (wave4ChildCount == 0)
        {
            Invoke("Wave5Start", WaitTime);
        }

        if (wave4ChildCount == 0)
        {
            // ���� ���߱�
            isGameStopped = true;
            //Time.timeScale = 0f;
        }
    }

    public void IncreaseScore(int value)
    {
        score += value;
        // Debug.Log("�� ����: " + score);
    }

    private void DecreaseScore(int value)
    {
        score -= value;
        // score�� 0 ���Ϸ� �������� �ʵ��� ����
        score = Mathf.Max(0, score);
        // Debug.Log("�� ����: " + score);
    }
    private void Wave1Start()
    {
        wave1.SetActive(true);
    }
    private void Wave2Start()
    {
        wave2.SetActive(true);
    }
    private void Wave3Start()
    {
        wave3.SetActive(true);
    }
    private void Wave4Start()
    {
        wave4.SetActive(true);
    }
    private void Wave5Start()
    {
        wave5.SetActive(true);
    }


    // UI��
    public int GetMonsterCount()
    {
        int totalMonsterCount = wave1ChildCount + wave2ChildCount + wave3ChildCount + wave4ChildCount + wave5ChildCount;
        return totalMonsterCount;
    }

    
}
