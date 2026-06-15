using msg.redDot;

[Control]
public class RedPointControl : BaseControl<RedPointControl>
{
    public RedPointModel Model { get; private set; }

    // 初始化成功调用
    protected override void onInit()
    {
        Model = new RedPointModel();
    }

    // 事件监听处理
    protected override void onEventListener()
    {
        on<RedDotGetInfoResp>((uint)Cmd.RedDotGetInfoResp, RedDotGetInfoResp);
        on<RedDotUpdateResp>((uint)Cmd.RedDotUpdateResp, RedDotUpdateResp);
    }

    // 清理数据调用
    protected override void onClear()
    {
    }

    protected override void onLoginSuccess()
    {
        RedPointManager.Instance.InitExcel();
        RedDotGetInfoReq(0);
    }

    // 红点获取信息请求, 0表示请求全部
    public void RedDotGetInfoReq(int id = 0)
    {
        Logger.PrintLog($"[红点]红点获取信息请求id:{id}");
        RedDotGetInfoReq req = new RedDotGetInfoReq()
        {
            Id = id,
        };
        SendNetMsg((int)Cmd.RedDotGetInfoReq, req);
    }

    void RedDotGetInfoResp(RedDotGetInfoResp resp)
    {
        Logger.PrintLog($"[红点]获取红点信息回调:{resp.Keys.Count}");
        foreach (var i in resp.Keys)
        {
            Logger.PrintLog($"[红点] id:{i.Id} subId:{i.subId}");
            RedPointManager.Instance.SetState(i.Id, i.subId == 1);
        }
    }

    // 更新红点信息回调
    void RedDotUpdateResp(RedDotUpdateResp resp)
    {
        Logger.PrintLog($"[红点]更新红点信息回调:{resp.Keys.Count}");
        foreach (var i in resp.Keys)
        {
            Logger.PrintLog($"[红点] id:{i.Id} subId:{i.subId}");
            RedPointManager.Instance.SetState(i.Id, i.subId == 1);
        }
    }
}
