using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour {

    public List<BallProperty> ballList;//存储玩家
    public Sprite[] ballSprites;
    public Sprite[] splitBallSprites;
    public RectTransform LeftUpPoint;
    public RectTransform RightDownPoint;
    public FoodManager  FoodManagerRect;

}
