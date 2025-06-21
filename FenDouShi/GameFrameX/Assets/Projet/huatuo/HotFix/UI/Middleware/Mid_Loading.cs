using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIWidget;

public class Mid_Loading:MonoBehaviour,IMiddleware
{
	public GameObject main;
	public TextWidget loadingText;
	public SliderWidget slider;

	void Awake() 
	{
		GameObject go =  this.gameObject;
		main =  this.gameObject;
		loadingText =  go.transform.Find("loadingText").GetComponent<TextWidget>();
		slider =  go.transform.Find("slider").GetComponent<SliderWidget>();
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
