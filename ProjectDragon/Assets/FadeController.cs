using UnityEngine;
using TMPro;
using System.Collections;

public class FadeController : MonoBehaviour
{

    public TextMeshProUGUI text;

    private void Start()
    {
        // ���� �� �ؽ�Ʈ Ȱ��ȭ
        text.alpha = 1f;
        // 1�ʸ��� Text ���İ��� �����ϴ� �ڷ�ƾ ����
        StartCoroutine(FlashText());
        Debug.Log("�ڷ�ƾ ����");
    }

    private IEnumerator FlashText()
    {
        
            // �ؽ�Ʈ ���İ��� ������ ���ҽ��� �����
            while (text.alpha > 0)
            {
                text.alpha -= Time.deltaTime;
                yield return null;
            }
            Debug.Log("���İ� ����");

            // 1�� ���
            yield return new WaitForSeconds(1f);

            // �ؽ�Ʈ ���İ��� ������ �������� �ٽ� ��������
            while (text.alpha < 1f)
            {
                text.alpha += Time.deltaTime;
                yield return null;
            }
            Debug.Log("���İ� ����");

            // 1�� ���
            yield return new WaitForSeconds(1f);
        
    }
}

