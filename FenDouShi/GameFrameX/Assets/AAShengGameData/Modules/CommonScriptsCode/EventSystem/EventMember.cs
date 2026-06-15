using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventMember : IClassPoolItem
{
    public int eventType;
    List<OnEventLister> eventListeners = new();
    List<OnEventLister> toDeleteList;
    List<OnEventLister> toAddList;
    bool isSending;

    public int ListenerCount => eventListeners.Count;

    public void Send(EventSysArgsBase args)
    {
        if (eventListeners != null)
        {
            isSending = true;
            foreach (OnEventLister listener in eventListeners)
            {
#if !UNITY_EDITOR
                try
                {
#endif
                    listener(args);
#if !UNITY_EDITOR
                }
                catch (Exception e)
                {

                    string _eventType = string.Empty;
                    if (EEventTypeHelper.Contains(eventType))
                    {
                        _eventType = ((EEventType)eventType).ToString();
                    }
                    else
                    {
                        _eventType = eventType.ToString();
                    }
                    Logger.PrintError($"[事件]触发事件出错:{_eventType}, 监听:{listener.Method}\n{e}\n\n");
                }
#endif
            }
        }
        isSending = false;

        DeleteCache();
        AddCache();
    }

    // 添加
    public void Add(OnEventLister listener)
    {
        if (!isSending)
        {
            ReallyAdd(listener);
        }
        else
        {
            toAddList ??= ClassPoolManger.Instance.Get<List<OnEventLister>>();
            toAddList.Add(listener);
        }
    }

    void ReallyAdd(OnEventLister listener)
    {
        if (!eventListeners.Contains(listener))
        {
            eventListeners.Add(listener);
        }
        else
        {
            Logger.PrintError($"[事件系统]重复添加回调");
        }
    }

    void AddCache()
    {
        if (toAddList != null)
        {
            for (int i = 0; i < toAddList.Count; i++)
            {
                ReallyAdd(toAddList[i]);
            }
            toAddList.Clear();
            toAddList.RecycleToPool();
            toAddList = null;
        }
    }

    // 删除
    public void Remove(OnEventLister listener)
    {
        if (!isSending)
        {
            ReallyRemove(listener);
        }
        else
        {
            toDeleteList ??= ClassPoolManger.Instance.Get<List<OnEventLister>>();
            toDeleteList.Add(listener);
        }
    }

    void ReallyRemove(OnEventLister listener)
    {
        if (eventListeners.Contains(listener))
        {
            eventListeners.Remove(listener);
        }
    }

    void DeleteCache()
    {
        if (toDeleteList != null)
        {
            for (int i = 0; i < toDeleteList.Count; i++)
            {
                ReallyRemove(toDeleteList[i]);
            }
            toDeleteList.Clear();
            toDeleteList.RecycleToPool();
            toDeleteList = null;
        }
    }

    void IClassPoolItem.OnRecycle()
    {
        eventType = 0;
        eventListeners.Clear();
        if (toAddList != null)
        {
            toAddList.Clear();
            toAddList.RecycleToPool();
            toAddList = null;
        }
        if (toDeleteList != null)
        {
            toDeleteList.Clear();
            toDeleteList.RecycleToPool();
            toDeleteList = null;
        }
        isSending = false;
    }

    public override string ToString()
    {
        if (EEventTypeHelper.Contains(eventType))
        {
            return $"类型：{(EEventType)eventType}, 绑定数量：{eventListeners.Count}";
        }
        else
        {
            return $"类型：{eventType}, 绑定数量：{eventListeners.Count}";
        }
    }
}
