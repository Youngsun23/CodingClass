using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public void OnClickGameStart() // public->�ҷ��ͼ� �Լ� ����
    {
        SceneManager.LoadScene("Stage1");
    }


    public void OnClickGameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �����Ϳ����� ���� �Լ� �� ������ ���� ����
#else
        Application.Quit(); // �������� �Լ�
#endif 
    }


}
