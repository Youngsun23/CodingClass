using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private float bulletSpeed=30f;
    private Rigidbody rb;
    public GameObject shooter;
    private float startTime;
    private float bulletMoveDistance;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startTime=Time.time;
    }

    private void Update()
    {
        Vector3 moveDirection = shooter.transform.forward;
        Vector3 gravity=new Vector3(0,-0.01f,0);
        rb.velocity = moveDirection * bulletSpeed+gravity;
        bulletMoveDistance = bulletSpeed * (Time.time - startTime);
        Debug.Log(transform.position);

        if (Time.time - startTime >= 5f)
        {
            Destroy(gameObject);
            Debug.Log("Timeout");
        }

        if (bulletMoveDistance >= 100f)
        {
            Destroy(gameObject);
            Debug.Log("Distance");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Debug.Log("Collision");
    }



}