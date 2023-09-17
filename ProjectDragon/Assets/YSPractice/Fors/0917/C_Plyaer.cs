using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class C_Plyaer : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public float currentHP;
    public float maxHP;
    public Image hpBar;

    private float currentRotate = 0;

    public Transform firePosition;
    public GameObject bulletPrefab;
    public float fireRate;
    public float angleSpeed;

    private float fireAngle;
    private float lastFireTime;

    public int maxBullet;
    public int currentBullet;
    public TextMeshProUGUI bulletTxt;

    private void Start()
    {
        currentHP = maxHP;
        hpBar.fillAmount = 1;
        currentBullet = maxBullet;
        bulletTxt.text = "100 / 100";
    }

    private void Update()
    {
        Update_Movement();
        Update_FireControl();
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Threat")
        {
            Update_PlayerHP();
        }
    }

    private void Update_PlayerHP()
    {
        currentHP -= 10;
        hpBar.fillAmount = currentHP / maxHP;
    }

    private void Update_bulletCount()
    {
        bulletTxt.text = currentBullet.ToString() + " / 100";
    }


    private void Update_FireControl()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if ((Time.time > lastFireTime + fireRate) && currentBullet>0)
            {
                var newBullet = Instantiate(bulletPrefab);
                newBullet.transform.position = firePosition.position;
                newBullet.transform.rotation = firePosition.rotation;
                newBullet.gameObject.SetActive(true);

                currentBullet -= 1;
                Update_bulletCount();

                lastFireTime = Time.time;
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            fireAngle -= angleSpeed * Time.deltaTime;
            firePosition.localRotation = Quaternion.Euler(fireAngle, 0, 0);
        }

        if (Input.GetKey(KeyCode.F))
        {
            fireAngle += angleSpeed * Time.deltaTime;
            firePosition.localRotation = Quaternion.Euler(fireAngle, 0, 0);
        }
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

        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);

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
