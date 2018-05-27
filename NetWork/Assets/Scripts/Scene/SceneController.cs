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
        SceneManager.LoadScene("loadingScene",LoadSceneMode.Additive);
        StartCoroutine(LoadScene(sceneName,action));
    }

    IEnumerator LoadScene(string sceneName, Action action)
    {
        yield return new WaitForSeconds(5);
        
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        print(async.progress);
        float value = async.progress;
        //场景加载好后不自动切换
        async.allowSceneActivation = false;


        while (async.progress < 0.9f)
        {
            yield return null;
            print("加载进度:      " + async.progress);
        }
        value = 0.9f;
        while (value < 1f)
        {
            Debug.LogError(value);
            value += 0.01f;
            yield return null;
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
