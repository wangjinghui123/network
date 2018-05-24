using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    static SceneController instance;

    public static SceneController Instance
    {
        get
        {
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
    public void ChangeScene(string sceneName, Action action = null)
    {
        SceneManager.LoadScene("loadingScene");
        StartCoroutine(LoadScene(sceneName,action));
    }

    IEnumerator LoadScene(string sceneName, Action action)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        print(async.progress);
        float value = async.progress;
        //场景加载好后不自动切换
        async.allowSceneActivation = false;

       

        while (async.progress <= 0.9f)
        {
            value = async.progress;
            yield return new WaitForSeconds(1);
            print("加载进度:      " + async.progress);
        }
        while(value >= 0.9f && value <= 1f)
        {
            value += 0.1f;
            yield return new WaitForSeconds(2);
            print(value);
        }
        yield return async;
        async.allowSceneActivation = true;
        if (action!=null)
        {
            action();
        }
    }





}
