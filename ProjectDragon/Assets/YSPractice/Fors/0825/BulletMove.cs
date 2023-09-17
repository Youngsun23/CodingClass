using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private float bulletSpeed = 30f;
    private Rigidbody rb;
    public GameObject shooter;
    private float startTime;
    private float bulletMoveDistance;

    private ShooterMove shooterMove;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startTime = Time.time;
        shooterMove = GetComponent<ShooterMove>();
    }
    private void Update()
    {
        Vector3 rotationVector = Quaternion.Euler(0f, shooterMove.shootAngle, 0f) * Vector3.forward;
        // Vector3 moveDirection = shooter.transform.forward+shooterMove.upMove+shooterMove.downMove;
        Vector3 moveDirection = shooter.transform.forward + rotationVector;
        /* (Quaternion.Euler(0f,shooterMove.shootAngle,0f))*/
        Vector3 gravity = new Vector3(0, -0.01f, 0);
        rb.velocity = moveDirection * bulletSpeed + gravity;
        bulletMoveDistance = bulletSpeed * (Time.time - startTime);
        Debug.Log(transform.position);

        if (Time.time - startTime >= 5f)
        {
            Destroy(gameObject);
            Debug.Log("Timeout"); //destroy ½ÃÄ×´Âµ¥ debug.log ¿Ö ¶ßÁö¤¾ »ìÂ¦ ½Ã°£ °É¸®³ª
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