using System.Collections.Generic;
#if TOOL
using XLua;

[CSharpCallLua]
#endif
public interface LuaPreloadOrder
{

    PreloadStyle getPreloadStyle();

    List<BaseView> getUIPreload();
    LuaScene getScenePreload();
    
    void onPreloadEnd();
    void onPreloadStepEnd(string loadStr=null);
    void release(BaseView baseView);
}
