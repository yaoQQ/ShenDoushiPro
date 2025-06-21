using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DefendGun : PlayerGun
{
    private DefendBullet DefendSkill;

    
    public override void Update()
    {
        base.Update();
        if (startInput&&startInput.isDefendGun)
        {
            BtnPressFun();
        }
    }

 
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

            if (DefendSkill != null && !DefendSkill.name.Equals(bulletName))//换了子弹类型
            {
                DefendSkill = null;
            }
            GameObject obj = getDefend();
            if (obj == null)
                return;
            //obj.transform.forward = this.transform.forward;
            obj.transform.localPosition = Vector3.zero;

        }

    }


    //从缓存的无用子弹中获取子弹
    private GameObject getDefend()
    {
        if (DefendSkill != null)
        {
            DefendSkill.gameObject.SetActive(true);
            return DefendSkill.gameObject;
        }

        GameObject obj = createDefend();
        return obj;
    }

    //创建子弹
    private GameObject  createDefend()
    {

        GameObject obj = MyUtils.LoadBulletPrefab(bulletName);//资源的默认设置，激光需偏移才看的到
        obj.transform.parent = OutSpaceCameraManager.Instance.Player;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = Vector3.zero;
        ColliderItem colliderItem = obj.GetComponent<ColliderItem>();
        colliderItem.camp = this.camp;
        //obj.transform.forward = this.transform.forward;
        if (obj == null)
            return null;
        //DefendSkill = obj.GetComponent<ShipBase>();
        //DefendSkill.gameObject.name = bulletName;
        ////判断是哪方的子弹
        //DefendSkill.shootTag = MyUtils.getEnemyTagByCamp(camp);
        return obj;
    }


    public override void OnDestroy()
    {
        base.OnDestroy();
        if (DefendSkill != null)
            GameObject.Destroy(DefendSkill);
    }
}

