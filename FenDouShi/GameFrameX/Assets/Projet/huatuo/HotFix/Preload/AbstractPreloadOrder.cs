
using System;
using System.Collections.Generic;
using UnityEngine;

public enum PreloadStyle
{
    None =-1,
    FullLoadingBar=1,//--全屏加载条
    Annulus =2,//--光圈加载
}

/// <summary>
/// 预加载包基类
/// </summary>
public class AbstractPreloadOrder : LuaPreloadOrder
{
    protected PreloadStyle preloadStyle= PreloadStyle.None;
    protected List<BaseView> uiPreloadList;
    protected LuaScene scenePreload;
    protected int loadCount = 0;
    public AbstractPreloadOrder()
    {

    }

    public  PreloadStyle getPreloadStyle()
    {
        return preloadStyle;
    }
    public  List<BaseView> getUIPreload()
    {
        return uiPreloadList;
    }
    public LuaScene getScenePreload()
    {
        return scenePreload;
    }
    public int getPreLoadCount()
    {
        int uiCount = 0;
        int sceneCount = 0;
        if (uiPreloadList != null)
        {
            uiCount = uiPreloadList.Count;
        }
        if (scenePreload!=null)
        {
            sceneCount = 1;
        }

        return uiCount + sceneCount; 
    }
    public virtual void onPreloadEnd()
    {
        if (uiPreloadList == null)
        {
            //+Loger.PrintError("uiPreloadList预加载未赋值 确定初始时显示界面？");
            return;
        }
        //for (int i = 0; i < uiPreloadList.Count; i++)
        //{
        //    uiPreloadList[i].getViewGO().SetActive(false);
        //}
        Logger.PrintColor("blue", "onPreloadEnd()========UI和场景预加载完成=========");
       
    }
    public virtual void onPreloadStepEnd(string loadStr)
    {
       
      
    }
    public void release(BaseView baseView)
    {
        if (uiPreloadList != null)
        {
            if (baseView != null)
            {
                uiPreloadList.Remove(baseView);
            }
        }
    }
}
