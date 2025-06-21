
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShipMoveAttrack : Enemy
{

    public GunBase gun;
    public override void Awake()
    {
        base.Awake();
        shipAttrackType = EnemyAttrackShipType.FarAttack;
        gun = this.gameObject.GetComponentInChildren<GunBase>();
    }

    public override void Start()
    {
        base.Start();
        //   targetPos = OutSpaceCameraManager.Instance.Player.transform.position + Vector3.up * 0.5f;
        Active();
    }
    public override void Active()
    {
        base.Active();
        LookAtPos(targetPos);
    }

    //protected override void MoveToPos(Vector3 pos)
    //{

    //    //Debug.Log("distance11) =" + Vector3.Distance(this.transform.position, OutSpaceCameraManager.Instance.Player.transform.position));
    //    //Debug.Log("attachDistance=" + attachDistance);
    //    //与玩家保持距离攻击
    //    if (Vector3.Distance(this.transform.position, OutSpaceCameraManager.Instance.Player.position) <= attrackDistance)
    //    {
    //        attackPlayer();

    //    }
    //    else
    //    {//敌机面对才攻击玩家
    //        lastFarward = getCurrFard();
    //    }
    //    if (isReadRote)
    //    {
    //        base.MoveToPos(pos);
    //    }
    //    LookAtPos(pos);
    //}
    private int lastFarward;
    private int getCurrFard()
    {
        Vector3 limiposNormal = OutSpaceCameraManager.Instance.Player.position - this.transform.position;
        limiposNormal.Normalize();
        int currFarward = limiposNormal.z > 0 ? 1 : -1;
        return currFarward;
    }

    //到达目标点 追逐玩家
    protected override void ArrivePos(Vector3 pos)
    {
        base.ArrivePos(pos);
        targetPos = MyUtils.findPlayerCirclePos(this.transform.position, 3);
        LookAtPos(targetPos);
    }

    //攻击玩家 条件和行为
    public override void attackPlayer()
    {
        base.attackPlayer();
        //targetPos = MyUtils.findPlayerCirclePos(this.transform.position, 3);
        //LookAtPos(targetPos);
        if (shipAttrackType == EnemyAttrackShipType.FarAttack)
        {
            int currFarward = getCurrFard();
            if (currFarward != lastFarward) return;//敌机面对才攻击玩家
            if (gun != null)
            {
                gun.transform.LookAt(OutSpaceCameraManager.Instance.Player.position);
                LookAtPos(targetPos);
                gun.Shoot();
            }

        }


    }

}



