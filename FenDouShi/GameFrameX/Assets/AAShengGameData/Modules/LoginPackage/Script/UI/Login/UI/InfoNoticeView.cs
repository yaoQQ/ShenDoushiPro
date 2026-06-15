//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：2025-08-15 10:54:17.563
//------------------------------------------------------------

using FairyGUI;
using login;
using System.Collections.Generic;

public enum NoticeEnum
{
    ageType,
    privacyType,
    userNoticeType,
}
/// <summary>
/// 自定义 FairyGUIView 模板
/// Script Name: AgeNoticeView
///	定义窗口标识UIViewEnum.AgeNoticeView为类型名，否则请填入自定义UIViewEnum标识
/// </summary>
[FGUIViewAttribute(UIViewEnum.InfoNoticeView, typeof(InfoNoticeView))]
public class InfoNoticeView : BaseView
{
    //G_AgeNoticeView 为fairyGUI导出的组件名，默认为与类型同名。否则请修改为自定义的导出组件名
    public override string PackageName => G_AgeNoticeView.PACKAGE_NAME;
    public override string ComponentName => G_AgeNoticeView.COMPONENT_NAME;
    G_AgeNoticeView view;

    //注册界面事件
    protected override Dictionary<EEventType, OnEventLister> EventList => new()
    {


    };


    public InfoNoticeView()
    {
        //（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.InfoNoticeView, false);
        Logger.PrintColor("yellow", "AgeNoticeView()");
    }
    /// <summary>
    /// 界面加载完成,触发函数
    /// </summary>
    /// <param name="gameObject">当前界面的fairyGUI对象</param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
        Logger.PrintColor("yellow", $"AgeNoticeView onLoadUIEnd complte!! gameObject={gameObject}");
        this.contentPane = gameObject.asCom;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        view = contentPane as G_AgeNoticeView;
        this.modal = true;

        CentralServerLogin.ReqHostTextByType(RequestHostType.age_appropriate_remind, (str) =>
        {
            view.contentText.text = str;
        });
    }

    //统一按钮回调
    protected override void OnButtonClick(GButton clickedButton)
    {
        if (view.closeBtn == clickedButton)
        {
            UIViewManager.Instance.Hide(UIViewEnum.InfoNoticeView);
        }
    }

    //打开界面,fairyGUI打开动画播放完触发
    protected override void OnShown()
    {
        base.OnShown();
        if (showArgs != null)
        {
            NoticeEnum noticeEnum = (NoticeEnum)showArgs;
            switch (noticeEnum)
            {
                case NoticeEnum.ageType:
                    view.Title.text = "适龄提示";
                    CentralServerLogin.ReqHostTextByType(RequestHostType.age_appropriate_remind, (str) =>
                    {
                        SwitchInfo(str);
                    });
                    break;
                case NoticeEnum.privacyType:
                    view.Title.text = "隐私协议";
                    CentralServerLogin.ReqHostTextByType(RequestHostType.privacy_agreement, (str) =>
                    {
                        SwitchInfo(str);
                    });
                    break;
                case NoticeEnum.userNoticeType:
                    view.Title.text = "用户协议";
                    CentralServerLogin.ReqHostTextByType(RequestHostType.user_agreement, (str) =>
                    {
                        SwitchInfo(str);
                    });
                    break;
            }

        }



    }
    private void SwitchInfo(string contentStr)
    {
        Logger.PrintGreen($"SwitchInfo contentStr= {contentStr} ");
        TextInfoDataRespone response = DataTableFrame.CongfigUtility.Json.ToObject<TextInfoDataRespone>(contentStr);
        Logger.PrintGreen($"RequestHostType.age_appropriate_remind #response.Data={response.Data}  #response.Data.Content={response.Data.Content} #response.Msg={response.Msg}");
        if (response.Data == null || response.Data.Content == null)
        {
            view.contentText.info.text = "===服务器传过来的内容为空===";
        }
        else
        {
            view.contentText.info.text = response.Data.Content as string;
        }
    }

    //关闭界面,fairyGUI关闭动画播放完触发
    protected override void OnHide()
    {
        base.OnHide();
    }

    //框架和fairyGUI底层销毁界面时触发
    protected override void OnDestroy()
    {

    }
}