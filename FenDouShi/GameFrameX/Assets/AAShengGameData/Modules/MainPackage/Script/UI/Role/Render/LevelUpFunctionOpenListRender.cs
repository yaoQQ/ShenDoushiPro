//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：__DATA_TABLE_CREATE_TIME__
//------------------------------------------------------------

using System.Collections.Generic;
using common;
using roleLevel;

public class LevelUpFunctionOpenListRender : BaseRender
{
    public new G_LevelUpFunctionOpenListRender mRoot
    {
        get { return (G_LevelUpFunctionOpenListRender)base.mRoot; }
    }

    public override string mPackageName => G_LevelUpFunctionOpenListRender.PACKAGE_NAME;
    public override string mComponentName => G_LevelUpFunctionOpenListRender.COMPONENT_NAME;

    /// <summary>
    /// //节点创建子
    /// </summary>
    protected override void onCreate()
    {

    }

    /// <summary>
    /// 注册事件监听
    /// </summary>
    protected override void onEventLister()
    {
    }


    /// <summary>
    /// 刷新数据
    /// </summary>
    protected override void dataChanged()
    {
        // 数据变化时刷新
        var data = mData as FuncVo;
        if (data != null)
        {
            //功能名称
            mRoot.funcName.text = data.Name;
            //功能图标
            mRoot.funcIcon.url = UIHelper.GetFguiUrl("icon", data.Icon);
            //开启条件
            mRoot.contdion.text = SystemOpenControl.Instance.Model.GetSystemOpenTipDes(data.Id);
        }
    }
}