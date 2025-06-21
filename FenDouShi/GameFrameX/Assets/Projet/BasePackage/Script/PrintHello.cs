using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIWidget;

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
     //   GameDataManager.Instance.Init();
        UIViewManager.Instance.Open(UIViewEnum.LoginView2);

        Debug.Log(" UILoadControl=" + UILoadControl.Instance);
        Debug.Log(" UIViewType.Alert_box=" + (int)UIViewType.Alert_box);
        Debug.Log(" UIViewType.End=" + (int)UIViewType.End);
        Debug.Log(" UIViewEnum.StatusbarView=" + (int)UIViewEnum.StatusbarView);
        Debug.Log(" UIViewEnum.End=" + (int)UIViewEnum.End);

        Debug.Log(" LoadingView.Instance.loadingText=" + LoadingView.Instance.loadingText);
    }
}
