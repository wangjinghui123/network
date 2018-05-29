using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CodeController : MonoBehaviour {

    private static CodeController instance;

    public static CodeController Instance
    {
        get
        {
            return instance;
        }
    }

    public Image Bg
    {
        get
        {
            if (bg == null)
            {
                bg = gameObject.GetComponent<Image>();
            }
            return bg;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    Image bg;
    public void SetAlpha(int alpha = 0)
    {
        Bg.DOFade(alpha, 0.1f);
    }
}
