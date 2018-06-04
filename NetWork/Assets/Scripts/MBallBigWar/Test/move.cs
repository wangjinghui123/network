
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private  ScrollCircle touch;
    public Vector3 dirRocker;
    public Vector3 dir;


    public  Vector3 dirNPC = Vector3.zero;
    private Cells cells;

    public void PlayerMove()
    {
        //获取horizontal 和 vertical 的值，其值位遥感的localPosition  
       
        if (cells . personState == Cells.PersonState.Player)
        {
            float hor = touch.Horizontal;
            float ver = touch.Vertical;
            dirRocker = new Vector3(hor, ver, 0);
            dirRocker.Normalize();
            dir = dirRocker;
           
        }
        else
        {
            dirNPC = transform .parent . GetComponent<AICtrl >().npcDir;
            dir = dirNPC;
        }
        this.GetComponent<BallProperty>().BallMove(dir);

    }
    public void Update()
    {
        PlayerMove();
    }
  

    private void Awake()
    {
        touch = GameObject.FindGameObjectWithTag("touch").GetComponent<ScrollCircle>();
    }

    private void Start()
    {
        cells = transform.parent.GetComponent<Cells>();
       
    }

}
