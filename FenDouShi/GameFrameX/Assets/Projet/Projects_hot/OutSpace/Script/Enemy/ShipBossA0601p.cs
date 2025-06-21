using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShipBossA0601p : Enemy
{

    public Transform gun;
    public float attachDistance;//攻击距离
    public string missleStr;
    //  public BossCreateEnemy enemyPool1;
    private List<BulletGuidedMissile> missleList;
    private GunBase[] gunList;
    private Enemy[] GunEnemyList;
    public string defendSkill; 
    public override void Awake()
    {
        base.Awake();
        shipAttrackType = EnemyAttrackShipType.FarAttack;
        gun = this.transform.Find("gun");
        gunList = this.transform.GetComponentsInChildren<GunBase>();
        Active();
       
    }
    public override void Active()
    {
        base.Active();
        this.transform.LookAt(OutSpaceCameraManager.Instance.Player);
        gun.gameObject.SetActive(false);
        isAttrack = false;
        ScaleFade scalefade = this.transform.GetComponent<ScaleFade>();
        scalefade.init();
        scalefade.overbackFun = show;
    }
    private void show()
    {
        addDefend();
    }
    private void addDefend()
    {
        if (!string.IsNullOrEmpty(defendSkill))
        {
            GameObject obj = MyUtils.LoadEffectPrefab(defendSkill);
            obj.transform.parent = this.transform;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localEulerAngles = Vector3.zero;
        }
    }

    //protected override void MoveToPos(Vector3 pos)
    //{
    //    targetPos = OutSpaceCameraManager.Instance.Player.position - this.transform.forward;
    //    //与玩家保持距离攻击
    //    if (Vector3.Distance(this.transform.position, OutSpaceCameraManager.Instance.Player.position) <= attachDistance)
    //    {
    //        attackPlayer();
    //        return;
    //    }
    //    base.MoveToPos(pos);
    //}
    //到达目标点 追逐玩家
    protected override void ArrivePos(Vector3 pos)
    {
        gun.gameObject.SetActive(true);
    }

    //攻击玩家 条件和行为
    public override void attackPlayer()
    {
        showMissMotion();
        if (shipAttrackType == EnemyAttrackShipType.FarAttack)
        {
            if (gunList != null)
            {
                for(int i=0;i< gunList.Length; i++)
                {
                  
                    gunList[i].Shoot();
                  //  gunList[i].GetComponent<Enemy>().LookAtPos(OutSpaceCameraManager.Instance.Player.position);
                }

            }

        }
    }

    public override void Dead()
    {
        base.Dead();
    }

    private bool isAttrack = false;
    private void showMissMotion()
    {
        if (isAttrack) return;
        isAttrack = true;
        gun.gameObject.SetActive(true);
        StartCoroutine(InitMissle());
        gun.transform.localPosition = new Vector3(0, 1.2f, 19.57f);
        iTween.MoveFrom(gun.gameObject, iTween.Hash("y",
       -3.29f,
       "time", 2f,
       "islocal", true,
        "oncomplete", "showComplete",
       "oncompletetarget", this.gameObject,
       "oncompleteparams", true,
       "easetype", iTween.EaseType.easeInSine
       ));
    }
    private void showComplete(bool test)
    {
        showMissle();
    }

    private void showMissle()
    {
     
    }
    IEnumerator InitMissle()
    {
        if (string.IsNullOrEmpty(missleStr)) yield return null;
        if (missleList == null)
        {
            missleList = new List<BulletGuidedMissile>();
        }
        int time = 2;
        missleList.Clear();
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                time++;
                BulletGuidedMissile missle = getMissle();
                missle.gameObject.SetActive(true);
                missle.transform.parent = gun.transform;
                missle.transform.localEulerAngles = new Vector3(-26.338f, 0, 0);
                missle.transform.localPosition = new Vector3(4 * (i - 1), 4.44f, -12f * (j - 1));
                missle.targetPos = missle.transform.position + missle.transform.forward * UnityEngine.Random.Range(0.5f, 1.5f);
                missle.pathList = new List<Vector3>() { missle.targetPos, OutSpaceCameraManager.Instance.Player.position};
                missle.stopAttrackTime(time);
                missleList.Add(missle);
            }
        }
        yield return new WaitForSeconds(time+3);
        if (this != null && this.gameObject.activeSelf == true)
        {
            isAttrack = false;
        }
    }
    private BulletGuidedMissile createMissle()
    {
        GameObject obj = MyUtils.LoadModelPrefab(missleStr);
        if (obj == null) return null;
        return obj.GetComponent<BulletGuidedMissile>();
    }
    private BulletGuidedMissile getMissle()
    {
        //for (int i = 0; i < missleList.Count; i++)
        //{
        //    if (missleList[i].gameObject.activeSelf == false)
        //    {
        //        return missleList[i];
        //    }

        //}
        BulletGuidedMissile obj = createMissle();
        return obj;
    }
}

