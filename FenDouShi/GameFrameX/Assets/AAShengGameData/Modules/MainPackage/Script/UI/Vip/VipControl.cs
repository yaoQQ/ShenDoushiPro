

using common;
using msg.vip;


[ControlAttribute]
public class VipControl : BaseControl<VipControl>
{
    public VipModel Model { get; private set; }

    // 初始化成功调用
    protected override void onInit()
    {
        Model = new VipModel();
    }


    // 事件监听处理
    protected override void onEventListener()
    {
        on<VipInfoResp>((uint)Cmd.VipInfoResp, OnVipInfoResp);
        on<VipGiftBuyResp>((uint)Cmd.VipGiftBuyResp, OnVipGiftBuyResp);
        on<LuxuryGiftBuyResp>((uint)Cmd.LuxuryGiftBuyResp, OnLuxuryGiftBuyResp);
        on<DailyReceiveResp>((uint)Cmd.DailyReceiveResp, OnDailyReceiveResp);
    }

    protected override void onLoginSuccess()
    {
        base.onLoginSuccess();
        ReqVipInfoReq();
    }

    /// <summary>
    /// 请求vip信息
    /// </summary>
    public void ReqVipInfoReq()
    {
        var req = new VipInfoReq();
        SendNetMsg((uint)Cmd.VipInfoReq, req);
    }

    /// <summary>
    /// 请求vip礼包购买
    /// </summary>
    /// <param name="level"></param>
    public void ReqVipGiftBuyReq(int level)
    {
        var req = new VipGiftBuyReq();
        req.Level = level;
        SendNetMsg((uint)Cmd.VipGiftBuyReq, req);
    }

    /// <summary>
    /// 请求奢侈礼包购买
    /// </summary>
    /// <param name="level"></param>
    public void ReqLuxuryGiftBuyReq(int level)
    {
        var req = new VipGiftBuyReq();
        req.Level = level;
        SendNetMsg((uint)Cmd.LuxuryGiftBuyReq, req);
    }

    /// <summary>
    /// 请求每日领奖
    /// </summary>
    public void ReqDailyReceiveReq(int level)
    {
        var req = new DailyReceiveReq();
        req.Level = level;
        SendNetMsg((uint)Cmd.DailyReceiveReq, req);
    }


    /// <summary>
    /// vip信息反回
    /// </summary>
    /// <param name="resp"></param>
    private void OnVipInfoResp(VipInfoResp resp)
    {
        Model.SetVipInfo(resp);
    }

    /// <summary>
    /// vip礼包购买反回
    /// </summary>
    /// <param name="resp"></param>
    private void OnVipGiftBuyResp(VipGiftBuyResp resp)
    {
        Model.AddVipGift(resp.Level);
    }

    /// <summary>
    /// 奢侈礼包购买反回
    /// </summary>
    /// <param name="resp"></param>
    private void OnLuxuryGiftBuyResp(LuxuryGiftBuyResp resp)
    {
        Model.AddLuxuryGift(resp.Level);
    }

    /// <summary>
    /// 每日领奖反回
    /// </summary>
    /// <param name="resp"></param>
    private void OnDailyReceiveResp(DailyReceiveResp resp)
    {
        Model.AddDailyReward(resp.Level);
    }

}
