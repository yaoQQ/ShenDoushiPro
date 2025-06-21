
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 预加载包基类
/// </summary>
public class AbstractPreloadOrder : PreloadOrder
{
    protected PreloadStyle preloadStyle= PreloadStyle.None;
    protected List<IBaseView> uiPreloadList;
    protected IScene scenePreload;
    protected int loadCount = 0;
    public AbstractPreloadOrder()
    {

    }

    public  PreloadStyle getPreloadStyle()
    {
        return preloadStyle;
    }
    public  List<IBaseView> getUIPreload()
    {
        return uiPreloadList;
    }
    public IScene getScenePreload()
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
            //+Logger.PrintError("uiPreloadList预加载未赋值 确定初始时显示界面？");
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
    public void release(IBaseView baseView)
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
