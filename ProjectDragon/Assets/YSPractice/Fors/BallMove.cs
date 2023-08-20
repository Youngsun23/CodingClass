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

        // Ray 를 캐릭터의 아래 방향으로 쏴서.
        // 레이가 충돌 된 오브젝트의 Layer 값을 기억해두고.
        // 기억해둔 layer 값과 새로 이번 프레임에 레이를 쏴서 얻은 Layer 값과 다르면. 다른 지형에 올라온 것으로 판단할 수 있음
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
