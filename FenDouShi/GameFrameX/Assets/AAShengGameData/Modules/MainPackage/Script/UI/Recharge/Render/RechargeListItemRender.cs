//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：__DATA_TABLE_CREATE_TIME__
//------------------------------------------------------------

using common;
using FairyGUI;
using Recharge;

public class RechargeListItemRender : BaseRender
{
    public new G_RechargeListItemRender mRoot
    {
        get { return (G_RechargeListItemRender)base.mRoot; }
    }

    public override string mPackageName => G_RechargeListItemRender.PACKAGE_NAME;
    public override string mComponentName => G_RechargeListItemRender.COMPONENT_NAME;

    private Controller controller;

    /// <summary>
    /// //节点创建子
    /// </summary>
    protected override void OnCreate()
    {
        controller = mRoot.GetController("control");
    }

    /// <summary>
    /// 注册事件监听
    /// </summary>
    protected override void InitEventLister()
    {
        On(EEventType.EventRecharegeInfoUpdate, UpdateDoubleTag);
    }


    /// <summary>
    /// 刷新数据
    /// </summary>
    protected override void DataChanged()
    {
        // 数据变化时刷新
        var data = mData as PayVo;
        if (data != null)
        {
            mRoot.money.text = "￥" + data.Money;
            //获得奖励
            var reward = data.Rewards[0];
            mRoot.itemCount.text = reward[1] + ItemTools.GetItemName(reward[0]);
            //充值图标
            UpdateDoubleTag();
        }
    }

    /// <summary>
    /// 充值信息更新
    /// </summary>
    private void UpdateDoubleTag()
    {
        var data = mData as PayVo;
        if (data != null)
        {
            //是否双倍
            bool isDouble = RechargeControl.Instance.Model.IsFristPay(data.Id);
            controller.selectedIndex = isDouble ? 1 : 0;
        }
    }

}