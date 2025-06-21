using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIWidget;
using UnityEngine.UI;

public class Mid_platform_game_rule_panel:MonoBehaviour,IMiddleware
{
	public class ShotItem
	{
		public GameObject main;
		public UIWidget.ImageWidget game_shot_image;
		public ShotItem(GameObject itemGo)
		{
			this.main=itemGo;
			game_shot_image =  itemGo.transform.Find("game_shot_image").GetComponent<UIWidget.ImageWidget>();
		}
	}
	public GameObject main;
	public UIWidget.ImageWidget mask_image;
	public UIWidget.ImageWidget bg_image;
	public UIWidget.TextWidget rule_title_text;
	public UIWidget.TextWidget rule_content_text;
	public UIWidget.ButtonWidget enter_game_btn;
	public UIWidget.PanelWidget game_shot_panel;
	public UIWidget.CellRecycleScrollWidget game_shot_scroll_panel;
	public UIWidget.EmptyImageWidget close_rule_image;
	public ShotItem[] shotItemArr;

	void Awake() 
	{
		GameObject go =  this.gameObject;
		main =  this.gameObject;
		mask_image =  go.transform.Find("mask_image").GetComponent<UIWidget.ImageWidget>();
		bg_image =  go.transform.Find("bg_image").GetComponent<UIWidget.ImageWidget>();
		rule_title_text =  go.transform.Find("bg_image/rule_title_text").GetComponent<UIWidget.TextWidget>();
		rule_content_text =  go.transform.Find("bg_image/rule_content_text").GetComponent<UIWidget.TextWidget>();
		enter_game_btn =  go.transform.Find("bg_image/enter_game_btn").GetComponent<UIWidget.ButtonWidget>();
		game_shot_panel =  go.transform.Find("bg_image/game_shot_panel").GetComponent<UIWidget.PanelWidget>();
		game_shot_scroll_panel =  go.transform.Find("bg_image/game_shot_panel/game_shot_scroll_panel").GetComponent<UIWidget.CellRecycleScrollWidget>();
		close_rule_image =  go.transform.Find("bg_image/close_rule_image").GetComponent<UIWidget.EmptyImageWidget>();
		List<ShotItem> shotItemList=new List<ShotItem>() ;
		shotItemList.Add(new ShotItem(go.transform.Find("bg_image/game_shot_panel/game_shot_scroll_panel/content/cellitem").gameObject));
		shotItemList.Add(new ShotItem(go.transform.Find("bg_image/game_shot_panel/game_shot_scroll_panel/content/cellitem_1").gameObject));
		shotItemList.Add(new ShotItem(go.transform.Find("bg_image/game_shot_panel/game_shot_scroll_panel/content/cellitem_2").gameObject));
		shotItemList.Add(new ShotItem(go.transform.Find("bg_image/game_shot_panel/game_shot_scroll_panel/content/cellitem_3").gameObject));
		shotItemArr=shotItemList.ToArray();
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
