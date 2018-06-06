using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;

public class AsyncImageDownload : MonoBehaviour
{
    public Sprite placeholder;
    [SerializeField]
    private List<Texture> headImageList = new List<Texture>();
    private static AsyncImageDownload _instance = null;
    public static AsyncImageDownload GetInstance() { return Instance; }
    public static AsyncImageDownload Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("AsyncImageDownload");
                _instance = obj.AddComponent<AsyncImageDownload>();
                DontDestroyOnLoad(obj);
                _instance.Init();
            }
            return _instance;
        }
    }

    public bool Init()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/ImageCache/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/ImageCache/");
        }
        if (placeholder == null)
        {
            placeholder = Resources.Load("placeholder") as Sprite;
        }
        return true;

    }

    public void SetAsyncImage(string url, RawImage image, Action<RawImage, Texture2D> action)
    {

        StartCoroutine(DownloadImage(url, image, action));
        ////开始下载图片前，将UITexture的主图片设置为占位图  
        //// image.sprite = placeholder;

        ////判断是否是第一次加载这张图片  
        //if (!File.Exists(path + url.GetHashCode()))
        //{
        //    //如果之前不存在缓存文件  

        //}
        //else
        //{
        //    StartCoroutine(LoadLocalImage(url));
        //}
    }
    public Sprite sprite;
    IEnumerator DownloadImage(string url, RawImage image, Action<RawImage, Texture2D> action)
    {
        Debug.Log("downloading new image:" + path + url.GetHashCode());//url转换HD5作为名字  
        WWW www = new WWW(url);
        while (!www.isDone)
        {
            yield return www;
        }

        Texture2D texture2D = www.texture;
        action(image, texture2D);
    }

    IEnumerator LoadLocalImage(string url)
    {
        string filePath = "file:///" + path + url.GetHashCode();

        Debug.Log("getting local image:" + filePath);
        WWW www = new WWW(filePath);
        yield return www;

        Texture2D texture = www.texture;
        Sprite m_sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));

        // image.sprite = m_sprite;
    }

    public string path
    {
        get
        {
            //pc,ios //android :jar:file//  
            return Application.persistentDataPath + "/ImageCache/";

        }
    }

    public List<Texture> HeadImageList
    {
        get
        {
            if (headImageList == null)
            {
                headImageList = new List<Texture>();
            }
            return headImageList;
        }

        set
        {
            headImageList = value;
        }
    }
}