using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerEffect : MonoBehaviour
{
    public GameObject JumpEffectPrefab;
   
    private Vector3 JumpEffectPosition;

    private PlayerManager playermanager; // ScriptA 스크립트를 참조하기 위한 변수
    private Animator playereffectAnimator;
    private GameObject JumpEffect;
    private bool IsJumpTriggered;
    public Transform playerTransform;
    private WaveManager waveManager;


    private void Start()
    {
        // ScriptA 스크립트를 참조합니다.
        playermanager = GetComponent<PlayerManager>();
        playereffectAnimator = JumpEffect.GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
        // WaveManager 스크립트가 있는 게임 오브젝트를 찾아서 waveManager 변수에 할당합니다.
        waveManager = GameObject.FindObjectOfType<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Jump 트리거가 발동되면 true를 반환하고, 아니면 false를 반환하는 함수
        bool IsJumpTriggered()
        {
            // "Jump"이라는 이름의 트리거가 발동되었는지 확인
            return playermanager.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump");
        }

        // 플레이어의 위치를 받아와서 해당 위치값으로부터 y축으로 -3에 위치하는 함수
        Vector3 GetJumpEffectPosition(Transform playerTransform)
        {
            // 플레이어의 현재 위치를 가져옵니다.
            Vector3 playerPosition = playerTransform.position;

            // 플레이어의 위치에서 y축으로 -3만큼 이동한 위치를 계산합니다.
            Vector3 jumpEffectPosition = new Vector3(playerPosition.x, playerPosition.y - 3f, playerPosition.z);

            // 계산된 위치값을 반환합니다.
            return jumpEffectPosition;
        }

        if (IsJumpTriggered())
        {
            Vector3 JumpEffectPosition = GetJumpEffectPosition(playerTransform);
            GameObject JumpEffect = Instantiate(JumpEffectPrefab, JumpEffectPosition, Quaternion.identity);
            playereffectAnimator.SetTrigger("Jump");

            StartCoroutine(DestroyJumpEffect(JumpEffect));
        }

       



        IEnumerator DestroyJumpEffect (GameObject JumpEffect)
        {
            // 애니메이션 재생 시간 만큼 대기
            yield return new WaitForSeconds(2.75f);

            // 애니메이션 및 콜라이더 파괴
            Destroy(JumpEffect);
        }



    }
}
