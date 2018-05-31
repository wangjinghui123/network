using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICtrl : MonoBehaviour {
    private  Cells cells;
    private Vector3 npcDir = Vector3.zero;//npc方向
    private void Start()
    {
        cells = this.GetComponent<Cells>();
    }

    public Vector3 SetNpcDir()
    {
        return npcDir;
    }

}
