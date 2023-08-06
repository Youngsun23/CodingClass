using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMove : MonoBehaviour
{
    public Transform playerTransform; // 플레이어 오브젝트의 Transform 컴포넌트를 여기에 할당하세요.
    public Transform cameraMoveTransform; // 카메라 이동 오브젝트의 Transform 컴포넌트를 여기에 할당하세요.
    public Transform level2PointTransform; // Level2_point 오브젝트의 Transform 컴포넌트를 여기에 할당하세요.
    public float cameraMoveAmount = 32f; // 카메라 이동량을 여기에 설정하세요.
    public Tilemap level2Tilemap;
    private void Start()
    {
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 카메라 이동
            Vector3 cameraMovePosition = cameraMoveTransform.position;
            cameraMovePosition.x += cameraMoveAmount;
            cameraMoveTransform.position = cameraMovePosition;

            // 플레이어 위치 변경
            playerTransform.position = level2PointTransform.position;

            level2Tilemap.gameObject.SetActive(true);

            // CameraMove 오브젝트 비활성화
            gameObject.SetActive(false);
        }
    }
}
