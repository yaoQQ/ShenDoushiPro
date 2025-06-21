using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//可碰撞摧毁物体
public class ColliderItem : ResourcePoolEnable
{
   [Header("子弹要打击的敌对放")]
    [SerializeField]
    protected string _shootTag;//打击的对象
    public campEnum camp = campEnum.None;
    public virtual void OnTriggerEnter(Collider other)
    {
    
    }
    public int shootTargetLay
    {
        get
        {
            if(camp== campEnum.Enemy)
            {
                return MyUtils.playerLayer;
            }else if(camp == campEnum.Player)
            {
                int lay = (1 << MyUtils.enemyLayer) + (1 << MyUtils.collderLayer);
                return lay;
            }
            return 0;
        }
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="num">伤害值</param>
    /// <param name="target">打击对象</param>
    public virtual void Damage(float num, GameObject target = null)
    {
   
    }
    //子弹要打击的敌对放
    public string shootTag
    {
        set
        {
            _shootTag = value;
        }
        get
        {
            return _shootTag;
        }
    }
}

