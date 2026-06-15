

using common;
using msg.role;

public class  RoleModel : BaseModel
{

    //角色信息
    private RoleInfoResp mRoleInfo;

    // 初始化调用
    protected override void onInit()
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