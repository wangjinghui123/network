using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qy_CSharp_NetWork.WebNet;
using Qy_CSharp_NetWork.Component;
using System;

public class NetWork : MonoBehaviour
{

    const string GameID = "8";

    //启动服务
    private JisightUnityComponent_LAM _lookAtMe = null;
    private GameMessageDataHandle _gameDataHandle = null;
    private NET_OPTION m_netOption = NET_OPTION.WAN;
    [SerializeField]
    private GameControllerAsTime _gameControllerAsTime;
    private CreateNode creatCode;
    public JisightUnityComponent_LAM LookAtMe
    {
        get
        {
            if (_lookAtMe == null)
                _lookAtMe = this.gameObject.AddComponent<JisightUnityComponent_LAM>();
            return _lookAtMe;
        }
    }

    public GameMessageDataHandle GameDataHandle
    {
        get
        {
            if (_gameDataHandle == null)
                _gameDataHandle = this.gameObject.AddComponent<GameMessageDataHandle>();
            return _gameDataHandle;
        }
    }

    public CreateNode CreatCode
    {
        get
        {
            if (creatCode == null)
            {
                creatCode = GetComponent<CreateNode>();
            }
            return creatCode;
        }
    }
    private void Awake()
    {
        Application.targetFrameRate = 60;

        Application.runInBackground = true;
        Cursor.visible = false;
    }
    // Use this for initialization
    void Start()
    {
        _gameControllerAsTime.StartGameEvent += StartGame;
        _gameControllerAsTime.Init(true, 15);
    }
    public void StartGame(object sender,EventArgs msg)
    {
        Debug.LogError(msg);
        StartWork();
    }
    public void StartWork(bool needBug = false,bool istest = true)
    {
        List<string> deviceIds = new List<string>();
        string token = "";
        string gamgId = "";

        gamgId = GameID;
        for (int idex = 0; idex <= 2; ++idex)
        {
            string tempId = QyCsharpProgramTools.Tools.GetEnvironmentValue("id" + idex.ToString());

            Debug.Log("尝试取参" + "【id" + idex.ToString() + "】: " + tempId);
            if (tempId != "")
            {
                deviceIds.Add(tempId);
            }
        }
        token = QyCsharpProgramTools.Tools.GetEnvironmentValue("token");
        Debug.Log("尝试取参" + "【token】: " + token);

        if (istest)
        {
            token = "WkE8fSqkq3Zm1Mfruj61uK0zj0OaScOF0znl";//
            deviceIds.Clear();
            deviceIds.Add("4000");
            deviceIds.Add("6005");
            //token = "91yrf3qkqstxlix3rlvouwvjaxrk28fflgwh";//自己
            //deviceIds.Clear();
            //deviceIds.Add("10000");
        }
        Debug.Log("token:   "+token );
        string ret = "";
        for (int i = 0; i < deviceIds.Count; i++)
        {
            ret += deviceIds[i] + "\n";
        }
        Debug.Log(ret);

        LookAtMe.needDebug = needBug;
        LookAtMe.token = token;
        LookAtMe.gameId = gamgId;
        print("初始化成功");
        LookAtMe.JLAM_Event_Fir += GameDataHandle.NetEventFir;
        _gameDataHandle.SomeOneTryLoginEvent += ResponseTryLoginMsg;
        //二维码生成
        _gameDataHandle.RoomVerifyCompleteEvent += OnRoomVerify;
        //Debug.Log(m_netOption + "," + deviceIds[0] + "," + deviceIds[1]);
        LookAtMe.StartLAM(m_netOption, 12 * 1000, deviceIds.ToArray());

    }

    public void ResponseTryLoginMsg(string receiver, MsgTryLoginRes msg)
    {
        Debug.Log("用户尝试登陆信息回复：--- in --- ");
        List<string> receivers = new List<string>();
        receivers.Add(receiver);

        Debug.Log(Qy_CSharp_NetWork.Tools.Json.JsonTools.ToJson(msg));
        _lookAtMe.PushMsgToOther(msg, receivers.ToArray(), false, true);
        Debug.Log("用户尝试登陆信息回复：--- out --- ");

    }

    private void OnRoomVerify(object sender, string url)
    {
        Debug.Log("收到验证成功");
        CreatCode.CreatCode(url);     //返回生成二维码
        GameStarusIsReady();
    }
    /// <summary>
    /// 向服务器发送准备的信息,不发会项目一直在准备中
    /// </summary>
    public void GameStarusIsReady()
    {
        Debug.LogError(" --------------_GameStarusIsReady -------------- ");
        LookAtMe.PushGameStatus("1", true, true);
    }

    private void OnDestroy()
    {
        
        LookAtMe.Dispose();
    }
}
