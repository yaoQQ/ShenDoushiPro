//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：2025-08-21 20:47:59.587
//------------------------------------------------------------

using FairyGUI;
using msg.role;
using roleLevel;
using System.Collections.Generic;

/// <summary>
/// 自定义 FairyGUIView 模板
/// Script Name: RoleLocationView
///	定义窗口标识UIViewEnum.RoleLocationView为类型名，否则请填入自定义UIViewEnum标识
/// </summary>
[FGUIViewAttribute(UIViewEnum.RoleLocationView,typeof(RoleLocationView))]
public class RoleLocationView : BaseView
{
	//G_RoleLocationView 为fairyGUI导出的组件名，默认为与类型同名。否则请修改为自定义的导出组件名
	public override string PackageName => G_RoleLocationView.PACKAGE_NAME;
	public override string ComponentName =>G_RoleLocationView.COMPONENT_NAME;
	G_RoleLocationView view;
	
	//注册界面事件
	protected override Dictionary<EEventType, OnEventLister> EventList => new()
	{
          { EEventType.RoleInfoGetLocationResp, Role_RoleLocationFun },

    };

    public RoleLocationView()
    {
        //（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.RoleLocationView, false);
        Logger.PrintColor("yellow", "RoleLocationView init!!");
    }
    void Role_RoleLocationFun(EventSysArgsBase argsBase)
	{
		if (view == null)
			return;
		if (argsBase is EventSysArgs<RoleInfoGetLocationResp> args)
		{
            LocationVo location = LocationDataManager.Instance.GetLocation(args.args1.Location.provinceCode, args.args1.Location.cityCode);
			if (location == null)
			{
				string msg = $"没有查找到 provinceCode={args.args1.Location.provinceCode} cityCode={args.args1.Location.cityCode} 的地区";
                MessageBoxVo msgVo = new MessageBoxVo();
                msgVo.title = "提示";
                msgVo.msg = msg;
                msgVo.OkBtnfunc = () =>
                {
                };
                CommonViewUtils.ShowMessageBox(msgVo);
                Logger.PrintError(msg);
				return;
			}
			view.locationText.text = location.ProvinceName;
        }
	}
	/// <summary>
    /// 界面加载完成,触发函数
    /// </summary>
    /// <param name="gameObject">当前界面的fairyGUI对象</param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
	    Logger.PrintColor("yellow", $"RoleLocationView onLoadUIEnd complte!! gameObject={gameObject}");
		this.contentPane = gameObject.asCom;
		contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
		view = contentPane as G_RoleLocationView;
		view.locationList.visible = false;
        view.ListBg.visible = false;
		view.showLocationToggle.selected = true;
        view.showLocationToggle.onChanged.Add(OnShowLocationToggleChanged);
    }

	private void OnShowLocationToggleChanged()
	{
        view.showLocal.visible = view.showLocationToggle.selected;
        view.hideLocal.visible = !view.showLocationToggle.selected;
    }


    Dictionary<int, LocationVo> locationDic;

    private void initLocationList()
	{
        locationDic=LocationDataManager.Instance.InitLocationList();
		foreach (var item in locationDic)
		{
			Logger.PrintDebug($"locationDic key={item.Key} value={item.Value}");
		}
        view.locationList.SetVirtual();
        view.locationList.itemRenderer = RenderLocationItem;
		//view.locationList.itemProvider = MessageItemProvider;
        view.locationList.numItems = locationDic.Count;
        view.locationList.ScrollToView(0);
        view.locationList.onClickItem.Add(OnSelect_Local);
    }

	// 聊天记录
	string MessageItemProvider(int index)
	{
        return G_locationItem.URL;
	}

    // 显示地区
    void RenderLocationItem(int index, GObject item)
	{
		if (locationDic == null)
		{
			return;
		}
		Logger.PrintDebug($"RenderLocationItem index={index} locationDic.Count="+ locationDic.Count);
		if(index>= locationDic.Count-1)
		{
			return;
		}
		if (item is not G_locationItem localItem)
		{
			return;
		}
		Logger.PrintDebug($"RenderLocationItem index={index} localItem={localItem}");
		var locationInfo = locationDic[index + 1];
        localItem.localText.text = $"{locationInfo.ProvinceName},{locationInfo.CityName}";
        localItem.data = locationInfo;
    }

	private LocationVo selectLocalInfo;
    void OnSelect_Local(EventContext context)
    {
        G_locationItem localItem= context.data as G_locationItem;
        LocationVo data = (LocationVo)localItem.data;
        selectLocalInfo = data;
        view.locationText.text = data.ProvinceName;
        Logger.PrintDebug($"选择服务器区 LocationVo.CountryName={data.CountryName} LocationVo.ProvinceName={data.ProvinceName}");
    }

    //统一按钮回调
    protected override void OnButtonClick(GButton clickedButton)
	{
        if (view.closeBtn == clickedButton)
        {
            ToHide();
        }
		else if(view.SelectListBtn == clickedButton)
		{
            view.locationList.visible = !view.locationList.visible;
            view.ListBg.visible = view.locationList.visible;

            if (view.locationList.visible)
			{
                initLocationList();
                view.locationList.ScrollToView(view.locationList.numItems - 1);
			}
		}

		else if (view.autoLocationBtn == clickedButton)
		{
            RoleControl.Instance.ReqRoleInfoGetLocationReq();
		}
		else if (view.okBtn == clickedButton)
		{
			if (selectLocalInfo == null)
			{
				return;
			}
            RoleControl.Instance.ReqRoleInfoUpdateLocationReq(view.showLocationToggle.selected, selectLocalInfo.ProvinceCode, selectLocalInfo.CityCode);
		}
        
    }
	
	//打开界面,fairyGUI打开动画播放完触发
	protected override void OnShown()
	{
		base.OnShown();
        RoleControl.Instance.ReqRoleInfoGetLocationReq();
    }
	
	//关闭界面,fairyGUI关闭动画播放完触发
	protected override void OnHide()
	{
		base.OnHide();
	}
	
	//框架和fairyGUI底层销毁界面时触发
	protected override void OnDestroy() { 
	
	}
}