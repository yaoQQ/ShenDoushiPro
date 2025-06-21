using System.Collections.Generic;

public interface IModule
{

    string getModuleName();

    List<string> getRegisterNotificationList();


    void onNotificationLister(string noticeType, BaseNotice vo);


    void initRegisterNet();

}
