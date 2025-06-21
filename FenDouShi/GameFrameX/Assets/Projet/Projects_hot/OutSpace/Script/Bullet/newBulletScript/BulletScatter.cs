using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScatter : Bullet
{
    // public GameObject enemyTarget;
    // Use this for initialization
    public float exploreTime = 1;
    public bool isShowEffect = true;
    public float life = 5;


    //对象池重新激活
    public override void Active()
    {
        base.Active();
        exploreTime = 1;
        timeLife = exploreTime;
        isShowEffect = true;
       // Logger.PrintColor("yellow", "BulletScatter  Active()");
    }
 
    public override void Update()
    {
       
        transform.Translate(Vector3.forward * Time.deltaTime * ButtleSpeed);
        timeLife -= Time.deltaTime;
        if (timeLife <= 0)
        {
           
            showSmallEffect();
            DisAcvtive();
            isShowEffect = false;
        }
    }
    ///// <summary>
    ///// 开始加载的重新激活子弹
    ///// </summary>
    //public override void ReAcvtive()
    //{
    //    exploreTime = 1;
    //    timeLife = exploreTime;
    //    isShowEffect = true;
    //    this.gameObject.SetActive(true);

    //}
    public override void DisAcvtive()
    {
        base.DisAcvtive();

    }
    private void showSmallEffect()
    {
        if (!isShowEffect)
        {
            return;
        }
        for (int i = 0; i <10; i++)
        {
            GameObject go = MyUtils.LoadBulletPrefab(this.gameObject.name);
            //  GameObject go = MyUtils.Instantiate(this.gameObject) as GameObject;
            go.layer = this.gameObject.layer;
            go.transform.position = this.transform.position;
            go.transform.rotation = this.transform.rotation * Quaternion.Euler(Random.Range(-10, 10), Random.Range(-10, 10), 0);
            BulletScatter smallBullet = go.GetComponent<BulletScatter>();
            smallBullet.Active();
            go.transform.localScale = Vector3.one * 0.1f;
            smallBullet.exploreTime = 4f;
            smallBullet.timeLife = smallBullet.exploreTime;
            smallBullet.camp = campEnum.Enemy;

            smallBullet.isShowEffect = false;
        }
    }
}
