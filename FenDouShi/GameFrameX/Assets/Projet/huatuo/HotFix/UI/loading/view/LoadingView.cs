using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UIWidget;
using System;

public class LoadingView : SingleMonobehaviour<LoadingView>
{

    public GameObject main;
    
    public TextWidget loadingText;
    public TextWidget loadingPercentText;
    
    public SliderWidget slider;
    public ButtonWidget PlayButton;
    public IconWidget loadingBg;
    private bool isInit = false;

    void Awake()
    {
        main = this.gameObject;
       
    }

    private void initView(Transform target, GameObject container=null)
    {
        if (isInit)
        {
            return;
        }
        main = target.gameObject;

        loadingBg = main.GetComponent<IconWidget>();
         loadingText = main.transform.Find("loadingText").GetComponent<TextWidget>();
        loadingPercentText= main.transform.Find("loadingPercentText").GetComponent<TextWidget>();
        slider = main.transform.Find("slider").GetComponent<SliderWidget>();
        PlayButton = main.transform.Find("Play Button").GetComponent<ButtonWidget>();
        if (container != null)
        {
           // main.transform.parent = container.transform;
            UITools.SetParentAndAlign(main, container);
        }

      
        PlayButton.gameObject.SetActive(false);
        PlayButton.AddEventListener(UIEvent.PointerClick, enterScene);
        isInit = true;
    }
    private void enterScene(PointerEventData eve)
    {
        Logger.PrintDebug("click enterScene");

        LoadingBarController.enterFun();
        LoadingBarController.Hide();
        

    }

    /// <summary>
    /// 改变loading背景图
    /// </summary>
    private void ShowRandomIconBg()
    {
        if (loadingBg.IconArr != null)
        {
            int iconLen = loadingBg.IconArr.Length;
            if (iconLen > 0)
            {
               
                 int index   = UnityEngine.Random.Range(0, iconLen);
                 loadingBg.ChangeIcon(index);

            }
        }

    }
    public void Show(bool isShow)
    {
        this.gameObject.SetActive(isShow);
        if (isShow)
        {
            ShowRandomIconBg();
            PlayButton.gameObject.SetActive(false);
        }
        
    }
    public void Init(Transform target, GameObject container)
    {
        if (isInit)
        {
            return;
        }
        initView(target, container);
    }
    public void setLoadValue(float loadValue)
    {
        
        this.slider.value = loadValue;
    }
    public void SetLoadContent(string str)
    {
       
        this.loadingText.text ="huatuoTest "+ str;
    }
    public void SetLoadPrecent(string str)
    {

        this.loadingPercentText.text = "" + str;
    }

}