
using msg.login;
public class UserInfoManager:Singleton<UserInfoManager>
{
    private LoginResp _userInfo;
    public void SetUserInfo(LoginResp userInfo)
    {
        this._userInfo = userInfo;
    }
    public LoginResp userInfo {
        get
        {
            return _userInfo;
        }
    }
}