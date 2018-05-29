using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public List<Trigger> manager;
    private void Awake()
    {
        Trigger[] _triggers = GameObject.FindObjectsOfType<Trigger>();

        for (int i = 0; i < _triggers.Length; i++)
        {
            Manager.Add(_triggers[i]);
        }
    }
    public List<Trigger[]> triggers;

    public List<Trigger[]> Triggers
    {
        get
        {
            if (triggers == null)
            {
                triggers = new List<Trigger[]>();
            }
            return triggers;
        }
    }

    private void Update()
    {
        if (Manager.Count != 0)
        {
            CheckTrigger();
        }
    }


    public List<Trigger> Manager
    {
        get
        {
            if (manager == null)
            {
                manager = new List<Trigger>();
            }
            return manager;
        }
    }

    public void CheckTrigger()
    {
        for (int i = 0; i < Manager.Count; i++)
        {
            for (int j = 1; j < Manager.Count; j++)
            {
                if (i != j)
                {
                    if (checkTwoTrigger(Manager[i], Manager[j]))
                    {
                        ///如果碰撞过
                        if (CheckOrTrigger(Manager[i], Manager[j])!=-1)
                        {
                        }
                        else
                        {
                            Trigger[] temp = new Trigger[2];
                            Manager[i].OnTrigger(Manager[j]);
                            Manager[j].OnTrigger(Manager[i]);
                            temp[0] = Manager[i];
                            temp[1] = Manager[j];
                            Triggers.Add(temp);
                        }

                    }
                    else
                    {
                        int index = CheckOrTrigger(Manager[i], Manager[j]);
                        if (index != -1)
                        {
                            Triggers.RemoveAt(index);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 判断两个物体是否已经碰撞了
    /// </summary>
    /// <returns></returns>
    public int CheckOrTrigger(Trigger v1, Trigger v2)
    {
        if (Triggers.Count == 0)
        {

            return -1;
        }

        for (int index = 0; index < Triggers.Count; index++)
        {
            Trigger[] item = Triggers[index];
            int status = 0;

            for (int i = 0; i < item.Length; i++)
            {
                if (item[i] == v1 || item[i] == v2)
                {
                    status++;
                }

                if (status == 2)
                {
                    return index;
                }

            }
        }

        return -1;
    }

    public bool checkTwoTrigger(Trigger v1, Trigger v2)
    {
        Rect rc1 = v1.rect;

        Rect rc2 = v2.rect;

        float verticalDistance;    //垂直距离  
        float horizontalDistance;  //水平距离  
        verticalDistance = Mathf.Abs(rc1.x - rc2.x);
        horizontalDistance = Mathf.Abs(rc1.y - rc2.y);

        float verticalThreshold;   //两矩形分离的垂直临界值  
        float horizontalThreshold; //两矩形分离的水平临界值  
        verticalThreshold = (rc1.height + rc2.height) / 2;
        horizontalThreshold = (rc1.width + rc2.height) / 2;

        if (verticalDistance > verticalThreshold || horizontalDistance > horizontalThreshold)
            return false;

        return true;
    }
}

