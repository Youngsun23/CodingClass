using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRotation : MonoBehaviour
{
    public float rotationSpeed = 90f;

    private void Update()
    {
        // 'A' 키를 누르면 카메라를 왼쪽으로 회전
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        // 'D' 키를 누르면 카메라를 오른쪽으로 회전
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }
    }
}