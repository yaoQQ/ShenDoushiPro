
using System;
using System.Threading.Tasks;
using common;
using FairyGUI;
using UnityEngine;

/// <summary>
/// 通用飘字提示内容
/// </summary>
public class MsgTipsContent : BaseRender
{
    protected new G_MsgTipsContent mRoot
    {
        get { return (G_MsgTipsContent)base.mRoot; }
    }

    public override string mPackageName => G_MsgTipsContent.PACKAGE_NAME;
    public override string mComponentName => G_MsgTipsContent.COMPONENT_NAME;

    //移动结束回调
    private Action<int, MsgTipsContent> mMoveEndCall;

    private float moveSpeed = 500;
    private float moveDis = 70;

    protected override void onCreate()
    {

    }

    protected override void dataChanged()
    {
        // 数据变化时刷新显示内容
        string content = (string)mData;
        mRoot.noticeText.text = content;
    }

    /**
    * 显示提示文本
    * @param str 
    */
    public void MoveTo(Action<int, MsgTipsContent> callBack)
    {
        mMoveEndCall = callBack;
        mRoot.y = moveDis;
        float t = moveDis / moveSpeed;
        GActions.StopAll(mRoot);
        var action = GActions.Sequence();
        action.Add(GActions.MoveTo(new Vector2(mRoot.x, 0), t));
        action.Add(GActions.CallFunc(() =>
        {
            mMoveEndCall?.Invoke(1, this);
        }));
        action.Add(GActions.Delay(1.5f));
        action.Add(GActions.FadeTo(0, 0.2f));
        action.Add(GActions.CallFunc(() =>
        {
            mMoveEndCall?.Invoke(2, this);
        }));

        GActions.Run(mRoot, action);
        mRoot.scale = new Vector2(0, 0);
        GActions.Run(mRoot, GActions.ScaleTo(new Vector2(1, 1), t));
        mRoot.alpha = 0;
        GActions.Run(mRoot, GActions.FadeTo(1f, t));
    }

    /// <summary>
    /// 移动到下一个位置
    /// </summary>
    public void MoveToNextPos()
    {
        mIndex++;
        var y = -moveDis * mIndex;
        float t = moveDis / moveSpeed;
        GActions.Run(mRoot, GActions.MoveTo(new Vector2(mRoot.x, y), t));
    }
}