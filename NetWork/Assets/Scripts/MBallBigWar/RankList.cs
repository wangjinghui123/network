using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WJH
{
    public class RankList : MonoBehaviour
    {
        private const int ranknum = 10;
        public RankItem[] rankItemList = new RankItem[10];
        public List<PlayerData> playerDataList = new List<PlayerData>();
        static RankList _instance;
        private void Awake()
        {
         _instance =this ;   
        }
        public static RankList Instance
        {
            get
            {
                return _instance;
            }
        }
        private void Start()
        {
            rankItemList = transform.GetComponentsInChildren<RankItem>();
        }
        public void UpdateData()
        {
            for (int i=0;i<10;i++)
            {
                rankItemList[i].headImage.sprite = PlayersManager.Instance.allplayerList[i]._playerHeadSprite;
                rankItemList[i].nikeNameText.text = PlayersManager.Instance.allplayerList[i]._nickName;
                rankItemList[i].scoreText.text = PlayersManager.Instance.allplayerList[i].playerMass.ToString () ;
            }
        }
    }
}

