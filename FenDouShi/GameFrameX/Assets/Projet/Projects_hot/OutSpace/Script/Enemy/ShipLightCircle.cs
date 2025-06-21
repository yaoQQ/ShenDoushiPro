
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShipLightCircle : Enemy
{

    public LightGun gun;
    public override void Awake()
    {
        base.Awake();
        shipAttrackType = EnemyAttrackShipType.FarAttack;
        gun = this.gameObject.GetComponentInChildren<LightGun>();
        transform.LookAt(OutSpaceCameraManager.Instance.Player.position);
    }

    public override void Start()
    {
        base.Start();

    }
    public override void Active()
    {
        base.Active();
        shootTime = 3;
        transform.LookAt(OutSpaceCameraManager.Instance.Player.position);
    }

    //protected override void MoveToPos(Vector3 pos)
    //{

    //    targetPos = OutSpaceCameraManager.Instance.Player.position+Vector3.up/4  -Vector3.forward*this.transform.forward.normalized.z;
    //    //与玩家保持距离攻击
    //    float distance = Vector3.Distance(this.transform.position, OutSpaceCameraManager.Instance.Player.position);
    //    float distance2 = attrackDistance / 1.5f;

    //    if (distance <= attrackDistance&&distance> distance2)
    //    {
    //        attackPlayer();
    //        return;
    //    }
    //    base.MoveToPos(targetPos);
    //}

    //到达目标点 追逐玩家
    protected override void ArrivePos(Vector3 pos)
    {
        base.ArrivePos(pos);
        attackPlayer();
    }

    //攻击玩家 条件和行为
    public override void attackPlayer()
    {
        base.attackPlayer();
        if (shipAttrackType == EnemyAttrackShipType.FarAttack)
        {

            if (gun != null)
            {
                transform.LookAt(OutSpaceCameraManager.Instance.Player.position);
                transform.RotateAround(OutSpaceCameraManager.Instance.Player.position, Vector3.up/2, 10 * Time.deltaTime);
                gun.transform.LookAt(OutSpaceCameraManager.Instance.Player.position);
                if (gun.lightBullet != null)
                {
                    gun.lightBullet.transform.position = gun.transform.position;
                    gun.lightBullet.transform.forward = gun.transform.forward;
                }
                shootTime -= Time.deltaTime;
                if (shootTime <= 0)
                {
                    gun.BtnPressFun();
                    shootTime = 3;
                }
            }
        }


    }
    public override void Dead()
    {
        base.Dead();
        if (gun != null)
        {
            gun.Dead();
        }
    }
    public void OnDestroy()
    {
        if (gun != null)
        {
            gun.OnDestroy();
            GameObject.Destroy(gun.gameObject);
        }
    }
}

