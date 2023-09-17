using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dragon
{
    public class Main : MonoBehaviour
    {
        public static Main Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            Instance = null;
        }


        private void Start()
        {
            StartCoroutine(Initialize());
        }

        public IEnumerator Initialize()
        {
            var loadStartUpAsync = SceneManager.LoadSceneAsync("StartUp");
            yield return new WaitUntil(() => loadStartUpAsync.isDone);

            yield return new WaitForEndOfFrame();

            UIManager.Singleton.Initialize();

            var loadIngameAsync = SceneManager.LoadSceneAsync("Ingame");
            yield return new WaitUntil(() => loadIngameAsync.isDone);

            UIManager.Show<IngameUI>(UIList.IngameUI);
        }
    }
}


