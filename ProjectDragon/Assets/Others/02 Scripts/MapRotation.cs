using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRotation : MonoBehaviour
{
    public float rotationSpeed = 90f;

    private void Update()
    {
        // 'A' Ű�� ������ ī�޶� �������� ȸ��
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        // 'D' Ű�� ������ ī�޶� ���������� ȸ��
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }
    }
}