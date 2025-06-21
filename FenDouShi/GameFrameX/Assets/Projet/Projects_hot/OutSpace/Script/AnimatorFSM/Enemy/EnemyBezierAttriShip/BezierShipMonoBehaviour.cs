using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BezierShipMonoBehaviour : EnemyBaseSMF
{
    public GunBase gun;
    private Transform _target;
    public Transform target { get { return _target; } }



    private string colliderEffect = "smallHit";
    public float attrackDisMin = 1;
    //曲线的集合点
    public Vector3 p0 = new Vector3(-0.03f, 0.1f, 5);
    public Vector3 p1 = new Vector3(-2, 0.1f, 2.36f);
    public Vector3 p2 = new Vector3(2.47f, 0.1f, 0.46f);
    public Vector3 p3 = new Vector3(-0.03f, 0.1f, -3.03f);
    //最后的位置

    private float t = 0f;
    private int add = 1;

    public bool isDrawGizmosPath = false;
   
    public override void Awake()
    {

        base.Awake();
        _animator = this.gameObject.GetComponent<Animator>();
        shipAttrackType = EnemyAttrackShipType.FarAttack;
        if (gun == null)
        {
            gun = this.gameObject.GetComponentInChildren<GunBase>();
        }
        gun.camp = campEnum.Enemy;
        _target = OutSpaceCameraManager.Instance.Player;
       

    }
    //初始化animator的参数
    public void initAnimator()
    {
        p0 = this.transform.position;//初始曲线位置与随机出生点一致
        reFreshPath(true);
    }
    //激活状态，初始化
    public override void Active()
    {

        base.Active();

    }

    private void OnEnable()
    {

        Active();
        if (_animator)
        {
            SceneLinkedSMB<BezierShipMonoBehaviour>.Initialise(_animator, this);
        }
    }

    public void MoveAttrack()
    {
        t += Time.deltaTime * this.currSpeed * add;


        Vector3 point = GetBezierPoint(p0, p1, p2, p3, t);
        Vector3 direction = point - this.transform.position;
        this.transform.forward = direction;
        //   this.transform.position = Vector3.MoveTowards(currPos, point, this.currSpeed * Time.deltaTime);
        this.transform.position = Vector3.Lerp(this.transform.position, point, Time.deltaTime);
        if (t > 1f)//起始点向尾部运动
        {
            t = 1f;
            add = -add;//反向
            reFreshPath(false);
        }
        else if (t < 0)//尾部向起始点运动
        {
            t = 0;
            add = -add;//反向
            reFreshPath(true);
        }

    }
    void OnDrawGizmos()
    {
        if (!isDrawGizmosPath)
        {
            return;
        }
        Gizmos.color = Color.white;
        for (float t = 0; t <= 1; t += 0.05f)
        {
            Vector3 point = GetBezierPoint(p0, p1, p2, p3, t);
            Gizmos.DrawSphere(point, 0.04f);
        }
    }
    Vector3 GetBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0; //first term
        p += 3 * uu * t * p1; //second term
        p += 3 * u * tt * p2; //third term
        p += ttt * p3; //fourth term

        return p;
    }
    //isHead true起始点开始运动  false 尾部返回运动
    private void reFreshPath(bool isHead)
    {
        Vector3 lastPos = isHead?p0: p3;
        float currX = lastPos.x;
        Vector3 initPos = target.position;
        float Zmax = Random.Range(4,5);
        //x:-5@5   y:-0.1@0.1 z:-3@5
        float yPos = Random.Range(-0.15f, 0.15f);
        if (yPos < 0.015f&& yPos>0)
        {
            yPos = 0.02f;
        }else if(yPos>-0.015f&& yPos < 0)
        {
            yPos = -0.02f;
        }


        float xP0 = Random.Range(currX-1, currX+1);
        xP0=Mathf.Clamp(xP0, -7, 7);
         p0 = new Vector3(xP0, yPos, Zmax) + initPos;

        float xP1 = Random.Range(-2.5f, 2.5f);
        p1 = new Vector3(xP1, yPos, Zmax/2) +initPos;

        p2 = new Vector3(-xP1, yPos, -Zmax / 2)+ initPos;

        p3 = new Vector3(-xP0, yPos, -Zmax)+ initPos;
    }


    //停止动画
    public void Stop()
    {
        _tailTransform.gameObject.SetActive(false);
    }
    //移动动画
    public void Move()
    {
        _tailTransform.gameObject.SetActive(true);
    }
  








    //更新与玩家的距离旁段时否攻击
    public float UpdateToPlayerDistance()
    {
        float disToPlayer = Vector3.Distance(this.transform.position, target.position);
        return disToPlayer;
    }


    public override void Dead()
    {
        base.Dead();
    }
    /// <summary>
    /// 果你只是想简单地检测两个物体是否发生了碰撞，可以使用 Collider 组件的 bounds.Intersects 方法。
    /// 该方法可以检测两个 Collider 组件是否相交，如果相交则返回 true
    //if (object1.GetComponent&lt;Collider&gt;().bounds.Intersects(object2.GetComponent&lt;Collider&gt;().bounds))
    /// </summary>
    /// <param name="other"></param>
    //public override void OnTriggerEnter(Collider other)
    //{

    //    if (other.tag == OutSpaceTags.player)
    //    {
    //        ShipBase ship = other.gameObject.GetComponent<ShipBase>();
    //        if (ship != null)
    //        {
    //            ship.Damage(damage, this.gameObject);
    //        }
    //        colliderDead();
    //    }
    //}

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
