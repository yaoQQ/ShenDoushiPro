using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AIState
{
    Idle,
    Move,
    Attrack,
    Rotate,
    Dead,

    None,

}
public class EnemyShipBase : ColliderItem
{
    public string ShipName;
 
    public EnemyAttrackShipType shipAttrackType = EnemyAttrackShipType.earAttack;
    public EnemyShipType shipType = EnemyShipType.None;
    public AIState shipState= AIState.None;
    public float life = 100;
    //属性速度
    public float speed = 0.5f;
    //当前速度
    public float currSpeed { get; set; }
    public float damage = 10;//撞击飞船伤害
    public float totalLife { get; private set; }
   

    //[SerializeField]
    //protected Blood blood;//是否要血条,预制件添加



  //  public float stopBehaviorTime = 2;
    public float RotateSpeed = 4;
    public float shootTime = 3;
    public float attrackDistance = 2;//攻击距离
    public int exp = 5;
    private Quaternion _targetRotation;
    [Header("Test set private ")]
    public Vector3 _targetPos=Vector3.zero;//目标点
  
    protected Transform _tailTransform;//尾飞行器




    [Header("摧毁特效：特效名，持续时间")]
    public effectObjc DestroyEffect;

    [Header("路径点")]
    [SerializeField]
    protected List<Vector3> _pathList = new List<Vector3>();
    public List<Vector3> pathList { get{ return _pathList; }}

    [Header("销毁声音")]
    public SimpleAudioEvent destoySound;

    protected float arriveDis = 0.001f;
    public virtual void Awake()
    {
        totalLife = life;
        currSpeed = speed;
        _tailTransform = this.transform.Find("tail");

    }

    //对象池重新获取激活调用
    public override void Active()
    {
        life = totalLife;
        currSpeed = speed;
        this.getNextPos();
        LookAtPos(targetPos);
        // stopBehaviorTime = 2;
      //  Logger.PrintColor("red","=====["+this.gameObject.name+ "]===="+_targetPos);
    }
    public bool getNextPos()
    {
        if (_pathList.Count <= 0)
        {
            return false;
        }
        if (_pathList.Count > 0)
        {

            _targetPos = _pathList[0];
           // Logger.PrintColor("blue", "getNextPos() _targetPos=" + _targetPos);
            _pathList.RemoveAt(0);

        }
        return true;
    }
 
    //添加移动点
    public void AddPathPos(Vector3 pos)
    {
        if (_pathList.Count > 10)
        {
            _pathList.RemoveAt(0);
        }
        _pathList.Add(pos);
       
    }
    public void AddPathPos(List<Vector3> posList)
    {
        _pathList.AddRange(posList);
    }
    public virtual void initPaths()
    {

    }

    //敌方旋转对准玩家 FSM 旋转对准对象
    protected virtual Quaternion LookAtPos(Vector3 lookPos)
    {
        Vector3 forwardPos = lookPos - transform.position;
        if (forwardPos == Vector3.zero)
        {
            return Quaternion.identity;
        }
        _targetRotation = Quaternion.LookRotation(forwardPos, Vector3.up);
        return _targetRotation;
    }
    //到达目标点 FSM 移动到对象
    protected virtual void ArrivePos(Vector3 pos)
    {
        

    }
    //攻击玩家行为  FSMAttack 对象
    public virtual void attackPlayer()
    {


    }
    //受到攻击行为  FSMHit 对象
    public override void Damage(float num, GameObject target = null)
    {
        life = life- num;
        if (life <= 0)
        {
            life = 0;
            Dead();
            if (this.gameObject.tag == OutSpaceTags.Enemy)
            {
                OutSpacePlayerInfoManager.Instance.AddExp(this.exp);
            }
        }
    }

    //死亡为  FSMDead 对象
    public virtual void Dead()
    {
        shipState = AIState.Dead;
        if (!string.IsNullOrEmpty(DestroyEffect.destroyEffect))
        {
            GameObject obj = MyUtils.LoadEffectPrefab(DestroyEffect.destroyEffect, false);
            if (obj != null)
            {
                obj.transform.position = this.transform.position;

            }
            if (destoySound != null)
            {
                OutSpaceAudioManager.Instance.PlayOnShotAtPos(destoySound, this.transform.position);
            }
        }
        gameObject.SetActive(false);

        MonsterManager.Instance.destoy(this);
        ResourceManagerPool.Instance.ReturnPoolObject(prefabName, ResourceType.ship, gameObject);
    }
    //public void stopAttrackTime(float time)
    //{
    //    stopBehaviorTime = time;
    //}
    public Quaternion targetRotation
    {
        get
        {
            return _targetRotation;
        }
        set
        {
            _targetRotation = value;
        }
    }
    public Vector3 targetPos
    {
        get
        {
            return _targetPos;
        }
        set
        {
            _targetPos = value;
        }
    }

    public Transform TailTransform
    {
        get
        {
            return _tailTransform;
        }
    }

    public virtual void IdleState()
    {
        shipState = AIState.Idle;
    }
    //移动动画
    public virtual void MoveState()
    {
        shipState = AIState.Move;
    }
    public virtual void AttrackState()
    {
        shipState = AIState.Attrack;
    }
    public virtual void RotateState()
    {
        shipState = AIState.Rotate;
    }
    public virtual void DeadState()
    {
        shipState = AIState.Dead;
    }
}
