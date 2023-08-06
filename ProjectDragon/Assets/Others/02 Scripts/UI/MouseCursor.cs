using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseCursor : MonoBehaviour
{
    public RectTransform cursorUI; // ���콺 Ŀ���� ǥ���� UI ���
    public Texture2D customCursor; // ������ ���콺 Ŀ�� �̹���

    void Start()
    {
        Cursor.visible = false; // �⺻ ���콺 Ŀ���� ����

        //// ���콺 Ŀ�� �ؽ�ó ���� �� Cursor ��뿡 ������ Ÿ������ ����
        //customCursor = new Texture2D(customCursor.width, customCursor.height, TextureFormat.RGBA32, false);
        //customCursor.SetPixels(customCursor.GetPixels());
        //customCursor.Apply();

        // Ŀ���� Ŀ�� ����
        Cursor.SetCursor(customCursor, new Vector2(customCursor.width / 2f, customCursor.height / 2f), CursorMode.Auto);
    }

    void Update()
    {
        // ���콺�� ��ǥ�� �о�� UI ��ġ�� ����
        Vector3 mousePosition = Input.mousePosition;
        cursorUI.position = mousePosition;

        // esc ������ �� �ý���Ŀ�� �ߴ� ��, �� �� Ŭ���ϸ� ������Բ� ó������. �ƿ� �� �߰� �ϴ� �� ���� //
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = false;
        }

        //// ȭ�� �ۿ����� �⺻ Ŀ����
        //if (!Cursor.visible && IsMouseOutsideGameWindow())
        //{
        //    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        //}
    }

    //bool IsMouseOutsideGameWindow()
    //{
    //    Vector3 mousePosition = Input.mousePosition;
    //    return mousePosition.x < 0 || mousePosition.y < 0 || mousePosition.x > Screen.width || mousePosition.y > Screen.height;
    //}
}




