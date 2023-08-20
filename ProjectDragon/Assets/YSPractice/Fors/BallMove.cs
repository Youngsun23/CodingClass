using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    public float moveSpeed;
    public int score;
    public int health;

    private void Start()
    {
        health = 3;
    }

    private void Update()
    {
        Vector3 position = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            position.z += 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            position.x -= 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            position.z -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            position.x += 1;
        }

        transform.Translate(position * moveSpeed * Time.deltaTime, Space.Self);
    }

        private void OnTriggerEnter(Collider trigger)
        { 
            if(trigger.gameObject.CompareTag("Threat"))
            {
                health--;
                Debug.Log("Current Health: "+health);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Target"))
            {
                score ++;
                Debug.Log("Current Score: "+score);
            }

            int pathLayer = LayerMask.NameToLayer("Path");
            if (collision.gameObject.layer == pathLayer)
            {
                Debug.Log("You are on the way");
            }
    }

        private void OnCollisionExit(Collision collision)
        {
            int pathLayer = LayerMask.NameToLayer("Path");
            if(collision.gameObject.layer==pathLayer)
            {
                Debug.Log("You are out of the way");
            }
        }


    }
