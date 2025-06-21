using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIWidget;

public class Mid_common_popup_window_panel:MonoBehaviour,IMiddleware
{
	public GameObject main;
	public PanelWidget popup_panel;
	public ImageWidget mask;
	public ImageWidget bg_image;
	public TextWidget info_text;
	public TextWidget title_text;
	public PanelWidget buttons_container;
	public ButtonWidget btn_1;
	public ButtonWidget btn_2;

	void Awake() 
	{
		GameObject go =  this.gameObject;
		main =  this.gameObject;
		popup_panel =  go.transform.Find("popup_panel").GetComponent<PanelWidget>();
		mask =  go.transform.Find("popup_panel/mask").GetComponent<ImageWidget>();
		bg_image =  go.transform.Find("popup_panel/bg_image").GetComponent<ImageWidget>();
		info_text =  go.transform.Find("popup_panel/bg_image/info_text").GetComponent<TextWidget>();
		title_text =  go.transform.Find("popup_panel/bg_image/info_text/title_text").GetComponent<TextWidget>();
		buttons_container =  go.transform.Find("popup_panel/bg_image/buttons_container").GetComponent<PanelWidget>();
		btn_1 =  go.transform.Find("popup_panel/bg_image/buttons_container/btn_1").GetComponent<ButtonWidget>();
		btn_2 =  go.transform.Find("popup_panel/bg_image/buttons_container/btn_2").GetComponent<ButtonWidget>();
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
