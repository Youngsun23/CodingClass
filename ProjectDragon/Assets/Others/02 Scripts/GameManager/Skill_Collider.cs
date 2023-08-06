using UnityEngine;

public class Skill_Collider : MonoBehaviour
{
    private WaveManager waveManager;

    private void Awake()
    {
        waveManager = FindObjectOfType<WaveManager>();
    }
    private void Start()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            waveManager.UpdateWaveStatus();
            waveManager.IncreaseScore(100);
        }
    }
}