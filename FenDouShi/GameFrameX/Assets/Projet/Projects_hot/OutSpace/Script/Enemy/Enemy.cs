using Assets.script.FSMFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyAttrackShipType
{
    FarAttack,
    earAttack
}
public enum EnemyShipType
{
    CashBoss,
    CashShip,
    ScatterShip,
    ScatterSingleShip,
    BezierShip,
    Asteriod,

    None
}
public enum ShipState
{
    stop,
    Move,
    Attrack,
    Dead,
    none
}
public class Enemy : ShipBase
{
    public string ShipName;
    public EnemyAttrackShipType shipAttrackType = EnemyAttrackShipType.earAttack;
   


    public float stopBehaviorTime = 2;
    public float RotateSpeed = 4;
    public float shootTime = 3;
    public float attrackDistance = 2;//攻击距离
    private Quaternion _targetRotation;
    private Vector3 _targetPos;//目标点

    private Transform tailTransform;//尾飞行器
    public string CurrState;
    public StateBase m_State;
    public override void Awake()
    {
        base.Awake();
        tailTransform = this.transform.Find("tail");
        playerFSM = new FiniteStateMachine();
        playerFSM.Register(ShipState.stop.ToString(), new EnemyStateStop(this));
        playerFSM.Register(ShipState.Move.ToString(), new EnemyStateMove(this));
        playerFSM.Register(ShipState.Dead.ToString(), new EnemyStateDead(this));
        playerFSM.Register(ShipState.Attrack.ToString(), new EnemyStateAttrack(this));
        playerFSM.SetEntryPoint(ShipState.Move.ToString());
        pathList.Add(Vector3.zero);
        getNextPos();
    }
    public virtual void Start()
    {

   
     
    }
    public Quaternion targetRotation {
        get {
            return _targetRotation;
        }
        set {
            _targetRotation = value;
        }
    }
    public Vector3 targetPos {
        get {
            return _targetPos;
        }
        set {
            _targetPos = value;
        }
    }

    public override void Active()
    {
        base.Active();
        getNextPos();
        stopBehaviorTime = 2;
    }

 
    public virtual void Update()
    {
        if (stopBehaviorTime > 0) {
            stopBehaviorTime -= Time.deltaTime;
            return;
        }
        m_State = playerFSM.Update();
     
    }
  

    //移动到目标点
    protected virtual void MoveToPos(Vector3 pos)
    {
        //float distance = Vector3.Distance(gameObject.transform.position, targetPos);
        //if (Vector3.Distance(gameObject.transform.position, targetPos) < speed * Time.deltaTime)
        //{
        //    ArrivePos(targetPos);
        //    setState = ShipState.stop;
        //    return;
        //}
        //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
        //setState = ShipState.Move;
    }

    //旋转到目标点
    protected virtual void LookAtPos(Vector3 lookPos) {
        Vector3 forwardPos = lookPos - transform.position;
        if (forwardPos == Vector3.zero) {
            return;
        }
        _targetRotation = Quaternion.LookRotation(forwardPos, Vector3.up);
    }
    //到达目标点 行为
    protected virtual void ArrivePos(Vector3 pos) {
        //if (pathList.Count > 0) {
        //    targetPos = pathList[0];
        //    pathList.Remove(targetPos);
        //}
        //else {
        //    if (Vector3.Distance(gameObject.transform.position, targetPos) < speed * Time.deltaTime) {
        //        setState = ShipState.stop;
        //        return;
        //    }
        //}

    }
    public Vector3 getNextPos()
    {
        if (pathList.Count > 0)
        {

            _targetPos = pathList[0];
            pathList.Remove(_targetPos);
             
        }
        return _targetPos;
    }
    //攻击玩家行为
    public virtual void attackPlayer()
    {
        
     
    }

    public override void Damage(float num, GameObject target = null)
    {
        life -= num;
        if (life <= 0) {
            life = 0;
            playerFSM.Trigger(ShipState.Dead.ToString());
            MonsterManager.Instance.addScore(10);

        }

    }
    public void stopAttrackTime(float time)
    {
        stopBehaviorTime = time;
    }
    public Transform TailTransform {
        get {
            return tailTransform;
        }
    }
    public override void Dead()
    {
        base.Dead();
        MonsterManager.Instance.destoy(this.gameObject);
        ResourceManagerPool.Instance.ReturnPoolObject(prefabName, ResourceType.ship, gameObject);
    }
  

}
