using msg.login;

public class  LoginModel : BaseModel
{
    LoginResp _loginResp;
    public long RoleId => _loginResp.Rid;
    //服务器时间
    public long ServerTime { get { return _loginResp.Ms; } }

    public LoginResp LoginResp
    {
        get { return _loginResp; }
        set { _loginResp = value; }
    }
    // 初始化调用
    protected override void onInit()
    {
        
    }

    // 监听事件
    protected override void onEventListener()
    {
    }
}