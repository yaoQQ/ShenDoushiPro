

using msg.bag;


[ControlAttribute]
public class BagControl : BaseControl<BagControl>
{
    public BagModel Model { get; private set; }

    // 初始化成功调用
    protected override void onInit()
    {
        Model = new BagModel();
        InterFaceManager.BagModel = Model;
    }
    protected override void onLoginSuccess()
    {
        OnReqBagList(eBagType.all);
    }

    // 事件监听处理
    protected override void onEventListener()
    {
        on<BagListResp>((uint)Cmd.BagListResp, OnBagListResp);// 返回背包列表
        on<BagItemUpdateResp>((uint)Cmd.BagItemUpdateResp, OnBagItemUpdateResp);// 物品更新回调
        on<BagItemChangeResp>((uint)Cmd.BagItemChangeResp, OnBagItemChangeResp);//背包物品变化通知
    }

    /// <summary>
    /// 请求背包数据
    /// </summary>
    /// <param name="type"></param>
    public void OnReqBagList(eBagType type)
    {
        var msg = new BagListReq() { Type = (int)type };
        SendNetMsg((uint)Cmd.BagListReq, msg);
    }

    /// <summary>
    /// 使用道具
    /// </summary>
    /// <param name="uniqueId"></param>
    /// <param name="id"></param>
    /// <param name="num"></param>
    /// <param name="param"></param>
    public void OnUseItemReq(int uniqueId, int id, int num, int[] param = null)
    {
        var msg = new UseItemReq() { uniqueId = uniqueId, Id = id, Num = num, Params = param };
        SendNetMsg((uint)Cmd.UseItemReq, msg);
    }

    /// <summary>
    /// 碎片合成
    /// </summary>
    public void OnBagCombineBatchReq()
    {
        var msg = new BagCombineBatchReq();
        SendNetMsg((uint)Cmd.BagCombineBatchReq, msg);
    }

    //----------------------------------------------------------------
    private void OnBagListResp(BagListResp resp)
    {
        Model.InitBagData(resp.resourceInfos);
    }
    private void OnBagItemUpdateResp(BagItemUpdateResp resp)
    {
        Model.UpDateBagData(resp);
    }

    private void OnBagItemChangeResp(BagItemChangeResp resp)
    {
        Model.SetItemRewardDatas(resp);
    }

    // 清理数据调用
    protected override void onClear()
    {
    }
}
