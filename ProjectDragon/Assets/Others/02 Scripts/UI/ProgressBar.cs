
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public int barmaximum;
    public int barcurrent;
    public Image mask;

    private WaveManager waveManager; // WaveManager 인스턴스를 저장할 변수
    public int monsterCount;


    // Start is called before the first frame update
    void Start()
    {
        // WaveManager의 인스턴스를 가져옴
        waveManager = FindObjectOfType<WaveManager>();
        barmaximum = 27; // 스테이지의 초기 총몬스터수 (최종 버전에 따라 변경할 것)
    }

    // Update is called once per frame
    void Update()
    {
        if (waveManager == null) return; // Null 체크 추가
        barcurrent = waveManager.GetMonsterCount(); // barcurrent는 남은 몬스터수
                                                     
        GetCurrentFill();
    }

        void GetCurrentFill()
    {
        float fillAmout = 1-(float)barcurrent / (float)barmaximum;
        mask.fillAmount = fillAmout;
    }

}
