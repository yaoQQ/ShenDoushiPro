
using UnityEngine;


public class AutoBulletGun : PlayerGun, IEnemyGun
{
    public override void Start()
    {

    }

    //设置玩家射击按钮，标志位
    //CoolTime=0.05~0.13
    public override void Update()
    {
       // base.Update();

        //if (!startInput.isAutoGunShoot)
        //{
        //    return;
        //}
   
        time += Time.deltaTime;
        if (time >= coolTime)
        {
            isCanShoot = true;
            time = 0;
        }
        if (!isCanShoot)
            return;
        Shoot();
    }
  

    //从缓存的无用子弹中获取子弹

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
       
        if (isCanShoot)
        {
            isCanShoot = false;
            Bullet obj = getBullet();
            if (obj == null)
                return;
            obj.transform.position = this.transform.position;
            obj.transform.forward = this.transform.forward;
        }

    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}


