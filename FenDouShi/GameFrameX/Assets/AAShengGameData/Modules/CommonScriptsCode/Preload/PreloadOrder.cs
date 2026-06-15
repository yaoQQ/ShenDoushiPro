using System.Collections.Generic;

public interface PreloadOrder
{

    PreloadStyle getPreloadStyle();

    //test@@@
    List<IBaseView> getUIPreload();
    IScene getScenePreload();
    
    void onPreloadEnd();
    void onPreloadStepEnd(string loadStr=null);
    void release(IBaseView baseView);

    void Dispose();
}
