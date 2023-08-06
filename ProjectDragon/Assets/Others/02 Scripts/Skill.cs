using DG.Tweening.Core.Easing;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float moveSpeed = 15f;

    private Transform target;

    void Start()
    {
        // �÷��̾ ã�Ƽ� Ÿ������ ����
        target = GameObject.FindGameObjectWithTag("Player").transform;

        // Ÿ�� ������ ���� �߻�ü ȸ��
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }

    void Update()
    {
        // �߻�ü�̵�
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
