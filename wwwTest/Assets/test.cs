using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Image[] spriteTest;
    public string[] url;

    public void Start()
    {

        for (int i = 0; i < url .Length; i++)
        {
            AsyncImageDownload.Instance.SetAsyncImage(url[i],spriteTest [i]);
        }
       
        
       // Debug.Log(AsyncImageDownload .Instance .headImageList .Count +"6666666666666666666666");
    }
}
