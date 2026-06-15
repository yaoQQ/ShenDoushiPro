using msg.role;

public class RoleData : IDataBase<RoleData>
{
    //角色信息
    private RoleInfoResp mRoleInfo;

    public override void OnLoginSuccess()
    {

    }

    /// <summary>
    /// 设置角色信息
    /// </summary>
    /// <param name="roleInfo"></param>
    public void setRoleInfo(RoleInfoResp roleInfo)
    {
        mRoleInfo = roleInfo;
    }

    /// <summary>
    /// 获取角色信息
    /// </summary>
    /// <returns></returns>
    public RoleInfoResp getRoleInfo()
    {
        return mRoleInfo;
    }

    /// <summary>
    /// 设置角色等级
    /// </summary>
    /// <param name="level"></param>
    public void setRoleLevel(int level)
    {
        if (mRoleInfo != null)
        {
            mRoleInfo.Level = level;
        }
        EventManager.Instance.Dispatch(EEventType.EventRoleInfoUpdate, level);
    }
}