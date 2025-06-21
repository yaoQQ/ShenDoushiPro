using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateEnemyWaveByAttath : MonoBehaviour
{
    //屏幕最多敌人数 ==============必须优化
    [Header("出兵点")]
    public GameObject attachTarget;

    [Header("关卡总敌方数")]
    public int totalCreateNum = 50;

    [Header("每次波生成敌方时间")]
    public float waveAttackTime = 15;
    private float delayTime = 0;//多少时间刷新一次敌人波
    public int currLevel = 0;
    [Header("EnemyLevel配置当前关卡敌方批数2222")]
    public List<EnemyLevelWave> enemyWaveList = new List<EnemyLevelWave>(1);


    public delegate void EndCallBackDelegate();
    public EndCallBackDelegate EndCallBackFun;
    // Use this for initialization
    void Start()
    {

        Logger.PrintColor("red", "=========" + Application.targetFrameRate + "=========");
    }

    IEnumerator showWaveEnemy()
    {
        if (MonsterManager.Instance.currEnemyCount >= totalCreateNum)
        {
            Logger.PrintColor("blue", "=========( MonsterManager.Instance.currEnemyCount>" + totalCreateNum + " return" + "=========");

            yield break;
        }


        for (int j = 0; j < enemyWaveList.Count; j++)
        {
            yield return new WaitForSeconds(enemyWaveList[j].showTime);//刷新小波等待时间
            Logger.PrintColor("yellow", "第[" + j + "]批=========刷新一次敌人=====");
            ShowSmallWave(enemyWaveList[j]);
        }
    }
    private void ShowSmallWave(EnemyLevelWave enemyWave)
    {
        //生成敌人列表的总批次
        if (enemyWave.waveTotalCreate <= 0)
        {
            return;
        }
        List<GameObject> enemyList = enemyWave.enemyList;
        int createNum = enemyWave.createListNum;//一次生成敌人多少批数

        for (int j = 0; j < createNum; j++)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] == null)
                    continue;
                EnemyShipBase enemyBase= enemyList[i].GetComponent<EnemyShipBase>();
                MonsterManager.Instance.CreateEnemey(enemyBase);
             
            }
        }
        enemyWave.waveTotalCreate--;


    }

    public void Update()
    {
        delayTime -= Time.deltaTime;
        if (delayTime > 0)
            return;
        StartCoroutine(showWaveEnemy());
        delayTime = waveAttackTime;
    }
   
}