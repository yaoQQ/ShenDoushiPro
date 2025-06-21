using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIWidget;
using UnityEngine.UI;

public class Mid_platform_game_panel:MonoBehaviour,IMiddleware
{
	public class HotGameItem
	{
		public GameObject main;
		public UIWidget.IconWidget bg_icon;
		public UIWidget.ImageWidget game_image;
		public UIWidget.TextWidget game_name_text;
		public UIWidget.TextWidget game_introduce_text;
		public HotGameItem(GameObject itemGo)
		{
			this.main=itemGo;
			bg_icon =  itemGo.transform.Find("bg_icon").GetComponent<UIWidget.IconWidget>();
			game_image =  itemGo.transform.Find("game_bg_image/game_image").GetComponent<UIWidget.ImageWidget>();
			game_name_text =  itemGo.transform.Find("game_name_text").GetComponent<UIWidget.TextWidget>();
			game_introduce_text =  itemGo.transform.Find("game_introduce_text").GetComponent<UIWidget.TextWidget>();
		}
	}
	public GameObject main;
	public UIWidget.BannerWidget head_banner;
	public UIWidget.PanelWidget hot_game_panel;
	public UIWidget.TextWidget hot_no_text;
	public UIWidget.ImageWidget hot_bg_image;
	public UIWidget.CellRecycleScrollWidget hot_group;
	public UIWidget.ButtonWidget closeBtn;
	public HotGameItem[] hotGameItemArr;

	void Awake() 
	{
		GameObject go =  this.gameObject;
		main =  this.gameObject;
		head_banner =  go.transform.Find("head_banner").GetComponent<UIWidget.BannerWidget>();
		hot_game_panel =  go.transform.Find("hot_game_panel").GetComponent<UIWidget.PanelWidget>();
		hot_no_text =  go.transform.Find("hot_game_panel/hot_no_text").GetComponent<UIWidget.TextWidget>();
		hot_bg_image =  go.transform.Find("hot_game_panel/hot_bg_image").GetComponent<UIWidget.ImageWidget>();
		hot_group =  go.transform.Find("hot_game_panel/hot_group").GetComponent<UIWidget.CellRecycleScrollWidget>();
		closeBtn =  go.transform.Find("closeBtn").GetComponent<UIWidget.ButtonWidget>();
		List<HotGameItem> hotGameItemList=new List<HotGameItem>() ;
		hotGameItemList.Add(new HotGameItem(go.transform.Find("hot_game_panel/hot_group/content/cellitem1").gameObject));
		hotGameItemList.Add(new HotGameItem(go.transform.Find("hot_game_panel/hot_group/content/cellitem2").gameObject));
		hotGameItemList.Add(new HotGameItem(go.transform.Find("hot_game_panel/hot_group/content/cellitem3").gameObject));
		hotGameItemList.Add(new HotGameItem(go.transform.Find("hot_game_panel/hot_group/content/cellitem4").gameObject));
		hotGameItemList.Add(new HotGameItem(go.transform.Find("hot_game_panel/hot_group/content/cellitem5").gameObject));
		hotGameItemList.Add(new HotGameItem(go.transform.Find("hot_game_panel/hot_group/content/cellitem6").gameObject));
		hotGameItemList.Add(new HotGameItem(go.transform.Find("hot_game_panel/hot_group/content/cellitem7").gameObject));
		hotGameItemArr=hotGameItemList.ToArray();
	}

	public GameObject go 
	{
     get
	    {
	      return this.main;
	    }
	}

	public void DelReference() 
	{
#if TOOL
#else
		if(main!=null) GameObject.Destroy(main);
#endif
	}

}
