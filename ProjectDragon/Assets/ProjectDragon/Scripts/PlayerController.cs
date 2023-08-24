using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position + Vector3.up, transform.forward + Vector3.up);
        //Gizmos.DrawWireSphere(transform.position, 10f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);        
    }

    public float moveSpeed;
    public float rotationSpeed;
    public float currentHP;

    public float raycastRange;
    public int raycastAccuracy = 1;
    public float detectionRadius;
    public List<GameObject> detectedObjectList = new List<GameObject>();

    private float currentRotate = 0;
    private float buffedSpeedRate = 1;
    private TriggerGate currentGate;

    private void Update()
    {
        Update_Movement();

        detectedObjectList.Clear();

        #region #1. Raycast & DrawLine
        //Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
        //Debug.DrawLine(ray.origin, ray.origin + (ray.direction) * raycastRange, Color.red);
        //if (Physics.Raycast(ray, out RaycastHit hitInfo, raycastRange))
        //{
        //    Debug.Log($"Front Object Name : {hitInfo.collider.name}", hitInfo.transform.gameObject);
        //}
        #endregion

        #region #2. 360' Raycast
        //if (raycastAccuracy <= 0)
        //{
        //    raycastAccuracy = 1;
        //}

        //for (int i = 0; i < 360; i += raycastAccuracy)
        //{
        //    Quaternion rotation = Quaternion.Euler(0, i, 0);
        //    Vector3 direction = rotation * transform.forward;

        //    Ray circleRay = new Ray(transform.position + Vector3.up, direction);
        //    Debug.DrawLine(circleRay.origin, circleRay.origin + (circleRay.direction) * raycastRange, Color.blue);

        //    if (Physics.Raycast(circleRay, out RaycastHit hitInfo, raycastRange))
        //    {
        //        if (false == detectedObjectList.Contains(hitInfo.transform.gameObject))
        //        {
        //            detectedObjectList.Add(hitInfo.transform.gameObject);
        //        }
        //    }
        //}
        #endregion

        #region #3. Physics.Overlap Example
        //Collider[] detectedColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        //for (int i = 0; i < detectedColliders.Length; i++)
        //{
        //    detectedObjectList.Add(detectedColliders[i].gameObject);
            
        //}
        #endregion


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

        transform.Translate(movement * moveSpeed * buffedSpeedRate * Time.deltaTime, Space.Self);

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

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    if (collision.gameObject.TryGetComponent(out Ground ground))
        //    {
        //        buffedSpeedRate = ground.speedRate;
        //    }
        //}

        int groundLayer = LayerMask.NameToLayer("Ground");
        if (collision.gameObject.layer == groundLayer)
        {
            if (collision.gameObject.TryGetComponent(out Ground ground))
            {
                buffedSpeedRate = ground.speedRate;
            }
        }
    }
    private void OnCollisionStay(Collision collision) { }
    private void OnCollisionExit(Collision collision) { }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gate"))
        {
            if (other.gameObject.TryGetComponent(out TriggerGate gate))
            {
                if (currentGate != null && currentGate == gate)
                    return;

                currentGate = gate;
                currentHP -= gate.damage;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("SpawnerField"))
        {
            if (other.gameObject.TryGetComponent(out SpawnerField spawnerField))
            {
                spawnerField.Spawn();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Gate"))
        {
            if (other.gameObject.TryGetComponent(out TriggerGate gate))
            {
                if (gate == currentGate)
                {
                    currentGate = null;
                }
            }
        }
    }
}
