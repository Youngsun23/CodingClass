using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFiller : MonoBehaviour
{
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        image.fillAmount = Mathf.Sin(timer) * 0.5f + 0.5f;
    }

}
