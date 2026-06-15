
using System;
using System.Collections.Generic;
using System.Linq;
using common;
using FairyGUI;
/// <summary>
/// 左侧tab渲染
/// 
/// 
/// 设置数据
//  protected override LeftTabData[] mleftTabDatas => new LeftTabData[]
//     {
//         new LeftTabData() { tabName = "普通", icon = "ui://Common/Mail_tab1", select = true,data =1 },
//         new LeftTabData() { tabName = "系统", icon = "ui://Common/Mail_tab1", select = false,data=2 },
//     };
// 回调监听
// protected override void selectLeftTab(LeftTabData data)
// {
//     Logger.PrintDebug($"点击了{data.tabName}");
// }

/// </summary>
public class LeftTabCompoment : BaseRender
{

    public new G_LeftTabCompoment mRoot
    {
        get { return (G_LeftTabCompoment)base.mRoot; }
    }

    public override string mPackageName => G_LeftTabCompoment.PACKAGE_NAME;
    public override string mComponentName => G_LeftTabCompoment.COMPONENT_NAME;
    //选择页签回调
    private Action<LeftTabData> mSelectCallBack;
    //当前选择下标
    private int _selectIdx = -1;

    private TableView<LeftTabRender> tableView = null;

    protected override void onCreate()
    {

        tableView = new TableView<LeftTabRender>(mRoot.tabList);
        tableView.setClickCallBack(onTabClick);
    }

    protected override void onEventLister()
    {
        
    }

    /**
        数据改变
    */
    protected override void dataChanged()
    {
        setTabDatas((LeftTabData[])mData);
    }

    /// <summary>
    /// 设置tab数据
    /// </summary>
    /// <param name="tabdatas"></param>
    public void setTabDatas(LeftTabData[] tabdatas)
    {
        //设置列表数据

        tableView.setDatas(tabdatas);
        //数量里面是否有选择
        _selectIdx = -1;
        for (int i = 0; i < tabdatas.Length; i++)
        {
            var d = tabdatas[i];
            if (d.select)
            {
                _selectIdx = i;
            }
        }
        tableView.setSelectIndex(_selectIdx);
        //触发选择回调
        mSelectCallBack?.Invoke(tabdatas[_selectIdx]);
    }

    /// <summary>
    /// 设置选择回调
    /// </summary>
    /// <param name="selectCallBack"></param>
    public void setSelectCallBack(Action<LeftTabData> selectCallBack)
    {
        mSelectCallBack = selectCallBack;
    }

    /// <summary>
    /// 设置选择页签
    /// </summary>
    /// <param name="index"></param>
    public void setSelectIndex(int index)
    {
        if (mRoot == null)
        {
            return;
        }
        //触发选择回调
        var data = (LeftTabData [])mData;
        _selectIdx = index;
        tableView.setSelectIndex(_selectIdx);
        mSelectCallBack?.Invoke(data[_selectIdx]);
    }

    protected override void OnDestroy()
    {
    }


    //列表点击回调
    private void onTabClick(object data, int index)
    {
        if (_selectIdx == index) {
            return;
        }
        _selectIdx = index;
        var tabData = (LeftTabData)data;
        mSelectCallBack?.Invoke(tabData);
        Logger.PrintDebug($"onTabClick   =>{tabData.data}");
    }

}