
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private  ScrollCircle touch;
    public Vector3 dirRocker;
    public Vector3 dir;


    private Vector3 dirNPC = Vector3.zero;
    private Cells cells;

    public void PlayerMove()
    {
        //获取horizontal 和 vertical 的值，其值位遥感的localPosition  
        float hor = touch.Horizontal;
        float ver = touch.Vertical;
        dirRocker = new Vector3(hor, ver, 0);
        dirRocker.Normalize();
        if (cells . personState == Cells.PersonState.Player)
        {
            dir = dirRocker;
        }
        else
        {
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
