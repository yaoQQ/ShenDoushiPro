using System;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class GActionManager : MonoBehaviour
{
    private static GActionManager _instance;
    private List<GAction> _runningActions = new List<GAction>();

    public static GActionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GActionManager");
                _instance = go.AddComponent<GActionManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    void Update()
    {
        for (int i = _runningActions.Count - 1; i >= 0; i--)
        {
            GAction action = _runningActions[i];

            if (action == null || !IsActionValid(action))
            {
                _runningActions.RemoveAt(i);
                continue;
            }

            action.Update(Time.deltaTime);

            if (action.IsDone)
            {
                _runningActions.RemoveAt(i);
            }
        }
    }

    private bool IsActionValid(GAction action)
    {
        if (action == null) return false;

        if (action.Target != null && action.Target.isDisposed)
        {
            action.IsDone = true;
            return false;
        }

        return true;
    }

    public void RunAction(GObject target, GAction action)
    {
        if (target == null || target.isDisposed || action == null)
            return;

        action.SetTargetSafe(target);
        action.Start();

        if (!action.IsDone)
        {
            _runningActions.Add(action);
        }
    }

    // 停止特定对象的特定动作
    public void StopAction(GObject target, GAction action)
    {
        if (target == null || action == null) return;

        _runningActions.RemoveAll(a => a == action && a.Target == target);
        action.Stop();
    }

    // 停止特定对象的所有动作
    public void StopAllActions(GObject target)
    {
        if (target == null) return;

        var actionsToStop = _runningActions.FindAll(a => a != null && a.Target == target);
        foreach (var action in actionsToStop)
        {
            action.Stop();
        }
        _runningActions.RemoveAll(a => a != null && a.Target == target);
    }

    // 停止所有动作
    public void StopAllActions()
    {
        foreach (var action in _runningActions)
        {
            if (action != null)
            {
                action.Stop();
            }
        }
        _runningActions.Clear();
    }

    // 暂停特定对象的所有动作
    public void PauseAllActions(GObject target)
    {
        if (target == null) return;

        foreach (var action in _runningActions)
        {
            if (action != null && action.Target == target)
            {
                // 这里可以添加暂停逻辑，如果需要的话
            }
        }
    }

    // 恢复特定对象的所有动作
    public void ResumeAllActions(GObject target)
    {
        if (target == null) return;

        foreach (var action in _runningActions)
        {
            if (action != null && action.Target == target)
            {
                // 这里可以添加恢复逻辑，如果需要的话
            }
        }
    }

    public bool HasRunningActions(GObject target)
    {
        if (target == null) return false;

        foreach (var action in _runningActions)
        {
            if (action != null && action.Target == target && !action.IsDone)
            {
                return true;
            }
        }
        return false;
    }

    // 获取特定对象的所有运行中动作
    public List<GAction> GetRunningActions(GObject target)
    {
        List<GAction> result = new List<GAction>();
        if (target == null) return result;

        foreach (var action in _runningActions)
        {
            if (action != null && action.Target == target && !action.IsDone)
            {
                result.Add(action);
            }
        }
        return result;
    }
}

public static class GActions
{
    public static GDelayTime Delay(float duration) => new GDelayTime(duration);
    public static GMoveTo MoveTo(Vector2 position, float duration) => new GMoveTo(position, duration);
    public static GScaleTo ScaleTo(Vector2 scale, float duration) => new GScaleTo(scale, duration);
    public static GFadeTo FadeTo(float alpha, float duration) => new GFadeTo(alpha, duration);
    public static GRotateTo RotateTo(float rotation, float duration) => new GRotateTo(rotation, duration);
    public static GCallFunc CallFunc(Action callback) => new GCallFunc(callback);
    public static GSequence Sequence(params GAction[] actions) => new GSequence(actions);
    public static GSpawn Spawn(params GAction[] actions) => new GSpawn(actions);
    public static GRepeat Repeat(GAction action, int count = -1) => new GRepeat(action, count);
    public static GBlink Blink(float duration, float interval = 0.2f) => new GBlink(duration, interval);
    public static GBounce Bounce(float height, float duration) => new GBounce(height, duration);
    
    // 运行动作
    public static void Run(GObject target, GAction action)
    {
        if (target == null || target.isDisposed) return;
        GActionManager.Instance.RunAction(target, action);
    }
    
    // 运行动作（带完成回调）
    public static void Run(GObject target, GAction action, Action onComplete)
    {
        if (target == null || target.isDisposed)
        {
            onComplete?.Invoke();
            return;
        }
        
        var sequence = new GSequence(action, new GCallFunc(onComplete));
        GActionManager.Instance.RunAction(target, sequence);
    }
    
    // 停止特定对象的特定动作
    public static void Stop(GObject target, GAction action)
    {
        if (target == null || action == null) return;
        GActionManager.Instance.StopAction(target, action);
    }
    
    // 停止特定对象的所有动作
    public static void StopAll(GObject target)
    {
        if (target == null) return;
        GActionManager.Instance.StopAllActions(target);
    }
    
    // 检查特定对象是否有运行中的动作
    public static bool HasRunningActions(GObject target)
    {
        if (target == null) return false;
        return GActionManager.Instance.HasRunningActions(target);
    }
    
    // 获取特定对象的所有运行中动作
    public static List<GAction> GetRunningActions(GObject target)
    {
        if (target == null) return new List<GAction>();
        return GActionManager.Instance.GetRunningActions(target);
    }
}