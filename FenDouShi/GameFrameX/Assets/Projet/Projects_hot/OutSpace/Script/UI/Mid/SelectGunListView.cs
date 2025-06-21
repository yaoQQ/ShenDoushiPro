using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGunListView : MonoBehaviour
{
    // Start is called before the first frame update
    private GunItem selectShowItem;
    private GunItem selectItem;//上一个选择的对象

    public GunItem cloneItem;//复制对象
    private List<GunItem> gunList= new List<GunItem>();
    private void Awake()
    {
        selectShowItem = this.transform.Find("GunShowItem").GetComponent<GunItem>();
        cloneItem = this.transform.Find("Viewport/Content/GunItem").GetComponent<GunItem>();
        cloneItem.gameObject.SetActive(false);
    }

    void Start()
    {
        Debug.Log("SelectGunListView Start()@@@@@@@@@@@@@@AddNoticeLister(OutSpaceNotice.SelectGunItem");
        NoticeManager.Instance.AddNoticeLister(OutSpaceNotice.SelectGunItem, SelectGun);
    }

    // Update is called once per frame
    private void SelectGun(string noticeType, BaseNotice notice) {
        if (selectItem != null)
        {
            selectItem.SetSelect(false);
        }
        ObjectNotice obj = (ObjectNotice)notice;
        selectItem = (obj.GetObj() as GunItem);
        if (selectItem)
        {
            selectShowItem.RefreshByData(selectItem.gameGunData);
        }
    }
    public void InitView(GunInfoView gunInfoView)
    {
        if (gunList.Count > 0)
        {//默认选择第一个
            Debug.Log("@@@@@@@@@@@@@@delayShow");
            selectItem = gunList[0];
            gunList[0].SetSelect(true);
            selectShowItem.RefreshByData(gunList[0].gameGunData);
            gunInfoView.reFresh(gunList[0]);
        }
    }
    public void showGunList(List<GameGunData> gunDataList)
    {
        RestView();
        for (int i = 0; i < gunDataList.Count; i++)
        {
            GunItem item;
            if (gunList.Count <= i)
            {
                GameObject obj = GameObject.Instantiate(cloneItem.gameObject);
                obj.transform.SetParent(cloneItem.transform.parent);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                item = obj.GetComponent<GunItem>();
                gunList.Add(item);
            }
            else
            {
                item = gunList[i];
            }
            item.gameObject.SetActive(true);
            item.RefreshByData(gunDataList[i]);
        }
     
    }
    
   
    private void RestView()
    {
        if (selectItem != null)
        {
            selectItem.SetSelect(false);
        }
        for (int i = 0; i < gunList.Count; i++)
        {
            gunList[i].gameObject.SetActive(false);
        }
    }
}
