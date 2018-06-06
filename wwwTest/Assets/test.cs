using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Image spriteTest;
    public string[] url;

    public void Start()
    {

        for (int i = 0; i < url .Length; i++)
        {
            AsyncImageDownload.Instance.SetAsyncImage(url[i]);
        }
        Sprite m_sprite = Sprite.Create(AsyncImageDownload.Instance.headImageList[0], new Rect(0, 0, AsyncImageDownload.Instance.headImageList[0].width, AsyncImageDownload.Instance.headImageList[0].height), new Vector2(0, 0));
        spriteTest.sprite = m_sprite;
        Debug.Log(AsyncImageDownload .Instance .headImageList .Count +"6666666666666666666666");
    }
}
