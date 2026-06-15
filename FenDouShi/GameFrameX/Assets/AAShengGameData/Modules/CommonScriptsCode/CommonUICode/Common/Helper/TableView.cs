using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 列表视图
/// </summary>
public class TableView<T> where T : BaseRender, new()
{
    //列表节点
    private GList mGList;

    public T itemRender { get; set; }

    public List<object> mDataList { get; set; }

    //点击页签回调
    private Action<object, int> mClickCallBack;

    private int mMax = 0; //最大数据长度

    public int mSelectIndex { get; private set; } = -1; //选中项索引

    private bool touchEnable = true; //是否可以点击


    public TableView(GList list)
    {
        mGList = list;
        mGList.SetVirtual();
        mSelectIndex = -1;
        //数据改变回调
        mGList.itemRenderer = gListItemRender;
        //点击回调
        mGList.onClickItem.Add(onClickItem);
    }

    /// <summary>
    /// 设置数据列表
    /// </summary>
    /// <param name="dataList"></param>
    public void setDatas(object dataList, bool isRefresh = true)
    {
        var enumerableDataList = dataList as IEnumerable;
        if (enumerableDataList != null)
        {
            mDataList = enumerableDataList.Cast<object>().ToList();
            if (isRefresh)
                mGList.numItems = Math.Max(mMax, mDataList.Count);
        }
        else
        {
            mDataList = null;
            mGList.numItems = 0; // 或者其他适当的处理
        }
    }

    /// <summary>
    /// 设置最大数据长度
    /// </summary>
    /// <param name="num"></param>
    public void setMaxNum(int num)
    {
        mMax = num;
        if (mGList != null)
        {
            mGList.numItems = num;
        }
    }

    /// <summary>
    /// 设置选中项索引
    /// </summary>
    /// <param name="index"></param>
    public void setSelectIndex(int index)
    {
        //var data = getDataByIndex(index);
        //if (data == null)
        //{
        //    return;
        //}
        if (mGList != null)
        {
            mGList.selectedIndex = index;
            updateSelectItemIndex(index);
        }
        //记录选中项索引
        mSelectIndex = index;
    }

    /// <summary>
    /// 列表数据变化填充数据 FGUI GList数据填充回调
    /// </summary>
    /// <param name="index"></param>
    /// <param name="item"></param>
    private void gListItemRender(int index, GObject item)
    {
        var render = item.data as BaseRender;
        if (null == item.data)
        {
            render = BaseRender.Create<T>((GComponent)item);
            item.data = render;
        }

        render?.setData(getDataByIndex(index), index);
        render?.setSelectIndex(mSelectIndex);
    }

    /// <summary>
    /// 设置选中项回调
    /// </summary>
    /// <param name="selectCallBack"></param>
    public void setClickCallBack(Action<object, int> selectCallBack)
    {
        mClickCallBack = selectCallBack;
    }

    /// <summary>
    /// 列表点击时触发
    /// </summary>
    /// <param name="context"></param>
    private void onClickItem(EventContext context)
    {
        if (touchEnable)
        {
            var obj = (GComponent)context.data;
            var render = (BaseRender)obj.data;
            var data = getDataByIndex(render.mIndex);
            if (data == null)
            {
                return;
            }
            mSelectIndex = render.mIndex;
            //更新选中状态
            updateSelectItemIndex(render.mIndex);

            //触发选择回调
            mClickCallBack?.Invoke(data, render.mIndex);
        }
    }

    /// <summary>
    /// 点击事件
    /// </summary>
    /// <param name="enable"></param>
    public void SetTouchEnable(bool enable)
    {
        touchEnable = enable;
    }



    public object getDataByIndex(int index)
    {
        if (mDataList == null || index < 0 || index >= mDataList.Count)
        {
            return null;
        }
        return mDataList[index];
    }

    /// <summary>
    /// 设置行距
    /// </summary>
    /// <param name="lineGap"></param>
    public void SetLineGap(int lineGap)
    {
        if (mGList != null)
        {
            mGList.lineGap = lineGap;
        }
    }

    /// <summary>
    /// 对齐方式
    /// </summary>
    /// <param name="lineSpace"></param>
    public void SetAlign(AlignType alignType)
    {
        if (mGList != null)
        {
            mGList.align = alignType;
        }
    }

    /// <summary>
    /// 列表自动大小
    /// </summary>
    // public void ResizeToFit()
    // {
    //     if (mGList != null)
    //     {
    //         mGList.ResizeToFit();
    //     }
    // }
    public void ResizeToFit(int itemCount = int.MaxValue)
    {
        if (mGList != null)
        {
            mGList.ResizeToFit(itemCount);
        }
    }

    /// <summary>
    /// 滚动到顶部
    /// </summary>
    public void ScrollTop()
    {
        mGList?.scrollPane.ScrollTop();
    }

    public void ScrollToView(int index)
    {
        if (mGList != null)
        {
            //var childIndex = mGList.ItemIndexToChildIndex(index);
            //var itemIndex = mGList.ChildIndexToItemIndex(index);
            //Logger.PrintLog($"{childIndex}, {itemIndex}");
            mGList?.ScrollToView(index);
        }
    }

    /// <summary>
    /// 更新选中
    /// </summary>
    /// <param name="index"></param>
    public void updateSelectItemIndex(int index)
    {
        if (mGList == null) return;
        var renders = mGList.GetChildren();
        foreach (var s in renders)
        {
            var render = s.data as BaseRender;
            if (render != null)
            {
                render?.setSelectIndex(index);
            }
        }
    }
}