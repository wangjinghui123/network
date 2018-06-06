using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

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
        public int npcCount = 8;
        public Sprite defaultHeadImage;//默认头像;
        private List<PlayerData> playerNpcList = new List<PlayerData>();
        public List<Cells> allplayerList = new List<Cells>();
        public  List<Sprite> tempsprite = new List<Sprite>();
        static PlayersManager _instance;
        public static PlayersManager Instance
        {
            get
            {
                return _instance;
            }
        }
        private void Awake()
        {
            _instance = this;
            SetNPC();
            InitiaNPC(npcCount);

        }

        private void Start()
        {



        }

        public void SetNPC()
        {
            PlayerData p1 = new PlayerData("strom98", "NPC_m9zo41ibo4wzs1ldkpfbvz7qf347cv2z", "http://boxact-1252079862.file.myqcloud.com/game/Weima_20180419/npc/p153f4-ux2.jpg", modleType.monsters_1);
            PlayerData p2 = new PlayerData("迷人的面容", "NPC_hzxi6ig47nzef19fircnsgc9hr0clz1z", "http://boxact-1252079862.file.myqcloud.com/game/Weima_20180419/npc/orf55z-23zj.jpg", modleType.monsters_2);
            PlayerData p3 = new PlayerData("孤单背影", "NPC_wmq3k7sifefbg60rik1rjugrpk7rqggs", "http://boxact-1252079862.file.myqcloud.com/game/Weima_20180419/npc/onw9xx-bsu.jpg", modleType.monsters_3);
            PlayerData p4 = new PlayerData("淡抹丶悲伤", "NPC_lgdz5vrpek0bvikry00nxuiyjayhpenw", "http://boxact-1252079862.file.myqcloud.com/game/Weima_20180419/npc/oz8sl7-qfp.png", modleType.monsters_4);
            PlayerData p5 = new PlayerData("虚 张 声 势丶", "NPC_xrexvccntck0epjmgkjnwrzkemo01nvt", "http://boxact-1252079862.file.myqcloud.com/game/Weima_20180419/npc/o0zj85-skc.jpg", modleType.monsters_5);
            PlayerData p6 = new PlayerData("冷落了♂自己·", "NPC_x0vzmp0e5h7brdjyotmkv0qwd8r0eopg", "http://boxact-1252079862.file.myqcloud.com/game/Weima_20180419/npc/oh7aq0-o8a.png", modleType.monsters_6);
            PlayerData p7 = new PlayerData("◆残留德花瓣", "NPC_urvc9dd9e9q53fwl3c5yy2kyy4oag624", "http://boxact-1252079862.file.myqcloud.com/game/Weima_20180419/npc/p085yo-1kcm.jpg", modleType.monsters_7);
            PlayerData p8 = new PlayerData("*丶 海阔天空", "NPC_cc2j28h0qaz7iivilz128zverojqbuqf", "http://boxact-1252079862.file.myqcloud.com/game/Weima_20180419/npc/p320aw-uh8.jpg", modleType.monsters_8);
            playerNpcList.Add(p1);
            playerNpcList.Add(p2);
            playerNpcList.Add(p3);
            playerNpcList.Add(p4);
            playerNpcList.Add(p5);
            playerNpcList.Add(p6);
            playerNpcList.Add(p7);
            playerNpcList.Add(p8);
          
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
                PlayerModule.Instance.Add(playerNpcList[i].userId, playerNpcList[i]);
                Cells cellsClass = obj.GetComponent<Cells>();
                // cellsClass._playerHeadSprite = LoadHeadImage(playerNpcList [i].headimgurl );
                Image image = this.GetComponent<Image >();
                StartCoroutine(LoadHeadImage(playerNpcList[i].headimgurl, image));
                Debug.Log(666);

                cellsClass._userID = playerNpcList[i].userId;
                cellsClass._nickName = playerNpcList[i].nickName;
                cellsClass.ballSpriteIndex = (playerNpcList[i].modelType) - 1;
                obj.transform.position = pos;
                cellsClass.personState = Cells.PersonState.NPC;
                Debug.Log(999);
                Debug.Log(888);
            }
        }
        int i = 0;

        private IEnumerator LoadHeadImage(string URL,Image image)
        {

            double startTime = (double)Time.time;
            WWW www = new WWW(URL);//只能放URL
            yield return www;
            if (www != null && string.IsNullOrEmpty(www.error))
            {
                Texture2D tex = www.texture;
               // byte[] spriteByte = tex.EncodeToPNG();
               // string filePath = Application.dataPath + "/Resources/HeadImage/" + i + ".jpg";
               // File.WriteAllBytes(filePath, spriteByte);
                i++;
                //  Sprite sprite = Sprite.Create(tex, new Rect(0, 0, 40, 40), new Vector2(0.5f, 0.5f));
               image .sprite = Sprite.Create(tex,new Rect (0,0,tex.width ,tex .height ) ,new Vector2 (0.5f,0.5f));
                double time = (double)Time.time - startTime;
                tempsprite.Add(image .sprite );
                Debug.Log(tempsprite.Count + "tempSpriteCount");
            }

        }
    }
}




