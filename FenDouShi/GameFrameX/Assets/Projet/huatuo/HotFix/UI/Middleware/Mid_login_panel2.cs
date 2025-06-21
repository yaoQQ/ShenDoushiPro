using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIWidget;

public class Mid_login_panel2:MonoBehaviour,IMiddleware
{
	public GameObject main;
	public PanelWidget top_Panel;
	public TextWidget top_Text;
	public ButtonWidget BtnRegisterBack;
	public ButtonWidget BtnResetBack;
	public PanelWidget PageMain;
	public PanelWidget PageLogin;
	public InputFieldWidget InputFieldLoginAccount;
	public InputFieldWidget InputFieldLoginPassword;
	public ButtonWidget BtnLoginSee;
	public IconWidget IconLoginSee;
	public ButtonWidget BtnLoginReset;
	public ButtonWidget BtnMainRegister;
	public ButtonWidget BtnLoginLogin;
	public ButtonWidget BtnLoginDirectly;
	public ToggleWidget RemenberCountToggle;
	public PanelWidget bottom_panel;
	public ButtonWidget BtnMainLoginAlipay;
	public ButtonWidget BtnMainLoginWx;
	public ButtonWidget BtnMainLoginPhone;
	public ButtonWidget BtnUserAgreement;
	public TextWidget LoginDecribe;
	public PanelWidget PageReset;
	public InputFieldWidget InputFieldResetAccount;
	public InputFieldWidget InputFieldResetAuth;
	public InputFieldWidget InputFieldResetPassword;
	public ButtonWidget BtnResetSee;
	public IconWidget IconResetSee;
	public ButtonWidget BtnResetAuth;
	public ButtonWidget BtnResetReset;
	public ImageWidget pagewechat;
	public ButtonWidget BtnWeChatClose;

	void Awake() 
	{
		GameObject go =  this.gameObject;
		Debug.Log("test Mid_login_panel2 Awake");
        main = this.gameObject;
        top_Panel = go.transform.Find("top_Panel").GetComponent<PanelWidget>();
        top_Text = go.transform.Find("top_Panel/top_bg_Image/bg/top_Text").GetComponent<TextWidget>();
        BtnRegisterBack = go.transform.Find("top_Panel/BtnRegisterBack").GetComponent<ButtonWidget>();
        BtnResetBack = go.transform.Find("top_Panel/BtnResetBack").GetComponent<ButtonWidget>();
        PageMain = go.transform.Find("mid_Panel/PageMain").GetComponent<PanelWidget>();
        PageLogin = go.transform.Find("mid_Panel/PageMain/mid_Panel/PageLogin").GetComponent<PanelWidget>();
        InputFieldLoginAccount = go.transform.Find("mid_Panel/PageMain/mid_Panel/PageLogin/Panel/InputFieldLoginAccount").GetComponent<InputFieldWidget>();
        InputFieldLoginPassword = go.transform.Find("mid_Panel/PageMain/mid_Panel/PageLogin/Panel/InputFieldLoginPassword").GetComponent<InputFieldWidget>();
        BtnLoginSee = go.transform.Find("mid_Panel/PageMain/mid_Panel/PageLogin/Panel/InputFieldLoginPassword/BtnLoginSee").GetComponent<ButtonWidget>();
        IconLoginSee = go.transform.Find("mid_Panel/PageMain/mid_Panel/PageLogin/Panel/InputFieldLoginPassword/BtnLoginSee/IconLoginSee").GetComponent<IconWidget>();
        BtnLoginReset = go.transform.Find("mid_Panel/PageMain/mid_Panel/PageLogin/Panel/BtnLoginReset").GetComponent<ButtonWidget>();
        BtnMainRegister = go.transform.Find("mid_Panel/PageMain/mid_Panel/PageLogin/Panel/BtnMainRegister").GetComponent<ButtonWidget>();
        BtnLoginLogin = go.transform.Find("mid_Panel/PageMain/mid_Panel/PageLogin/Panel/BtnLoginLogin").GetComponent<ButtonWidget>();
        BtnLoginDirectly = go.transform.Find("mid_Panel/PageMain/mid_Panel/PageLogin/Panel/BtnLoginDirectly").GetComponent<ButtonWidget>();
        RemenberCountToggle = go.transform.Find("mid_Panel/PageMain/mid_Panel/PageLogin/Panel/RemenberCountToggle").GetComponent<ToggleWidget>();
        bottom_panel = go.transform.Find("mid_Panel/PageMain/bottom_panel").GetComponent<PanelWidget>();
        BtnMainLoginAlipay = go.transform.Find("mid_Panel/PageMain/bottom_panel/BtnMainLoginAlipay").GetComponent<ButtonWidget>();
        BtnMainLoginWx = go.transform.Find("mid_Panel/PageMain/bottom_panel/BtnMainLoginWx").GetComponent<ButtonWidget>();
        BtnMainLoginPhone = go.transform.Find("mid_Panel/PageMain/bottom_panel/BtnMainLoginPhone").GetComponent<ButtonWidget>();
        BtnUserAgreement = go.transform.Find("mid_Panel/PageMain/Text/BtnUserAgreement").GetComponent<ButtonWidget>();
        LoginDecribe = go.transform.Find("mid_Panel/PageMain/LoginDecribe").GetComponent<TextWidget>();
        PageReset = go.transform.Find("mid_Panel/PageReset").GetComponent<PanelWidget>();
        InputFieldResetAccount = go.transform.Find("mid_Panel/PageReset/Panel/InputFieldResetAccount").GetComponent<InputFieldWidget>();
        InputFieldResetAuth = go.transform.Find("mid_Panel/PageReset/Panel/InputFieldResetAuth").GetComponent<InputFieldWidget>();
        InputFieldResetPassword = go.transform.Find("mid_Panel/PageReset/Panel/InputFieldResetPassword").GetComponent<InputFieldWidget>();
        BtnResetSee = go.transform.Find("mid_Panel/PageReset/Panel/InputFieldResetPassword/BtnResetSee").GetComponent<ButtonWidget>();
        IconResetSee = go.transform.Find("mid_Panel/PageReset/Panel/InputFieldResetPassword/BtnResetSee/IconResetSee").GetComponent<IconWidget>();
        BtnResetAuth = go.transform.Find("mid_Panel/PageReset/Panel/BtnResetAuth").GetComponent<ButtonWidget>();
        BtnResetReset = go.transform.Find("mid_Panel/PageReset/Panel/BtnResetReset").GetComponent<ButtonWidget>();
        pagewechat = go.transform.Find("mid_Panel/pagewechat").GetComponent<ImageWidget>();
        BtnWeChatClose = go.transform.Find("mid_Panel/pagewechat/BtnWeChatClose").GetComponent<ButtonWidget>();

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
