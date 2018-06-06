using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class AsyncImageDownload : MonoBehaviour
{
    public Sprite placeholder;
    public List<Texture2D > headImageList = new List<Texture2D>();
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

    public void SetAsyncImage(string url)
    {

        StartCoroutine(DownloadImage(url));
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

    IEnumerator DownloadImage(string url)
    {
        Debug.Log("downloading new image:" + path + url.GetHashCode());//url转换HD5作为名字  
        WWW www = new WWW(url);
        yield return www;
        Texture2D tex2d = www.texture;
        //将图片保存至缓存路径  
        //byte[] pngData = tex2d.EncodeToPNG();
        //File.WriteAllBytes(path + url.GetHashCode(), pngData);
       
        Debug.Log("加載圖片");
        headImageList.Add(tex2d);
        Debug.Log("添加列表");
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


}