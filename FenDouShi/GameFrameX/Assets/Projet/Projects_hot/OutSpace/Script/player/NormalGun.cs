using UnityEngine;


public class NormalGun : GunBase, IEnemyGun
{

    private ShipBase thisParent;
    public float soundTime = 0;
    public override void  Awake()
    {
        base.Awake();
        thisParent = this.transform.parent.GetComponent<ShipBase>();
    }
    //设置玩家射击按钮，标志位
    //CoolTime=0.05~0.13
    public override void Update()
    {
        if (isCanShoot)
        {
            return;
        }
        time += Time.deltaTime;
        if (time >= coolTime)
        {
            isCanShoot = true;
            time = 0;
        }
    }


    public void UpdateUVABulletByGunInfo()
    {
        base.UpdateBulletByGunInfo(this.gunInfo);
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
            if (soundTime <= 0)
            {
                OutSpaceAudioManager.Instance.PlayOnShot(this.gunShootAudio);
                soundTime = 0.2f;  
            }
            soundTime -= Time.deltaTime;
        }
      
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}


