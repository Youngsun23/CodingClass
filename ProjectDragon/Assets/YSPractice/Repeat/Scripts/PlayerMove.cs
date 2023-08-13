
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float movespeed;
    public float rotationspeed;

    private float currentrotation=0;

    private void Update()
    {
        // 이동
        Vector3 movement = Vector3.zero;

        if(Input.GetKey(KeyCode.W))
        {
            movement.x -= 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement.z -= 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movement.x += 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movement.z += 1;
        }

        transform.Translate(movement*movespeed*Time.deltaTime,Space.Self);

        // 회전
        if (Input.GetKey(KeyCode.Q))
        {
            currentrotation -= rotationspeed * Time.deltaTime;
        }


        if (Input.GetKey(KeyCode.E))
        {
            currentrotation += rotationspeed * Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(0, currentrotation, 0);

        // 점프
        if (Input.GetKey(KeyCode.Space))
        {
            movement.y += 1;
        }
    }
}
