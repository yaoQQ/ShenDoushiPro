using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIWidget;

public class Mid_common_top_cost_panel:MonoBehaviour,IMiddleware
{
	public GameObject main;
	public ImageWidget head_Image;
	public CircleImageWidget head_Icon;
	public EmptyImageWidget press_Image;
	public ImageWidget top_gold;
	public EffectWidget goldEffect;
	public TextWidget top_gold_Text;
	public EmptyImageWidget top_gold_Button;
	public ImageWidget top_diamond;
	public EffectWidget diamondEffect;
	public TextWidget top_diamond_Text;
	public EmptyImageWidget top_diamond_Button;
	public ImageWidget top_yo_card;
	public EffectWidget ucardEffect;
	public TextWidget top_yo_card_Text;
	public EmptyImageWidget top_yo_card_Button;
	public ImageWidget top_packet;
	public EffectWidget packetEffect;
	public TextWidget top_packet_Text;
	public EmptyImageWidget top_packet_Button;

	void Awake() 
	{
		GameObject go =  this.gameObject;
		main =  this.gameObject;
		head_Image =  go.transform.Find("head_Image").GetComponent<ImageWidget>();
		head_Icon =  go.transform.Find("head_Image/head_Icon").GetComponent<CircleImageWidget>();
		press_Image =  go.transform.Find("head_Image/press_Image").GetComponent<EmptyImageWidget>();
		top_gold =  go.transform.Find("top_gold").GetComponent<ImageWidget>();
		goldEffect =  go.transform.Find("top_gold/Image/top_gold_Image/goldEffect").GetComponent<EffectWidget>();
		top_gold_Text =  go.transform.Find("top_gold/Image/top_gold_Text").GetComponent<TextWidget>();
		top_gold_Button =  go.transform.Find("top_gold/top_gold_Button").GetComponent<EmptyImageWidget>();
		top_diamond =  go.transform.Find("top_diamond").GetComponent<ImageWidget>();
		diamondEffect =  go.transform.Find("top_diamond/Image/top_diamond_Image/diamondEffect").GetComponent<EffectWidget>();
		top_diamond_Text =  go.transform.Find("top_diamond/Image/top_diamond_Text").GetComponent<TextWidget>();
		top_diamond_Button =  go.transform.Find("top_diamond/top_diamond_Button").GetComponent<EmptyImageWidget>();
		top_yo_card =  go.transform.Find("top_yo_card").GetComponent<ImageWidget>();
		ucardEffect =  go.transform.Find("top_yo_card/Image/top_yo_card_Image/ucardEffect").GetComponent<EffectWidget>();
		top_yo_card_Text =  go.transform.Find("top_yo_card/Image/top_yo_card_Text").GetComponent<TextWidget>();
		top_yo_card_Button =  go.transform.Find("top_yo_card/top_yo_card_Button").GetComponent<EmptyImageWidget>();
		top_packet =  go.transform.Find("top_packet").GetComponent<ImageWidget>();
		packetEffect =  go.transform.Find("top_packet/Image/top_yo_card_Image/packetEffect").GetComponent<EffectWidget>();
		top_packet_Text =  go.transform.Find("top_packet/Image/top_packet_Text").GetComponent<TextWidget>();
		top_packet_Button =  go.transform.Find("top_packet/top_packet_Button").GetComponent<EmptyImageWidget>();
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
