using System.Collections.Generic;

public class OutSpacePackage : AbstractPackage
{
    public OutSpacePackage()
    {
        this.packName = ProjectControler.OutSpacePro;
        this.moduleList = new List<BaseModule>()
        {
            new  OutSpaceModule(),
        };

        this.viewList = new List<BaseView>()
        {
        //  new OutSpaceMainMenuView(),
#if TEST_SCENE
            new TopTipsView(),
#endif
          new GunInfoTopPanel(),
          new GunAttriTopPanel(),
           new GunListPanel(),
          new CharacterInfoTopPanel(),
        };
        this.protoList = new List<uint>()
        {

        };

        Logger.PrintDebug("@@@@OutSpacePackage() init ");
        //Logger.PrintDebug("@@@@@viewList[0]=" + viewList[0]);
    }
    public List<BaseView> getAllList()
    {
        return this.getPackAllUIMidList();
    }


}