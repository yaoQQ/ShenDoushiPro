using Assets.script.FSMFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public struct effectObjc
{
    public string destroyEffect;//爆炸特效名
    public float time;//爆炸时间
}


public class ShipBase : ColliderItem
{

    public float speed = 0.5f;
    [Header("当前生命")]
    public float life = 100;
   
    public float totalLife { get; private set; }

    [Header("摧毁特效：特效名，持续时间")]
    public effectObjc DestroyEffect;

    [Header("路径点")]
    public List<Vector3> pathList = new List<Vector3>();
    public ShipState _State = ShipState.none;
    public delegate void deadCallBack();
    public deadCallBack DeadCallBackFun;
    public FiniteStateMachine playerFSM;

    [Header("销毁声音")]
    public SimpleAudioEvent destoySound;
    public virtual void Awake()
    {
        totalLife = life;
    
    }

    //对象池重新获取激活调用
    public override void Active()
    {
        life = totalLife;
    }
    public override void Damage(float num, GameObject target = null)
    {
        life -= num;
        if (life <= 0)
        {
            life = 0;
            Dead();

        }
    }

    public virtual void Dead()
    {
        if (!string.IsNullOrEmpty(DestroyEffect.destroyEffect))
        {
            GameObject obj = MyUtils.LoadEffectPrefab(DestroyEffect.destroyEffect, false);
            if (obj != null)
            {
                obj.transform.position = this.transform.position;
              
            }
            if (destoySound != null) {
                OutSpaceAudioManager.Instance.PlayOnShotAtPos(destoySound,this.transform.position);
            }
        }
        gameObject.SetActive(false);
        if (DeadCallBackFun != null)
        {
            DeadCallBackFun();
        }
    }
  
  
}

