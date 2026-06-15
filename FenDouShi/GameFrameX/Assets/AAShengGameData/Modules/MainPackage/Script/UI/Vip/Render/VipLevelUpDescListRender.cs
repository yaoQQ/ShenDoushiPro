//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：__DATA_TABLE_CREATE_TIME__
//------------------------------------------------------------

using System.Collections.Generic;
using common;
using vip;

public class VipLevelUpDescListRender : BaseRender
{
    public new G_VipLevelUpDescListRender mRoot
    {
        get { return (G_VipLevelUpDescListRender)base.mRoot; }
    }

    public override string mPackageName => G_VipLevelUpDescListRender.PACKAGE_NAME;
    public override string mComponentName => G_VipLevelUpDescListRender.COMPONENT_NAME;

    /// <summary>
    /// //节点创建子
    /// </summary>
    protected override void OnCreate()
    {

    }

    /// <summary>
    /// 注册事件监听
    /// </summary>
    protected override void InitEventLister()
    {
    }


    /// <summary>
    /// 刷新数据
    /// </summary>
    protected override void DataChanged()
    {
        // 数据变化时刷新
        List<int> data = mData as List<int>;
        if (data != null)
        {
            var key = data[0];
            var value = data.Count > 1 ? data[1] : 0;
            var conf = ConfigMgr.Instance.GetConfigVoById<VipPrivilegesTextVo>(key);
            if (conf != null)
            {
                var str = conf.PrivilegesText;
                if (value != 0)
                {
                    if (key == 2)
                    {
                        str = str.Replace("{val}", "[color=#8EFF73]" + (value / 3600) + "[/color]" + "小时");
                    }
                    else
                    {
                        str = str.Replace("{per}", "[color=#8EFF73]" + (value / 100) + "%" + "[/color]").Replace("{val}", "[color=#8EFF73]" + value + "[/color]");
                    }
                }
                mRoot.n48.text = str;
            }
        }
    }

}