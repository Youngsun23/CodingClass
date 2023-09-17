using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController09 : MonoBehaviour
{
    public float movespeed;
    public GameObject bullet;

    // -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- //

    private void Update()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        Vector3 move=new Vector3(0,0,0);

        if(Input.GetKey(KeyCode.W))
        {
            move.z += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            move.z -= 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            move.x -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            move.x += 1;
        }

        // transform.Translate(Vector3 translation, Space relativeTo)
        // Rigidbody.AddForce(Vector3 force, ForceMode mode)
        // Rigidbody.MovePosition(Vector3 position)

        transform.Translate(move * movespeed * Time.deltaTime);
    }

    private void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet);
        }
    }


}
