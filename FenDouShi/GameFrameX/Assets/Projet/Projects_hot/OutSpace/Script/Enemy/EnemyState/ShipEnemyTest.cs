using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShipEnemyTest : Enemy  
{

    public GunBase gun;
   
    public override void Awake()
    {
        base.Awake();
        shipAttrackType = EnemyAttrackShipType.FarAttack;
        gun = this.gameObject.GetComponentInChildren<GunBase>();
        targetPos = OutSpaceCameraManager.Instance.Player.position;
        LookAtPos(targetPos);
    }
    public override void Active()
    {
        base.Active();
        targetPos = OutSpaceCameraManager.Instance.Player.position;
        LookAtPos(targetPos);
    }
    protected override void MoveToPos(Vector3 pos)
    {
        //与玩家保持距离攻击
        //if (Vector3.Distance(this.transform.position, OutSpaceCameraManager.Instance.Player.position) <= attachDistance /*&& isReadRote*/)
        //{
        //    attackPlayer();
        //    setState = ShipState.stop;
        //    return;
        //}
      //  Debug.LogFormat("MoveToPos=" + pos);
        //base.MoveToPos(pos);
        //LookAtPos(pos);
    }
    //到达目标点 追逐玩家

    //攻击玩家 条件和行为
    public override void attackPlayer()
    {
        base.attackPlayer();
        if (shipAttrackType== EnemyAttrackShipType.FarAttack)
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

