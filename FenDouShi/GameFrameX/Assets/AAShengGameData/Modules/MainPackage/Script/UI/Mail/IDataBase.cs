public class IDataBase<T> : Singleton<T>
    where T : class
{
    public virtual void OnLoginSuccess() { }    // 登录游戏
    public virtual void OnReconnect() { }       // 断线重连
    public virtual void OnLogoutSuccess() { }   // 换号/退出登录/退出游戏
    public virtual void OnRefreshOnZero() { }   // 凌晨0点刷新
}