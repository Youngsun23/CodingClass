
using UnityEngine;
using System.Collections;

using TMPro;

public class textplz : MonoBehaviour
{
    public float blinkDuration = 2f; // �Դٰ����ϴ� �ִϸ��̼��� ��ü �ð� (��)
    public float minAlpha = 0f; // ���İ��� �ּҰ� (����)
    public float maxAlpha = 1f; // ���İ��� �ִ밪 (������)

    private bool isIncreasing = true; // ���İ��� ���� ������ ���θ� ��Ÿ���� �÷���

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

            // ���İ��� ����/���Ҹ� ����
            isIncreasing = !isIncreasing;
        }
    }
}
