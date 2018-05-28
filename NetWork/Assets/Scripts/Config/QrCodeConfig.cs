using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

namespace WJH
{
    public class QrCodeConfig : MonoBehaviour
    {
        public EasyConfig config;
        public RawImage qrCode;


        void OnEnable()
        {
            GetConfigStart();
        }
        void OnDisable()
        {
            //SetConfig();
        }

        public void GetConfigStart()
        {
            Vector2 size = Vector2.zero;
            size.x = config.GetPosData(1).width;
            size.y = config.GetPosData(1).height;
            qrCode.rectTransform.sizeDelta = size;

            Vector3 pos = Vector3.zero;
            pos.x = config.GetPosData(1).x;
            pos.y = config.GetPosData(1).y;
            qrCode.rectTransform.anchoredPosition3D = pos;
        }


        public void GetConfigOnGame()
        {

            Vector2 size = Vector2.zero;
            size.x = config.GetPosData(2).width;
            size.y = config.GetPosData(2).height;
            qrCode.rectTransform.sizeDelta = size;

            Vector3 pos = Vector3.zero;
            pos.x = config.GetPosData(2).x;
            pos.y = config.GetPosData(2).y;

            qrCode.rectTransform.DOAnchorPos3D(pos, 0.5f);//.anchoredPosition3D = pos;


        }



    }
}