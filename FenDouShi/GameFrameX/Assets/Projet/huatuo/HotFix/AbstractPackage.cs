using UnityEngine;
using System.Collections.Generic;
using System;



/// <summary>
/// *****资源包基类  命名与dll名相同
/// </summary>
public class AbstractPackage
{
    protected bool initSign = false;
    protected string packName;

    protected List<BaseModule> moduleList;
    protected List<uint> protoList;

    protected List<BaseView> viewList;
    protected List<GameObject> modelList;
    private List<uint> tableList;
    private LuaPreloadOrder _preloadOrder =null;

    // private Action
    public void init(Action finishCallback)
    {
        Logger.PrintColor("white", packName + " init() this.initSign="+ this.initSign);
        if (string.IsNullOrEmpty(packName))
        {
            Logger.PrintError(this+ " 模块名没有初始化");
        }

        if (this.initSign == false)
        {
            this.initSign = true;
            this.initModule();
            this.initView();
            this.initTable();
            if (this.protoList.Count > 0)
            {
                this.initProto(finishCallback);
            }
            else
            {
                if (finishCallback != null)
                {
                    finishCallback();
                }
            }
        }
        else
        {
            if (finishCallback != null)
            {
                finishCallback();
            }
        }
    }
    private void initModule()
    {
        if (this.moduleList == null)
        {
            Logger.PrintError(this.packName + " 模块列表没有初始化");
        }
        else
        {
            for(int i = 0; i < this.moduleList.Count; i++)
            {
              
                ModuleManager.Instance.RegisterLuaModule(this.moduleList[i]);
            }
        }
    }

    private void initProto(Action finishCallback)
    {
        if (this.protoList == null)
        {
            Logger.PrintError(this.packName + " 协议列表没有初始化");
        }
        else
        {
            // ProtobufManager.initPackPb(this.packName, this.protoList, finishCallback)
        }
    }

    private void initView()
    {
        Logger.PrintColor("white", packName + " initView()");
        if (this.viewList == null)
        {
            Logger.PrintError(this.packName + " 视图列表没有初始化");
        }
        else
        {
            for(int i = 0; i < this.viewList.Count; i++)
            {
                Logger.PrintColor("white", packName +i+ " initView() this.viewList"+ this.viewList[i].getViewEnum());
                UIViewManager.Instance.RegisterView(this.packName, this.viewList[i]);
            }
        }
    }

    //配置文件
    private void initTable()
    {

    }
    public LuaPreloadOrder PreloadOrder
    {
        set
        {
            _preloadOrder = value;
        }
        get
        {
            return _preloadOrder;
        }
    }
    public List<BaseView> getPackAllUIMidList()
    {
        if (this.viewList == null)
        {
            return null;
        }
        else
        {
            List<BaseView> arr = new List<BaseView>();
            for (int i = 0; i < this.viewList.Count; i++)
            {
                BaseView view = this.viewList[i];
                arr.Add(view);
            }
            return arr;
        }
    }
    public void ReleaseView(BaseView baseView)
    {
        if (baseView!=null)
        {
            viewList.Remove(baseView);
            destoyView(baseView);
        }

        if (_preloadOrder != null)
        {
            _preloadOrder.release(baseView);
        }
    }
    private void destoyView(BaseView baseView)
    {
       
        GameObject ViewGameObject = baseView.getViewGO();
        if (ViewGameObject != null)
        {
            GameObject.DestroyImmediate(ViewGameObject);
        }
        baseView.onDestroy();

    }
    
    public void ReleaseAll()
    {
        if (viewList != null)
        {
            for (int i = 0; i < viewList.Count; i++)
            {
                BaseView baseView = viewList[i];
                destoyView(baseView);

            }
            viewList = null;
        }
        
        if (modelList != null)
        {
            for (int i = 0; i < modelList.Count; i++)
            {
                GameObject.Destroy(modelList[i]);
            }
            modelList = null;
        }
        if (moduleList != null)
        {
            for(int i = 0; i < moduleList.Count; i++)
            {
                moduleList[i].resetModule();
            }
            moduleList = null;
        }
        protoList = null;
        if (_preloadOrder != null)
        {
            if (_preloadOrder.getUIPreload() != null)
            {
                _preloadOrder.getUIPreload().Clear();
            }

            _preloadOrder = null;

        }
        this.initSign = false;
    }
}