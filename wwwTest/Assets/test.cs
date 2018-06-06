using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public RawImage spriteTest;
    public string[] url;

    public void Start()
    {

        for (int i = 0; i < url .Length; i++)
        {
             AsyncImageDownload.Instance.SetAsyncImage(url[i], spriteTest);
        }
        if (AsyncImageDownload.Instance.HeadImageList.Count==0)
        {
            return;
        }
        //Sprite m_sprite = Sprite.Create(AsyncImageDownload.Instance.HeadImageList[0], new Rect(0, 0, AsyncImageDownload.Instance.HeadImageList[0].width, AsyncImageDownload.Instance.HeadImageList[0].height), new Vector2(0, 0));
        //spriteTest.sprite = m_sprite;
        Debug.Log(AsyncImageDownload .Instance .HeadImageList .Count +"6666666666666666666666");
    }
}
