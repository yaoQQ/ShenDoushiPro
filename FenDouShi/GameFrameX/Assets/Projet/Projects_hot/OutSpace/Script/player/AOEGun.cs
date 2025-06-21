using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AOEGun : PlayerGun
{
    //回收
    //设置玩家射击按钮，标志位

    public override void Update()
    {
        base.Update();
        if (isCanShoot)
            return;

      //  Debug.Log("startInput.isAOEGun=" + startInput.isAOEGun);
        if (startInput && startInput.isAOEGun)
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
   
    //按钮点击回调
    public override void BtnPressFun()
    {
        if (!isCool)
        {
            return;
        }
        Shoot();
    }
    public override void Shoot()
    {
        base.Shoot();
        if (isCanShoot)
        {
            if (cloneBullet != null && !cloneBullet.name.Equals(bulletName))//换了子弹类型
            {
                cloneBullet = null;
            }
            isCanShoot = false;
            Bullet obj = getBullet();
            if (obj == null)
                return;
            obj.transform.position = this.transform.position;
            obj.transform.forward = this.transform.forward;

            OutSpaceAudioManager.Instance.PlayOnShot(this.gunShootAudio);
        }
    }
  


}

