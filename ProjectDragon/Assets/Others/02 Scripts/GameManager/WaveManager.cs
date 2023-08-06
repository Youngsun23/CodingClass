
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject wave1; // Wave_1 오브젝트
    public GameObject wave2; // Wave_2 오브젝트
    public GameObject wave3; // Wave_3 오브젝트
    public GameObject wave4; // Wave_4 오브젝트
    public GameObject wave5; // Wave_5 오브젝트
    public int score = 0;
    public float WaitTime = 2f;
    private bool isGameStopped = false; // 게임 멈춤 상태를 나타내는 플래그
    private float timeSinceLastUpdate = 0f;
    private float updateInterval = 1f; // score 감소 간격

    // UI용
    private int wave1ChildCount;
    private int wave2ChildCount;
    private int wave3ChildCount;
    private int wave4ChildCount;
    private int wave5ChildCount;
    // 임시 combo용
    private ComboManager comboManager;
    private int totalMonsterCount = 0;


    private void Start()
    {
        // Wave_1 오브젝트 활성화
        Invoke("Wave1Start", 1f);
        // Wave_2 오브젝트 비활성화
        wave2.SetActive(false);
        wave3.SetActive(false);
        wave4.SetActive(false);
        wave5.SetActive(false);

        // 임시 Combo용
        comboManager = GetComponent<ComboManager>();
    }

    private void Update()
    {
        if (!isGameStopped)
        {
            // deltaTime마다 score 감소
            timeSinceLastUpdate += Time.deltaTime;
            if (timeSinceLastUpdate >= updateInterval)
            {
                timeSinceLastUpdate = 0f;
                DecreaseScore(1); // 1씩 감소
            }
        }

        // 자식 개체 수를 매 프레임마다 판단
        UpdateWaveStatus();

        // 임시 Combo용
        int currentTotalMonsterCount = GetMonsterCount();
        if(currentTotalMonsterCount != totalMonsterCount)
        {
            totalMonsterCount=currentTotalMonsterCount;
            comboManager.IncreaseCombo();
        }
    }

    public void UpdateWaveStatus()
    {

        // Wave_1 자식 개체 수를 확인
        wave1ChildCount = wave1.transform.childCount;
        wave2ChildCount = wave2.transform.childCount;
        wave3ChildCount = wave3.transform.childCount;
        wave4ChildCount = wave4.transform.childCount;
        wave5ChildCount = wave5.transform.childCount;

        // Wave_1 자식 개체가 모두 사라졌을 때
        if (wave1ChildCount == 0)
        {
            // Wave_2 오브젝트 활성화
            Invoke("Wave2Start", WaitTime);
        }

        // Wave_2 자식 개체가 모두 사라졌을 때
        if (wave2ChildCount == 0)
        {
            Invoke("Wave3Start", WaitTime);
        }

        // 모든 Wave 개체의 자식 개체가 사라졌을 때
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
            // 게임 멈추기
            isGameStopped = true;
            //Time.timeScale = 0f;
        }
    }

    public void IncreaseScore(int value)
    {
        score += value;
        // Debug.Log("내 점수: " + score);
    }

    private void DecreaseScore(int value)
    {
        score -= value;
        // score가 0 이하로 내려가지 않도록 제한
        score = Mathf.Max(0, score);
        // Debug.Log("내 점수: " + score);
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


    // UI용
    public int GetMonsterCount()
    {
        int totalMonsterCount = wave1ChildCount + wave2ChildCount + wave3ChildCount + wave4ChildCount + wave5ChildCount;
        return totalMonsterCount;
    }

    
}
