using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShipA0303p : Enemy
{

    public GunBase gun;
    public override void Awake()
    {
        base.Awake();
        shipAttrackType = EnemyAttrackShipType.FarAttack;
        gun = this.gameObject.GetComponentInChildren<GunBase>();
    }
    public override void Active()
    {
        base.Active();
        this.transform.LookAt(OutSpaceCameraManager.Instance.Player);
    }

    //protected override void MoveToPos(Vector3 pos)
    //{
    //    //与玩家保持距离攻击
    //    if (Vector3.Distance(this.transform.position, OutSpaceCameraManager.Instance.Player.position) <= attrackDistance /*&& isReadRote*/)
    //    {
    //        attackPlayer();
    //        return;
    //    }
    //    base.MoveToPos(pos);
    //    LookAtPos(pos);
    //}
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
                gun.transform.LookAt(OutSpaceCameraManager.Instance.Player.position);
                targetPos = OutSpaceCameraManager.Instance.Player.position;
                LookAtPos(targetPos);
                gun.Shoot();
            }
        }


    }




}

