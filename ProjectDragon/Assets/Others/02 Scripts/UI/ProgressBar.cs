
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public int barmaximum;
    public int barcurrent;
    public Image mask;

    private WaveManager waveManager; // WaveManager �ν��Ͻ��� ������ ����
    public int monsterCount;


    // Start is called before the first frame update
    void Start()
    {
        // WaveManager�� �ν��Ͻ��� ������
        waveManager = FindObjectOfType<WaveManager>();
        barmaximum = 27; // ���������� �ʱ� �Ѹ��ͼ� (���� ������ ���� ������ ��)
    }

    // Update is called once per frame
    void Update()
    {
        if (waveManager == null) return; // Null üũ �߰�
        barcurrent = waveManager.GetMonsterCount(); // barcurrent�� ���� ���ͼ�
                                                     
        GetCurrentFill();
    }

        void GetCurrentFill()
    {
        float fillAmout = 1-(float)barcurrent / (float)barmaximum;
        mask.fillAmount = fillAmout;
    }

}
