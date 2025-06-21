using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIWidget;

public class Mid_platform_mall_panel:MonoBehaviour,IMiddleware
{
	public class Diamondcell
	{
		public GameObject main;
		public ImageWidget go_circle_image;
		public IconWidget go_Icon;
		public EffectWidget mallEffect;
		public TextWidget return_num_Text;
		public TextWidget cost_Text;
		public ButtonWidget go_circle_Button;
		public Diamondcell(GameObject itemGo)
		{
			this.main=itemGo;
			go_circle_image =  itemGo.transform.Find("go_circle_image").GetComponent<ImageWidget>();
			go_Icon =  itemGo.transform.Find("go_Icon").GetComponent<IconWidget>();
			mallEffect =  itemGo.transform.Find("go_Icon/Panel/mallEffect").GetComponent<EffectWidget>();
			return_num_Text =  itemGo.transform.Find("return_num_Text").GetComponent<TextWidget>();
			cost_Text =  itemGo.transform.Find("cost_Text").GetComponent<TextWidget>();
			go_circle_Button =  itemGo.transform.Find("go_circle_Button").GetComponent<ButtonWidget>();
		}
	}
	public class Goldcell
	{
		public GameObject main;
		public ImageWidget go_circle_image;
		public IconWidget go_Icon;
		public EffectWidget mallEffect;
		public TextWidget return_num_Text;
		public TextWidget cost_Text;
		public ButtonWidget go_circle_Button;
		public Goldcell(GameObject itemGo)
		{
			this.main=itemGo;
			go_circle_image =  itemGo.transform.Find("go_circle_image").GetComponent<ImageWidget>();
			go_Icon =  itemGo.transform.Find("go_Icon").GetComponent<IconWidget>();
			mallEffect =  itemGo.transform.Find("go_Icon/mallEffect").GetComponent<EffectWidget>();
			return_num_Text =  itemGo.transform.Find("return_num_Text").GetComponent<TextWidget>();
			cost_Text =  itemGo.transform.Find("cost_Text").GetComponent<TextWidget>();
			go_circle_Button =  itemGo.transform.Find("go_circle_Button").GetComponent<ButtonWidget>();
		}
	}
	public class YoCardcell
	{
		public GameObject main;
		public ImageWidget go_circle_image;
		public ImageWidget go_Image;
		public TextWidget cost_Text;
		public TextWidget return_num_Text;
		public ButtonWidget go_circle_Button;
		public YoCardcell(GameObject itemGo)
		{
			this.main=itemGo;
			go_circle_image =  itemGo.transform.Find("go_circle_image").GetComponent<ImageWidget>();
			go_Image =  itemGo.transform.Find("go_Image").GetComponent<ImageWidget>();
			cost_Text =  itemGo.transform.Find("cost_Text").GetComponent<TextWidget>();
			return_num_Text =  itemGo.transform.Find("return_num_Text").GetComponent<TextWidget>();
			go_circle_Button =  itemGo.transform.Find("go_circle_Button").GetComponent<ButtonWidget>();
		}
	}
	public class BlendCell
	{
		public GameObject main;
		public ImageWidget return_image;
		public ButtonWidget return_btn;
		public TextWidget return_num_text;
		public TextWidget cost_text;
		public ImageWidget cost_image;
		public EffectWidget mall_effect;
		public BlendCell(GameObject itemGo)
		{
			this.main=itemGo;
			return_image =  itemGo.transform.Find("return_image").GetComponent<ImageWidget>();
			return_btn =  itemGo.transform.Find("return_btn").GetComponent<ButtonWidget>();
			return_num_text =  itemGo.transform.Find("return_num_text").GetComponent<TextWidget>();
			cost_text =  itemGo.transform.Find("cost_text").GetComponent<TextWidget>();
			cost_image =  itemGo.transform.Find("cost_text/cost_image").GetComponent<ImageWidget>();
			mall_effect =  itemGo.transform.Find("mall_effect").GetComponent<EffectWidget>();
		}
	}
	public GameObject main;
	public IconWidget diamond_Icon;
	public IconWidget gold_Icon;
	public PanelWidget diamond_Panel;
	public GridRecycleScrollWidget diamond_GridRecycleScrollPanel;
	public TextWidget diamong_diamond_Text;
	public TextWidget diamong_gold_Text;
	public PanelWidget gold_Panel;
	public GridRecycleScrollWidget gold_GridRecycleScrollPanel;
	public PanelWidget gold_top_Panel;
	public TextWidget gold_diamond_Text;
	public TextWidget gold_gold_Text;
	public PanelWidget yo_card_Panel;
	public GridRecycleScrollWidget yo_card_GridRecycleScrollPanel;
	public PanelWidget yo_card_top_Panel;
	public TextWidget yo_card_num_Text;
	public PanelWidget blend_panel;
	public IconWidget gold_icon_pool;
	public IconWidget diamond_icon_pool;
	public IconWidget cost_icon_pool;
	public GridRecycleScrollWidget blend_grid_scroll;
	public ImageWidget bg_Image;
	public EmptyImageWidget close_Image;
	public ImageWidget top_packet;
	public EffectWidget packetEffect;
	public TextWidget top_packet_Text;
	public EmptyImageWidget top_packet_Button;
	public ImageWidget top_yo_card;
	public EffectWidget ucardEffect;
	public TextWidget top_yo_card_Text;
	public EmptyImageWidget top_yo_card_Button;
	public ImageWidget top_diamond;
	public EffectWidget diamondEffect;
	public TextWidget top_diamond_Text;
	public EmptyImageWidget top_diamond_Button;
	public ImageWidget top_gold;
	public EffectWidget goldEffect;
	public TextWidget top_gold_Text;
	public EmptyImageWidget top_gold_Button;
	public ImageWidget head_Image;
	public CircleImageWidget head_Icon;
	public EmptyImageWidget press_Image;
	public Diamondcell[] diamondcellArr;
	public Goldcell[] goldcellArr;
	public YoCardcell[] yoCardcellArr;
	public BlendCell[] blendCellArr;

	void Awake() 
	{
		GameObject go =  this.gameObject;
		main =  this.gameObject;
		diamond_Icon =  go.transform.Find("mid_Panel/diamond_Icon").GetComponent<IconWidget>();
		gold_Icon =  go.transform.Find("mid_Panel/gold_Icon").GetComponent<IconWidget>();
		diamond_Panel =  go.transform.Find("mid_Panel/diamond_Panel").GetComponent<PanelWidget>();
		diamond_GridRecycleScrollPanel =  go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel").GetComponent<GridRecycleScrollWidget>();
		diamong_diamond_Text =  go.transform.Find("mid_Panel/diamond_Panel/diamond_top_Panel/diamong_logo_Image/diamong_diamond_Text").GetComponent<TextWidget>();
		diamong_gold_Text =  go.transform.Find("mid_Panel/diamond_Panel/diamond_top_Panel/golg_logo_Image/diamong_gold_Text").GetComponent<TextWidget>();
		gold_Panel =  go.transform.Find("mid_Panel/gold_Panel").GetComponent<PanelWidget>();
		gold_GridRecycleScrollPanel =  go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel").GetComponent<GridRecycleScrollWidget>();
		gold_top_Panel =  go.transform.Find("mid_Panel/gold_Panel/gold_top_Panel").GetComponent<PanelWidget>();
		gold_diamond_Text =  go.transform.Find("mid_Panel/gold_Panel/gold_top_Panel/diamong_logo_Image/gold_diamond_Text").GetComponent<TextWidget>();
		gold_gold_Text =  go.transform.Find("mid_Panel/gold_Panel/gold_top_Panel/golg_logo_Image/gold_gold_Text").GetComponent<TextWidget>();
		yo_card_Panel =  go.transform.Find("mid_Panel/yo_card_Panel").GetComponent<PanelWidget>();
		yo_card_GridRecycleScrollPanel =  go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel").GetComponent<GridRecycleScrollWidget>();
		yo_card_top_Panel =  go.transform.Find("mid_Panel/yo_card_Panel/yo_card_top_Panel").GetComponent<PanelWidget>();
		yo_card_num_Text =  go.transform.Find("mid_Panel/yo_card_Panel/yo_card_top_Panel/yo_card_logo_Image/yo_card_num_Text").GetComponent<TextWidget>();
		blend_panel =  go.transform.Find("mid_Panel/blend_panel").GetComponent<PanelWidget>();
		gold_icon_pool =  go.transform.Find("mid_Panel/blend_panel/gold_icon_pool").GetComponent<IconWidget>();
		diamond_icon_pool =  go.transform.Find("mid_Panel/blend_panel/diamond_icon_pool").GetComponent<IconWidget>();
		cost_icon_pool =  go.transform.Find("mid_Panel/blend_panel/cost_icon_pool").GetComponent<IconWidget>();
		blend_grid_scroll =  go.transform.Find("mid_Panel/blend_panel/blend_grid_scroll").GetComponent<GridRecycleScrollWidget>();
		bg_Image =  go.transform.Find("top_Panel/bg_Image").GetComponent<ImageWidget>();
		close_Image =  go.transform.Find("top_Panel/close_Image").GetComponent<EmptyImageWidget>();
		top_packet =  go.transform.Find("common_top_panel/top_packet").GetComponent<ImageWidget>();
		packetEffect =  go.transform.Find("common_top_panel/top_packet/Image/top_yo_card_Image/packetEffect").GetComponent<EffectWidget>();
		top_packet_Text =  go.transform.Find("common_top_panel/top_packet/Image/top_packet_Text").GetComponent<TextWidget>();
		top_packet_Button =  go.transform.Find("common_top_panel/top_packet/top_packet_Button").GetComponent<EmptyImageWidget>();
		top_yo_card =  go.transform.Find("common_top_panel/top_yo_card").GetComponent<ImageWidget>();
		ucardEffect =  go.transform.Find("common_top_panel/top_yo_card/Image/top_yo_card_Image/ucardEffect").GetComponent<EffectWidget>();
		top_yo_card_Text =  go.transform.Find("common_top_panel/top_yo_card/Image/top_yo_card_Text").GetComponent<TextWidget>();
		top_yo_card_Button =  go.transform.Find("common_top_panel/top_yo_card/top_yo_card_Button").GetComponent<EmptyImageWidget>();
		top_diamond =  go.transform.Find("common_top_panel/top_diamond").GetComponent<ImageWidget>();
		diamondEffect =  go.transform.Find("common_top_panel/top_diamond/Image/top_diamond_Image/diamondEffect").GetComponent<EffectWidget>();
		top_diamond_Text =  go.transform.Find("common_top_panel/top_diamond/Image/top_diamond_Text").GetComponent<TextWidget>();
		top_diamond_Button =  go.transform.Find("common_top_panel/top_diamond/top_diamond_Button").GetComponent<EmptyImageWidget>();
		top_gold =  go.transform.Find("common_top_panel/top_gold").GetComponent<ImageWidget>();
		goldEffect =  go.transform.Find("common_top_panel/top_gold/Image/top_gold_Image/goldEffect").GetComponent<EffectWidget>();
		top_gold_Text =  go.transform.Find("common_top_panel/top_gold/Image/top_gold_Text").GetComponent<TextWidget>();
		top_gold_Button =  go.transform.Find("common_top_panel/top_gold/top_gold_Button").GetComponent<EmptyImageWidget>();
		head_Image =  go.transform.Find("common_top_panel/head_Image").GetComponent<ImageWidget>();
		head_Icon =  go.transform.Find("common_top_panel/head_Image/head_Icon").GetComponent<CircleImageWidget>();
		press_Image =  go.transform.Find("common_top_panel/head_Image/press_Image").GetComponent<EmptyImageWidget>();
		List<Diamondcell> diamondcellList=new List<Diamondcell>() ;
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_0_1").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_0_2").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_0_3").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_1_0").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_1_1").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_1_2").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_1_3").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_2_0").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_2_1").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_2_2").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_2_3").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_3_0").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_3_1").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_3_2").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_3_3").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_4_0").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_4_1").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_4_2").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_4_3").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_5_0").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_5_1").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_5_2").gameObject));
		diamondcellList.Add(new Diamondcell(go.transform.Find("mid_Panel/diamond_Panel/diamond_GridRecycleScrollPanel/content/cellitem_5_3").gameObject));
		diamondcellArr=diamondcellList.ToArray();
		List<Goldcell> goldcellList=new List<Goldcell>() ;
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_0_1").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_0_2").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_0_3").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_1_0").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_1_1").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_1_2").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_1_3").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_2_0").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_2_1").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_2_2").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_2_3").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_3_0").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_3_1").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_3_2").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_3_3").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_4_0").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_4_1").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_4_2").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_4_3").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_5_0").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_5_1").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_5_2").gameObject));
		goldcellList.Add(new Goldcell(go.transform.Find("mid_Panel/gold_Panel/gold_GridRecycleScrollPanel/content/cellitem_5_3").gameObject));
		goldcellArr=goldcellList.ToArray();
		List<YoCardcell> yoCardcellList=new List<YoCardcell>() ;
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_0_1").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_0_2").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_0_3").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_1_0").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_1_1").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_1_2").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_1_3").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_2_0").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_2_1").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_2_2").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_2_3").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_3_0").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_3_1").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_3_2").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_3_3").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_4_0").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_4_1").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_4_2").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_4_3").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_5_0").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_5_1").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_5_2").gameObject));
		yoCardcellList.Add(new YoCardcell(go.transform.Find("mid_Panel/yo_card_Panel/yo_card_GridRecycleScrollPanel/content/cellitem_5_3").gameObject));
		yoCardcellArr=yoCardcellList.ToArray();
		List<BlendCell> blendCellList=new List<BlendCell>() ;
		blendCellList.Add(new BlendCell(go.transform.Find("mid_Panel/blend_panel/blend_grid_scroll/content/cellitem").gameObject));
		blendCellList.Add(new BlendCell(go.transform.Find("mid_Panel/blend_panel/blend_grid_scroll/content/cellitem_0_1").gameObject));
		blendCellList.Add(new BlendCell(go.transform.Find("mid_Panel/blend_panel/blend_grid_scroll/content/cellitem_0_2").gameObject));
		blendCellList.Add(new BlendCell(go.transform.Find("mid_Panel/blend_panel/blend_grid_scroll/content/cellitem_0_3").gameObject));
		blendCellList.Add(new BlendCell(go.transform.Find("mid_Panel/blend_panel/blend_grid_scroll/content/cellitem_1_0").gameObject));
		blendCellList.Add(new BlendCell(go.transform.Find("mid_Panel/blend_panel/blend_grid_scroll/content/cellitem_1_1").gameObject));
		blendCellList.Add(new BlendCell(go.transform.Find("mid_Panel/blend_panel/blend_grid_scroll/content/cellitem_1_2").gameObject));
		blendCellList.Add(new BlendCell(go.transform.Find("mid_Panel/blend_panel/blend_grid_scroll/content/cellitem_1_3").gameObject));
		blendCellList.Add(new BlendCell(go.transform.Find("mid_Panel/blend_panel/blend_grid_scroll/content/cellitem_2_0").gameObject));
		blendCellList.Add(new BlendCell(go.transform.Find("mid_Panel/blend_panel/blend_grid_scroll/content/cellitem_2_1").gameObject));
		blendCellList.Add(new BlendCell(go.transform.Find("mid_Panel/blend_panel/blend_grid_scroll/content/cellitem_2_2").gameObject));
		blendCellList.Add(new BlendCell(go.transform.Find("mid_Panel/blend_panel/blend_grid_scroll/content/cellitem_2_3").gameObject));
		blendCellList.Add(new BlendCell(go.transform.Find("mid_Panel/blend_panel/blend_grid_scroll/content/cellitem_3_0").gameObject));
		blendCellList.Add(new BlendCell(go.transform.Find("mid_Panel/blend_panel/blend_grid_scroll/content/cellitem_3_1").gameObject));
		blendCellList.Add(new BlendCell(go.transform.Find("mid_Panel/blend_panel/blend_grid_scroll/content/cellitem_3_2").gameObject));
		blendCellList.Add(new BlendCell(go.transform.Find("mid_Panel/blend_panel/blend_grid_scroll/content/cellitem_3_3").gameObject));
		blendCellArr=blendCellList.ToArray();
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
