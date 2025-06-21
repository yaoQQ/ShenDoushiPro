using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIWidget;
using UnityEngine.UI;

public class Mid_MainMenu_OutSpace:MonoBehaviour,IMiddleware
{
	public GameObject main;
	public UIWidget.PanelWidget MainMenu_OutSpace;
	public UIWidget.PanelWidget MainMenuView;
	public UIWidget.ButtonWidget Continue;
	public UIWidget.ButtonWidget Newgame;
	public UIWidget.ButtonWidget Quit;
	public UIWidget.ButtonWidget SettingsGameplay;
	public UIWidget.ButtonWidget SettingsVideo;
	public UIWidget.ButtonWidget SettingsAudio;
	public UIWidget.PanelWidget GamePlayWindow;
	public UIWidget.PanelWidget VideoWindow;
	public UIWidget.PanelWidget AudioWindow;
	public UIWidget.SliderWidget MasterVolumeSlider;
	public UIWidget.SliderWidget MusicSlider;

	void Awake() 
	{
		GameObject go =  this.gameObject;
		main =  this.gameObject;
		MainMenu_OutSpace =  go.GetComponent<UIWidget.PanelWidget>();
		MainMenuView =  go.transform.Find("MainMenuView").GetComponent<UIWidget.PanelWidget>();
		Continue =  go.transform.Find("MainMenuView/Window/Continue").GetComponent<UIWidget.ButtonWidget>();
		Newgame =  go.transform.Find("MainMenuView/Window/Newgame").GetComponent<UIWidget.ButtonWidget>();
		Quit =  go.transform.Find("MainMenuView/Window/Quit").GetComponent<UIWidget.ButtonWidget>();
		SettingsGameplay =  go.transform.Find("Settings/SettingsWindow/SettingsGameplay").GetComponent<UIWidget.ButtonWidget>();
		SettingsVideo =  go.transform.Find("Settings/SettingsWindow/SettingsVideo").GetComponent<UIWidget.ButtonWidget>();
		SettingsAudio =  go.transform.Find("Settings/SettingsWindow/SettingsAudio").GetComponent<UIWidget.ButtonWidget>();
		GamePlayWindow =  go.transform.Find("Settings/SettingsPanels/GamePlayWindow").GetComponent<UIWidget.PanelWidget>();
		VideoWindow =  go.transform.Find("Settings/SettingsPanels/VideoWindow").GetComponent<UIWidget.PanelWidget>();
		AudioWindow =  go.transform.Find("Settings/SettingsPanels/AudioWindow").GetComponent<UIWidget.PanelWidget>();
		MasterVolumeSlider =  go.transform.Find("Settings/SettingsPanels/AudioWindow/Panel/Settings/MasterVolumeSlider").GetComponent<UIWidget.SliderWidget>();
		MusicSlider =  go.transform.Find("Settings/SettingsPanels/AudioWindow/Panel/Settings/MusicSlider").GetComponent<UIWidget.SliderWidget>();
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
