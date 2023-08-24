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

    private void Update()
    {
        Update_Movement();

        if (Input.GetKey(KeyCode.Space))
            {
                Shoot();
            }

    }


    private void Shoot()
    {
        if (Time.time < currentSpawnTime + spawnCoolTime)
            return;
        currentSpawnTime = Time.time;

        var newInstance = Instantiate(bullet);
        newInstance.transform.position = spawnPoint.position;
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
