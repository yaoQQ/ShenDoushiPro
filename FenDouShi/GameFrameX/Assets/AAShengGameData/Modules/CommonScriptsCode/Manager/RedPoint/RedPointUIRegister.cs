using FairyGUI;
using System.Collections.Generic;

public class RedPointUIRegister
{
    Dictionary<GComponent, RedPointUIBase> registerRedPoints = new();

    public RedPointUIRegister() { }

    // 注册
    public void Register(int redPointType, GComponent parentCom, ERedPointAlignment align = ERedPointAlignment.RightTop, float offsetX = 0, float offsetY = 0)
    {
        if (parentCom == null) return;
        // TODO:重复添加红点,改为设置红点位置
        if (registerRedPoints.ContainsKey(parentCom))
        {
            Logger.PrintError($"[红点]重复给组件添加红点:{redPointType},{parentCom}");
            return;
        }

        var uiFrom = ClassPoolManger.Instance.Get<RedPointUI_Create>();
        uiFrom.Init(redPointType);
        uiFrom.parent = parentCom;
        uiFrom.position = new RedPointPosition(redPointType, align, offsetX, offsetY);
        registerRedPoints.Add(parentCom, uiFrom);
        RedPointManager.Instance.Register(redPointType, uiFrom);
    }

    // 如果界面上已经拼了红点UI，则使用该方法绑定
    public void Attach(int redPointType, GComponent redPointCom)
    {
        if (redPointCom == null) return;
        if (registerRedPoints.ContainsKey(redPointCom))
        {
            Logger.PrintError($"[红点]重复给组件绑定红点:{redPointType},{redPointCom}");
            return;
        }

        var uiFrom = ClassPoolManger.Instance.Get<RedPointUI_Attach>();
        uiFrom.Init(redPointType);
        uiFrom.attachCom = redPointCom;
        registerRedPoints.Add(redPointCom, uiFrom);
        RedPointManager.Instance.Register(redPointType, uiFrom);
    }

    // 注销
    public void DeregisterAll()
    {
        foreach (var i in registerRedPoints)
        {
            RedPointManager.Instance.Deregister(i.Value);
        }
        registerRedPoints.Clear();
    }

    public void Deregister(GComponent parent)
    {
        if (parent != null && registerRedPoints.Remove(parent, out var uiData))
        {
            RedPointManager.Instance.Deregister(uiData);
        }
    }
}