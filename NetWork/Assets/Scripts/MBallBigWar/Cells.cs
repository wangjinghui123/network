using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cells : MonoBehaviour {
    public List<GameObject> cells;
    public Sprite cellSprite;
    public Sprite splitFoodSprite;
    public  int ballSpriteIndex;
    private PlayersManager playerManager;
    public  bool isInvincible = true;//玩家初始为无敌状态;
    private void Awake()
    {
        playerManager = transform.parent.GetComponent<PlayersManager >();
        cells.Add(transform .GetChild (0).gameObject );
        InitBall();
       
    }
    public void InitBall()  
    {
        ballSpriteIndex = Random.Range(0, playerManager.ballSprites.Length);
        cellSprite = playerManager.ballSprites[ballSpriteIndex];
        splitFoodSprite = playerManager.splitBallSprites[ballSpriteIndex ];
    }
}
