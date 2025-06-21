using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateEnemyWave : MonoBehaviour
{
    [Header("是否当前wave全部完成")]
    public bool isEnd = false;
    //屏幕最多敌人数 ==============必须优化
    [Header("当前波数")]
    public int curentWave = 0;

    [Header("当前第几小波")]
    public int curentWaveIndex = 0;
    //[Header("只读计数：关卡生成的敌方数")]
    //[SerializeField]
    //public static int createEnemyNum = 0;



    [Header("每次波生成敌方时间")]
    public float waveAttackTime = 15;


    [Header("当前波的总时间")]
    public float waveTotalTime = 90;
    public int currLevel = 0;
    [Header("EnemyLevel配置当前关卡敌方批数2222")]
    public List<EnemyLevelWave> enemyWaveList = new List<EnemyLevelWave>(1);



    public delegate void EndCallBackDelegate();
    public EndCallBackDelegate EndCallBackFun;


    void Start()
    {
        isEnd = false;
        Logger.PrintColor("yellow", "=========" + Application.targetFrameRate + "=========");

        StartCoroutine(showWaveEnemy());
    }

    IEnumerator showWaveEnemy()
    {
       
        int enemyListCount = enemyWaveList.Count;
        Logger.PrintColor("yellow", "*****************************showWaveEnemy() enemyListCount=" + enemyListCount);
        for (int j = 0; j < enemyListCount; j++)
        {
            Logger.PrintColor("yellow", "=======wait begian第[" + j + "]批" + "enemyWave=" + enemyWaveList[j].index + " =========刷新一次敌人=====");
            Logger.PrintColor("yellow", "=======wait 第[" + j + "]批 showTime=" + enemyWaveList[j].showTime);
            yield return new WaitForSeconds(enemyWaveList[j].showTime);//刷新小波等待时间
            Logger.PrintColor("yellow", "-------WaitForSeconds end 第[" + j + "]批  enemyWave = "+ enemyWaveList[j].index+"------刷新一次敌人-----");
            CommonView.showTopTips("第[" + j + "]波来了！");
            enemyWaveList[j].index = j;
            if (enemyWaveList[j].motionType == MotionType.None)
            {
                StartCoroutine(ShowSmallWave(enemyWaveList[j]));
            }
            else
            {
                ShowMotionWave(enemyWaveList[j], j);
            }
           
            Logger.PrintColor("yellow", "  ==========第[" + j + "]遍历完成=====");
            curentWaveIndex++;
            if (j == enemyListCount - 1)//是最后一波
            {
                isEnd = true;
                Logger.PrintColor("yellow", "end第[" + j + "]波=========来了！最后一波=====");
                CommonView.showTopTips("end第[" + j + "]波=========来了！最后一波=====");
            }
           

        }
    }
    IEnumerator  ShowSmallWave(EnemyLevelWave enemyWave)
    {
        //生成敌人列表的总批次
        //if (enemyWave.waveTotalCreate <= 0)
        //{
        //    yield break;
        //}
        List<GameObject> enemyList = enemyWave.enemyList;
        int createNum = enemyWave.createListNum;//一次生成敌人多少批数
        int enemyNum = enemyList.Count;
        int enemyTotalCount = createNum * enemyNum;
        //List<Vector3> motionDataList =BoidsMotionManager.Instance.GetCircleMotionData(enemyTotalCount, 0.9801924f);
        Vector3 borePos = MyUtils.findPlayeHeadrCircleHenPos(MyUtils.MaxEnemyDistance, 90, false);
        int dataIndex = 0;
      //  Transform noralEnemyParent = BoidsMotionManager.Instance.normalEnemyParent;
        int waveTotalCreate = enemyWave.waveTotalCreate;
       
        for (int j = 0; j < createNum; j++)
        {
            for (int i = 0; i < enemyNum; i++)
            {
                if (enemyList[i] == null)
                    continue;
                yield return new WaitForSeconds(enemyWave.everyEnemyDelayTime);//刷新小波等待时间
                EnemyShipBase enemyBase = enemyList[i].GetComponent<EnemyShipBase>();
                MonsterManager.Instance.CreateEnemey(enemyBase);
                dataIndex++;
            }
        }
        enemyWave.waveTotalCreate--;
        NoticeManager.Instance.Dispatch(OutSpaceNotice.UpdateWaveMonsterCount);
        enemyWave.isCreate = true;
        Logger.PrintColor("red", "  ==========第[" + enemyWave.index + "]生成完成=====");
    }

    /// <summary>
    /// 生成群组对象
    /// </summary>
    /// <param name="enemyWave"></param>
    /// <param name="index"></param>
    private void ShowMotionWave(EnemyLevelWave enemyWave, int index)
    {
        //生成敌人列表的总批次
        if (enemyWave.waveTotalCreate <= 0)
        {
            return;
        }
        GameObject obj= GameObject.Instantiate(OutSpaceLevel.Instance.boidPrefabe.gameObject);
        obj.name = "boidsMotionWave_" + index;
        EnemyBoidsMotionBehaviour boidsBhv = obj.GetComponent<EnemyBoidsMotionBehaviour>();
       // enemyWave.boidsMotionBehaviour = boidsBhv;
        boidsBhv.Init(enemyWave, WaveCompleteFun);
     
        enemyWave.waveTotalCreate--;
        NoticeManager.Instance.Dispatch(OutSpaceNotice.UpdateWaveMonsterCount);
        enemyWave.isCreate = true;
        Logger.PrintColor("red", "  ==========第[" + enemyWave.index + "]生成完成=====");
    }
    private void WaveCompleteFun(EnemyLevelWave enemyWave)
    {
        //生成完毕
        if (enemyWave.index>= enemyWaveList.Count)
        {
            CommonView.showTopTips("当前关卡结束 开始下一关卡");
            GameObject.Destroy(this.gameObject);
        }
    }

    public void ToUpdate(float deltTime)
    {
        waveTotalTime -= deltTime;
      //  StartCoroutine(showWaveEnemy());
    }


    public void OnDestroy()
    {

        Logger.PrintColor("red", "CreateEnemyWave.OnDestroy()");
        enemyWaveList.Clear();
        enemyWaveList = null;
        EndCallBackFun = null;
    }

}