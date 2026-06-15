using System;

/// <summary>
/// 全局生命周期
/// </summary>
public class GlobalLiftCycle : SingleMonobehaviour<GlobalLiftCycle>
{

    private Action<GlobalLiftCycle> _onDestroy;
    private Action<bool> _onApplicationPause;
    private Action<GlobalLiftCycle> _onApplicationQuit;


    /// <summary>
    /// 当销毁
    /// </summary>
    public event Action<GlobalLiftCycle> onDestroy
    {
        add => _onDestroy += value;
        remove => _onDestroy -= value;
    }

    /// <summary>
    /// 应用暂停
    /// </summary>
    public event Action<bool> onApplicationPause
    {
        add => _onApplicationPause += value;
        remove => _onApplicationPause -= value;
    }

    /// <summary>
    /// 应用退出
    /// </summary>
    public event Action<GlobalLiftCycle> onApplicationQuit
    {
        add => _onApplicationQuit += value;
        remove => _onApplicationQuit -= value;
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        _onApplicationPause?.Invoke(pauseStatus);
    }

    private void OnApplicationQuit()
    {
        _onApplicationQuit?.Invoke(this);
    }

    private void OnDestroy()
    {
        _onDestroy?.Invoke(this);
        ClearEvent();
    }

    private void ClearEvent()
    {
        _onDestroy = null;
        _onApplicationPause = null;
        _onApplicationQuit = null;
    }
}

