using System.Collections.Generic;

public interface IModule
{
    List<EEventType> getRegisterNotificationList();


    void onNotificationLister(EEventType noticeType, EventSysArgsBase vo);
}
