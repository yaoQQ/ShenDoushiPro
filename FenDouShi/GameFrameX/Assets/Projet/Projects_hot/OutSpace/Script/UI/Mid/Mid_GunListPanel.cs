using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIWidget;
using UnityEngine.UI;

public class Mid_GunListPanel:MonoBehaviour,IMiddleware
{
	public GameObject main;
	public UIWidget.PanelWidget GunListPanel;
	public GunAttriItem gunItem;
	public Text titleText;
	public Text InfoTip;
	public ButtonWidget closeBtn;

	void Awake() 
	{
		GameObject go =  this.gameObject;
		main =  this.gameObject;
		GunListPanel =  go.GetComponent<UIWidget.PanelWidget>();
		titleText = go.transform.Find("Title").GetComponent<Text>();
		InfoTip = go.transform.Find("InfoTip").GetComponent<Text>();
		closeBtn = go.transform.Find("colseBtn").GetComponent<ButtonWidget>();
		gunItem = go.transform.Find("Viewport/Content/GunItem").gameObject.AddComponent<GunAttriItem>();
		Debug.Log("@@@@@@@@@@@@@gunItem=" + gunItem);
		gunItem.gameObject.SetActive(false);
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
