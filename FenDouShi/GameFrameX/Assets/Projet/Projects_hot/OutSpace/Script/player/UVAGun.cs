using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVAGun : PlayerGun, IEnemyGun
{
    //设置玩家射击按钮，标志位
    //CoolTime=0.05~0.13
    private List<UVAMonoBehaviour> UVAShipList=new List<UVAMonoBehaviour>();

    public override void  Awake()
    {
        base.Awake();
        GunInfoUpdateCallBack = UpdateGunInfoCallBack;
    }
    private void UpdateGunInfoCallBack(GunInfo gunInfo)
    {
        resetShoot();
    }
    public override void Update()
    {
       
        if (isCanShoot)
            return;

        //Debug.Log("startInput.isFastGun=" + startInput.isFastGun);
        if (startInput && startInput.isUVAGun)
        {
            BtnPressFun();
        }

        time += Time.deltaTime;
        if (time >= coolTime)
        {
            isCanShoot = true;
            time = 0;
        }

    }


    //从缓存的无用子弹中获取子弹
    //按钮点击回调
    public override void BtnPressFun()
    {
        if (!isCool)
        {
            return;
        }
        Shoot();


    }
    public override void BtnUptFun()
    {
        base.BtnUptFun();
    }


    public override void Shoot()
    {
        if (bulletCount <= 0)
        {
            CommonView.showTopTips("无人机已经达到上限");
            return;
        }
        if (camp == campEnum.Player)
        {
            if (PlayerGun.targetLog != null)
            {
                PlayerGun.targetLog.shoot();
            }
        }
        
        if (isCanShoot)
        {
            isCanShoot = false;
            UVAMonoBehaviour obj = getUVAShip();
            if (obj == null)
                return;
            obj.transform.position = this.transform.position;
            obj.transform.forward = this.transform.forward;
            obj.ShowOnGuard();
            bulletCount--;
            updateBullet();
        }

    }
    //刷新重置释放飞机
    protected override void resetShoot()
    {
        int showCount = 0;
        for (int i = 0; i < UVAShipList.Count; i++)
        {
            UVAShipList[i].UpdateByGunInfo(this.gunInfo);
            if (UVAShipList[i].gameObject.activeSelf == true)
            {
                showCount++;
            }
        }
        bulletCount = gunInfo.getAttriBulletTotolCount - showCount;
        Logger.PrintColor("blue", "resetShoot()");
        Logger.PrintColor("blue", "showCount=" + showCount);
        Logger.PrintColor("blue", "bulletCount="+ bulletCount);
        updateBullet();
    }
    protected override void updateBullet()
    {
        BulletCollTimeCount = bulletCount <= 0 ? gunInfo.BulletCollTimeTotal : 0;
        updateFun();
        
    }
    private UVAMonoBehaviour getUVAShip()
    {
        for(int i=0;i< UVAShipList.Count; i++)
        {
            if (UVAShipList[i].gameObject.activeSelf == false)
            {
                UVAShipList[i].Active();
                UVAShipList[i].AfterActive();
                return UVAShipList[i];
            }
        }
        
        cloneBullet = MyUtils.LoadBulletPrefab(bulletName);
        cloneBullet.name = bulletName;

        UVAMonoBehaviour obj = cloneBullet.GetComponent<UVAMonoBehaviour>();
        //判断是哪方的子弹
        obj.UpdateByGunInfo(this.gunInfo);
        obj.camp = camp;
      //  obj.shootTag = MyUtils.getEnemyTagByCamp(camp);
        UVAShipList.Add(obj);
        return obj;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}
