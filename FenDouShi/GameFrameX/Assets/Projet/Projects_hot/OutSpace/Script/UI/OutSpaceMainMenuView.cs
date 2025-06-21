using System.Collections.Generic;
using UIWidget;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutSpaceMainMenuView : BaseView
{

    [SerializeField]
    private Mid_MainMenu_OutSpace main_mid;

    public OutSpaceMainMenuView()
    {

        this.viewName = "OutSpaceMainMenuView";
        this.loadOrders = new List<string>() { "OutSpacePackage:MainMenu_OutSpace" };

        setViewAttribute(UIViewType.Game_1, UIViewEnum.OutSpaceMainMenuView, false);
        Logger.PrintDebug("@@@@ OutSpaceMainMenuView()");
    }
    protected override void onLoadUIEnd(string uiName, GameObject gameObject)
    {
        // main_mid = gameObject.GetComponent<UIBaseMono>();
        Logger.PrintDebug("onLoadUIEnd complte!! uiName=" + uiName);

        main_mid = gameObject.AddComponent<Mid_MainMenu_OutSpace>();
        this.BindMonoTable(gameObject, main_mid);
        UITools.SetParentAndAlign(gameObject, this.container);

        this.main_mid.Quit.AddEventListener(UIEvent.PointerClick, CloseFun);
        this.main_mid.SettingsAudio.AddEventListener(UIEvent.PointerClick, OpenSetting);
        this.main_mid.MasterVolumeSlider.slider.onValueChanged.AddListener(SetGameSound);
        this.main_mid.MusicSlider.slider.onValueChanged.AddListener(SetMusicSound);

        //this.main_mid.BtnLoginReset.AddEventListener(UIEvent.PointerClick, onBtnLoginReset);
        //this.main_mid.BtnLoginLogin.AddEventListener(UIEvent.PointerClick, onBtnLoginLogin);
        //this.main_mid.BtnLoginDirectly.AddEventListener(UIEvent.PointerClick, LoginDirectly);
        addEvent();

        //showTopTips("选中倒计时 this.scanTime="..tostring(this.scanTime))
    }
    private void OpenSetting(PointerEventData pointData)
    {
        Logger.PrintDebug("click openSetting");
    }
    private void CloseFun(PointerEventData pointData)
    {
        UIViewManager.Instance.Close(UIViewEnum.OutSpaceMainMenuView);
    }
    private void SetGameSound(float value)
    {
        if(OutSpaceAudioManager.Instance.audioSource)
        OutSpaceAudioManager.Instance.audioSource.volume = value;
    }
    private void SetMusicSound(float value)
    {
        #if SHOW_MUSIC_DATA
        if(GetAudioDataManager.Instance.getMusicAudioSource)
        GetAudioDataManager.Instance.getMusicAudioSource.volume = value;
#endif
    }
    protected override void onShowHandler(object msg)
    {
        Logger.PrintDebug("onShowHandler() 显示界面：msg=" + msg);
        GameObject go = this.getViewGO();
        if (go == null)
        {
            Logger.PrintError("go == null");
            return;
        }
        go.transform.SetAsLastSibling();
        OutSpaceGameManager.Instance.PauseGame();
        //string textStr = msg as string;
        //onShowTopTips(textStr);
    }
    private void addEvent()
    {

    }




    protected override void onClose()
    {
        base.onClose();
        OutSpaceGameManager.Instance.ResumeGame();
    }


}
