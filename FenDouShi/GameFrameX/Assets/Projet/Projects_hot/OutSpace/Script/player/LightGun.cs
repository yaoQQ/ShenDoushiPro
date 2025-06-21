using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LightGun : PlayerGun, IEnemyGun
{
    public LineLengthController lightBullet;
    private F3DLightning light3D;

    private float countTime = 0;//子弹时间计数
    public float DamageRateTime =0.3f;//伤害频率
    private float demage = 0;
    private float soundTime=0;
    public override void Awake()
    {
        base.Awake();
        demage = this.gunInfo.getAttriButtleDamage;
        DamageRateTime = this.gunInfo.getAttriCoolTime;
    }
    public override void Update()
    {
        base.Update();

    }

    public override void BtnPressFun()
    {
        //if (!isCool)
        //{
        //    return;
        //}
        Shoot();
    }
    protected override void updateBullet()
    {

    }
    public override void Shoot()
    {
        base.Shoot();
     
        if (isCanShoot)
        {

            //if (lightBullet != null && !lightBullet.name.Equals(bulletName))//换了子弹类型
            //{
            //    lightBullet = null;
            //}
            LineLengthController obj = createLightBullet();
            if (obj == null)
                return;
            lightBullet.transform.position = this.transform.position;
            obj.transform.forward = this.transform.forward;
            if (lightBullet.length > lightBullet.distaceLimit)
            {
                lightBullet.length = lightBullet.distaceLimit;
            }
            else
            {
                lightBullet.length += lightBullet.ButtleSpeed * Time.deltaTime;
            }
            RaycastHit hit = light3D.ToUpdate();
            soundTime-=Time.deltaTime;
            if (soundTime <= 0)
            {
                OutSpaceAudioManager.Instance.PlayOnShot(gunShootAudio);
                soundTime = 0.5f;
            }
            if (hit.collider)
            {
                countTime += Time.deltaTime;

                if(countTime>= DamageRateTime)
                {

                   
                    ColliderItem colliderItem = hit.collider.GetComponent<ColliderItem>();
                    if (colliderItem != null)
                    {
                        colliderItem.Damage(demage, this.gameObject);
                    }
                    countTime = 0;
                    demage += demage;
                    if(demage> 5*lightBullet.ButtleDamage)
                    {
                        demage = 5 * lightBullet.ButtleDamage;
                    }
                }
            }
            else
            {
                Resert();
            }
        }
        else
        {
            BtnUptFun();
        }
    }
    public override void BtnUptFun()
    {
        if (lightBullet != null)
        {
            lightBullet.length = 0;
            lightBullet.gameObject.SetActive(false);
        }
    }
    private void InitLightBullet()
    {
        GameObject cloneBullet = MyUtils.LoadBulletPrefab(bulletName);
        lightBullet = cloneBullet.GetComponent<LineLengthController>();
        light3D = lightBullet.GetComponentInChildren<F3DLightning>();
        Resert();
    }
    private void Resert()
    {
        demage = this.gunInfo.getAttriButtleDamage;
        DamageRateTime = this.gunInfo.getAttriCoolTime;
        countTime = DamageRateTime;
    }
 
    protected  LineLengthController createLightBullet()
    {
        if (lightBullet == null)
        {
            InitLightBullet();
            
        }
        else {
            if (!lightBullet.gameObject.activeSelf)
            {
                lightBullet.gameObject.SetActive(true);
                countTime = DamageRateTime;
            }
           
        }
        return lightBullet;
    }


    public override void Dead()
    {
        if (lightBullet != null)
        {
            lightBullet.gameObject.SetActive(false);
        }
    }

}

