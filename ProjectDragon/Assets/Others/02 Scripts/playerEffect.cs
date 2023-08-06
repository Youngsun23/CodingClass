using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerEffect : MonoBehaviour
{
    public GameObject JumpEffectPrefab;
   
    private Vector3 JumpEffectPosition;

    private PlayerManager playermanager; // ScriptA ��ũ��Ʈ�� �����ϱ� ���� ����
    private Animator playereffectAnimator;
    private GameObject JumpEffect;
    private bool IsJumpTriggered;
    public Transform playerTransform;
    private WaveManager waveManager;


    private void Start()
    {
        // ScriptA ��ũ��Ʈ�� �����մϴ�.
        playermanager = GetComponent<PlayerManager>();
        playereffectAnimator = JumpEffect.GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
        // WaveManager ��ũ��Ʈ�� �ִ� ���� ������Ʈ�� ã�Ƽ� waveManager ������ �Ҵ��մϴ�.
        waveManager = GameObject.FindObjectOfType<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Jump Ʈ���Ű� �ߵ��Ǹ� true�� ��ȯ�ϰ�, �ƴϸ� false�� ��ȯ�ϴ� �Լ�
        bool IsJumpTriggered()
        {
            // "Jump"�̶�� �̸��� Ʈ���Ű� �ߵ��Ǿ����� Ȯ��
            return playermanager.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump");
        }

        // �÷��̾��� ��ġ�� �޾ƿͼ� �ش� ��ġ�����κ��� y������ -3�� ��ġ�ϴ� �Լ�
        Vector3 GetJumpEffectPosition(Transform playerTransform)
        {
            // �÷��̾��� ���� ��ġ�� �����ɴϴ�.
            Vector3 playerPosition = playerTransform.position;

            // �÷��̾��� ��ġ���� y������ -3��ŭ �̵��� ��ġ�� ����մϴ�.
            Vector3 jumpEffectPosition = new Vector3(playerPosition.x, playerPosition.y - 3f, playerPosition.z);

            // ���� ��ġ���� ��ȯ�մϴ�.
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
            // �ִϸ��̼� ��� �ð� ��ŭ ���
            yield return new WaitForSeconds(2.75f);

            // �ִϸ��̼� �� �ݶ��̴� �ı�
            Destroy(JumpEffect);
        }



    }
}
