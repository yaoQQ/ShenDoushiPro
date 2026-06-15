using System.Collections.Generic;
/// <summary>
/// 登入场景资源包类 包含相关view 和module注册 初始化
/// </summary>
public class LoginPackage : AbstractPackage
{
    
    public LoginPackage()
    {
        Logger.PrintColor("yellow", "BasePackage()类初始化完成");
        this.packName = PackageEnum.LoginPackage;
        //当前场景模块包含的网络功能模块注册
        this.moduleList = new List<BaseModule>()
        {
          //  new WebLoginModule(),//登入网络协议模块
        };

        //登入场景,相关界面注册和预加载
        this.viewList = new List<BaseView>()
        {

            //new LoginOnInitView(),
            //new LoginSelectServerView(),
            //new TopTipsView(),
            //new AlertWindowView(),
        //    new LoginCreateUserView(),
          //  new PlatformGameUpdateView(),
        };
        //登入场景,相关界面配置表注册和预加载
        this.tableList = new List<string>() 
        {
             //"item",
             //"chat",
             //"chat_msg",
             //"result",
        };
        this.protoList = new List<uint>
        {

        };

        this._preloadOrder = new LoginPackagePreload(this);
  
    }

    public List<IBaseView> getAllList()
    {
        return this.getPackAllUIMidList();
    }
}