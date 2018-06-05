using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WJH
{
    public class AICtrl : MonoBehaviour
    {
        private Cells cells;
        public Vector3 npcDir;//npc方向
        private PlayersManager playerManager;
        //public GameObject tempScale;
        //public GameObject tempNearestBall;
        private void Start()
        {
            cells = this.GetComponent<Cells>();
            transform.GetComponent<Cells>().personState = Cells.PersonState.NPC;
            playerManager = transform.parent.GetComponent<PlayersManager>();
            //tempScale = cells.cells[0];
            //tempNearestBall = playerManager.ballList[0].gameObject;
        }

        //设置AI移动方向 
        //public void SetAiDir()
        //{
        //    //  GameObject tempScale = cells.cells[0];
        //    //选出最大Scale的球的Scale
        //    for (int i = 0; i < cells.cells.Count; i++)
        //    {
        //        tempScale = tempScale.transform.localScale.x > cells.cells[i].transform.localScale.x ? tempScale : cells.cells[i];
        //    }
        //    // 使其移动到最近的玩家位置
        //    // GameObject tempNearestBall = playerManager.ballList[0].gameObject;
        //    for (int i = 0; i < playerManager.ballList.Count; i++)
        //    {
        //        Debug.Log(666);
        //        if (i == playerManager.ballList.Count - 1)
        //        {
        //            return;
        //        }
        //        float distance = Vector3.Distance(tempScale.transform.position, playerManager.ballList[i].transform.position);
        //        Debug.Log(111);
        //        if (Vector3.Distance(tempScale.transform.position, playerManager.ballList[i].transform.position) > Vector3.Distance(tempScale.transform.position, playerManager.ballList[i + 1].transform.position))
        //        {
        //            tempNearestBall = playerManager.ballList[i + 1].gameObject;
        //            Debug.Log(9922229);
        //            npcDir = Vector3.Normalize(tempNearestBall.transform.position - tempScale.transform.position);
        //        }
        //    }
        //}
        //private void Update()
        //{
        //    if (Input.GetKey(KeyCode.E))
        //    {
        //        SetAiDir();
        //    }
        //}

        private float alltimer = 0f;
        private float nowtimer = 0;
        private void Update()
        {

        }

        private void FixedUpdate()
        {
            nowtimer += Time.deltaTime;

            if (nowtimer >= alltimer)
            {
                alltimer = Random.Range(2.0f, 5.0f);
                float x = Random.Range(-1f, 1f);
                float y = Random.Range(-1f, 1f);
                npcDir = new Vector3(x, y, 0);
                nowtimer = 0;

            }
        }


    }
}

