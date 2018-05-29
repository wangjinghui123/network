using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ITrigger
{
    void OnTrigger(Trigger other);
}

public class Trigger : MonoBehaviour,ITrigger
{
    public Rect rect;
    Image selfImage;

    public Image SelfImage
    {
        get
        {
            if (selfImage == null)
            {
                selfImage = GetComponent<Image>();
            }
            return selfImage;
        }
    }
    private void Start()
    {
        UpdateRect();
    }
    private void UpdateRect()
    {
        rect.size = SelfImage.rectTransform.rect.size * SelfImage.transform.localScale.x;
        rect.position = SelfImage.rectTransform.anchoredPosition;
    }

    private void Update()
    {
        UpdateRect();
    }

    public void OnTrigger(Trigger other)
    {
        Debug.Log(other.name);
    }
}
