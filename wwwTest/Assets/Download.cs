using UnityEngine;
using System.Collections;
using System.IO;
public class Download : MonoBehaviour
{
    public string url = "http://ww3.sinaimg.cn/large/80dfe250jw1dle1r2v4t9j.jpg";
    public GUIText Test;
    WWW www;
    Color Alpha;
    bool Appear = false;
    int a = 0; IEnumerator Start()
    {
        www = new WWW(url);      //定义www为WWW类型并且等于所下载下来的WWW中内容。
        yield return www;
        //返回所下载的www的值 		
      // this .GetComponent <Renderer >().material.shader = Shader.Find("Transparent/Diffuse");
         this .GetComponent <Renderer >().material.mainTexture = www.texture;
        Texture2D newTexture = www.texture;
        byte[] pngData = newTexture.EncodeToPNG();
        try
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                File.WriteAllBytes(Application.persistentDataPath + "/ICO.png", pngData);
            }
            else
            {
                //File.WriteAllBytes(Application.dataPath + "/download/ICO.png", pngData); 
                File.WriteAllBytes(Application.dataPath + "/ICO.png", pngData);
            }
        }
        catch (IOException e)
        {
            print(e);
        }
        Alpha = this.GetComponent<Renderer>().material.color;
        Alpha.a = 0;
        Appear = true;
        this.GetComponent<Renderer>().enabled = true;
        this.GetComponent<Renderer>().material.color = Alpha;
        //将下载下来的WWW中的图片赋予到默认物体的材质上进行渲染出来
    }
    void Update()
    {
        Test.text = "DownLoad: " + www.progress;
        if (www.progress == 1 && Appear)
        {
            a++;
            Alpha = this.GetComponent<Renderer>().material.color;
            Alpha.a += 0.01F;
            this.GetComponent<Renderer>().material.color = Alpha;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width - 120, Screen.height - 40, 120, 30), "Click to XIAOWEI!"))
        {
            Application.OpenURL("http://blog.csdn.net/dingxiaowei2013");
        }
    }
}