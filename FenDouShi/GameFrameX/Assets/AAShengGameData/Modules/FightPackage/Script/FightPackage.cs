using System.Collections.Generic;
/// <summary>
/// Base 资源包类 包含相关view 和module注册 初始化
/// </summary>
public class FightPackage : AbstractPackage
{

    public FightPackage()
    {
        Logger.PrintColor("yellow", "FightPackage()类初始化完成");
        this.packName = PackageEnum.FightPackage;
        this.moduleList = new List<BaseModule>()
        {
        };

        this.viewList = new List<BaseView>()
        {
           new  FightMainView(),
        };
        this.protoList = new List<uint>()
        {

        };
        this._preloadOrder = new FightPackagePreload(this);
    }

    public List<IBaseView> getAllList()
    {
        return this.getPackAllUIMidList();
    }
}