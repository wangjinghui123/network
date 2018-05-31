using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{

    public List<BallProperty> ballList;//存储玩家
    public Sprite[] ballSprites;
    public Sprite[] splitBallSprites;
    public RectTransform LeftUpPoint;
    public RectTransform RightDownPoint;
    public FoodManager FoodManagerRect;
    public GameObject cells;
    public float cellWith = 40;
    public int npcCount = 10;

    private void Start()
    {
        for (int i = 0; i < npcCount; i++)
        {
            float x = Random.Range(LeftUpPoint.position.x + cellWith / 2.0f, RightDownPoint.position.x - cellWith / 2.0f);
            float y = Random.Range(RightDownPoint.position.y + cellWith / 2.0f, LeftUpPoint.position.y - cellWith / 2.0f);
            Vector3 pos = new Vector3(x, y, 0);
            GameObject obj = Instantiate(cells, transform);
            obj.transform.position = pos;
            obj.GetComponent<Cells>().personState = Cells.PersonState.NPC;

        }
    }

}
