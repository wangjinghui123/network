using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WJH
{


    public class GameController : MonoBehaviour
    {

        float time = 0;
        bool OnStart = false;
        float maxTime = 0;

        private static GameController instance;

        public static GameController Instance
        {
            get
            {
                return instance;
            }
        }

        public Action<float> OnReadyTime;
        public Action OnReadyEndTime;
        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            OnReadyTime += ((time) =>
            {

            });


        }
        /// <summary>
        /// 倒计时
        /// </summary>
        public void InitTime(float time = 3f)
        {
            maxTime = time;
            this.time = 0;
            OnStart = true;
            InitSceneData();
        }


        public void InitSceneData()
        {
            List<PlayerData> data = PlayerModule.Instance.GetAllPlayer();
            if (data.Count == 0)
            {
                Debug.LogError("还没有人加入");
                return;
            }
            foreach (PlayerData item in data)
            {
                //进行玩家数据的初始化
                Debug.LogError("玩家数据:"+item);
            }


        }

        // Update is called once per frame
        void Update()
        {
            if (OnStart)
            {
                this.time += Time.deltaTime;
                if (time <= maxTime)
                {
                    if (OnReadyTime != null)
                    {
                        OnReadyTime(Mathf.Floor(time));
                    }
                }
                else
                {
                    if (OnReadyEndTime != null)
                    {
                        OnReadyEndTime();
                    }
                    OnStart = false;
                }
            }
        }
        private void OnDestroy()
        {

        }
    }

}