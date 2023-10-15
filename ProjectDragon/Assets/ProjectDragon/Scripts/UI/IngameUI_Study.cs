using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Dragon.Study
{
    public class IngameUI_Study : UIBase
    {
        public Image hpBar;
        public TextMeshProUGUI hpText;
        public TextMeshProUGUI ammoText;

        private void OnEnable()
        {
            if (PlayerController.Instance)
            {
                PlayerController.Instance.OnChangedHP += SetHealth;
                PlayerController.Instance.OnChangedAmmo += SetAmmoText;

                SetHealth(PlayerController.Instance.currentHP, PlayerController.Instance.maxHP);
                SetAmmoText(PlayerController.Instance.curAmmo, PlayerController.Instance.maxAmmo);
            }
        }

        private void OnDisable()
        {
            if (PlayerController.Instance)
            {
                PlayerController.Instance.OnChangedHP -= SetHealth;
                PlayerController.Instance.OnChangedAmmo -= SetAmmoText;
            }
        }

        public void SetHealth(float cur, float max)
        {
            SetHealthBar(cur / max);
            SetHealthText(cur, max);
        }

        public void SetHealthBar(float value)
        {
            hpBar.fillAmount = value;
        }

        public void SetHealthText(float current, float max)
        {
            hpText.text = $"{current:0.0} / {max:0.0}";
        }

        public void SetAmmoText(int current, int max)
        {
            ammoText.text = $"{current} / {max}";
        }

        public void OnClickMenuButton()
        {
            UIManager.Show<IngameExitPopupUI>(UIList.IngameExitPopupUI);
        }
    }
}

