using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace WJH
{
    public class Cells : MonoBehaviour
    {
        [SerializeField] public Sprite _cellSprite;//玩家Sprite
        [SerializeField] public Sprite _playerHeadSprite;//玩家头像
        [SerializeField] public string _userID;
        [SerializeField] public string _nickName;
        public List<GameObject> cells;
        [SerializeField] public Sprite splitFoodSprite;
        public int ballSpriteIndex;
        private PlayersManager playerManager;
        public bool isInvincible = true;//玩家初始为无敌状态;
        public bool isDeath = false;//玩家是否死亡
        public float startSurvivalTime = 0f;//玩家进入时间
        public float survivalTime = 0f;//玩家生存时间
        public float playerMass;
        public enum PersonState
        {
            Player,
            NPC
        }
        public PersonState personState;
        private void Start()
        {
            playerManager = transform.parent.GetComponent<PlayersManager>();
            cells.Add(transform.GetChild(0).gameObject);
            InitBall();
            transform.GetComponentInChildren<Text>().text = _nickName;

        }
        public void InitBall()
        {
           // ballSpriteIndex = Random.Range(0, playerManager.ballSprites.Length);

            _cellSprite = playerManager.ballSprites[ballSpriteIndex];
            splitFoodSprite = playerManager.splitBallSprites[ballSpriteIndex];
        }
        //public void Start()
        //{
           
        //}
    }
}

