using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterMove : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    private float currentRotate = 0;

    public GameObject bullet;
    public Transform spawnPoint;
    private float currentSpawnTime = 0;
    public float spawnCoolTime;

    // public Vector3 upMove;
    // public Vector3 downMove;
     public float shootAngle=10f; // unity에서 각도 개념 ?

    private void Update()
    {
        Update_Movement();

        if (Input.GetKey(KeyCode.Space))
            {
                Shoot();
            }

        if (Input.GetKey(KeyCode.R))
        {
            // upMove =new Vector3(0, 0.1f, 0);
            // transform.rotation=Quaternion.Euler(0,0.1f,0);
            shootAngle = 10f;
        }

        if (Input.GetKey(KeyCode.F))
        {
            // downMove = new Vector3(0, -0.1f, 0);
            // transform.rotation=Quaternion.Euler(0,-0.1f,0);
            shootAngle = -10f;
        }
    }


    private void Shoot()
    {
        if (Time.time < currentSpawnTime + spawnCoolTime)
            return;
        currentSpawnTime = Time.time;

        var newInstance = Instantiate(bullet);
        newInstance.transform.position = spawnPoint.position;
        // var newInstance=Instantiate(bullet, spawnPoint.position, Quaternion.??);
        newInstance.SetActive(true);
        
    }



    private void Update_Movement()
    {
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movement.z += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movement.z -= 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement.x -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movement.x += 1;
        }

        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);

        if (Input.GetKey(KeyCode.Q))
        {
            currentRotate -= rotationSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.E))
        {
            currentRotate += rotationSpeed * Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(0, currentRotate, 0);
    }
}
