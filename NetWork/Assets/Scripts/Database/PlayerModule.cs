using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerData
{

}


public class PlayerModule
{
    static PlayerModule instance;
    Dictionary<string, PlayerData> playerdata;
    public static PlayerModule Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerModule();
            }
            return instance;
        }
    }

    private Dictionary<string, PlayerData> PlayerdataDic
    {
        get
        {
            if (playerdata == null)
            {
                playerdata = new Dictionary<string, PlayerData>();
            }
            return playerdata;
        }
    }

    private PlayerModule() { }


    public void Add(string userID, PlayerData data)
    {

        if (PlayerdataDic.ContainsKey(userID))
        {
            Debug.LogException(new System.Exception("已存在该玩家:" + userID));
        }
        else
        {
            UpdateInfo(userID,data);
        }
    }

    public void UpdateInfo(string userID, PlayerData data)
    {
        PlayerdataDic.Add(userID, data);
    }

    public void ClearData()
    {
        PlayerdataDic.Clear();
    }

}
