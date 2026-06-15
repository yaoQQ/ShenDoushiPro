

using common;
using msg.role;
using roleLevel;
using System.Diagnostics;


[ControlAttribute]
public class RoleControl : BaseControl<RoleControl>
{
    public  RoleModel Model { get; private set; }

    // 初始化成功调用
    protected override void onInit()
    {
        Model = new RoleModel();
    }

    protected override void onLoginSuccess()
    {
        ReqRoleInfoReq();
    }


    // 事件监听处理
    protected override void onEventListener()
    {
        //角色信息返回
        on<RoleInfoResp>((uint)Cmd.RoleInfoResp, RoleInfoResp);
        //角色升级返回
        on<RoleLevelUpResp>((uint)Cmd.RoleLevelUpResp, RoleLevelUpResp);
        //角色战力对比返回
        on<RolePowerCompareResp>((uint)Cmd.RolePowerCompareResp, RolePowerCompareResp);
        //角色战力对比详情返回
        on<RolePowerCompareDetailResp>((uint)Cmd.RolePowerCompareDetailResp, RolePowerCompareDetailResp);
        //获取位置信息返回
        on<RoleInfoGetLocationResp>((uint)Cmd.RoleInfoGetLocationResp, RoleInfoGetLocationResp);
        //重命名角色
        on<RoleRenameResp>((uint)Cmd.RoleRenameResp, RoleRenameResp);
    }

    /// <summary>
    /// 请求角色信息
    /// </summary>
    public void ReqRoleInfoReq()
    {
        var req = new RoleInfoReq();
        Logger.PrintDebug("请求角色信息 UserInfoManager.Instance.userInfo="+ UserInfoManager.Instance.userInfo);
        req.roleId = UserInfoManager.Instance.userInfo.Rid;
        SendNetMsg((uint)Cmd.RoleInfoReq, req);
    }

    /// <summary>
    /// 请求战力对比
    /// </summary>
    /// <param name="roleId">目标角色ID</param>
    public void ReqRolePowerCompareReq(long roleId)
    {
        var req = new RolePowerCompareReq { roleId = roleId };
        SendNetMsg((uint)Cmd.RolePowerCompareReq, req);
    }

    /// <summary>
    /// 请求战力对比详情
    /// </summary>
    /// <param name="targetRoleId">目标角色ID</param>
    /// <param name="modType">模块类型</param>
    public void ReqRolePowerCompareDetailReq(long targetRoleId, int modType)
    {
        var req = new RolePowerCompareDetailReq { targetRoleId = targetRoleId, modType = modType };
        SendNetMsg((uint)Cmd.RolePowerCompareDetailReq, req);
    }

    /// <summary>
    ///  自动定位请求
    /// </summary>
    public void ReqRoleInfoGetLocationReq()
    {
        var req = new RoleInfoGetLocationReq();
        SendNetMsg((uint)Cmd.RoleInfoGetLocationReq, req);
    }

    /// <summary>
    /// 修改地区请求
    /// </summary>
    /// <param name="openLocation">是否公开位置</param>
    /// <param name="provinceCode">省份代码</param>
    /// <param name="cityCode">城市代码</param>
    public void ReqRoleInfoUpdateLocationReq(bool openLocation, int provinceCode, int cityCode)
    {
        var req = new RoleInfoUpdateLocationReq
        {
            openLocation = openLocation,
            Location = new LocationInfo { provinceCode = provinceCode, cityCode = cityCode }
        };
        SendNetMsg((uint)Cmd.RoleInfoUpdateLocationReq, req);
    }

    /// <summary>
    /// 请求隐藏进度
    /// </summary>
    /// <param name="hiddenProgress">是否隐藏进度</param>
    public void ReqRoleInfoHiddenProgressReq(bool hiddenProgress)
    {
        var req = new RoleInfoHiddenProgressReq { hiddenProgress = hiddenProgress };
        SendNetMsg((uint)Cmd.RoleInfoHiddenProgressReq, req);
    }

    /// <summary>
    /// 请求角色重命名
    /// </summary>
    /// <param name="newName">新名称</param>
    public void ReqRoleRenameReq(string newName)
    {
        var req = new RoleRenameReq { Name = newName };
        SendNetMsg((uint)Cmd.RoleRenameReq, req);
    }



    ///------------------------服务返回----------------

    /// <summary>
    /// 角色信息返回
    /// </summary>
    /// <param name="buffer"></param>
    private void RoleInfoResp(RoleInfoResp resp)
    {
        Logger.PrintDebug("收到角色信息返回 resp=" + resp);
        Model.setRoleInfo(resp);

        EventManager.Instance.Dispatch(EEventType.EventRoleInfoUpdate, resp);
    }

    /// <summary>
    /// 角色升级成功返回
    /// </summary>
    /// <param name="buffer"></param>
    private void RoleLevelUpResp(RoleLevelUpResp resp)
    {
        Logger.PrintLog("角色升级成功，新等级：" + resp.newLevel);
        //设置升级等级
        Model.setRoleLevel(resp.newLevel);
        //设置开放功能列表
        SystemOpenControl.Instance.Model.setOpenfunctionIds(resp.openfunctionIds);
        // 挂机触发的升级延迟打开升级界面
        if (!OnHookControl.Instance.isHookLevelUp)
        {
            UIViewManager.Instance.Show(UIViewEnum.RoleLevelView, resp.newLevel);
        }
    }

    /// <summary>
    /// 角色战力对比返回
    /// </summary>
    /// <param name="resp"></param>
    private void RolePowerCompareResp(RolePowerCompareResp resp)
    {
        Logger.PrintLog("左边角色战力：" + resp.Left?.roleInfo?.fightPower + " 右边角色战力：" + resp.Right?.roleInfo?.fightPower);
        // 这里可以添加战力对比的处理逻辑} 
        EventManager.Instance.Dispatch(EEventType.RolePowerCompareResp, resp);
    }


    /// <summary>
    /// 角色战力对比详情返回
    /// </summary>
    /// <param name="resp"></param>
    private void RolePowerCompareDetailResp(RolePowerCompareDetailResp resp)
    {
        Logger.PrintDebug("收到角色战力对比详情返回");
        // 这里可以添加战力对比详情的处理逻辑
        EventManager.Instance.Dispatch(EEventType.RolePowerCompareDetailResp, resp);
    }

    /// <summary>
    /// 获取位置信息返回
    /// </summary>
    /// <param name="resp"></param>
    private void RoleInfoGetLocationResp(RoleInfoGetLocationResp resp)
    {
        Logger.PrintDebug("收到位置信息返回");
        if (resp.Location != null)
        {
            Logger.PrintLog($"省份代码：{resp.Location.provinceCode}, 城市代码：{resp.Location.cityCode}");
            // 可以在这里更新位置信息
            EventManager.Instance.Dispatch(EEventType.RoleInfoGetLocationResp, resp);
        }
    }

    /// <summary>
    ///重新命名返回
    /// </summary>
    /// <param name="resp"></param>
    private void RoleRenameResp(RoleRenameResp resp)
    {
        Logger.PrintDebug("重新命名返回");

        EventManager.Instance.Dispatch(EEventType.RoleRenameResp, resp);

    }

   


}
