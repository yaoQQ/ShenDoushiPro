using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Shiplight : Enemy
{

    public LightGun gun;
    public override void Awake()
    {
        base.Awake();
        shipAttrackType = EnemyAttrackShipType.FarAttack;
        gun = this.gameObject.GetComponentInChildren<LightGun>();
    }
    public override void Active()
    {
        base.Active();
    }

    protected override void MoveToPos(Vector3 pos)
    {
        //与玩家保持距离攻击
        if (Vector3.Distance(this.transform.position, OutSpaceCameraManager.Instance.Player.position) <= attrackDistance)
        {
            attackPlayer();
            return;
        }
        base.MoveToPos(pos);
        LookAtPos(pos);
    }
    //到达目标点 追逐玩家
    protected override void ArrivePos(Vector3 pos)
    {
        base.ArrivePos(pos);
    }


    //攻击玩家 条件和行为
    public override void attackPlayer()
    {
        base.attackPlayer();
        if (shipAttrackType == EnemyAttrackShipType.FarAttack)
        {

            if (gun != null)
            {
                targetPos = OutSpaceCameraManager.Instance.Player.position;
                transform.LookAt(targetPos);
                //   gun.transform.LookAt(targetPos);
                shootTime -= Time.deltaTime;
                if (shootTime<=0)
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

