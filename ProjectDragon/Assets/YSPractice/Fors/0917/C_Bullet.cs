using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Bullet : MonoBehaviour
{
    public float speed;
    public float range;
    public float lifeTime;
    public float gravity;

    private Vector3 startPosition;
    private float startTime;

    private void Start()
    {
        startPosition = transform.position;
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time > startTime + lifeTime)
        {
            Destroy(gameObject);
            return;
        }

        if (Vector3.Distance(startPosition, transform.position) > range)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 movement = transform.position + (transform.forward * speed * Time.deltaTime);
        movement.y += gravity * Time.deltaTime;

        transform.position = movement;
    }
}
