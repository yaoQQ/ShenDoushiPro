

using msg.func;


[ControlAttribute]
public class SystemOpenControl : BaseControl<SystemOpenControl>
{
    public SystemOpenModel Model { get; private set; }

    // 初始化成功调用
    protected override void onInit()
    {
        Model = new SystemOpenModel();
    }

    // 事件监听处理
    protected override void onEventListener()
    {
        on<FuncLoginInfoResp>((uint)Cmd.FuncLoginInfoResp, OnFuncLoginInfoResp);// 功能登录信息回调
        on<FuncOpenResp>((uint)Cmd.FuncOpenResp, OnFuncOpenResp);// 功能新开启回调
        on<FuncGetInfoResp>((uint)Cmd.FuncGetInfoResp, OnFuncGetInfoResp);// 功能信息查询回调
        on<FuncManualInfoResp>((uint)Cmd.FuncManualInfoResp, OnFuncManualInfoResp);// 功能手册查询回调
        on<FuncManualUpdateResp>((uint)Cmd.FuncManualUpdateResp, OnFuncManualUpdateResp);// 功能手册更新回调
    }

    protected override void onLoginSuccess()
    {
        OnReqTaskInfo();
        OnReqSystemInfo();
    }

    //    req
    public void OnReqTaskRewardInfo(int functionId)
    {
        var msg = new FuncManualRewardReq() { functionId = functionId };
        SendNetMsg((uint)Cmd.FuncManualRewardReq, msg);
    }

    public void OnReqSystemInfo()
    {
        var msg = new FuncGetInfoReq();
        SendNetMsg((uint)Cmd.FuncGetInfoReq, msg);
    }

    public void OnReqTaskInfo()
    {
        var msg = new FuncManualInfoReq();
        SendNetMsg((uint)Cmd.FuncManualInfoReq, msg);
    }

    public void OnDrawSystemOpenReward(int funcId)
    {
        var msg = new FuncManualRewardReq() { functionId = funcId };
        SendNetMsg((uint)Cmd.FuncManualRewardReq, msg);
    }

    private void OnFuncLoginInfoResp(FuncLoginInfoResp resp)
    {
        Model.InitData(resp.openfunctionIds);
    }

    private void OnFuncOpenResp(FuncOpenResp resp)
    {
        Model.OnNewSystemOpen(resp.functionIds);
    }

    private void OnFuncGetInfoResp(FuncGetInfoResp resp)
    {
        Model.InitData(resp.openfunctionIds);
    }


    private void OnFuncManualInfoResp(FuncManualInfoResp resp)
    {
        Model.InitTaskInfo(resp.Infoes);
    }

    private void OnFuncManualUpdateResp(FuncManualUpdateResp resp)
    {
        Model.UpdateTaskInfo(resp.Infoes);
    }

    // 清理数据调用
    protected override void onClear()
    {
    }

}
