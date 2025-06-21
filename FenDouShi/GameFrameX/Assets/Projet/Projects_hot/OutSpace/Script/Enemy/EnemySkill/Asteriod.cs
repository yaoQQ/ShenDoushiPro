
using UnityEngine;

public class Asteriod : EnemyShipBase
{
    public Vector3 AngularVelocity = Vector3.up;
    public float rotateSpeed = 1;
    public Space RelativeTo = Space.World;
    [Header("准备销毁")]
    public float liveTime = 5f;
    private Transform player;

    [Header("测试：与玩家距离")]
    private float distanceToPlayer = 0;
    public override void Awake()
    {
        base.Awake();
        player = OutSpaceCameraManager.Instance.Player;

    }
    //对象池重新获取激活调用
    public override void Active()
    {
        base.Active();
        this.gameObject.SetActive(true);
        rotateSpeed = Random.Range(20, 50)*this.speed;
        this.currSpeed = Random.Range(0.5f, 1) * this.speed;
        this.transform.localScale = new Vector3(Random.Range(0.1f, 0.4f), Random.Range(0.1f, 0.4f), Random.Range(0.1f, 0.4f));
        liveTime = 5;
        
    }
 

    public  void Update()
    {

        transform.Rotate(AngularVelocity * Time.deltaTime * rotateSpeed, RelativeTo);
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.targetPos, this.currSpeed * Time.deltaTime);
        float distance = Vector3.Distance(this.transform.position, this.targetPos);
        distanceToPlayer = Vector3.Distance(this.transform.position, player.position);
        if (distance < 0.01f)
        {
            // Debug.Log("arrvie arriveDis=" + arriveDis);
            // Debug.Log("arrvie currDistance=" + distance);

            bool isMove = this.getNextPos();
        }
        else if (distanceToPlayer >= MyUtils.MaxEnemyDistance)
        {
            liveTime -= Time.deltaTime;
            if (liveTime < 0)
            {
                base.Dead();
            }
        }
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer==MyUtils.playerLayer)
        {
            //  Debug.Log(Time.time+"ShipA0203p OnTriggerEnter=" + damage);

            ShipBase ship = other.gameObject.GetComponent<ShipBase>();
            if (ship != null)
            {
                ship.Damage(damage, this.gameObject);
                
            }
            
            colliderDead();
            //Logger.PrintColor("yellow", "陨石=" + this.gameObject.name + "碰撞 other= " + other.name);
        }
        if (life <= 0)
        {
            colliderDead();
        }
        
    }

    public void colliderDead()
    {
        this.gameObject.SetActive(false);
        if (!string.IsNullOrEmpty(DestroyEffect.destroyEffect))
        {
            GameObject obj = MyUtils.LoadEffectPrefab(DestroyEffect.destroyEffect, false);
            if (obj != null)
            {
                obj.transform.position = this.transform.position;
            }
        }
        base.Dead();
    }
}

