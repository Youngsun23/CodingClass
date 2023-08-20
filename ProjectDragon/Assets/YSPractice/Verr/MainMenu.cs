using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnClickNewGame()
    {

    }

    public void OnClickLoad()
    {

    }

    public void OnClickOption()
    { 

    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
UnityEditor.EditorApplication.isPlaying=false;
#else
        Application.Quit();
#endif
    }

}
