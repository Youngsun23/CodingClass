using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    private float currentRotate = 0;
    public int score;
    public int health;

    public float raycastRange;
    private string groundInfo;
    private string pastgroundInfo;
    public float detectionRadius;
    public List<GameObject> detectedObjectList = new List<GameObject>();

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

        if (Input.GetKey(KeyCode.Q))
        {
            currentRotate -= rotationSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.E))
        {
            currentRotate += rotationSpeed * Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(0, currentRotate, 0);

        // Ray 를 캐릭터의 아래 방향으로 쏴서.
        // 레이가 충돌 된 오브젝트의 Layer 값을 기억해두고.
        // 기억해둔 layer 값과 새로 이번 프레임에 레이를 쏴서 얻은 Layer 값과 다르면. 다른 지형에 올라온 것으로 판단할 수 있음

        Ray ray = new Ray(transform.position, -transform.up);
        // Debug.DrawLine(ray.origin, ray.origin + (ray.direction) * raycastRange, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, raycastRange))
        {
            // Debug.Log($"Front Object Name : {hitInfo.collider.name}", hitInfo.transform.gameObject);
            groundInfo = LayerMask.LayerToName(hitInfo.collider.gameObject.layer);
            // Debug.Log(groundInfo);

            if (groundInfo != pastgroundInfo)
            {
                if(groundInfo=="Ground")
                {
                    Debug.Log("You are out of the way");
                }
                else
                {
                    Debug.Log("You are on the way");
                }
            }

                pastgroundInfo = groundInfo;

        }



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

            //int pathLayer = LayerMask.NameToLayer("Path");
            //if (collision.gameObject.layer == pathLayer)
                //{
                //    Debug.Log("You are on the way");
                //}
    }

        private void OnCollisionExit(Collision collision)
        {
            //int pathLayer = LayerMask.NameToLayer("Path");
            //if(collision.gameObject.layer==pathLayer)
            //{
            //    Debug.Log("You are out of the way");
            //}
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);

            Collider[] targets = Physics.OverlapSphere(transform.position, detectionRadius);
            int pathLayer = LayerMask.NameToLayer("Path");
            int groundLayer = LayerMask.NameToLayer("Ground");
            foreach (Collider target in targets)
            {
                if(target.gameObject.layer != pathLayer && target.gameObject.layer != groundLayer)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(transform.position, target.transform.position);
                }

            }
            
        }

}
