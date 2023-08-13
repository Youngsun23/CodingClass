using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public float currentHP;


    private float currentRotate = 0;
    private float buffedSpeedRate = 1;



    private TriggerGate currentGate;


    private void Update()
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
