

using common;
using msg.pay;


[ControlAttribute]
public class RechargeControl : BaseControl<RechargeControl>
{
    public RechargeModel Model { get; private set; }

    // 初始化成功调用
    protected override void onInit()
    {
        Model = new RechargeModel();
    }


    // 事件监听处理
    protected override void onEventListener()
    {
        //充值信息返回
        on<PayInfoResp>((uint)Cmd.PayInfoResp, OnPayInfoResp);
        //充值订单返回
        on<PayOrderResp>((uint)Cmd.PayOrderResp, OnPayOrderResp);
        //直接充值信息返回
        on<DirectPayInfoResp>((uint)Cmd.DirectPayInfoResp, OnDirectPayInfoResp);
    }

    protected override void onLoginSuccess()
    {
        ReqDirectPayInfoReq();
        ReqPayInfoReq();
    }

    /// <summary>
    /// 请求充值信息
    /// </summary>
    public void ReqPayInfoReq()
    {
        var msg = new PayInfoReq();
        SendNetMsg((uint)Cmd.PayInfoReq, msg);
    }

    /// <summary>
    /// 请求充值订单
    /// </summary>
    /// <param name="payId"></param>
    /// <param name="args"></param>
    public void ReqPayOrderReq(int payId, int[] args = null)
    {
        var msg = new PayOrderReq();
        msg.payId = payId;
        msg.extLists = args;
        SendNetMsg((uint)Cmd.PayOrderReq, msg);
    }

    /// <summary>
    /// 代金卷充值
    /// </summary>
    /// <param name="payId"></param>
    /// <param name="args"></param>
    public void ReqVoucherPayReq(int payId, int[] args = null)
    {
        var req = new VoucherPayReq();
        req.payId = payId;
        req.extLists = args;
        SendNetMsg((uint)Cmd.VoucherPayReq, req);
    }

    /// <summary>
    /// 请求直接充值信息
    /// </summary>
    public void ReqDirectPayInfoReq()
    {
        var req = new DirectPayInfoReq();
        SendNetMsg((uint)Cmd.DirectPayInfoReq, req);
    }

    /// <summary>
    /// 充值信息响应
    /// </summary>
    /// <param name="msg"></param>
    private void OnPayInfoResp(PayInfoResp msg)
    {
        Model.SetPayInfo(msg);
    }

    /// <summary>
    /// 充值订单响应
    /// </summary>
    /// <param name="msg"></param>
    private void OnPayOrderResp(PayOrderResp msg)
    {
        // TODO: 处理充值订单响应
        Logger.PrintDebug("OnPayOrderResp");
        
    }   
    
    /// <summary>
    /// // 直接充值信息响应
    /// </summary>
    /// <param name="msg"></param>
    private void OnDirectPayInfoResp(DirectPayInfoResp msg)
    {
        Model.SetDirectPayInfo(msg.firstIdLists);
    }

}
