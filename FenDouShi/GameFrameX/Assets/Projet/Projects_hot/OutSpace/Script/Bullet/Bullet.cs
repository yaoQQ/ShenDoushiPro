using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ColliderItem
{

    // Use this for initialization
    public string BoomEffectName;
    [Header("子弹速度[config]")]
    public float ButtleSpeed;
    [Header("子弹伤害[config]")]
    public float ButtleDamage;
    [Header("子弹持续时间[config]")]
    public float timeLife = 3;
    public GunInfo gunInfo;

    [Header("销毁声音")]
    public SimpleAudioEvent destoySound;



    public virtual void Awake()
    {

    }

    public override void Active()
    {
        base.Active();
        timeLife = 3;
        if (GunManager.Bullets != null)//默认子弹父类
            this.transform.parent = GunManager.Bullets.transform;
        this.gameObject.SetActive(true);
    }
   
    public virtual void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * ButtleSpeed);
        timeLife -= Time.deltaTime;
        if (timeLife <= 0)
        {

            DisAcvtive();
        }
    }
    public override void OnTriggerEnter(Collider other)
    {
        bool isCollider = false;
        if(camp== campEnum.Player)
        {
            if (other.gameObject.layer == MyUtils.enemyLayer || other.gameObject.layer == MyUtils.collderLayer)
            {
                isCollider = true;
            }
        }
        else if(camp == campEnum.Enemy)
        {
            if (other.gameObject.layer == MyUtils.playerLayer)
            {
                isCollider = true;
            }
        }
        if (isCollider)
        {
            ColliderItem colliderItem = other.gameObject.GetComponent<ColliderItem>();
            if (colliderItem != null)
            {
                ShowHit();
                colliderItem.Damage(ButtleDamage, this.gameObject);
            }
        }
  

    }
   
    public virtual void ShowHit()
    {
        showHitEffect();
        DisAcvtive();
    }
    /// <summary>
    /// 重新激活子弹
    /// </summary>
    public virtual void ReAcvtive()
    {
        //timeLife = 3;
        //this.transform.parent = GunManager.Bullets.transform;
        //this.gameObject.SetActive(true);

    }
    /// <summary>
    /// 重新取消子弹
    /// </summary>
    public virtual void DisAcvtive()
    {
        this.gameObject.SetActive(false);
        ResourceManagerPool.Instance.ReturnPoolObject(prefabName, ResourceType.bullet, gameObject);
    }
    protected virtual void showHitEffect()
    {
        if (!string.IsNullOrEmpty(BoomEffectName))
        {
            GameObject BoomEffect = MyUtils.LoadEffectPrefab(BoomEffectName);
            if (BoomEffect != null)
            {
                BoomEffect.transform.position = this.transform.position;
               
                BoomEffect.gameObject.SetActive(true);
                OutSpaceAudioManager.Instance.PlayOnShotAtPos(destoySound, this.transform.position);
            }
        }
    }
  
    /// <summary>
    /// 刷新子弹属性
    /// </summary>
    /// <param name="gunInfo"></param>
    public virtual  void SetBulletInfo(GunInfo gunInfo)
    {
        if (gunInfo == null)
        {
           // Logger.PrintError("gunInfo == null bullet.name=" + this.gameObject.name);
            return;
        }
        this.gunInfo = gunInfo;
        ButtleSpeed = gunInfo.getAttriButtleSpeed;
        ButtleDamage = gunInfo.getAttriButtleDamage;
        timeLife = gunInfo.getAttriTimeLife;
    }

    //子弹互相碰撞
    public override void Damage(float num, GameObject target = null)
    {
        DisAcvtive();
    }
    public virtual void OnDestroy()
    {

    }
}
