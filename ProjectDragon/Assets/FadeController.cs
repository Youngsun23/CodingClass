using UnityEngine;
using TMPro;
using System.Collections;

public class FadeController : MonoBehaviour
{

    public TextMeshProUGUI text;

    private void Start()
    {
        // 시작 시 텍스트 활성화
        text.alpha = 1f;
        // 1초마다 Text 알파값을 조절하는 코루틴 실행
        StartCoroutine(FlashText());
        Debug.Log("코루틴 실행");
    }

    private IEnumerator FlashText()
    {
        
            // 텍스트 알파값을 서서히 감소시켜 흐려짐
            while (text.alpha > 0)
            {
                text.alpha -= Time.deltaTime;
                yield return null;
            }
            Debug.Log("알파값 감소");

            // 1초 대기
            yield return new WaitForSeconds(1f);

            // 텍스트 알파값을 서서히 증가시켜 다시 선명해짐
            while (text.alpha < 1f)
            {
                text.alpha += Time.deltaTime;
                yield return null;
            }
            Debug.Log("알파값 증가");

            // 1초 대기
            yield return new WaitForSeconds(1f);
        
    }
}

