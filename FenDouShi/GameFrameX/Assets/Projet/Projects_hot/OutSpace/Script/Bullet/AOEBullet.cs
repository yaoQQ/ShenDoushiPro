using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AOEBullet : Bullet
{
    public float exploreTime = 1;

    public void Start()
    {
        timeLife = exploreTime;
    }
    public override void Active()
    {
        base.Active();
        timeLife = exploreTime;
    }
    public override void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * ButtleSpeed);
        timeLife -= Time.deltaTime;
        if (timeLife <= 0)
        {

            ShowHit();
        }
    }
    protected override void showHitEffect()
    {
        base.showHitEffect();
        Explosion();
    }

    public void Explosion()
    {
        List<GameObject> enemys = MyUtils.CalculateEnemiesByDistance(MonsterManager.Instance.getMonsterList, gunInfo.getAttriShootDistance, this.transform.position);
        float damageNum = gunInfo.getAttriButtleDamage;
        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i] == null)
            {
                continue;
            }
            ColliderItem enemy = enemys[i].GetComponent<ColliderItem>();
            enemy.Damage(damageNum);
        }
    }
    ///// <summary>
    ///// 重新激活子弹
    ///// </summary>
    //public override void ReAcvtive()
    //{
    //    timeLife = exploreTime;
    //    this.gameObject.SetActive(true);

    //}
}

