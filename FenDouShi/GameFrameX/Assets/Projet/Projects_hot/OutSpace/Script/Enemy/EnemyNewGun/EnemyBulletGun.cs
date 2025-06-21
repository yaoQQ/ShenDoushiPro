
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class EnemyBulletGun : GunBase, IEnemyGun
{

    //从缓存的无用子弹中获取子弹


    public override void Shoot()
    {
        time += Time.deltaTime;
        if (time >= coolTime)
        {
            isCanShoot = true;
            time = 0;
        }


        if (isCanShoot)
        {
            isCanShoot = false;
            Bullet obj = getBullet();
            if (obj == null)
                return;
            obj.transform.position = this.transform.position;
            obj.transform.forward = this.transform.forward;
            OutSpaceAudioManager.Instance.PlayOnShot(this.gunShootAudio);
        }

    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}

