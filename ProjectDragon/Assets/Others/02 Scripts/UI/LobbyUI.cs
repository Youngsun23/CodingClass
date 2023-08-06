using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public void OnClickGameStart() // public->불러와서 함수 실행
    {
        SceneManager.LoadScene("Stage1");
    }


    public void OnClickGameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서는 밑의 함수 안 먹혀서 따로 설정
#else
        Application.Quit(); // 게임종료 함수
#endif 
    }


}
