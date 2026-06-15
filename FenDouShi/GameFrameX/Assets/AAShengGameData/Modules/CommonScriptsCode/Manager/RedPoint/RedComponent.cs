using System.Numerics;
using FairyGUI;
using UnityEngine;

public class RedData
{
    /// <summary>
    /// 红点类型
    /// </summary>
    public int RedPointType;
    /// <summary>
    /// 红点显示位置偏移
    /// </summary>
    public float OffsetX;
    public float OffsetY;
    //红点显示位置，默认右上角
    public ERedPointAlignment RedPointAlignment = ERedPointAlignment.RightTop;
}

/// <summary>
/// 红点组件
/// </summary>
/**
var redCom = GComponent.AddComponent<RedComponent>();
//1.只设置红点类型，其他默认
redCom.SetRedType(ERedPointType.Bag);
//2.设置红点类型、位置、偏移
redCom.SetRedType(ERedPointType.Bag, ERedPointAlignment.Center, 0, 0);
//3.设置红点数据对象
var redData = new RedData();
redData.RedPointType = ERedPointType.Bag;
redData.RedPointAlignment = ERedPointAlignment.LeftTop;
redData.OffsetX = 0;
redData.OffsetY = 0;
redCom.SetRedData(redData);
*
*/
public class RedComponent : FGUIComponent
{
    //红点数据
    public RedData mRedData;

    public bool mState = false;
    private GImage aImage;

    void Start()
    {
        //创建红点
        aImage = UIPackage.CreateObject("common", "common_icon_hongdian").asImage;
        gComponent.AddChild(aImage);
        aImage.pivotX = 0.5f;
        aImage.pivotY = 0.5f;
        aImage.visible = mState;
        aImage.TweenScale(new UnityEngine.Vector2(1.2f, 1.2f), 0.5f) // 放大到1.2倍，用时0.5秒
            .SetEase(EaseType.SineInOut) // 平滑的缓动效果
            .SetRepeat(-1, true); // -1 表示无限循环

        //注册事件监听
        EventManager.Instance.AddEventLister(EEventType.OnRedPointUpdate, UpdateRedPoint);
        if (mRedData != null && mRedData.RedPointType != (int)ERedPointType.None) {
            //更新数据
            UpdateRedState();
        }
        //更新位置
        UpdateRedPos();
    }

    void OnDestroy()
    {
        //注销事件监听
        EventManager.Instance.RemoveEventLister(EEventType.OnRedPointUpdate, UpdateRedPoint);
    }

    /// <summary>
    /// 更新红点状态
    /// </summary>
    private void UpdateRedState()
    {
        if (mRedData != null && mRedData.RedPointType != 0)
        {
            var state = RedPointManager.Instance.GetState(mRedData.RedPointType);
            SetRedState(state);
        }
        else
        {
            SetRedState(false);
        }
    }

    /// <summary>
    /// 更新红点位置
    /// </summary>
    private void UpdateRedPos()
    {
        if (mRedData != null && aImage != null)
        {
            var size = gComponent.size;
            var size1 = aImage.size;
            float x = mRedData.OffsetX, y = mRedData.OffsetY;
            switch (mRedData.RedPointAlignment)
            {
                case ERedPointAlignment.LeftTop:
                    break;
                case ERedPointAlignment.LeftBottom:
                    y += size.y - size1.y;
                    break;
                case ERedPointAlignment.RightTop:
                    x += size.x - size1.x;
                    break;
                case ERedPointAlignment.RightBottom:
                    x += size.x - size1.x;
                    y += size.y - size1.y;
                    break;
                case ERedPointAlignment.Center:
                    x += (size.x - size1.x) / 2;
                    y += (size.y - size1.y) / 2;
                    break;
                case ERedPointAlignment.TopCenter:
                    x += (size.x - size1.x) / 2;
                    break;
            }
            aImage.SetPosition(x, y, 0);
        }
    }

    /// <summary>
    /// 设置红点数据
    /// </summary>
    /// <param name="redData"></param>
    public void SetRedData(RedData redData)
    {
        mRedData = redData;
        //更新状态
        UpdateRedState();
        //更新位置
        UpdateRedPos();
    }

    /// <summary>
    /// 设置红点类型
    /// </summary>
    /// <param name="redPointType"></param>
    public void SetRedType(ERedPointType redPointType)
    {
        SetRedType((int)redPointType);
    }

    public void SetRedType(int redPointType)
    {
        if (mRedData == null)
        {
            //没有红点数据，创建
            mRedData = new RedData();
            mRedData.RedPointType = redPointType;
        }
        else
        { 
            mRedData.RedPointType = redPointType;
        }
        SetRedData(mRedData);
    }

    /// <summary>
    /// 设置红点类型
    /// </summary>
    /// <param name="redPointType"></param>
    public void SetRedType(int redPointType, ERedPointAlignment redPointAlignment, float offsetX = 0, float offsetY = 0)
    {
        if (mRedData == null)
        {
            //没有红点数据，创建
            mRedData = new RedData();
            mRedData.RedPointType = redPointType;
            mRedData.RedPointAlignment = redPointAlignment;
            mRedData.OffsetX = offsetX;
            mRedData.OffsetY = offsetY;
        }
        else
        { 
            mRedData.RedPointType = redPointType;
            mRedData.RedPointAlignment = redPointAlignment;
            mRedData.OffsetX = offsetX;
            mRedData.OffsetY = offsetY;
        }
        SetRedData(mRedData);
    }

    public void SetRedType(ERedPointType redPointType, ERedPointAlignment redPointAlignment, float offsetX = 0, float offsetY = 0)
    {
        SetRedType((int)redPointType, redPointAlignment, offsetX, offsetY);
    }

    /// <summary>
    /// 设置红点状态
    /// </summary>
    /// <param name="state"></param>
    public void SetRedState(bool state)
    {
        mState = state;
        if (aImage != null)
        {
            aImage.visible = state;
        }
    }

    /// <summary>
    /// 红点显示更新
    /// </summary>
    /// <param name="args"></param>
    private void UpdateRedPoint(EventSysArgsBase args)
    {
        if (mRedData != null && args != null && args is EventSysArgs<int, bool> eventSys)
        {
            var redPointType = eventSys.args1;
            bool state = eventSys.args2;
            if (redPointType == (int)mRedData.RedPointType)
            {
                SetRedState(state);
            }
        }
    }
}
