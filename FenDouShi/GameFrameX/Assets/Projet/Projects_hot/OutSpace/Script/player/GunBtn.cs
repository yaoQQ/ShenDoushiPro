using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UIWidget;

public class GunBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler//, IPointerClickHandler
{

    public Image GunImg;
    public Image mask;
    public ButtonWidget InfoBtn;
    public Text BulletCountLabel;
    public Text levelText;
    public GunType _gunType = GunType.None;
    public PlayerGunBtnsEnum btnPos = PlayerGunBtnsEnum.rightDown;
    public PlayerGun[] playerGunList;
    public delegate void shootPressDelegate();
    public shootPressDelegate ShootPressFun;

    public delegate void shootUpDelegate();
    public shootUpDelegate ShooUptFun;


    private GameGunData _gameGunData;
    public void Awake() {
        
        if (mask==null){
            mask = this.transform.Find("mask").GetComponent<Image>();
        }
        GunImg = this.transform.Find("Image").GetComponent<Image>();
        if (BulletCountLabel == null)
        {
            BulletCountLabel = this.transform.Find("Text").GetComponent<Text>();
        }
        Transform tran = this.transform.Find("LevelText");
        if (tran) {
            levelText = tran.GetComponent<Text>();
            if (InfoBtn == null) {
                InfoBtn = this.transform.Find("InfoImg").GetComponent<ButtonWidget>();
            }
        }
    }

    public void Start()
    {
        updateGun(_gunType);
        if(InfoBtn)
        InfoBtn.AddEventListener(UIEvent.PointerClick, ShowGunInfo);
    }
    private void ShowGunInfo(PointerEventData pointData)
    {
        Debug.Log("click ShowGunInfo");
        UIViewManager.Instance.Open(UIViewEnum.OutSpaceGunAttriInfoPanel, playerGunList[0].gunInfo);
    }
    /// <summary>
    /// 打完子弹 换弹夹 更新界面回调
    /// </summary>
    public void updateCoolTime()
    {
        if (playerGunList == null || playerGunList.Length <= 0)
        {
            Logger.PrintDebug("updateCoolTime playerGunList.Length<=0=" + playerGunList.Length);
            return;
        }
     //   Logger.PrintColor("blue", "updateCoolTime playerGunList[0].gunInfo=" + playerGunList[0].gunInfo);
      //  Logger.PrintColor("blue", "updateCoolTime playerGunList[0].getCurrBulletCount=" + playerGunList[0].getCurrBulletCount);
        BulletCountLabel.text = playerGunList[0].getCurrBulletCount.ToString();

        mask.fillAmount = playerGunList[0].getBulletCollTimeCount / playerGunList[0].BulletCollTimeTotal;
    }


    //更新玩家枪
    private GunType gunType
    {
        set
        {
            if(_gunType== value)
            {
                Logger.PrintDebug("@@@@@@@@@@@@GunBtn _gunType=" + _gunType);
                return;
            }
            updateGun(value);
            _gunType = value;
        }
        get
        {
            return _gunType;
        }
    }

    //更新枪支
    private void updateGun(GunType _gunType)
    {
     
        Logger.PrintDebug("更新枪支 _gunType="+ _gunType);
        if (playerGunList !=null&& playerGunList.Length>0)
        {
            for (int i = 0; i < playerGunList.Length; i++)
            {
                ShootPressFun -= playerGunList[i].BtnPressFun;
                ShooUptFun -= playerGunList[i].BtnUptFun;
               
            }
            Logger.PrintDebug("更新枪支 playerGunList=" + playerGunList);
            Logger.PrintDebug("更新枪支 playerGunList.count=" + playerGunList.Length);
            GameObject.Destroy(playerGunList[0].transform.parent);
        }
        if (_gunType == GunType.None)
        {
            GunImg.sprite = null;
            return;
        }
        string gunName = _gunType.ToString();
        GameObject playerGunParent= MyUtils.LoadGunPrefab(gunName);
        playerGunParent.layer = OutSpaceLayer.charecterLayer;
        MyUtils.LoadSprite(gunName+"_Img",(Sprite obj)=> {
            GunImg.sprite = obj;
        });
        GunManager.Instance.addPlayerGun(_gunType, playerGunParent);
         playerGunList = playerGunParent.GetComponentsInChildren<PlayerGun>();
        for (int i = 0; i < playerGunList.Length; i++)
        {
            playerGunList[i].updateBtn = updateCoolTime;
            playerGunList[i].GunBtnPos = btnPos;
            ShootPressFun += playerGunList[i].BtnPressFun;
            ShooUptFun += playerGunList[i].BtnUptFun;
            playerGunList[i].gameObject.layer = OutSpaceLayer.charecterLayer;
        }


    }
    public void UpdateByGameHunData(GameGunData gameGunData)
    {
        if(gameGunData.gunType== GunType.None)
        {
            return;
        }
        _gameGunData = gameGunData;
        levelText.text =""+ gameGunData.level;
        gunType = gameGunData.gunType;
        Logger.PrintColor("blue", "更新武器按钮表现成功 gunType=" + gunType + "selectData.level=" + gameGunData.level);
    }
    private bool isPress = false;
  //  private float shakeTime = 0.5f;
    public void Update()
    {
        if (isPress)
        {
            if (ShootPressFun != null)
            {

                ShootPressFun();
            }
            //shakeTime -= Time.deltaTime;
            //if (shakeTime <= 0)
            //{
            //    Handheld.Vibrate();
            //    shakeTime = 0.5f;
            //}
        }
    }

    // 当按钮被按下后系统自动调用此方法  
    public void OnPointerDown(PointerEventData eventData)
    {
        isPress = true;
        if (ShootPressFun != null)
        {

            ShootPressFun();
        }
    }


    // 当按钮抬起的时候自动调用此方法  
    public void OnPointerUp(PointerEventData eventData)
    {
        isPress = false;
        if (ShooUptFun != null)
        {
            ShooUptFun();
        }
    }
}

