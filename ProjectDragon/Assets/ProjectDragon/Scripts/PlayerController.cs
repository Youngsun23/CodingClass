using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;

    private float currentRotate = 0;

    private void Update()
    {
        Vector3 movement = Vector3.zero; // New Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            movement.z += 1; // movement = movement + 1;
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

        // Vector3.forward;  // (0, 0, 1)
        // Vector3.left;     // (-1, 0, 0)
        // Vector3.right;    // (1, 0, 0)
        // Vector3.back;     // (0, 0, -1)
        // Vector3.up;       // (0, 1, 0)
        // Vector3.down;     // (0, -1, 0)

        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);
        //transform.position += movement * moveSpeed * Time.deltaTime;

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
