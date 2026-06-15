using DataTableFrame;
using System.Collections.Generic;
/// <summary>
/// Base 资源包类 包含相关view 和module注册 初始化
/// </summary>
public class GameMainPackage : AbstractPackage
{

    public GameMainPackage()
    {
        Logger.PrintColor("yellow", "GameMainPackage()类初始化完成");
        this.packName = PackageEnum.GameMainPackage;
        this.moduleList = new List<BaseModule>()
        {
            //new ShopModule(),
            //new BagModule(),
            new GmModule(),
          //  new RoleModel(),
        };

        this.viewList = new List<BaseView>()
        {
             new GameMainView(),
             new RedPointTest_View(),
             new HeroView(),
             new MailView(),
             new MailDeleteView(),
             new SystemOpenView(),
             new SystemNewOpenView(),
             new ChatView(),
             new ChatEmojiView(),
        };
        this.protoList = new List<uint>()
        {

        };
        this.tableList = new List<string>
        {
            "item",
            "mail",
            "func",
            "func_condition",
            "func_task",
            "jump",
        };

        this._preloadOrder = new GameMainPackagePreload(this);
    }

    public List<IBaseView> getAllList()
    {
        return this.getPackAllUIMidList();
    }
}