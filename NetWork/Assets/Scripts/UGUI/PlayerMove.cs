using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    RectTransform selfTransform;

    public RectTransform SelfTransform
    {
        get
        {
            if (selfTransform == null)
            {
                selfTransform = GetComponent<RectTransform>();
            }
            return selfTransform;
        }
    }

    private void Start()
    {
        OnMove();
    }
    public Vector2 dic = Vector2.zero;
    public void OnMove()
    {

    }
    public void OnMove(Vector2 ve2)
    {
        dic = ve2;
    }
    public void OnMove(float x, float y)
    {
        dic = new Vector2(x, y);
    }

    private void Update()
    {
        SelfTransform.Translate(dic * Time.deltaTime);
    }
}
