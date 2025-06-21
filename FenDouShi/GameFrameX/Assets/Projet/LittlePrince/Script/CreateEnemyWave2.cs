using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;

[Serializable]
public struct EnemyLevel2
{
    [Header("敌人出现时间")]
    public float showTime;
    [Header("敌人列表")]
    public List<GameObject> enemyList;
}
public class CreateEnemyWave2 : MonoBehaviour
{
    public static int totalEnemyNum = 10;
    public int curentWave = 0;
    [Header("延迟等待波数时间")]
    public float delayTime = 0;
    public int totalCreateNum = 15;

   
    [Header("EnemyLevel配置当前关卡敌方批数2222")]
    public List<EnemyLevelWave2> enemyWaveList2 = new List<EnemyLevelWave2>(1);
    [Header("EnemyLevel配置当前关卡敌方批数")]
    public List<EnemyLevel2> enemyWaveList = new List<EnemyLevel2>(1);

    public float time;
    public int currLevel=0;

    private float randomPos = 0.6f;
    private bool isInit = false;

    public delegate void EndCallBackDelegate();
    public EndCallBackDelegate EndCallBackFun;
    // Use this for initialization
    void Start()
    {

    }

    private void showWaveEnemy()
    {
        //if(MonsterManager.Instance.currEnemyCount>= CreateEnemyWave.totalEnemyNum)
        //{
        //    return;
        //}
        if (monsterList.Count >= 4)
        {
            return;
        }
        if (enemyWaveList2.Count <= 0)
        {
            return;
        }

        if (!isInit)
        {
           // time = enemyWaveList[0].showTime;
            time = enemyWaveList2[0].showTime;
            isInit = true;
            return;
        }
     //   List<GameObject> enemyList = enemyWaveList[0].enemyList;
        List<GameObject> enemyList = enemyWaveList2[0].enemyList;
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] == null)
                continue;
            //   MonsterManager.Instance.createrDoolMonster(enemyList[i]); 
            createrDoolMonster(enemyList[i]);
        }
        enemyWaveList2.RemoveAt(0);
        currLevel++;
        isInit = false;
        if (enemyWaveList2.Count==0)
        {
            if (EndCallBackFun != null)
            {
                EndCallBackFun();
            }
          //  GameObject.Destroy(this.gameObject);
        }
    }


    public void Update()
    {
        delayTime -= Time.deltaTime;
        if (delayTime > 0)
            return;
        time -= Time.deltaTime;
        if (time > 0)
            return;
        showWaveEnemy();
    }

    private int addCount = 0;
    private List<GameObject> monsterList = new List<GameObject>();
    private Dictionary<string, GameObject> PrefabDic = new Dictionary<string, GameObject>();//特效对象池
    public GameObject createrDoolMonster(GameObject MonsterClone)
    {
        if (MonsterClone == null)
        {
            return null;
        }

        addCount++;
        //GameObject monster = MyUtils.LoadModelPrefab(MonsterClone.name);
        GameObject monsterAssert = getPrefabDirect(MonsterClone.name);
        GameObject monster = GameObject.Instantiate(monsterAssert);
        monster.name = MonsterClone.name;

       
        monsterList.Add(monster);


        //生成门
        //EnemyDool enemyDool = getEnemyDool();
        //enemyDool.showDoll(enemy);
        //map.createEnemy(monster);
        return monster;
    }
    private GameObject getPrefabDirect(string addressStr)
    {

        string lable = ProjectControler.OutSpacePro;
        GameObject getObj = null;
        if (PrefabDic.ContainsKey(addressStr))
        {
            getObj = PrefabDic[addressStr];
        }
        else
        {
            AsyncOperationHandle<IList<GameObject>> boundleOper = Addressables.LoadAssetsAsync<GameObject>(new List<string> { addressStr, lable }, null, Addressables.MergeMode.Intersection);
            IList<GameObject> objList = boundleOper.WaitForCompletion();
            if (objList != null && objList.Count > 0)
            {
                getObj = objList[0];
                PrefabDic[addressStr] = getObj;
            }
            else
            {
               
                Debug.LogError("加载错误 packName" + ProjectControler.OutSpacePro + " addressStr=" + addressStr + " objList=" + objList);
            }
        }
        return getObj;
    }
    //private Vector3 RandomBorePos()
    //{

    //    Vector3 initPos = this.transform.position;
    //    float z = initPos.z;
    //    float x = initPos.x;
    //    float y = OutSpaceCameraManager.Instance.Player.position.y + UnityEngine.Random.Range(0, 1); ;
    //    z = initPos.z + UnityEngine.Random.Range(0, 0.6f);
    //    x = initPos.x + UnityEngine.Random.Range(0, randomPos);


    //    return new Vector3(x, y, z);
    //}
}