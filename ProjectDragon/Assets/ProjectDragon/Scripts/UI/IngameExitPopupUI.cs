using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Dragon
{
    public class IngameExitPopupUI : UIBase
    {


        public void OnClickOKButton()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }

        public void OnClickCancelButton()
        {
            UIManager.Hide<IngameExitPopupUI>(UIList.IngameExitPopupUI);
        }
    }
}