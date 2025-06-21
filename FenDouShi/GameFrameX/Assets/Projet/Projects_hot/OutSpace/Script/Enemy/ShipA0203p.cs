using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShipA0203p : Enemy
{
    public float damage = 10;//撞击飞船伤害
    private string colliderEffect= "smallHit";
    public override void Awake()
    {
        base.Awake();
        Active();
    }

    public override void Active()
    {
        base.Active();
        this.transform.LookAt(OutSpaceCameraManager.Instance.Player);
    }
    //protected override void MoveToPos(Vector3 pos)
    //{
    //    LookAtPos(pos);
    //    base.MoveToPos(pos);

    //}
 
  
    //到达目标点 追逐玩家
    //protected override void ArrivePos(Vector3 pos)
    //{
    //    base.ArrivePos(pos);
    //    targetPos = OutSpaceCameraManager.Instance.Player.position;
    //    MoveToPos(OutSpaceCameraManager.Instance.Player.position);
    //}

    //攻击玩家 条件和行为
    public override void attackPlayer()
    {
        base.attackPlayer();
        targetPos = OutSpaceCameraManager.Instance.Player.position;
        MoveToPosHit(targetPos);
    }
    private  void MoveToPosHit(Vector3 targetPos)
    {
        LookAtPos(targetPos);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
        //setState = ShipState.Move;
    }



    public override void Dead()
    {
        base.Dead();
    }
    public override void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == MyUtils.playerLayer)
        {
            //  Debug.Log(Time.time+"ShipA0203p OnTriggerEnter=" + damage);
      
            ShipBase ship = other.gameObject.GetComponent<ShipBase>();
            if (ship != null)
            {
                ship.Damage(damage,this.gameObject);
              //  Debug.Log(this.gameObject.name + " damage " + damage);
            }
            this.gameObject.SetActive(false);
            colliderDead();
        }
    }

    public void colliderDead()
    {
        if (!string.IsNullOrEmpty(colliderEffect))
        {
            GameObject obj = MyUtils.LoadEffectPrefab(colliderEffect, false);
            if (obj != null)
            {
                obj.transform.position = this.transform.position;
            }
        }
        Dead();
    }
}

