using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIWidget;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mid_GunAttriTopPanel : MonoBehaviour, IMiddleware
{
	public GameObject main;

	public GunInfoView gunAttriview;
	public GunInfoView addAttriInfoview;
	public ImageWidget colseBtnBg;
	void Awake()
	{
		GameObject go = this.gameObject;
		main = this.gameObject;
		gunAttriview = go.transform.Find("GunAttriPanel").GetComponent<GunInfoView>();
		addAttriInfoview = go.transform.Find("AddAttriPanel").GetComponent<GunInfoView>();

		Transform colseBtnBgObj = this.transform.Find("CloseBg");
		if (colseBtnBgObj != null)
		{
			colseBtnBg = colseBtnBgObj.GetComponent<ImageWidget>();
		}
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
		if (main != null) GameObject.Destroy(main);
#endif
	}

}
