using UnityEngine.UI;
using UnityEngine;
using UIWidget;
using UnityEngine.EventSystems;

public class GunInfoView : MonoBehaviour, IMiddleware
{
    // Start is called before the first frame update
    public GameObject main;
    private GunItem gunItem;
    private Text infoText;
    private Text titleText;
    private ImageWidget colseBtnBg;

    public AttriInfo attriInfo;
    public GunInfo gunInfo;

    private void Awake()
    {
        main = this.gameObject;
        titleText = this.transform.Find("Viewport/Content/characterTitle").GetComponent<Text>();
        infoText = this.transform.Find("Viewport/Content/characterInfoText").GetComponent<Text>();
       
    }
    void Start()
    {
        Debug.Log("GunInfoView Start()@@@@@@@@@@@@@@AddNoticeLister(OutSpaceNotice.SelectGunItem");
        NoticeManager.Instance.AddNoticeLister(OutSpaceNotice.SelectGunItem, SelectGun);
    
    }

    private void SelectGun(string noticeType, BaseNotice notice)
    {
        ObjectNotice obj = (ObjectNotice)notice;
        gunItem = (obj.GetObj() as GunItem);
        GameGunData gameGunData = gunItem.gameGunData;

        GunType gunType = gameGunData.gunType;
        AttriEnum attriType = gameGunData.attriType;
        attriInfo = null;
        gunInfo = null;
        if (gameGunData.gunType == GunType.None)
        {
            titleText.text = attriType.ToString();
            OutSpaceResourceManager.Instance.GetAttriInfo(gameGunData.attriInfoAssertPath, (getAttriInfo) =>
            {
                attriInfo = getAttriInfo;
                infoText.text = getAttriInfo.getAttriText(gameGunData.level) ;
            });
        }
        else
        {
            titleText.text = gunType.ToString();
            OutSpaceResourceManager.Instance.GetGunInfo(gameGunData.gunInfoAssertPath, (getGunInfo) =>
            {
                gunInfo = getGunInfo;
                infoText.text = getGunInfo.getAttriText();
            });
        }

    }

    #region 这里是GunAttriTopPanel要用的共用方法
    public void reFreshByGunInfo(GunInfo gunInfoTem)
    {
        gunInfo = gunInfoTem;
        titleText.text = gunInfoTem.name.ToString();
        infoText.text = gunInfoTem.getAttriText();
    }
    public void reFreshByAddAttriInfo()
    {
        CharacterInfo characterInfo= OutSpacePlayerInfoManager.Instance.CharacterAddInfo;
        infoText.text = characterInfo.getAttri();
    }
    #endregion
    public void reFresh(GunItem currGunItem)
    {
        if (currGunItem == null)
        {
            return;
        }
        gunItem = currGunItem;
        GameGunData gameGunData = gunItem.gameGunData;

        GunType gunType = gameGunData.gunType;
        AttriEnum attriType = gameGunData.attriType;
        attriInfo = null;
        gunInfo = null;
        if (gameGunData.gunType == GunType.None)
        {
            titleText.text = attriType.ToString();
            OutSpaceResourceManager.Instance.GetAttriInfo(gameGunData.attriInfoAssertPath, (getAttriInfo) =>
            {
                attriInfo = getAttriInfo;
                infoText.text = getAttriInfo.getAttriText(gameGunData.level);
            });
        }
        else
        {
            titleText.text = gunType.ToString();
            OutSpaceResourceManager.Instance.GetGunInfo(gameGunData.gunInfoAssertPath, (getGunInfo) =>
            {
                gunInfo = getGunInfo;
                infoText.text = getGunInfo.getAttriText();
            });
        }
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
        if (main != null) GameObject.Destroy(main);
#endif
    }
    private void OnDestroy()
    {
        NoticeManager.Instance.RemoveNoticeLister(OutSpaceNotice.SelectGunItem, SelectGun);
    }
}
