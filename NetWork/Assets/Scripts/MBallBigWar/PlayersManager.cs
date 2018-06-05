using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WJH
{
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

        static PlayersManager _instance;
        public static PlayersManager Instance
        {
            get {
                return _instance;
            }
        }
        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            PlayerData p1 = new PlayerData("666", "NPC_m9zo41ibo4wzs1ldkpfbvz7qf347cv2z", "http://boxact-1252079862.file.myqcloud.com/game/Weima_20180419/npc/p153f4-ux2.jpg", modleType.monsters_1 );
            PlayerData p2 = new PlayerData("111", "NPC_hzxi6ig47nzef19fircnsgc9hr0clz1z", "http://boxact-1252079862.file.myqcloud.com/game/Weima_20180419/npc/orf55z-23zj.jpg", modleType.monsters_2 );
            PlayerData p3 = new PlayerData("55555", "NPC_wmq3k7sifefbg60rik1rjugrpk7rqggs", "http://boxact-1252079862.file.myqcloud.com/game/Weima_20180419/npc/onw9xx-bsu.jpg", modleType.monsters_3 );
            PlayerData p4 = new PlayerData("888", "NPC_lgdz5vrpek0bvikry00nxuiyjayhpenw", "http://boxact-1252079862.file.myqcloud.com/game/Weima_20180419/npc/oz8sl7-qfp.png", modleType.monsters_4 );
            PlayerData p5 = new PlayerData("777", "NPC_xrexvccntck0epjmgkjnwrzkemo01nvt", "http://boxact-1252079862.file.myqcloud.com/game/Weima_20180419/npc/o0zj85-skc.jpg", modleType.monsters_5 );
            PlayerData p6 = new PlayerData("99", "NPC_x0vzmp0e5h7brdjyotmkv0qwd8r0eopg", "http://boxact-1252079862.file.myqcloud.com/game/Weima_20180419/npc/p153f4-ux2.jpg", modleType.monsters_6 );
            PlayerData p7 = new PlayerData("55", "NPC_urvc9dd9e9q53fwl3c5yy2kyy4oag624", "http://boxact-1252079862.file.myqcloud.com/game/Weima_20180419/npc/p153f4-ux2.jpg", modleType.monsters_7 );
            PlayerData p8 = new PlayerData("6", "NPC_cc2j28h0qaz7iivilz128zverojqbuqf", "http://boxact-1252079862.file.myqcloud.com/game/Weima_20180419/npc/p153f4-ux2.jpg", modleType.monsters_8 );
            PlayerData p9 = new PlayerData("333", "NPC_iiuydz81lk0rq39unwy4qfe34dweo706", "http://boxact-1252079862.file.myqcloud.com/game/Weima_20180419/npc/p153f4-ux2.jpg", modleType.monsters_5 );
        }

      
        public void InitiaNPC(int NpcCount)
        {
            if (NpcCount == 0)
                return;
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
}

