using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dragon
{
    public class IngameUI : UIBase
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private TextMeshProUGUI healthText;

        [SerializeField] private Image staminaBar;
        [SerializeField] private TextMeshProUGUI staminaText;


        public void SetHealth(float current, float max)
        {
            healthBar.fillAmount = current / max;
            healthText.text = $"{current:0.0} / {max:0.0}";
        }

        public void SetStamina(float current, float max)
        {
            staminaBar.fillAmount = current / max;
            staminaText.text = $"{current:0.0} / {max:0.0}";
        }


    }
}

