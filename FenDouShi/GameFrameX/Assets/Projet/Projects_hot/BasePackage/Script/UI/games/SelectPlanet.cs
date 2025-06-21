using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlanet : MonoBehaviour
{
    public SelectTextLog selectLog;
    public GameEnum gameEnum;
    void Start()
    {
        if (selectLog == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Map");
          //  Debug.Log(" gameEnum=" + gameEnum + " selectLog=" + selectLog);
            if (obj != null)
            {
                selectLog = obj.GetComponent<SelectTextLog>();
            }
        }
        else
        {
            Logger.PrintColor("red", "selectLog == null");
        }

    }


    public void MouseIn()
    {
        //  Logger.PrintColor("red", "OnMouseEnter() " + this.gameObject.name);
        if (selectLog)
        {
            Vector3 screenPos = CameraManager.Instance.MainCamera.WorldToScreenPoint(this.transform.position);
            selectLog.gameObject.SetActive(true);
            selectLog.setText(this.gameObject.name);
            selectLog.transform.position = screenPos + new Vector3(0, 60, 0);
        }
    }
    public void MouseLeave()
    {
      //  Logger.PrintColor("red", "OnMouseExit() " + this.gameObject.name);
        if (selectLog)
        {
            selectLog.gameObject.SetActive(false);
        }
    }
    public void ShowScene()
    {

         Logger.PrintColor("red", "OnMouseDown() gameEnum=" + gameEnum);
        if (gameEnum != GameEnum.OutSpacePackage&& gameEnum != GameEnum.ProceduralPlanetPackage) {
            CommonView.showTopTips(gameEnum+"功能尚未开放，敬请期待！");
           // CommonView.showTopTips("场景 ！" + gameEnum);
            return;
        }
        PreloadManager.Instance.PreLoadPackage(gameEnum);
        UIViewManager.Instance.Close(UIViewEnum.LoginView2);
    }

}
