using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonTester : MonoBehaviour
{
    Button button;

    public void OnClickButton()
    {
        Debug.Log("Button Click");
    }

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
    }
}
