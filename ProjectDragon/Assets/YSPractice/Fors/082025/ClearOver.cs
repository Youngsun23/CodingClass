using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearOver : MonoBehaviour
{
    private BallMove ballmove;
    public GameObject clearMenu;
    public GameObject overMenu;
    private int currentHealth;
    private int currentScore;

    private void Start()
    {
        Time.timeScale = 1f;
        ballmove = FindObjectOfType<BallMove>();
        clearMenu.gameObject.SetActive(false);
        overMenu.gameObject.SetActive(false);
    }

    private void Update()
    {
        currentHealth = ballmove.health;
        currentScore = ballmove.score;
        if (currentHealth<=0)
        {
            Time.timeScale = 0f;
            overMenu.gameObject.SetActive(true);
        }

        if(currentScore>=1)
        {
            Time.timeScale = 0f;
            clearMenu.gameObject.SetActive(true);
        }
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene("For0820");
    }
}
