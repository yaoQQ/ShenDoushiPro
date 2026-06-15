
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using common;
using DataTableFrame;
using FairyGUI;
using UnityEngine.PlayerLoop;

/// <summary>
/// 
///  
// protected override TopRenderData mTopRenderData => new TopRenderData
// {
//     titleName = "GM",
//     icon = "ui://Common/Gm/icon_gm",
//     helpId = 10000,
//     currencyId = 10001,
// };
/// </summary>
public class TopContentCompoment : BaseRender
{
    public new G_TopContentCompoment mRoot
    {
        get { return (G_TopContentCompoment)base.mRoot; }
    }

    public override string mPackageName => G_TopContentCompoment.PACKAGE_NAME;

    public override string mComponentName => G_TopContentCompoment.COMPONENT_NAME;

    //是否全屏适配
    protected override bool mIsFullScreen => true;

    //视图
    private BaseView mView = null;

    //货币列表
    private List<CurrencyItemRender> mCurrencyItemRenders = new List<CurrencyItemRender>();

    /// <summary>
    /// 创建成功
    /// </summary>
    protected override void onCreate()
    {
        //将4个列表项加入到列表中方便后面使用
        var render1 = Create<CurrencyItemRender>(mRoot.currencyList.currenItem1);
        mCurrencyItemRenders.Add(render1);
        var render2 = Create<CurrencyItemRender>(mRoot.currencyList.currenItem2);
        mCurrencyItemRenders.Add(render2);
        var render3 = Create<CurrencyItemRender>(mRoot.currencyList.currenItem3);
        mCurrencyItemRenders.Add(render3);
        var rende4 = Create<CurrencyItemRender>(mRoot.currencyList.currenItem4);
        mCurrencyItemRenders.Add(rende4);

    }

    /// <summary>
    /// 隐藏
    /// </summary>
    protected override void OnHide()
    {
    }

    /// <summary>
    /// 销毁
    /// </summary>
    protected override void OnDestroy()
    {
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        base.OnButtonClick(clickedButton);
        if (clickedButton == mRoot.closeButton)
        {
            //返回按钮点击
            if (mView != null)
            {
                mView.Hide();
            }
        }
        else if (clickedButton == mRoot.Button_help)
        {
            //帮助按钮点击
            var data = mData as TopRenderData;
            if (data.helpId > 0)
            {
                Logger.PrintDebug("点击帮助按钮", data.helpId.ToString());
            }
        }
    }

    /// <summary>
    /// 数据改变
    /// </summary>
    protected override void DataChanged()
    {
        var data = mData as TopRenderData;
        if (data != null)
        {
            mRoot.Text_title.text = data.titleName;
            //加载货币列表
            LoadCurrencyList(data.currencyId);
        }
    }

    /// <summary>
    /// 绑定一个视图
    /// </summary>
    /// <param name="view"></param>
    public void setView(BaseView view)
    {
        mView = view;
    }


    private void LoadCurrencyList(int currencyId)
    {
        try
        {
            for (int i = 0; i < mCurrencyItemRenders.Count; i++)
            {
                mCurrencyItemRenders[i].Hide();
            }
            //这里需要去读取currencySystemExcel表格还需要去读取背包里面对应的数据
            int[] currencyIds = new int[] { 1, 2 };
            var cfg = ConfigMgr.Instance.GetConfigVoById<CurrencySystemVo>(currencyId);
            if (cfg == null)
            {
                Logger.PrintDebug($"创建货币 currencyId isNull {currencyId}");
                return;
            }
            Logger.PrintDebug($"创建货币列表项111{currencyIds.Length}");
            for (int i = 0; i < cfg.ItemsLink.Count; i++)
            {
                CurrencySystemItemsLinkVo vo = cfg.ItemsLink[i];
                if (vo != null)
                {
                    CurrencyItemRender itemRender = mCurrencyItemRenders[i];
                    itemRender.setData(vo);
                    mCurrencyItemRenders[i].Show();
                }
            }
        }
        catch (System.Exception e)
        {
            Logger.PrintError(e.ToString());
            //throw;
        }
    }

    /// <summary>
    /// 设置帮助id
    /// </summary>
    /// <param name="helpId"></param>
    public void SetHelpId(int helpId)
    {
        var data = mData as TopRenderData;
        if (data != null)
        {
            data.helpId = helpId;
        }
    }

    /// <summary>
    /// 设置货币id
    /// </summary>
    /// <param name="currencyId"></param>
    public void SetCurrencyId(int currencyId)
    {
        var data = mData as TopRenderData;
        if (data != null)
        {
            data.currencyId = currencyId;
            //加载货币列表
            LoadCurrencyList(data.currencyId);
        }
    }


}