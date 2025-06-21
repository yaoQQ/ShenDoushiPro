using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIWidget;
using UnityEngine.UI;

public class Mid_platform_game_update_panel:MonoBehaviour,IMiddleware
{
	public GameObject main;
	public UIWidget.ImageWidget mask_image;
	public UIWidget.PanelWidget game_down_panel;
	public UIWidget.EmptyImageWidget close_down_image;
	public UIWidget.ImageWidget down_game_image;
	public UIWidget.ImageWidget down_process_fg;
	public UIWidget.TextWidget down_game_text;
	public UIWidget.TextWidget down_percent_text;
	public UIWidget.TextWidget down_capacity_text;
	public UIWidget.ButtonWidget down_cancel_btn;
	public UIWidget.ImageWidget update_mask_image;
	public UIWidget.PanelWidget update_panel;
	public UIWidget.TextWidget update_text;
	public UIWidget.ButtonWidget update_left_btn;
	public UIWidget.ButtonWidget update_right_btn;

	void Awake() 
	{
		GameObject go =  this.gameObject;
		main =  this.gameObject;
		mask_image =  go.transform.Find("mask_image").GetComponent<UIWidget.ImageWidget>();
		game_down_panel =  go.transform.Find("game_down_panel").GetComponent<UIWidget.PanelWidget>();
		close_down_image =  go.transform.Find("game_down_panel/close_down_image").GetComponent<UIWidget.EmptyImageWidget>();
		down_game_image =  go.transform.Find("game_down_panel/down_game_bg/down_game_image").GetComponent<UIWidget.ImageWidget>();
		down_process_fg =  go.transform.Find("game_down_panel/down_process_bg/down_process_fg").GetComponent<UIWidget.ImageWidget>();
		down_game_text =  go.transform.Find("game_down_panel/down_game_text").GetComponent<UIWidget.TextWidget>();
		down_percent_text =  go.transform.Find("game_down_panel/down_percent_text").GetComponent<UIWidget.TextWidget>();
		down_capacity_text =  go.transform.Find("game_down_panel/down_capacity_text").GetComponent<UIWidget.TextWidget>();
		down_cancel_btn =  go.transform.Find("game_down_panel/down_cancel_btn").GetComponent<UIWidget.ButtonWidget>();
		update_mask_image =  go.transform.Find("update_mask_image").GetComponent<UIWidget.ImageWidget>();
		update_panel =  go.transform.Find("update_panel").GetComponent<UIWidget.PanelWidget>();
		update_text =  go.transform.Find("update_panel/update_text").GetComponent<UIWidget.TextWidget>();
		update_left_btn =  go.transform.Find("update_panel/update_left_btn").GetComponent<UIWidget.ButtonWidget>();
		update_right_btn =  go.transform.Find("update_panel/update_right_btn").GetComponent<UIWidget.ButtonWidget>();
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
