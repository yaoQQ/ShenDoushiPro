using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommonModule : BaseModule
{

    public override ModuleEnum ModuleName()
    {
        return ModuleEnum.Common;
    }
    public override void InitRegisterNet()
    {
       
    }
    public override void OnNetMsgLister(uint protoID, byte[] buffer)
    {
     //   PlayFabProtocol
    }
    public override void OnJsonMsgLister(uint protoID, string jsonData) {

    }
    public override List<string> GetRegisterNotificationList()
    {
        if (notificationList == null)
        {
            notificationList = new List<string>();
            notificationList.Add(NoticeType.Normal_QuitGame);
            notificationList.Add(NoticeType.Show_Dialogue_Audio);
        }
        return notificationList;
    }

    public override void OnNotificationLister(string noticeType, BaseNotice notice)
    {
        ObjectNotice onValue = notice as ObjectNotice;
        switch (noticeType)
        {
            case NoticeType.Show_Dialogue_Audio://无用了
                ObjectNotice objNotice = (ObjectNotice)notice;
                System.Object obj = objNotice.GetObj();
                if (obj == null)
                {
                    Debug.LogError("说话音频为空。");
                    return;
                }
                //test@@@
              //  StarterAssets.ThirdPersonController.Instance.SpeakVoice((AudioClip)obj);
                break;
            case NoticeType.Normal_QuitGame:
             //   Driver.Instance.QuitGame();
                break;
        }
    }
}
