using UnityEngine;

public class straightFire : MonoBehaviour
{
    public float moveSpeed = 10f; // 이동 속도를 조정하기 위한 변수
    public float lifeTime = 10f; // 오브젝트의 생존 시간

    void Start()
    {
        // 오브젝트 생성 후 lifeTime 변수에 지정된 시간이 지나면 자동으로 삭제됩니다.
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // 오브젝트의 로컬 좌표계에서의 위쪽 방향을 이동 방향으로 사용합니다.
        Vector3 moveDirection = transform.up;

        // 오브젝트의 위치를 이동 방향으로 이동시킵니다.
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // 오브젝트가 항상 정면을 향하도록 지정된 지점을 바라보도록 회전시킵니다.
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