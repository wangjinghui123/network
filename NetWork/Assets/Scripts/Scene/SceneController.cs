using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace WJH
{


    public class SceneController : MonoBehaviour
    {
        static SceneController instance;
        AsyncOperation async;
        public static SceneController Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject g = new GameObject("SceneController");
                    instance = g.AddComponent<SceneController>();
                }
                return instance;
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
            instance = this;

        }
        /// <summary>
        /// 切换场景
        /// </summary>
        /// <param name="sceneName">切换到目标场景</param>
        /// <param name="action">切换到目标场景后要做的事情</param>
        public void ChangeScene(string sceneName, Action action = null, float time = 0.1f)
        {
            SceneManager.LoadScene("loadingScene", LoadSceneMode.Additive);

            StartCoroutine(LoadScene(sceneName, action,time));
        }

        IEnumerator LoadScene(string sceneName, Action action,float time = 0.1f)
        {
            yield return new WaitForSeconds(time);
            async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            async.allowSceneActivation = false;
            print(async.progress);
            float value = async.progress;

            while (async.progress < 0.9f)
            {
                yield return null;
                print("加载进度:      " + async.progress);
            }
            value = 0.9f;
            SceneManager.UnloadSceneAsync("loadingScene");

            async.allowSceneActivation = true;
            if (action != null)
            {
                action();
            }
        }





    }
}