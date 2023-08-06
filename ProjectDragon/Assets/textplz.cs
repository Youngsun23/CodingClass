
using UnityEngine;
using System.Collections;

using TMPro;

public class textplz : MonoBehaviour
{
    public float blinkDuration = 2f; // 왔다갔다하는 애니메이션의 전체 시간 (초)
    public float minAlpha = 0f; // 알파값의 최소값 (투명)
    public float maxAlpha = 1f; // 알파값의 최대값 (불투명)

    private bool isIncreasing = true; // 알파값이 증가 중인지 여부를 나타내는 플래그

    private void Start()
    {
        StartCoroutine(BlinkingCoroutine());
    }

    private IEnumerator BlinkingCoroutine()
    {
        while (true)
        {
            float t = 0f;

            while (t < blinkDuration)
            {
                t += Time.deltaTime;
                float normalizedTime = t / blinkDuration;
                float alpha;

                if (isIncreasing)
                    alpha = Mathf.Lerp(minAlpha, maxAlpha, normalizedTime);
                else
                    alpha = Mathf.Lerp(maxAlpha, minAlpha, normalizedTime);

                Color newColor = gameObject.GetComponent<TextMeshProUGUI>().color;
                newColor.a = alpha;
                gameObject.GetComponent<TextMeshProUGUI>().color = newColor;

                yield return null;
            }

            // 알파값의 증가/감소를 반전
            isIncreasing = !isIncreasing;
        }
    }
}
