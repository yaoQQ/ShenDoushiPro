
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UVAMonoBehaviour : EnemyShipBase
{
    public static readonly int hashIdle = Animator.StringToHash("Idle");
    public static readonly int hashOnGuard = Animator.StringToHash("OnGuard");
    public static readonly int hashAttrack = Animator.StringToHash("Attrack");


    [SerializeField]
    private Transform _target;
    public Transform target { get { return _target; } }
    private Animator _animator;
    public ParticleSystem particleMusic;

    [Header("无人机攻击距离限制")]
    public float m_limitDistance = 4;
    [Header("无人机环绕的半径")]
    public float AroundRadius;
    public Vector3 m_targetPoint;
    private Transform Owner;
    public bool isRandomPos = false;
    /// <summary>
    /// 环绕的半径
    /// </summary>
    public float m_moveArround = 1;
    /// <summary>
    /// 生命时间(经过多少秒后返回到拥有者)
    /// < 0：无限时间
    /// </summary>
    public float lifeTime = -1;

    /// <summary>
    /// 坏绕平面的法向量
    /// </summary>
    private Vector3 m_pNormal;
    /// <summary>
    /// 移动参考点
    /// </summary>
    private Vector3 m_refPoint;
    //public Vector3 velocity;
    private DistanceComparer m_distanceComparer;

    [SerializeField]
    private List<NormalGun> UVAGunBase;

    public GunInfo gunInfo;

    public override void Awake()
    {
        base.Awake();
        _animator = this.gameObject.GetComponent<Animator>();
        // _target = OutSpaceCameraManager.Instance.Player;
        _target = null;
         Owner = OutSpaceCameraManager.Instance.Player;
        m_distanceComparer = new DistanceComparer(this.transform, false);
        NormalGun[] guns = this.transform.GetComponentsInChildren<NormalGun>();
        UVAGunBase = new List<NormalGun>(guns);

    }
    private void OnEnable()
    {
        Active();
        if (_animator)
        {
            SceneLinkedSMB<UVAMonoBehaviour>.Initialise(_animator, this);
        }
       

    }


    //初始化animator的参数
    public void initAnimator()
    {
        _animator.SetBool(hashOnGuard, false);
        _animator.SetTrigger(hashIdle);
    }
    //激活状态，初始化
    public override void Active()
    {

        base.Active();
        lifeTime = 30;
        InitOnGuild();
    }
    public void InitOnGuild()
    {
        this.m_targetPoint = this.Owner.transform.position + Vector3.forward;
        m_randomPNTimer = Random.Range(2.5f, 5.5f);
        m_pNormal = Random.onUnitSphere;
    }



    //停止动画
    public void Stop()
    {
        TailTransform.gameObject.SetActive(false);
    }
    //移动动画
    public void Move()
    {
        _tailTransform.gameObject.SetActive(true);
    }
    public void ShowOnGuard()
    {
        _animator.SetBool(hashOnGuard, true);
    }
    public void DisGuard()
    {
        _animator.SetBool(hashOnGuard, false);
    }
    //移动到目标点

   
    public void reFresh()
    {
        if (isRandomPos)
        {
            m_randomPNTimer -= Time.deltaTime;
            if (m_randomPNTimer < 0)
            {
                // pNormal = (pNormal + Random.onUnitSphere).normalized;
                m_pNormal = Random.onUnitSphere;
                m_randomPNTimer = Random.Range(0.2f, 3.5f);
            }
        }
        //this.lifeTime -= Time.deltaTime;
        //if (this.lifeTime <= 0 && this.Owner != null)
        //{
        //    Debug.Log("<color='yellow'>onGuard11111 uav.lifeTime=" + this.lifeTime + "</color>");
        //    OnGuard(this.Owner.transform.position, 0f, this.speed);
        //}
        //else
        //{
  
            OnGuard(this.m_targetPoint, AroundRadius, this.speed);
       // }
    }
    public void BackToPlayer()
    {
        this._target = null;
        this.m_targetPoint = this.Owner.transform.position + Vector3.forward;
    }
    /// <summary>
    /// 随机平面法向量计时器
    /// </summary>
    private float m_randomPNTimer;
    /// <summary>
    /// 在宿主周边警戒
    /// </summary>
    private void OnGuard( Vector3 point, float radius, float speed)
    {
        // 环绕宿主

        var vector = this.transform.position - point;
        var project = Vector3.ProjectOnPlane(vector, m_pNormal);
        var cross = Vector3.Cross(m_pNormal, project);
      

        m_refPoint = point + Vector3.Lerp(project, cross, 0.5f).normalized * radius;
        var forward = m_refPoint - this.transform.position;
        if (forward != Vector3.zero)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(forward), Time.deltaTime * 3);
        }
        this.transform.position += this.transform.forward * speed * Time.deltaTime;
      //  velocity = this.transform.forward * speed;
    }
  
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, m_pNormal * 2);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(m_refPoint, 1);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.m_targetPoint, 1);
    
    }




    public void ToUpdate()
    {

    }
    public string GetName()
    {
        return "UVAShip";
    }

    public override void Dead()
    {
        base.Dead();
    }
    public Transform getEarestEnemy()
    {
        EnemyShipBase obj = MonsterManager.Instance.SortDistance(m_distanceComparer);
        if (obj != null&& obj.gameObject.activeSelf)
        {
            float dis = Vector3.Distance(obj.transform.position, this.transform.position);
            if (dis < m_limitDistance)
            {
                    this._target = obj.transform;
                    this.m_targetPoint = obj.transform.position;
                    return obj.transform;
              
            }
            //else
            //{
            //    Logger.PrintColor("red", "获得最近的敌人失败  Vector3.Distance="+dis+"  限制!");
            //}
        }
        
        return null;
    }
    public void IsToAttrack()
    {
        Transform enemy = getEarestEnemy();
        if (enemy != null)
        {
            _animator.SetBool(hashAttrack, true);
        }
    }
    public void Attrack()
    {

        if (_target&&_target.gameObject.activeSelf)
        {
            reFresh();
            Shoot();
        }
        else
        {
            Transform enemy = getEarestEnemy();
           if (enemy == null) {
              //  Logger.PrintDebug("enemy == null");
                _animator.SetBool(hashAttrack, false);
            }
            else
            {
                //Logger.PrintDebug("获取成功");
            }
        }
    }

    private GunInfo UVABulletInfo;//获取无人机子弹信息，与无人机信息区别加载
    public void UpdateByGunInfo(GunInfo gunInfo)
    {
        this.speed = gunInfo.getAttriButtleSpeed;
        this.attrackDistance = gunInfo.getAttriShootDistance;
        this.lifeTime = gunInfo.getAttriTimeLife;
        for (int i = 0; i < UVAGunBase.Count; i++)
        {
            //现在只更新无人机子弹伤害 根据玩家无人机枪
            //无人机子弹 gunInfo（UVANormalGunInfo_1） 与 玩家无人机枪（UVAGunInfo_1） 不同
            UVAGunBase[i].gunInfo.ButtleDamage = gunInfo.getAttriButtleDamage;
            UVAGunBase[i].UpdateUVABulletByGunInfo();//更新属性
        }


    }
    private void Shoot()
    {
        Vector3 forward = this.transform.forward;
        //敌人位置
        Vector3 _enemyPos = target.transform.position;
        // 敌人到发射器的向量
        Vector3 _enemyToMuzzleNormal = (_enemyPos - this.transform.position).normalized;

        // 发射器与敌人的角度
        float _emitterToEnermyAngle = Vector3.Angle(forward, _enemyToMuzzleNormal);
      //  Logger.PrintColor("blue", "_emitterToEnermyAngle=" + _emitterToEnermyAngle);
        if(_emitterToEnermyAngle <= 120)
        {
            for(int i=0;i< UVAGunBase.Count; i++)
            {
                UVAGunBase[i].transform.LookAt(target);
                UVAGunBase[i].Shoot();
            }
        }
    }

}
