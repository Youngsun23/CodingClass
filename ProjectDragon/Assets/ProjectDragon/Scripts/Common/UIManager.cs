using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Dragon
{
    public class UIManager : SingletonBase<UIManager>
    {
        public static T Show<T>(UIList ui) where T : UIBase
        {
            var newUI = Singleton.GetUI<T>(ui);
            newUI.Show();
            return newUI;
        }

        public static T Hide<T>(UIList ui) where T : UIBase
        {
            var targetUI = Singleton.GetUI<T>(ui);
            if (targetUI == null)
                return null;

            targetUI.Hide();
            return targetUI;
        }

        /// <summary> 2D Panel UI Container </summary>
        private Dictionary<UIList, UIBase> panels = new Dictionary<UIList, UIBase>();

        /// <summary> 2D Popup UI Container </summary>
        private Dictionary<UIList, UIBase> popups = new Dictionary<UIList, UIBase>();

        [SerializeField] private Transform panelRoot;
        [SerializeField] private Transform popupRoot;

        private const string UI_PATH = "UI/Prefab/";

        public void Initialize()
        {
            if (panelRoot == null)
            {
                GameObject goPanelRoot = new GameObject("Panel Root");
                panelRoot = goPanelRoot.transform;
                panelRoot.parent = transform;
                panelRoot.localPosition = Vector3.zero;
                panelRoot.localRotation = Quaternion.identity;
                panelRoot.localScale = Vector3.one;
            }

            if (popupRoot == null)
            {
                GameObject goPopupRoot = new GameObject("Popup Root");
                popupRoot = goPopupRoot.transform;
                popupRoot.parent = transform;
                popupRoot.localPosition = Vector3.zero;
                popupRoot.localRotation = Quaternion.identity;
                popupRoot.localScale = Vector3.one;
            }

            for (int index = (int)UIList.SCENE_PANEL + 1; index < (int)UIList.MAX_SCENE_PANEL; index++)
            {
                panels.Add((UIList)index, null);
            }

            for (int index = (int)UIList.SCENE_POPUP + 1; index < (int)UIList.MAX_SCENE_POPUP; index++)
            {
                popups.Add((UIList)index, null);
            }
        }

        public bool GetUI<T>(UIList uiName, out T ui, bool reload = false) where T : UIBase
        {
            ui = GetUI<T>(uiName, reload);
            return ui != null;
        }

        public T GetUI<T>(UIList uiName, bool reload = false) where T : UIBase
        {
            // Get Panel
            if (UIList.SCENE_PANEL < uiName && uiName < UIList.MAX_SCENE_PANEL)
            {
                if (panels.ContainsKey(uiName))
                {
                    if (reload && panels[uiName] != null)
                    {
                        Destroy(panels[uiName].gameObject);
                        panels[uiName] = null;
                    }

                    if (panels[uiName] == null)
                    {
                        string path = UI_PATH + uiName.ToString();
                        GameObject loadedUI = Resources.Load<GameObject>(path) as GameObject;
                        if (loadedUI == null) return null;

                        T result = loadedUI.GetComponent<T>();
                        if (result == null) return null;

                        panels[uiName] = Instantiate(loadedUI, panelRoot).GetComponent<T>() as T;

                        if (panels[uiName]) panels[uiName].gameObject.SetActive(false);
                        return panels[uiName].GetComponent<T>();
                    }
                    else
                    {
                        return panels[uiName].GetComponent<T>();
                    }
                }
            }

            // Get Popup
            if (UIList.SCENE_POPUP < uiName && uiName < UIList.MAX_SCENE_POPUP)
            {
                if (popups.ContainsKey(uiName))
                {
                    if (reload && popups[uiName] != null)
                    {
                        Destroy(popups[uiName].gameObject);
                        popups[uiName] = null;
                    }

                    if (popups[uiName] == null)
                    {
                        string path = UI_PATH + uiName.ToString();
                        GameObject loadedUI = Resources.Load<GameObject>(path) as GameObject;
                        if (loadedUI == null) return null;

                        T result = loadedUI.GetComponent<T>();
                        if (result == null) return null;

                        popups[uiName] = Instantiate(loadedUI, popupRoot).GetComponent<T>() as T;

                        if (popups[uiName]) popups[uiName].gameObject.SetActive(false);
                        return popups[uiName].GetComponent<T>();
                    }
                    else
                    {
                        return popups[uiName].GetComponent<T>();
                    }
                }
            }

            return null;
        }

    }
}

