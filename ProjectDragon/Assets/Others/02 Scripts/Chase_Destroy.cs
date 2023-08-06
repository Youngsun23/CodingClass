using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
