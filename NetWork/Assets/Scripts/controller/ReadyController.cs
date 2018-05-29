using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace WJH
{



    public class ReadyController : MonoBehaviour
    {
        private static ReadyController instance;

        public static ReadyController Instance
        {
            get
            {

                return instance;
            }
        }
        public UGUISpriteSelector selector;
        private void Awake()
        {
            instance = this;
        }
        int time = -1;
        public GameObject bg;
        public void StartReady(bool status = true)
        {
            bg.SetActive(status);
        }


        public void OnReady()
        {
            GameController.Instance.OnReadyTime += (time) =>
            {
                if (this.time == time)
                {
                    return;
                }
                else
                {
                   
                    Tween tween = selector.circle.DOFillAmount(0, 1);

                    tween.OnStart(() =>
                    {
                        selector.GetComponent<RectTransform>().DOScale(1.2f, 1);
                    });
                    tween.OnComplete(() =>
                    {
                        selector.transform.DOScale(1f, 0);
                        selector.circle.fillAmount = 1;
                    });
                    if (time == Utils.READY_TIME - 1)
                    {
                        selector.circle.gameObject.SetActive(false);
                        tween.OnComplete(()=>
                        {
                            StartReady(false);
                        });
                    }
                    this.time = time;
                    Debug.LogError("++++++++++++++++");

                }
                selector.index = time;
            };
        }
    }
}