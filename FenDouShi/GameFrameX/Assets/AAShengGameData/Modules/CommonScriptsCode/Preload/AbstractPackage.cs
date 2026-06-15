using DataTableFrame;
using System;
using System.Collections.Generic;
using UnityEngine;
//对应包名
public enum PackageEnum
{
    LoginPackage,
    CommonScriptCode,
    GameMainPackage,
    FightPackage,
    None
}
/// <summary>
/// *****资源包基类  命名与dll名相同
/// </summary>
public class AbstractPackage
{
    protected bool initSign = false;
    protected PackageEnum packName= PackageEnum.None;

    protected List<BaseModule> moduleList;
    protected List<uint> protoList;

    protected List<BaseView> viewList;
    protected List<GameObject> modelList;
    protected List<string> tableList;
    protected PreloadOrder _preloadOrder =null;

    // private Action
    public void init(Action finishCallback)
    {
        Logger.PrintColor("white", packName + " init() this.initSign="+ this.initSign);
        if (packName== PackageEnum.None)
        {
            Logger.PrintError(this+ " 模块名没有初始化");
        }

        //对界面进行绑定
        AttributeBindManger.Instance.AutoBindAllViewAttriBinder();
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
              
                ModuleManager.Instance.RegisterModule(this.moduleList[i]);
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
            for (int i = 0; i < this.viewList.Count; i++)
            {
                //  Logger.PrintColor("white",  $"[{packName}] initView() this.viewList[{i}]=" + this.viewList[i].getViewEnum());
               //之后弃用
                UIViewManager.Instance.RegisterView(this.packName, this.viewList[i]);
            }
            List<BaseView> addListView =new List<BaseView>();
            //加载公共的界面
            if (packName == PackageEnum.LoginPackage)
            {
                addListView=  UIViewManager.Instance.RegisterViewByAttribute(PackageEnum.CommonScriptCode);
                this.viewList.AddRange(addListView);
            }
            addListView = UIViewManager.Instance.RegisterViewByAttribute(this.packName);
            this.viewList.AddRange(addListView);
        }
    }

    //配置文件
    private void initTable()
    {
        ConfigMgr.Instance.LoadAllConfig();
    }
    public PreloadOrder PreloadOrder
    {
        private set
        {
            _preloadOrder = value;
        }
        get
        {
            return _preloadOrder;
        }
    }
    public List<IBaseView> getPackAllUIMidList()
    {
        if (this.viewList == null)
        {
            return null;
        }
        else
        {
            List<IBaseView> arr = new List<IBaseView>();
            for (int i = 0; i < this.viewList.Count; i++)
            {
                IBaseView view = this.viewList[i];
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
    private void destoyView(IBaseView baseView)
    {
       
        baseView.Destroy();

    }
    
    public void ReleaseAll()
    {
        if (viewList != null)
        {
            for (int i = 0; i < viewList.Count; i++)
            {
                IBaseView baseView = viewList[i];
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
            _preloadOrder.Dispose();
            _preloadOrder = null;

        }
        this.initSign = false;
    }
}