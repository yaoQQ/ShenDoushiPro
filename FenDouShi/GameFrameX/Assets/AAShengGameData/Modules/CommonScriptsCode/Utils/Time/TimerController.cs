using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TimerController
{
    public const int TimerMinInterval_ms = 50;
    public  TimerController()
    {

    }
    public class Timer
    {
        public bool isDefer = false;
        public bool paused = false;

        public System.Object key;
        /// <summary>
        /// 单位：100纳秒;
        /// </summary>
        public long interval_ns;
        public int maxCount;
        public Action<int> onTimeFun;
        /// <summary>
        /// 忽略的时间暂停类型;
        /// </summary>
        public TimePauseType ignoreType;
        int curCount = 0;
        long totalTime = 0;

        public Timer(System.Object p_key, long p_interval_ms, int p_maxCount, Action<int> p_onTimeFun,bool p_isDefer, TimePauseType p_ignoreType = TimePauseType.None)
        {

            key = p_key; interval_ns = p_interval_ms * 10000; maxCount = p_maxCount; onTimeFun = p_onTimeFun; isDefer = p_isDefer; ignoreType = p_ignoreType;
        }
        public void resetTimerData(long p_interval_ms, int p_cmaxCount, Action<int> p_onTimeFun, bool p_isDefer, TimePauseType p_ignoreType = TimePauseType.None)
        {
            interval_ns = p_interval_ms * 10000; maxCount = p_cmaxCount; onTimeFun = p_onTimeFun; isDefer = p_isDefer; ignoreType = p_ignoreType;
            reset();
        }
        public bool Execute(long deltaTime)
        {
            paused=CheckPause();
            if (!paused)
            {
                if (key == null) return false;
                long oleTime = totalTime;
                totalTime += deltaTime;
                long newTime = totalTime;
                long targetTime = interval_ns * (curCount + 1);
                if (interval_ns<0)
                {
                    curCount++;

                    if (curCount > maxCount && maxCount!=-1)
                    {
                        //计时器结束;
                        return false;
                    }
                    else
                    {
                        onTimeFun.Invoke(curCount);
                    }
                }else{
                
                    if (oleTime < targetTime && newTime >= targetTime)
                    {
                      
                        //Debug.Log("触发时间点：" + totalTime);
                        curCount++;

                        if (curCount > maxCount && maxCount != -1)
                        {
                            //计时器结束;
                            return false;
                        }
                        else
                        {
                            onTimeFun.Invoke(curCount);
                        }
                    }
                }
               
            }
            return true;
        }
        bool CheckPause()
        {
            switch (ignoreType)
            {
                case TimePauseType.None:
                    if (GlobalTimeManager.Instance.CurPauseType != TimePauseType.None)
                    {
                        return true;
                    }
                    break;
                case TimePauseType.All:
                    return false;
                case TimePauseType.SceneExceptPlot:
                    if (GlobalTimeManager.Instance.CurPauseType != TimePauseType.None || GlobalTimeManager.Instance.CurPauseType != TimePauseType.SceneExceptPlot)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }
        /// <summary>
        /// 重置时间;
        /// </summary>
        public void reset()
        {
            totalTime = 0;
            curCount = 0;
           
        }
        public void Clear()
        {
            key = null; onTimeFun = null; 
        }
    }
    Dictionary<System.Object, Timer> timerDic = new Dictionary<object, Timer>();
    List<Timer> timerList = new List<Timer>();
    Dictionary<System.Object, Timer> timerDeferDic = new Dictionary<object, Timer>();
    List<Timer> timerDeferList = new List<Timer>();



    public void Init()
    {
        timerDic.Clear();
        timerList.Clear();
        timerDeferDic.Clear();
        timerDeferList.Clear();
    }
    public int lastDeltaTime;
    public void Execute(long deltaTime)
    {
        lastDeltaTime =(int)(deltaTime/ 10000f);
        for (int i = timerList.Count-1; i >=0; i--)
        {
            if (i >= timerList.Count)
                continue;
            Timer timer=timerList[i];
            if (!timer.Execute(deltaTime))
            {
                RemoveTimer(timer);
            }
            if (timer.onTimeFun==null)
            {
                RemoveTimer(timer);
            }
        }
       
    }
    public void DeferExecute(long deltaTime)
    {

        for (int i = timerDeferList.Count - 1; i >= 0; i--)
        {
           
            Timer timer = timerDeferList[i];
             //Debug.LogError(timer.key.ToString());
            if (!timer.Execute(deltaTime))
            {
                RemoveTimer(timer);
            }
            if (timer.onTimeFun == null)
            {
                RemoveTimer(timer);
            }
        }
    }

    /// <summary>
    /// 添加或更新一个计时器
    /// </summary>
    /// <param name="p_key">计时器唯一标识键值，用于查找和管理计时器</param>
    /// <param name="p_interval_ms">计时器触发间隔时间（毫秒）：
    ///     - 正值表示固定时间间隔触发（单位：毫秒）
    ///     - -1 表示按帧触发（每帧执行）
    ///     - 其他负值无效</param>
    /// <param name="p_maxCount">最大触发次数：
    ///     - 正整数：计时器达到指定次数后自动移除
    ///     - -1：无限循环，直到手动移除</param>
    /// <param name="p_onTimeFun">计时器触发时的回调函数，参数为当前触发次数（从1开始）</param>
    /// <param name="p_isDefer">是否延迟执行：
    ///     - true：在LateUpdate阶段执行
    ///     - false：在Update阶段执行（默认）</param>
    /// <param name="p_ignoreType">计时器忽略的暂停类型：
    ///     - None：任何暂停都会停止计时器（默认）
    ///     - All：不受任何暂停影响
    ///     - SceneExceptPlot：忽略场景暂停但响应剧情暂停</param>
    /// <returns>是否添加成功：
    ///     - true：添加/更新成功
    ///     - false：参数无效或添加失败</returns>
    /// <remarks>
    /// 使用说明：
    /// 1. 时间间隔必须 ≥50ms（-1除外），否则会触发警告并返回false
    /// 2. 如果key已存在，会更新现有计时器的参数并重置状态
    /// 3. 回调函数中不应包含耗时操作，以免影响游戏性能
    /// 4. 按帧触发的计时器（p_interval_ms=-1）不受时间缩放影响
    /// </remarks>
    public bool AddTimer(System.Object p_key, long p_interval_ms, int p_maxCount, Action<int> p_onTimeFun, bool p_isDefer = false, TimePauseType p_ignoreType = TimePauseType.None)
    {
        if (p_interval_ms!=-1&&p_interval_ms < TimerMinInterval_ms)
        {
            Debug.Log("AddTimer,时间间隔必须大于50毫秒，否则等同0");
            return false;
        }
        if (p_key == null) return false;

        Dictionary<System.Object, Timer> tempTimerDic;
        List<Timer> tempTimerList; 

        if (p_isDefer)
        {
            tempTimerDic = timerDeferDic;
            tempTimerList = timerDeferList;
        }
        else
        {
            tempTimerDic = timerDic;
            tempTimerList = timerList;
        }

        Timer tempTimer;
        if (tempTimerDic.TryGetValue(p_key, out tempTimer))
        {
            //Logger.PrintWarning("timerController已存在key值：" , p_key.ToString());
            tempTimer.resetTimerData(p_interval_ms, p_maxCount, p_onTimeFun, p_isDefer, p_ignoreType);
        }
        else
        {
            Timer timer = new Timer(p_key, p_interval_ms, p_maxCount, p_onTimeFun, p_isDefer, p_ignoreType);
            tempTimerDic.Add(p_key, timer);
            tempTimerList.Add( timer);
        }
       
        return true;
    }
   
    public bool RemoveTimer(Timer timer)
    {
        if (timerDic.ContainsValue(timer))
        {
            timerDic.Remove(timer.key);
            timerList.Remove(timer);
            timer.Clear();
            return true;
        }
        else if (timerDeferDic.ContainsValue(timer))
        {
            timerDeferDic.Remove(timer.key);
            timerDeferList.Remove(timer);
            timer.Clear();
            return true;
        }
        return false;
    }
    public bool RemoveTimerByKey(System.Object key)
    {
       Timer timer;
        if (timerDic.TryGetValue(key,out timer))
        {
            timerDic.Remove(key);
            timerList.Remove(timer);
            timer.Clear();
            return true;
        }
        else if (timerDeferDic.TryGetValue(key, out timer))
        {
            timerDeferDic.Remove(key);
            timerDeferList.Remove(timer);
            timer.Clear();
            return true;
        }
        return false;
    }
    public Timer GetTimerByKey(System.Object p_key)
    {
        Timer timer;
        if(timerDic.TryGetValue(p_key, out timer))
        {
            return timer;
        }
        else if (timerDeferDic.TryGetValue(p_key, out timer))
        {
            return timer;
        }
        return null;
    }
    public bool CheckExistByKey(System.Object p_key)
    {
        if (timerDic.ContainsKey(p_key))
        {
            return true;
        }
        else if (timerDeferDic.ContainsKey(p_key))
        {
            return true;
        }
        return false;
    }
}
