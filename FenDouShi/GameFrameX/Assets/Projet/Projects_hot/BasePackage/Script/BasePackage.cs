using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base 资源包类 包含相关view 和module注册 初始化
/// </summary>
public class BasePackage : AbstractPackage
{
    
    public BasePackage()
    {
        this.packName = ProjectControler.basePackage;
        this.moduleList = new List<BaseModule>()
        {

            new  LoginModule(),
            new  PlayFabUserModule(),
            new ShopModule(),
        };

        this.viewList = new List<BaseView>()
        {

            new LoginView2(),
            new TopTipsView(),
            new AlertWindowView(),
          //  new PlatformGlobalGameView(),
          //  new PlatformGameRuleView(),
          //  new PlatformGameUpdateView(),
            new PlatformCommonTopCostView(),
          //  new PlatformMallView(),
        };
        this.protoList = new List<uint>()
        {

        };

        //Logger.PrintDebug("@@@@viewList.count=" + viewList.Count);
        //Logger.PrintDebug("@@@@@viewList[0]=" + viewList[0]);
    }

    public List<BaseView> getAllList()
    {
        return this.getPackAllUIMidList();
    }
}