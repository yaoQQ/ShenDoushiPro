using StarterAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerGun : GunBase, IPlayerGun
{
    //设置玩家射击按钮，标志位
    public PlayerGunBtnsEnum GunBtnPos = PlayerGunBtnsEnum.none;

    //枪的类型
    public GunType gunType = GunType.None;

    protected int bulletCount;//子弹计数
    [Header("总子弹数[config] 舍弃--gunInfo.BulletTotolCount")]
    public int BulletTotolCount;//总子弹数[config]

    protected float BulletCollTimeCount;//重置子弹冷却计数
    [Header("(换弹夹)总子弹冷却时间[config]")]
    public float BulletCollTimeTotal;//总子弹冷却时间[config]

    //更新按钮回调
    public delegate void updateGunBtn();
    public updateGunBtn updateBtn;

    public static TargetLog targetLog;  //瞄准器

    protected OutSpaceStarterAssetsInputs startInput;

    public override void Awake()
    {
        base.Awake();
        if (PlayerGun.targetLog == null)
        {
            GameObject obj = GameObject.Find("targetLog");
            if (obj)
                PlayerGun.targetLog = obj.GetComponent<TargetLog>();
        }

        startInput = OutSpaceCameraManager.Instance.StartInput;
        if (startInput == null)
        {
            Logger.PrintWarning("startInput == null");
        }
        if (gunInfo)
            this.bulletCount = gunInfo.getAttriBulletTotolCount;
    }
    public override void Start()
    {

        base.Start();
        BulletCollTimeCount = 0;
        updateFun();
        NoticeManager.Instance.AddNoticeLister(OutSpaceNotice.PlayerAttriDataUpdate, PlayerAttriChange);
    }
    /// <summary>
    /// 玩家属性改变
    /// </summary>
    private void PlayerAttriChange(string noticeType, BaseNotice notice)
    {

        ObjectNotice obj = (ObjectNotice)notice;
        AttriInfo attriInfo = obj.GetObj() as AttriInfo;
        switch (attriInfo.attriType)
        {
            case AttriEnum.coolTime:
                base.UpdateGunByGunInfo(this.gunInfo);
                break;
            case AttriEnum.bulletSpeed:
            case AttriEnum.shipDamage:
            case AttriEnum.lifeTime:
                base.UpdateBulletByGunInfo(this.gunInfo);
                break;
            case AttriEnum.bulletNum://无人机数 ，机枪数
                if (GunInfoUpdateCallBack != null)
                {
                    GunInfoUpdateCallBack(this.gunInfo);
                }
                break;
        }


    }
    public override void Update()
    {
        if (BulletCollTimeCount > 0)
        {
            BulletCollTimeCount -= Time.deltaTime;
           
            if (BulletCollTimeCount <= 0)
            {
                
                resetShoot();
            }
            updateFun();
        }
    }
    public PlayerGunBtnsEnum GetGunBtnPos
    {
        get
        {
            return GunBtnPos;
        }
    }
    public int getCurrBulletCount
    {
        get
        {
            return bulletCount;
        }
    }
    public float getBulletCollTimeCount
    {
        get
        {
            return BulletCollTimeCount;
        }
    }
    //是否冷却完毕
    public bool isCool
    {
        get
        {
            return BulletCollTimeCount>0?false:true;
        }
    }

    public override void Shoot()
    {
        if (camp == campEnum.Player)
        {
            if (PlayerGun.targetLog != null)
            {
                PlayerGun.targetLog.shoot();
            }
        }
        updateBullet();
       
    }

    //更新子弹和刷新时间
    protected virtual void updateBullet()
    {
        if (isCanShoot)
        {
            bulletCount--;
            if (bulletCount <= 0)
            {
                BulletCollTimeCount = gunInfo.BulletCollTimeTotal;
             
            }
            updateFun();
        }
    }


    //刷新重置z子弹
    protected virtual void resetShoot()
    {
        BulletCollTimeCount = 0;
        bulletCount = gunInfo.getAttriBulletTotolCount;
    }
    public void UpdateGunInfoByData(GameGunData gunData)
    {
        UpdateGunInfo(gunData);
    }
    protected void UpdateGunInfo(GameGunData gameGunData)
    {
        int level = gameGunData.level;
        if (gunInfo!=null&&gunInfo.level!= level)//删除上一级的数据
        {
            OutSpaceResourceManager.Instance.RemoveGunInfo(this.gunType.ToString() + "Info_" + this.gunInfo.level);
        }
        if (gunInfo == null || gunInfo.level != level)
        {
            OutSpaceResourceManager.Instance.GetGunInfo(gameGunData.gunInfoAssertPath, (gunInfoT) => {
                this.gunInfo = gunInfoT;
                updateByGameInfo();
                CommonView.showTopTips("升级武器成功");
            });
        }
        updateFun();
    }

    //根据最新gunInfo 更新枪和子弹属性
    public void updateByGameInfo()
    {
        this.BulletCollTimeCount =0;
        this.bulletCount = gunInfo.getAttriBulletTotolCount;
        base.UpdateGunInfo(gunInfo);
    }

    /// <summary>
    /// 换弹夹更新界面回调
    /// </summary>
    protected void updateFun()
    {
        //更新UI按钮界面显示
        if (updateBtn != null)
        {
   
            updateBtn();

        }
    }
    public virtual void BtnPressFun()
    {
        bulletCount--;
    }
    public virtual void BtnUptFun()
    {

    }
    public override void Dead()
    {

    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        NoticeManager.Instance.RemoveNoticeLister(OutSpaceNotice.PlayerAttriDataUpdate, PlayerAttriChange);
    }
}

