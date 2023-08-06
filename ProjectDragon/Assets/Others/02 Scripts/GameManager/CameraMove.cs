using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMove : MonoBehaviour
{
    public Transform playerTransform; // �÷��̾� ������Ʈ�� Transform ������Ʈ�� ���⿡ �Ҵ��ϼ���.
    public Transform cameraMoveTransform; // ī�޶� �̵� ������Ʈ�� Transform ������Ʈ�� ���⿡ �Ҵ��ϼ���.
    public Transform level2PointTransform; // Level2_point ������Ʈ�� Transform ������Ʈ�� ���⿡ �Ҵ��ϼ���.
    public float cameraMoveAmount = 32f; // ī�޶� �̵����� ���⿡ �����ϼ���.
    public Tilemap level2Tilemap;
    private void Start()
    {
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ī�޶� �̵�
            Vector3 cameraMovePosition = cameraMoveTransform.position;
            cameraMovePosition.x += cameraMoveAmount;
            cameraMoveTransform.position = cameraMovePosition;

            // �÷��̾� ��ġ ����
            playerTransform.position = level2PointTransform.position;

            level2Tilemap.gameObject.SetActive(true);

            // CameraMove ������Ʈ ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }
}
