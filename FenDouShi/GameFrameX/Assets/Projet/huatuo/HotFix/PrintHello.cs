using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIWidget;

public enum testNum
{
    hhh=1,
    kkk=2,
    None=0,

    Map_View = 2000,
    MapFloat_View = 2100,
    Main_view = 3100,
    Platform_Second_View = 3200,
    Pop_view = 4000,
    Loading_View = 4100,
   // Plot_View = 4600,
}
public class PrintHello : MonoBehaviour
{

    public string text;

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogFormat("ÑÔÔÕÑà×ÓPrintHello hello, huatuo. {0}", text);
        ButtonWidget button= this.gameObject.AddComponent<ButtonWidget>();
        Debug.LogFormat("PrintHello button={0}", button);
        button.testButton2();
        showUIView();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void showUIView()
    {
        Debug.Log("loginview()" );
        //PlatformMallView shopView = new PlatformMallView();
        // Debug.Log("PlatformMallView()=" + shopView);
        //Debug.Log("PlatformMallView()   shopView.getViewType()=" + shopView.getViewType());
      
       //   LoginView2.TestHuaTuo();
        GameDataManager.Instance.Init();
        UIViewManager.Instance.Open(UIViewEnum.LoginView2);

        Debug.Log(" UILoadControl=" + UILoadControl.Instance);
        Debug.Log(" testNum¡£hhh=" + (int)testNum.hhh);
        Debug.Log(" UIViewType.Alert_box=" + (int)UIViewType.Alert_box);
        Debug.Log(" UIViewType.End=" + (int)UIViewType.End);
        Debug.Log(" UIViewEnum.StatusbarView=" + (int)UIViewEnum.StatusbarView);
        Debug.Log(" UIViewEnum.End=" + (int)UIViewEnum.End);

        Debug.Log(" LoadingView.Instance.loadingText=" + LoadingView.Instance.loadingText);
        //LittlePrinceScene scene = new LittlePrinceScene();
        //Debug.Log("scene.name ="+ scene.getSceneName());
    }
}
