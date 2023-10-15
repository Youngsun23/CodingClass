using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragon
{
    public class IngamePresenter : MonoBehaviour
    {
        private void Start()
        {
            UIManager.Show<IngameUI>(UIList.IngameUI);
        }
    }
}

