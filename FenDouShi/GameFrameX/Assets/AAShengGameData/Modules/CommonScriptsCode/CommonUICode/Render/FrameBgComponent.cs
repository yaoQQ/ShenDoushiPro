//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：__DATA_TABLE_CREATE_TIME__
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using common;
using FairyGUI;

public class FrameBgComponent : BaseRender
{
    public new G_FrameBgComponent mRoot
    {
        get { return (G_FrameBgComponent)base.mRoot; }
    }

    public override string mPackageName => G_FrameBgComponent.PACKAGE_NAME;
    public override string mComponentName => G_FrameBgComponent.COMPONENT_NAME;

    //背景视图
    private BaseView view;
    //页签点击回调
    private Action<LeftTabData> mTabCallback;
    //页签数据列表
    private TableView<FrameTabRender> tableView;
    //当前选择下标
    private int _selectIdx = -1;

    /// <summary>
    /// //节点创建子
    /// </summary>
    protected override void onCreate()
    {
        tableView = new TableView<FrameTabRender>(mRoot.FrameLeftCompoent.tabList);
        tableView.setClickCallBack(OnTabClick);
    }

    /// <summary>
    /// 注册事件监听
    /// </summary>
    protected override void onEventLister()
    {
    }

    //按钮点击
    protected override void OnButtonClick(GButton clickedButton)
    {
        base.OnButtonClick(clickedButton);
        if (mRoot.closeBtn == clickedButton)
        {
            //关闭按钮点击
            if (view != null)
            {
                view.HideView();
            }
        }
    }


    /// <summary>
    /// 刷新数据
    /// </summary>
    protected override void dataChanged()
    {
        SetLeftTabData(mData);
    }

    /// <summary>
    /// 设置标题
    /// </summary>
    /// <param name="title"></param>
    public void SetTitle(string title)
    {
        mRoot.frameTitle.text = title;
    }

    public void SetView(BaseView v)
    {
        view = v;
    }

    //设置页签点击
    public void SetTabCallback(Action<LeftTabData> action)
    {
        mTabCallback = action;
    }

    //设置选择页签
    public void SetSelectTab(int index)
    {
        if (tableView != null)
        {
            tableView.setSelectIndex(index);
        }
        var tabdatas = tableView.mDataList.Cast<LeftTabData>().ToList();
        //触发选择回调
        mTabCallback?.Invoke(tabdatas[index]);
    }

    /// <summary>
    /// 设置左侧页签数据
    /// </summary>
    /// <param name="data"></param>
    public void SetLeftTabData(object data)
    {
        // 数据变化时刷新
        if (tableView != null && data != null)
        {
            var tabdatas = data as List<LeftTabData>;
            tableView.setDatas(data);
            mRoot.FrameLeftCompoent.visible = true;
            //数量里面是否有选择
            _selectIdx = -1;
            for (int i = 0; i < tabdatas.Count; i++)
            {
                var d = tabdatas[i];
                if (d.select)
                {
                    _selectIdx = i;
                }
            }
            tableView.setSelectIndex(_selectIdx);
            //触发选择回调
            mTabCallback?.Invoke(tabdatas[_selectIdx]);
        }
        else
        {
            mRoot.FrameLeftCompoent.visible = false;
        }
    }

    //列表点击回调
    private void OnTabClick(object data, int index)
    {
        if (mTabCallback != null)
        {
            mTabCallback((LeftTabData)data);
        }
    }

}