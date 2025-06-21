using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum campEnum
{
    Player,
    Enemy,

    None
}
public class GunBase : MonoBehaviour, IGunBase
{
    [Header("子弹Layer")]
    public int bulletLayer;
    [Header("子弹名addressable[config]")]
    public string bulletName;//子弹对象名[config]

    [Header("子弹间隔时间[config]")]
    public float coolTime = 0.3f;//子弹间隔时间[config]

    protected float time = 0;
    
    [Header("子弹间隔时间 标志")]
    [SerializeField]
    protected bool isCanShoot = true;

    public campEnum camp = campEnum.Player;

    //回收
    protected GameObject cloneBullet;
    protected List<Bullet> bulletList = new List<Bullet>();

    [Header("子弹声音")]
    public SimpleAudioEvent gunShootAudio;

    [Header("子弹属性信息")]
    public GunInfo gunInfo;

    public Action<GunInfo> GunInfoUpdateCallBack;

    public virtual void Awake()
    {
        if (this.gameObject.tag.Equals(OutSpaceTags.Enemy))
        {
            camp = campEnum.Enemy;
        }
        else
        {
            camp = campEnum.Player;
        }
        
    }

    public virtual void Start()
    {
       
    }

  
    public virtual void Update()
    {

    }
    protected virtual Bullet createBullet()
    {
        cloneBullet = MyUtils.LoadBulletPrefab(bulletName);
 
        cloneBullet.layer = this.gameObject.layer;

        Bullet obj = cloneBullet.GetComponent<Bullet>();
        bulletList.Add(obj);
        obj.SetBulletInfo(gunInfo);
        //判断是哪方的子弹
        obj.camp = camp;
       // obj.shootTag = MyUtils.getEnemyTagByCamp(camp);
        obj.AfterActive();
        return obj;
    }

  

    //从缓存的无用子弹中获取子弹
    protected virtual Bullet getBullet()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (bulletList[i] == null)
            {
                continue;
            }
            if (bulletList[i].gameObject.activeSelf == false)
            {
                bulletList[i].Active();
                bulletList[i].AfterActive();

                return bulletList[i];
            }
        }
        Bullet obj = createBullet();
        return obj;
    }

    //射击
    public virtual void Shoot()
    {
      
    }


    public virtual void OnDestroy()
    {
        //GunManager.Instance.removeGunBtnEvnt(this);
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (bulletList[i] == null)
                continue;
            ResourceManagerPool.Instance.RemoveGameObject(bulletList[i].gameObject);
            GameObject.Destroy(bulletList[i].gameObject);
        }
    }
    public void UpdateGunInfo(GunInfo currGunInfo)
    {
        UpdateGunByGunInfo(currGunInfo);
        UpdateBulletByGunInfo(currGunInfo);
        if (GunInfoUpdateCallBack != null)
        {
            GunInfoUpdateCallBack(currGunInfo);
        }
    }
    protected void UpdateGunByGunInfo(GunInfo currGunInfo)
    {
        this.bulletName = currGunInfo.BulletName;
        this.coolTime = currGunInfo.getAttriCoolTime;
    }
    protected void UpdateBulletByGunInfo(GunInfo currGunInfo)
    {
        this.gunInfo = currGunInfo;
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (bulletList[i] == null)
                continue;
            bulletList[i].SetBulletInfo(this.gunInfo);
        }
    }
 
    public virtual void Dead()
    {
 
    }
}