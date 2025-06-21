
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UIWidget;
using System;

public class GunAttriItem : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    private Image gunImg;
    private Text gunName;
    private Text levelText;
    private Text newText;
    private Text descripText;
    private IconWidget boxIcon;
    private IconWidget bgIcon;
    public GunType gunType= GunType.None;
    public AttriEnum attriType = AttriEnum.None;

    public Action<GameGunData> SelectFun;
    private GameGunData gameGunData;
    public GunInfo gunInfo;
    public AttriInfo attriInfo;
    // Start is called before the first frame update
    void Awake()
    {
        gunImg = this.transform.Find("gunImg").GetComponent<Image>();
        gunName = this.transform.Find("name").GetComponent<Text>();
        newText = this.transform.Find("newText").GetComponent<Text>();
        levelText = this.transform.Find("level").GetComponent<Text>();
        descripText = this.transform.Find("descrip").GetComponent<Text>();
        boxIcon = this.transform.Find("box").GetComponent<IconWidget>();
        bgIcon = this.transform.Find("bg").GetComponent<IconWidget>();

    }
    public void RefreshByData(GameGunData gundata)
    {
        reFresh(gundata);
        levelText.text = "Lv."+gundata.level;
        gunName.text = gundata.name;
        if (gundata.level == 1)
        {
            newText.gameObject.SetActive(true);
        }
        else
        {
            newText.gameObject.SetActive(false);
        }
        gameGunData = gundata;
    }

   
    private void reFresh(GameGunData gundata)
    {
        gunType = gundata.gunType;
        attriType = gundata.attriType;
        if (gunType== GunType.None)
        {
            gunName.text = attriType.ToString();
            string attriImgStr = gundata.attriImgStrPath;
            OutSpaceResourceManager.Instance.GetSpriteByName(attriImgStr, (Sprite sp) =>
            {
                gunImg.sprite = sp;
            });
            OutSpaceResourceManager.Instance.GetAttriInfo(gundata.attriInfoAssertPath, (getAttriInfo) =>
            {
                attriInfo = getAttriInfo;
                descripText.text = getAttriInfo.getDescribeByLevel(gundata.level-1);
            });
        }
        else
        {
            gunName.text = gunType.ToString();
            string gunImgStr = gundata.gunImgStrPath;
            OutSpaceResourceManager.Instance.GetSpriteByName(gunImgStr, (Sprite sp) =>
            {
                gunImg.sprite = sp;
            });
            OutSpaceResourceManager.Instance.GetGunInfo(gundata.gunInfoAssertPath, (getGunInfo) =>
            {
                gunInfo = getGunInfo;
                descripText.text = getGunInfo.Descrip;
            });
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (SelectFun != null)
        {
            SelectFun(this.gameGunData);
        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        SetSelect(true); 
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        SetSelect(false); 
    }
    public void SetSelect(bool isSelect)
    {
        if (isSelect)
        {
            boxIcon.ChangeIcon(1);
            bgIcon.ChangeIcon(1);
        }
        else
        {
            boxIcon.ChangeIcon(0);
            bgIcon.ChangeIcon(0);
        }

       
    }
    private void OnDestroy()
    {
        SelectFun = null;
        gameGunData = null;
    }
}
