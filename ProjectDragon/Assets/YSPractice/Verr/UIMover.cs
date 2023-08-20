using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMover : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            rectTransform.anchoredPosition=Input.mousePosition;
        }

        if(Input.GetKey(KeyCode.KeypadPlus))
        {
            rectTransform.sizeDelta = rectTransform.sizeDelta * 1.1f;
        }

        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            rectTransform.sizeDelta = rectTransform.sizeDelta * 0.9f;
        }

    }
}
