using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIWidget;
using UnityEngine.UI;

public class Mid_GunInfoPanel:MonoBehaviour,IMiddleware
{
	public GameObject main;

	public GunInfoView gunInfoview;
	public SelectGunListView selectGunListView;
	void Awake() 
	{
		GameObject go =  this.gameObject;
		main =  this.gameObject;
		gunInfoview = go.transform.Find("GunInfo").GetComponent<GunInfoView>();
		selectGunListView = go.transform.Find("GunListPanel").GetComponent<SelectGunListView>();
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
