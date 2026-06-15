using FairyGUI;
using System;
using UnityEditor.SceneManagement;

public class AthenaTrialTabItem : BaseRender
{
    public override string mPackageName { get; }
    public override string mComponentName { get; }

    private Action ClickCB;

    //private RedComponent RedComponent;

    private bool isLock = false;

    public void Renderer(AthenaTrialTabItemData data)
    {
        var copyType = data.Type;
        //RedComponent = data.ButtonClick.TryGetComponent<RedComponent>();
        //RedComponent.SetRedData(new RedData() { RedPointAlignment = ERedPointAlignment.RightTop });
        //RedComponent.SetRedType(ERedPointType.AthenaTrial + copyType);
        var typeCfg = AthenaTrialControl.Instance.Model.GetTypeCfgByTypeId(copyType);
        data.ButtonClick.onClick.Clear();
        data.ButtonClick.onClick.Add(OnClickCB);
        data.TextTitle.text = typeCfg.Name;
        isLock = !AthenaTrialControl.Instance.Model.GetIsCopyTypeOpen(copyType);
        var descStr = "";
        if (isLock)
        {
            descStr = AthenaTrialControl.Instance.Model.GetCopyTypeOpenStr(copyType);
        }
        else
        {
            var curLevel = AthenaTrialControl.Instance.Model.GetPassMaxLevel(copyType);
            var nextCfg = AthenaTrialControl.Instance.Model.GetLevelCfg(copyType, curLevel + 1);
            var isFullLevel = nextCfg == null;
            if (isFullLevel)
            {
                nextCfg = AthenaTrialControl.Instance.Model.GetLevelCfg(copyType, curLevel);
            }
            descStr = Utility.Text.Format("第{0}关", nextCfg.Level);
        }
        var redPointName = (int)ERedPointType.AthenaTrial + copyType;
        data.RedPoint.visible = RedPointManager.Instance.GetState(redPointName);
        data.TextDesc.text = descStr;
    }

    private void OnClickCB(EventContext context)
    {
        ClickCB?.Invoke();
    }

    public void SetClickCallBack(Action cb)
    {
        ClickCB = cb;
    }
}

public class AthenaTrialTabItemData
{
    public GTextField TextTitle;
    public GTextField TextDesc;
    public GButton ButtonClick;
    public GComponent RedPoint;
    public int Type;
}



