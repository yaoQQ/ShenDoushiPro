
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class EnemyGun:GunBase,IEnemyGun
{

    //设置玩家射击按钮，标志位

    public override void Update()
    {
        base.Update();
        if (isCanShoot)
            return;
        time += Time.deltaTime;
        if (time >= coolTime)
        {
            isCanShoot = true;
            time = 0;
        }
    }
 
    public override void Shoot()
    {
        Vector3 pos = OutSpaceCameraManager.Instance.Player.position;
        this.transform.LookAt(pos);
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
        }
    }
    //创建子弹
   
    public override void Dead()
    {
       
    }
 
}

