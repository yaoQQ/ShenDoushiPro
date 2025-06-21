using System.Collections.Generic;
#if TOOL
using XLua;

[CSharpCallLua]
#endif

public interface LuaPreloadOrder2
{

    PreloadStyle2 getPreloadStyle();

    //test@@@
  //  List<BaseView> getUIPreload();
    LuaScene2 getScenePreload();
    
    void onPreloadEnd();
    void onPreloadStepEnd(string loadStr=null);

}
