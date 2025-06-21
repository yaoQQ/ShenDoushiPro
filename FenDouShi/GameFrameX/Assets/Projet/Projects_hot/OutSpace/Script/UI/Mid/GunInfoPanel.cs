using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIWidget;
using UnityEngine.EventSystems;

public class GunInfoPanel : MonoBehaviour
{
    // Start is called before the first frame update
    private GunInfoView gunInfoView;
    private SelectGunListView gunListView;
    private ButtonWidget closeBtn;
    private void Awake()
    {
        gunInfoView = this.transform.Find("GunInfo").GetComponent<GunInfoView>();
        gunListView = this.transform.Find("GunListPanel").GetComponent<SelectGunListView>();
        closeBtn = this.transform.Find("CloseBtn").GetComponent<ButtonWidget>();
    }
    void Start()
    {
        closeBtn.AddEventListener(UIEvent.PointerClick, closeFun);
    }
    private void OnEnable()
    {
      //  Time.timeScale = 0;
    }
    private void OnDisable()
    {
       // Time.timeScale = 1;
    }
  
    private void closeFun(PointerEventData evt)
    {
        UIViewManager.Instance.Close(UIViewEnum.OutSpaceGunTopInfoPanel);
    }
}
