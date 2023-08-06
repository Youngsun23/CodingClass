using UnityEngine;

public class straightFire : MonoBehaviour
{
    public float moveSpeed = 10f; // �̵� �ӵ��� �����ϱ� ���� ����
    public float lifeTime = 10f; // ������Ʈ�� ���� �ð�

    void Start()
    {
        // ������Ʈ ���� �� lifeTime ������ ������ �ð��� ������ �ڵ����� �����˴ϴ�.
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // ������Ʈ�� ���� ��ǥ�迡���� ���� ������ �̵� �������� ����մϴ�.
        Vector3 moveDirection = transform.up;

        // ������Ʈ�� ��ġ�� �̵� �������� �̵���ŵ�ϴ�.
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // ������Ʈ�� �׻� ������ ���ϵ��� ������ ������ �ٶ󺸵��� ȸ����ŵ�ϴ�.
        LookAtTarget(Vector3.zero);
    }

    void LookAtTarget(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - transform.position;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = rotation;
    }
}