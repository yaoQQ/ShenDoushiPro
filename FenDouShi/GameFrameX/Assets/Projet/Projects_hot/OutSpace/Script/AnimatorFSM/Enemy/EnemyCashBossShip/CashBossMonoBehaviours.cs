
using UnityEngine;
using System.Collections.Generic;

public class CashBossMonoBehaviours : EnemyShipBase
{

    public static readonly int hashIdle = Animator.StringToHash("Idle");
    public static readonly int hashRotate = Animator.StringToHash("Rotate");
    public static readonly int hashAttack = Animator.StringToHash("Attack");
    public static readonly int hashMove = Animator.StringToHash("Move");

    public static readonly int hashDistanceToPos = Animator.StringToHash("distanceToPos");
    public static readonly int hashPathCount = Animator.StringToHash("PathCount");
    public static readonly int hashAngleToPos = Animator.StringToHash("AngleToPos");
    public static readonly int hashRatateToPos = Animator.StringToHash("RatateToPos");
    public static readonly int hashDistanceToPlayer = Animator.StringToHash("DistanceToPlayer");

    public static readonly int hashTargetPos = Animator.StringToHash("targetPos");

  //  public GunBase gun;
    private Transform _target;
    public Transform target { get { return _target; } }
    private Animator _animator;
    private string colliderEffect = "smallHit";

    public BossCreateEnemy bossCreateEnemy;
    public BossCreateEnemy bossCreateEnemy2;

    public ParticleSystem particleMusic;
    public Transform attrackLight;
    public override void Awake()
    {

        base.Awake();
        _animator = this.gameObject.GetComponent<Animator>();
        shipAttrackType = EnemyAttrackShipType.FarAttack;
        _target = OutSpaceCameraManager.Instance.Player;

        bossCreateEnemy.gameObject.SetActive(false);
        bossCreateEnemy2.gameObject.SetActive(false);
        attrackLight.gameObject.SetActive(false);
        

    }
    private void Start()
    {
     
    }

    private void OnEnable()
    {

        Active();
        if (_animator)
        {
            SceneLinkedSMB<CashBossMonoBehaviours>.Initialise(_animator, this);
        }
    }

    //初始化animator的参数
    public void initAnimator()
    {


        _animator.SetInteger(hashPathCount, this.pathList.Count);
        float distance = Vector3.Distance(this.transform.position, this.targetPos);
        _animator.SetFloat(hashDistanceToPos, distance);
        _animator.SetFloat(hashTargetPos, this.targetPos.z);

        LookAtRotate();
        float angel = Quaternion.Angle(this.targetRotation, this.transform.rotation);
        _animator.SetFloat(hashAngleToPos, angel);


        Debug.Log("this.pathList.Count=" + this.pathList.Count);
        Debug.Log("distance=" + distance);
        Debug.Log("targetPos=" + targetPos);
        Debug.Log("angel=" + angel);
    }
    //激活状态，初始化
    public override void Active()
    {

        base.Active();
      //  GetAudioDataManager.Instance.addMusicDoFun(musicDoFun);
    }

    public override void initPaths()
    {
        Debug.Log("pathList=" + pathList);
        Debug.Log("target=" + target);
        pathList.Clear();

        Vector3 iniMovePos = this.transform.position + Vector3.left * Random.Range(-1, 1)+Vector3.up;
        Vector3 iniMovePos2 = this.transform.position + Vector3.right * Random.Range(-1, 1) + Vector3.up;
        this.AddPathPos(new List<Vector3> { iniMovePos, iniMovePos2, OutSpaceCameraManager.Instance.Player.position });

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
    //移动到目标点
    public void MoveToPos()
    {
        if (!isRotateCompelet())
        {
            _animator.SetTrigger(hashRotate);
            return;
        }
        LookAtPos(this.targetPos);
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.targetPos, this.currSpeed * Time.deltaTime);
        float distance = Vector3.Distance(this.transform.position, this.targetPos);
        _animator.SetFloat(hashDistanceToPos, distance);
        float arriveDis = this.currSpeed * Time.deltaTime;
        if (distance < 0.001f)
        {
            // Debug.Log("arrvie arriveDis=" + arriveDis);
            // Debug.Log("arrvie currDistance=" + distance);

            bool isMove = this.getNextPos();
            //  Logger.PrintColor("blue", "getNextPos() isMove=" + isMove);
            _animator.SetInteger(hashPathCount, this.pathList.Count);

            if (!isMove)
            {
                _animator.SetTrigger(hashIdle);
            }
        }
    }

    public bool MoveAttrack()
    {

        this.transform.position = Vector3.MoveTowards(this.transform.position, this.targetPos, this.currSpeed * Time.deltaTime);
        float distance = Vector3.Distance(this.transform.position, this.targetPos);
        _animator.SetFloat(hashDistanceToPos, distance);
        float arriveDis = this.currSpeed * Time.deltaTime;
        if (distance < 0.001f)
        {
            return true;
          
        }
        return false;
    }

    //设置旋转到目标的角度
    public void LookAtRotate()
    {
        this.LookAtPos(this.targetPos);
    }
    //更新旋转到目标位置的角度
    public void UpdateRotate()
    {
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this.targetRotation, this.RotateSpeed * Time.deltaTime);
        if (isRotateCompelet())
        {
            _animator.SetTrigger(hashIdle);
        }
    }
    //是否旋转到目标位置的角度
    public bool isRotateCompelet()
    {
        float angel = Quaternion.Angle(this.targetRotation, this.transform.rotation);
        _animator.SetFloat(hashAngleToPos, angel);
        float dot = Quaternion.Dot(this.transform.rotation, this.targetRotation);
        _animator.SetFloat(hashRatateToPos, dot);
        if (angel <= 1f && angel >= -1f)
        {
            _animator.SetFloat(hashAngleToPos, angel);

            return true;
        }
        return false;
    }

    public void PlayerEscape()
    {
        Logger.PrintColor("red", "攻击状态取消 转为Idle");
        _animator.SetTrigger(CashBossMonoBehaviours.hashIdle);
    }



    //更新与玩家的距离旁段时否攻击
    public float UpdateToPlayerDistance()
    {
        float disToPlayer = Vector3.Distance(this.transform.position, target.position);
        //  Debug.Log("UpdateToPlayerDistance disToPlayer=" + disToPlayer);
        _animator.SetFloat(hashDistanceToPlayer, disToPlayer);
        return disToPlayer;
    }
    public void musicDoFun(float averMusic)
    {
        if (particleMusic)
        {
           
            if (averMusic > 2)
            {
                particleMusic.Emit(50);
            }else if (averMusic>3)
            {
                particleMusic.Emit(100);
            }
            else
            {
                particleMusic.Emit(25);
            }
            if (bossCreateEnemy)
            {
                bossCreateEnemy.ShowEnemy();
            }
        }
    }
     public void ToUpdate()
    {

    }
    public string GetName()
    {
        return "CashBoss";
    }

    public override void Dead()
    {
        //GetAudioDataManager.Instance.removeMusicDoFun(musicDoFun);
        base.Dead();
    }
    public override void OnTriggerEnter(Collider other)
    {

        if (other.tag == OutSpaceTags.player)
        {
            ShipBase ship = other.gameObject.GetComponent<ShipBase>();
            if (ship != null)
            {
                ship.Damage(damage, this.gameObject);
            }
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
