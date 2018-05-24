using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Qy_CSharp_NetWork.Tools.Json;
using Qy_CSharp_NetWork.Component;


public class MsgTryLoginRes             //用户尝试登陆信息
{
    public int type
    {
        get { return (int)m_type; }
    }
    public object data
    {
        get { return m_data; }
    }
    private GAME_MESSAGE_TYPE m_type = GAME_MESSAGE_TYPE.TRY_LOGIN_RESP;
    private RankInfo m_data = new RankInfo();
    public void SetMessage(int modelType, int steps, int rankNum)
    {
        m_data.model = modelType;
        m_data.steps = steps;
        m_data.rankNum = rankNum;
    }
    public class RankInfo
    {
        public int model = 0;
        public int steps = 0;
        public int rankNum = 0;
    }
}
public enum GAME_MESSAGE_TYPE
{
    TRY_LOGIN_RESP,
    TRY_LOGOUT_RESP,
}

public class LoginInfo
{

}
public class LogoutInfo
{

}

public class ScoreInfo
{

}

public delegate void RoomVerifyCompleteDelegate(object sender, string message);
public delegate void SomeOneIsTryLoginDelegate(string receiver, MsgTryLoginRes tryLogInfo);
public delegate void SomeOneIsLoginDelegate(object sender, LoginInfo newUser);
public delegate void SomeOneIsLogoutDelegate(object sender, LogoutInfo logOutUser);



public delegate void SomeOneUpMessageDelegate(object sender, ScoreInfo scoreInfo);
public class GameMessageDataHandle : MonoBehaviour
{
    public event RoomVerifyCompleteDelegate RoomVerifyCompleteEvent;
    public Action<string> UpdateLoginPlayer;

    public void NetEventFir(object sender, LAMEventArgs evAgs)
    {
        switch (evAgs.status)
        {
            case DATA_STATUS_CODE.DATA_BUILD_ROOM_T:
                Debug.LogError("收到 建房 回复 成功！！！！！  :" + evAgs.message);
                break;
            case DATA_STATUS_CODE.DATA_BUILD_ROOM_F:
                Debug.LogError("收到 建房 回复 失败！！！！！  :" + evAgs.message);
                break;
            case DATA_STATUS_CODE.DATA_ROOM_VERIFY_T:
                Debug.LogError("收到 验证 回复 成功！！！！！  :" + evAgs.message);
                if (RoomVerifyCompleteEvent != null)
                    RoomVerifyCompleteEvent(this, evAgs.message);
                break;
            case DATA_STATUS_CODE.DATA_ROOM_VERIFY_F:
                Debug.LogError("收到 验证 回复 失败！！！！！  :" + evAgs.message);
                break;
            case DATA_STATUS_CODE.DATA_ROOM_STATUS_T:
                Debug.LogError("收到 更新状态 回复 成功！！！！！  :" + evAgs.message);
                if (UpdateStatusEvent!=null)
                {
                    UpdateStatusEvent(evAgs.message);
                }
                else
                {
                    Debug.LogError("无状态更新");
                }
                break;
            case DATA_STATUS_CODE.DATA_ROOM_STATUS_F:
                Debug.LogError("收到 更新状态 回复 失败！！！！！  :" + evAgs.message);
                break;
            case DATA_STATUS_CODE.DATA_CUSTOM_MSG_T:
                Debug.LogError("收到 自定义信息 成功！！！！！  :" + evAgs.message);
                _UserMessageHandle(evAgs.message);//用户数据处理
                //UpdateLoginPlayer
                break;
            case DATA_STATUS_CODE.DATA_CUSTOM_MSG_F:
                Debug.LogError("收到 自定义信息 失败！！！！！  :" + evAgs.message);
                break;
            case DATA_STATUS_CODE.DATA_SUMMARIZE_T:
                Debug.LogError("收到 汇总数据 回复 成功！！！！！  :" + evAgs.message);
                break;
            case DATA_STATUS_CODE.DATA_SUMMARIZE_F:
                Debug.LogError("收到 汇总数据 回复 失败！！！！！  :" + evAgs.message);
                break;
            case DATA_STATUS_CODE.DATA_AWARD_LIST_T:
                Debug.LogError("收到 奖励列表 回复 成功！！！！！  :" + evAgs.message);
                _OnGetAwadeMessage(evAgs.message);
                break;
            case DATA_STATUS_CODE.DATA_AWARD_LIST_F:
                Debug.LogError("收到 奖励列表 回复 失败！！！！！  :" + evAgs.message);
                _OnGetAwadeMessage(null);
                break;
            case DATA_STATUS_CODE.ERROR_TRIGGER_CODE:
                Debug.LogError("链接 出现 错误！！！！！  :" + evAgs.message);
                break;
            case DATA_STATUS_CODE.LINK_USER_BROKEN:
                Debug.LogError("收到 H5 用户断开链接的信息    !!! ----:" + evAgs.message);
                JsonData jdata = JsonTools.GetJsonData(evAgs.message);
                LogoutInfo logoutInfo = new LogoutInfo();
                break;
        }
    }
    /// <summary>
    /// 更新状态
    /// </summary>
    public event Action<string> UpdateStatusEvent;


    public event SomeOneIsTryLoginDelegate SomeOneTryLoginEvent;
    public event SomeOneIsLoginDelegate SomeOneIsLoginEvent;
    public event SomeOneIsLogoutDelegate SomeOneIsLogoutEvent;
    public event SomeOneUpMessageDelegate SomeOneIsUpMessageEvent;
    private List<LoginInfo> _loginInfoList = new List<LoginInfo>();
    private List<LogoutInfo> _logOutInfoList = new List<LogoutInfo>();
    public List<EventArgs> gameMessageList { get { return _gameMessageList; } }
    private List<EventArgs> _gameMessageList = new List<EventArgs>();
    /// <summary>
    /// 收到玩家信息进行处理
    /// </summary>
    /// <param name="message"></param>
    private void _UserMessageHandle(string message)
    {
        JsonData jsonData = JsonTools.GetJsonData(message);

        print(jsonData);
    }



    public event EventHandler RcvMsgOverEvent;
    public void MyGameOver()
    {
        Debug.Log("is game over ???????????");
        
    }
    void _OnGetAwadeMessage(string awardMessage)
    {
        print("获取奖励");
        
        
    }
}
