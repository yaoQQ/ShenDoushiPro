using common;
using FairyGUI;
using login;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

[FGUIViewAttribute(UIViewEnum.TopTips_View, typeof(TopTipsView))]
public class TopTipsView : BaseView
{
    public override string PackageName => G_TopTipsView.PACKAGE_NAME;
    public override string ComponentName => G_TopTipsView.COMPONENT_NAME;

    private string topTipsTimer = "TopTipsTimer";
    private G_TopTipsView view;
    private List<string> mTipsList = new List<string>();
    private List<MsgTipsContent> tipsPool = new List<MsgTipsContent>();
    private List<MsgTipsContent> tipsShow = new List<MsgTipsContent>();
    private bool showNext = true;

    public TopTipsView()
    {
        //声明界面{fairyGUI所在包:包里面Componet名字}
        //（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
        setViewAttribute(UIViewLayerType.Alert_box, UIViewEnum.TopTips_View, false);
        Logger.PrintColor("yellow", "TopTipsView()");
    }
    /// <summary>
    /// 界面加载完成,触发函数
    /// </summary>
    /// <param name="uiName">界面名</param>
    /// <param name="gameObject">当前界面的fairyGUI对象</param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
        Logger.PrintColor("yellow", $"onLoadUIEnd complte!! gameObject={gameObject}");
        this.contentPane = gameObject.asCom;
        view = this.contentPane as G_TopTipsView;
        this.Center();
        ////调用fairyGUI的Window方法展示界面,加载完直接展示
    }

    protected override void OnShown()
    {
        base.OnShown();
        //  Logger.PrintColor("red", $"InitView() ");
        string textStr = showArgs as string;

    }

    /// <summary>
    /// 显示界面生命周期
    /// </summary>
    /// <param name="msg"></param>
    protected override void OnShow(object msg)
    {
        string textStr = showArgs as string;
        if (textStr != null)
        {
            AddTips(textStr);
        }

    }

    //重写window不要动画
    override protected void DoHideAnimation()
    {

    }

    /// <summary>
    /// 增加一个tips
    /// </summary>
    /// <param name="msg"></param>
    private void AddTips(string msg)
    {
        if (!string.IsNullOrEmpty(msg))
        {
            mTipsList.Add(msg);

            if (mTipsList.Count > 1)
            {
                return;
            }
            //显示一个tips
            _ = ShowNextTips();
        }
    }

    /// <summary>
    /// 显示下一个tips
    /// </summary>
    private async Task ShowNextTips()
    {
        if (mTipsList.Count == 0)
        {
            return;
        }

        //正在显示中
        if (!showNext)
        {
            return;
        }

        showNext = false;

        var msgStr = mTipsList.First();
        mTipsList.RemoveAt(0);

        MsgTipsContent tipsRender = null;
        //创建一个提示，如果提示池子里有，就从池子里取，没有就创建
        if (tipsPool.Count > 0)
        {
            //池子里面取出一个
            tipsRender = tipsPool[0];
            tipsPool.RemoveAt(0);
        }
        else
        {
            //创建一个新的提示
            tipsRender = await BaseRender.Create<MsgTipsContent>();
            view.AddChild(tipsRender.mRoot);
        }
        //显示提示
        tipsRender.mRoot.visible = true;
        tipsRender.setData(msgStr, 0);
        tipsRender.Show();
        tipsRender.MoveTo(TipsMoveEndCall);
        tipsShow.Add(tipsRender);
    }

    /// <summary>
    /// 提示移动结束回调
    /// </summary>
    /// <param name="type"></param>
    /// <param name="tips"></param>
    private void TipsMoveEndCall(int type, MsgTipsContent tips)
    {
        if (type == 1)
        {
            //显示下一条
            foreach (var item in tipsShow)
            {
                item.MoveToNextPos();
            }
            showNext = true;
            _ = ShowNextTips();
        }
        else if (type == 2)
        {
            //提示消失
            tipsShow.Remove(tips);
            tipsPool.Add(tips);
            tips.Hide();
        }
    }
}
