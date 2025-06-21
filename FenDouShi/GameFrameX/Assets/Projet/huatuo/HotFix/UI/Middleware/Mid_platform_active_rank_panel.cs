using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIWidget;

public class Mid_platform_active_rank_panel:MonoBehaviour,IMiddleware
{
	public class Rank_Cell
	{
		public GameObject main;
		public ImageWidget highLight_Image;
		public IconWidget rank_Icon;
		public TextWidget rank_Text;
		public CircleImageWidget head_CircleImage;
		public TextWidget player_name_Text;
		public TextWidget use_Time_Text;
		public Rank_Cell(GameObject itemGo)
		{
			this.main=itemGo;
			highLight_Image =  itemGo.transform.Find("highLight_Image").GetComponent<ImageWidget>();
			rank_Icon =  itemGo.transform.Find("rank_Icon").GetComponent<IconWidget>();
			rank_Text =  itemGo.transform.Find("rank_Text").GetComponent<TextWidget>();
			head_CircleImage =  itemGo.transform.Find("head_CircleImage").GetComponent<CircleImageWidget>();
			player_name_Text =  itemGo.transform.Find("player_name_Text").GetComponent<TextWidget>();
			use_Time_Text =  itemGo.transform.Find("use_Time_Text").GetComponent<TextWidget>();
		}
	}
	public GameObject main;
	public CircleImageWidget back_Image;
	public TextWidget change_type_Text;
	public TextWidget rank_title_Text;
	public TextWidget player_title_Text;
	public TextWidget time_title_Text ;
	public IconWidget none_game;
	public TextWidget none_game_txt;
	public ToggleWidget eliminateToggle;
	public ToggleWidget bowlingToggle;
	public PanelWidget Mid_Panel;
	public CellRecycleScrollWidget rank_CellRecycleScrollPanel;
	public PanelWidget buttom_Panel;
	public TextWidget buttom_rank_Text;
	public TextWidget buttom_time_Text;
	public ButtonWidget sendRankBtn;
	public InputFieldWidget valueField;
	public ButtonWidget refreshRankBtn;
	public Rank_Cell[] rank_CellArr;

	void Awake() 
	{
		GameObject go =  this.gameObject;
		main =  this.gameObject;
		back_Image =  go.transform.Find("Panel/back_Image").GetComponent<CircleImageWidget>();
		change_type_Text =  go.transform.Find("Panel/change_type_Text").GetComponent<TextWidget>();
		rank_title_Text =  go.transform.Find("Panel/rank_title_Text").GetComponent<TextWidget>();
		player_title_Text =  go.transform.Find("Panel/player_title_Text").GetComponent<TextWidget>();
		time_title_Text  =  go.transform.Find("Panel/time_title_Text ").GetComponent<TextWidget>();
		none_game =  go.transform.Find("Panel/none_game").GetComponent<IconWidget>();
		none_game_txt =  go.transform.Find("Panel/none_game/none_game_txt").GetComponent<TextWidget>();
		eliminateToggle =  go.transform.Find("Panel/ToggleGroup/eliminateToggle").GetComponent<ToggleWidget>();
		bowlingToggle =  go.transform.Find("Panel/ToggleGroup/bowlingToggle").GetComponent<ToggleWidget>();
		Mid_Panel =  go.transform.Find("Mid_Panel").GetComponent<PanelWidget>();
		rank_CellRecycleScrollPanel =  go.transform.Find("Mid_Panel/rank_CellRecycleScrollPanel").GetComponent<CellRecycleScrollWidget>();
		buttom_Panel =  go.transform.Find("buttom_Panel").GetComponent<PanelWidget>();
		buttom_rank_Text =  go.transform.Find("buttom_Panel/buttom_rank_Text").GetComponent<TextWidget>();
		buttom_time_Text =  go.transform.Find("buttom_Panel/buttom_time_Text").GetComponent<TextWidget>();
		sendRankBtn =  go.transform.Find("sendRankBtn").GetComponent<ButtonWidget>();
		valueField =  go.transform.Find("valueField").GetComponent<InputFieldWidget>();
		refreshRankBtn =  go.transform.Find("refreshRankBtn").GetComponent<ButtonWidget>();
		List<Rank_Cell> rank_CellList=new List<Rank_Cell>() ;
		rank_CellList.Add(new Rank_Cell(go.transform.Find("Mid_Panel/rank_CellRecycleScrollPanel/content/cellitem").gameObject));
		rank_CellList.Add(new Rank_Cell(go.transform.Find("Mid_Panel/rank_CellRecycleScrollPanel/content/cellitem_1").gameObject));
		rank_CellList.Add(new Rank_Cell(go.transform.Find("Mid_Panel/rank_CellRecycleScrollPanel/content/cellitem_2").gameObject));
		rank_CellList.Add(new Rank_Cell(go.transform.Find("Mid_Panel/rank_CellRecycleScrollPanel/content/cellitem_3").gameObject));
		rank_CellList.Add(new Rank_Cell(go.transform.Find("Mid_Panel/rank_CellRecycleScrollPanel/content/cellitem_4").gameObject));
		rank_CellList.Add(new Rank_Cell(go.transform.Find("Mid_Panel/rank_CellRecycleScrollPanel/content/cellitem_5").gameObject));
		rank_CellList.Add(new Rank_Cell(go.transform.Find("Mid_Panel/rank_CellRecycleScrollPanel/content/cellitem_6").gameObject));
		rank_CellList.Add(new Rank_Cell(go.transform.Find("Mid_Panel/rank_CellRecycleScrollPanel/content/cellitem_7").gameObject));
		rank_CellList.Add(new Rank_Cell(go.transform.Find("Mid_Panel/rank_CellRecycleScrollPanel/content/cellitem_8").gameObject));
		rank_CellList.Add(new Rank_Cell(go.transform.Find("Mid_Panel/rank_CellRecycleScrollPanel/content/cellitem_9").gameObject));
		rank_CellList.Add(new Rank_Cell(go.transform.Find("Mid_Panel/rank_CellRecycleScrollPanel/content/cellitem_10").gameObject));
		rank_CellList.Add(new Rank_Cell(go.transform.Find("Mid_Panel/rank_CellRecycleScrollPanel/content/cellitem_11").gameObject));
		rank_CellArr=rank_CellList.ToArray();
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
