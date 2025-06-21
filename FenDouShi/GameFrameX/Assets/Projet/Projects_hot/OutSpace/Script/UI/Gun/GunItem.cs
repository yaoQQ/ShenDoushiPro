
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UIWidget;
using System;

public class GunItem : MonoBehaviour, IPointerClickHandler
{
    private Image gunImg;
    private Text gunName;
    public Text levelText;
    public bool isCanSelect = false;
    private IconWidget boxIcon;
    public GunType gunType = GunType.None;
    public AttriEnum attriType = AttriEnum.None;

    private GameGunData _gameGunData;
    public Action<GameGunData> SelectFun;
    // Start is called before the first frame update
    void Awake()
    {
        gunImg = this.transform.Find("gunImg").GetComponent<Image>();
        gunName = this.transform.Find("name").GetComponent<Text>();

        levelText = this.transform.Find("level").GetComponent<Text>();


        if (isCanSelect)
        {
            boxIcon = this.transform.Find("box").GetComponent<IconWidget>();
        }
    }

 
    public void RefreshByData(GameGunData gundata)
    {
        _gameGunData = gundata;
        ReFresh(gundata);
    }
    public GameGunData gameGunData
    {
        get
        {
            return _gameGunData;
        }
    }

    private void ReFresh(GameGunData gundata)
    {
        gunType = gundata.gunType;
        attriType = gundata.attriType;
        levelText.text = "Lv." + gundata.level;
        gunName.text = gundata.name;
        if (gunType == GunType.None)
        {
            gunName.text = attriType.ToString();
            string attriImgStr = attriType.ToString() + "_Img";
            OutSpaceResourceManager.Instance.GetSpriteByName(attriImgStr, (Sprite sp) =>
            {
                gunImg.sprite = sp;
            });
        }
        else
        {
            gunName.text = gunType.ToString();
            string gunImgStr = gunType.ToString() + "_Img";
            OutSpaceResourceManager.Instance.GetSpriteByName(gunImgStr, (Sprite sp) =>
            {
                gunImg.sprite = sp;
            });
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetSelect(true);
    }
    public void SetSelect(bool isSelect)
    {
        if (!isCanSelect)
        {
            return;
        }
        if (isSelect)
        {
            
            if (boxIcon.initIndex == 1)
            {
                return;
            }
            NoticeManager.Instance.Dispatch(OutSpaceNotice.SelectGunItem, this);
            boxIcon.ChangeIcon(1);
  
            if (SelectFun != null)
            {
                SelectFun(this._gameGunData);
            }
        }
        else
        {
            boxIcon.ChangeIcon(0);
        }

       
    }
}
