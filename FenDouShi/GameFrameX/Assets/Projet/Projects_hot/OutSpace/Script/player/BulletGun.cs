using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// FastGun 枪所属类
/// 属性相关BulletTotolCount、BulletCollTimeTotal、bulletName、coolTime
/// </summary>
public class BulletGun : PlayerGun, IEnemyGun
{

   public override void Awake()
   {
        base.Awake();
   


       // UpdateGunInfo(1);
    }
    //设置玩家射击按钮，标志位
    //CoolTime=0.05~0.13
    public SimpleAudioEvent pressShootAudio;
    public override void Update()
    {
        base.Update();
        if (isCanShoot)
            return;

        //Debug.Log("startInput.isFastGun=" + startInput.isFastGun);
        if (startInput!=null&&startInput.isFastGun)
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
        isFirstShoot = true;
        AudioPressTime = 0;
    }

    private bool isFirstShoot = true;
    private float AudioPressTime = 0;
    public override void Shoot()
    {
        base.Shoot();
        if (isCanShoot)
        {
            isCanShoot = false;
            Bullet obj = getBullet();
            if (obj == null)
                return;
            obj.transform.position = this.transform.position;
            obj.transform.forward = this.transform.forward;
             if (isFirstShoot)
            {
                OutSpaceAudioManager.Instance.PlayOnShot(this.gunShootAudio);
                isFirstShoot = false;
            }
            else
            {
                AudioPressTime -= Time.deltaTime;
                if (AudioPressTime <= 0)
                {
                    OutSpaceAudioManager.Instance.PlayOnShot(this.pressShootAudio);
                    AudioPressTime = this.coolTime/5;
                  //  Debug.Log("OutSpaceAudioManager.Instance.PlayOnShot=" + Time.time);
                }
            }
        }
        
    }
 
    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}

