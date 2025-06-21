using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRedBall : Bullet
{
    public float life = 1;
    //对象池重新激活
    public override void Active()
    {
        base.Active();
        timeLife = 4;
    }
    public override void AfterActive()
    {
      
        //this.shootTag = OutSpaceTags.ColliderItem;
    }
    public override void Damage(float num, GameObject target = null)
    {
        life -= num;
        if (life <= 0)
        {
            life = 0;
            DisAcvtive();

        }
    }
    ///// <summary>
    ///// 开始加载的重新激活子弹
    ///// </summary>
    //public override void ReAcvtive()
    //{

    //    base.ReAcvtive();
    //    timeLife = 4;
    //}
    public override void DisAcvtive()
    {
        base.DisAcvtive();
   
    }
  
}
