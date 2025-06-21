
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShipBoss : Enemy
{

    public GunBase gun;
    public BossCreateEnemy enemyPool1;
    public BossCreateEnemy enemyPool2;
    public override void Awake()
    {
        base.Awake();
        shipAttrackType = EnemyAttrackShipType.FarAttack;
        Active();


    }
    public override void Active()
    {
        base.Active();
        this.transform.LookAt(OutSpaceCameraManager.Instance.Player);
        enemyPool1.gameObject.SetActive(false);
        enemyPool2.gameObject.SetActive(false);
        ScaleFade scalefade = this.transform.GetComponent<ScaleFade>();
        scalefade.init();
    }
    protected override void MoveToPos(Vector3 pos)
    {
        targetPos = OutSpaceCameraManager.Instance.Player.position + Vector3.up*0.5f -this.transform.forward;
        //与玩家保持距离攻击
        if (Vector3.Distance(this.transform.position, OutSpaceCameraManager.Instance.Player.position) <= attrackDistance)
        {
            attackPlayer();
            return;
        }
        base.MoveToPos(pos);

    }
    //到达目标点 追逐玩家
    protected override void ArrivePos(Vector3 pos)
    {
        enemyPool1.gameObject.SetActive(true);
        enemyPool2.gameObject.SetActive(true);
    }

    //攻击玩家 条件和行为
    public override void attackPlayer()
    {
        if (!enemyPool1.gameObject.activeSelf)
        {
            enemyPool1.gameObject.SetActive(true);
            enemyPool2.gameObject.SetActive(true);
        }
        if (shipAttrackType == EnemyAttrackShipType.FarAttack)
        {

            if (gun != null)
            {
                gun.Shoot();
                
            }
        
        }


    }

    public override void Dead()
    {
        base.Dead();
    }


}

