using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseCursor : MonoBehaviour
{
    public RectTransform cursorUI; // 마우스 커서를 표시할 UI 요소
    public Texture2D customCursor; // 설정한 마우스 커서 이미지

    void Start()
    {
        Cursor.visible = false; // 기본 마우스 커서를 숨김

        //// 마우스 커서 텍스처 생성 시 Cursor 사용에 적합한 타입으로 설정
        //customCursor = new Texture2D(customCursor.width, customCursor.height, TextureFormat.RGBA32, false);
        //customCursor.SetPixels(customCursor.GetPixels());
        //customCursor.Apply();

        // 커스텀 커서 설정
        Cursor.SetCursor(customCursor, new Vector2(customCursor.width / 2f, customCursor.height / 2f), CursorMode.Auto);
    }

    void Update()
    {
        // 마우스의 좌표를 읽어와 UI 위치로 설정
        Vector3 mousePosition = Input.mousePosition;
        cursorUI.position = mousePosition;

        // esc 눌렀을 때 시스템커서 뜨는 거, 한 번 클릭하면 사라지게끔 처리했음. 아예 안 뜨게 하는 건 보류 //
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = false;
        }

        //// 화면 밖에서는 기본 커서로
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




