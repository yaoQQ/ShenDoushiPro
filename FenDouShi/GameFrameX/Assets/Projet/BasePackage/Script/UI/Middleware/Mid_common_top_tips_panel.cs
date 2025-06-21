using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIWidget;

public class Mid_common_top_tips_panel:MonoBehaviour,IMiddleware
{
	public GameObject main;
	public ImageWidget topTips_bg_Image;
	public TextWidget topTips_Text;

	void Awake() 
	{
		GameObject go =  this.gameObject;
		main =  this.gameObject;
		topTips_bg_Image =  go.transform.Find("topTips/topTips_bg_Image").GetComponent<ImageWidget>();
		topTips_Text =  go.transform.Find("topTips/topTips_Text").GetComponent<TextWidget>();
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
