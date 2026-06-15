

using msg.experience;

[ControlAttribute]
public class ExperienceViewControl : BaseControl<ExperienceViewControl>
{
    public ExperienceViewModel Model { get; private set; }

    // 初始化成功调用
    protected override void onInit()
    {
        Model = new ExperienceViewModel();
    }

    // 事件监听处理
    protected override void onEventListener()
    {
        on<ExperienceGetInfoResp>((uint)Cmd.ExperienceGetInfoResp, OnExperienceGetInfoResp);
    }

    // 清理数据调用
    protected override void onClear()
    {
    }

    // 登录成功后调用
    protected override void onLoginSuccess()
    {
        // 登录成功后请求经验信息
       // ReqExperienceGetInfoReq();
    }

    /// <summary>
    /// 请求经验信息
    /// </summary>
    public void ReqExperienceGetInfoReq()
    {
        ExperienceGetInfoReq req = new ExperienceGetInfoReq();
        Logger.PrintGreen("@@@@@@@@@@@@@@@@@@@@@@  ReqExperienceGetInfoReq()@@@@@@@@@@@@@@@@@@");
        SendNetMsg((uint)Cmd.ExperienceGetInfoReq, req);
    }

    /// <summary>
    /// 处理经验信息响应
    /// </summary>
    private void OnExperienceGetInfoResp(ExperienceGetInfoResp resp)
    {
        Logger.PrintDebug("ExperienceViewControl OnExperienceGetInfoResp resp:");
        Logger.PrintToJson(resp);
        if (resp != null && resp.Infoes != null)
        {
            // 设置模型数据
            Model.SetExperienceInfoList(resp.Infoes);
        }
    }
}
