using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaning : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Bullet_player") || gameObject.CompareTag("Bullet_enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
