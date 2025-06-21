
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UIWidget;

public class CharacterInfoTopPanel : BaseView
{
    [SerializeField]
    private CharacterInfoPanel main_mid;
    public CharacterInfoTopPanel()
    {

        this.viewName = "CharacterInfoTopPanel";
        this.loadOrders = new List<string>() { "OutSpacePackage:CharacterInfoTopPanel" };

        setViewAttribute(UIViewType.Game_1, UIViewEnum.OutSpacePlayerInfoView, false);
        Logger.PrintDebug("@@@@ CharacterInfoTopPanel()");
    }
    protected override void onLoadUIEnd(string uiName, GameObject gameObject)
    {

        Logger.PrintDebug("onLoadUIEnd complte!! uiName=" + uiName);
        Logger.PrintDebug("CharacterInfoTopPanel onLoadUIEnd complte!! gameObject=" + gameObject);
        main_mid = gameObject.AddComponent<CharacterInfoPanel>();
        Logger.PrintDebug("onLoadUIEnd complte!! main_mid=" + main_mid);
        this.BindMonoTable(gameObject, main_mid);
        UITools.SetParentAndAlign(gameObject, this.container);
        main_mid.closeBtn.AddEventListener(UIEvent.PointerClick, closeFun);


    }
    protected override void onShowHandler(System.Object msg)
    {


        GameObject go = this.main_mid.gameObject;
        if (go == null)
        {
            Logger.PrintError("go == null");
            return;
        }
        go.transform.SetAsLastSibling();

        OutSpaceGameManager.Instance.PauseGame();
        main_mid.characterInfoText.text = OutSpacePlayerInfoManager.Instance.CharacterAddInfo.getAttri();
    }


    private void closeFun(PointerEventData eve)
    {
        UIViewManager.Instance.Close(UIViewEnum.OutSpacePlayerInfoView);
    }


    protected override void onClose()
    {
        OutSpaceGameManager.Instance.ResumeGame();
        base.onClose();

    }

}