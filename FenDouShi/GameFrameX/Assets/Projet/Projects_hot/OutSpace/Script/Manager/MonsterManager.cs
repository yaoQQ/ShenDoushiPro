
using System.Collections.Generic;
using UnityEngine;

public enum GameTypeOutSpace
{
    MusicHeadRun,
    MusicCircle,

    None
}
public class MonsterManager : Singleton<MonsterManager>
{

    private List<EnemyShipBase> monsterList = new List<EnemyShipBase>();
    private Map _map;
    private OutSpaceRadar _radar;
    private Transform _enemyParent;
    public Transform enemyParent { get { return _enemyParent; } }

    public void Init()
    {
        _enemyParent = new GameObject("enemyParent").transform;
    }
    public int currEnemyCount
    {
        get
        {
            return monsterList.Count;
        }
    }
    public EnemyShipBase ShowMotionMonsterByPos(GameObject MonsterClone, Vector3 borePos, Vector3 targetPos,Transform parentT)
    {
        if (MonsterClone == null)
        {
            return null;
        }

        GameObject monster = ResourceManagerPool.Instance.GetPoolObject(MonsterClone.name, ResourceType.ship);
        EnemyShipBase enemy = monster.GetComponent<EnemyShipBase>();
        monster.transform.parent = parentT;
        monster.transform.position = borePos;
        monster.name = MonsterClone.name;
        enemy.targetPos = borePos;
        enemy.AddPathPos(new List<Vector3> {targetPos});

        enemy.life = 5;
        monsterList.Add(enemy);
        monster.SetActive(true);

        raDar.createEnemy(enemy);
        return enemy;
    }

    public GameObject CreateEnemey(EnemyShipBase enemyBase)
    {
        if (enemyBase == null)
        {
            Debug.LogError("CreateEnemey() prefab enemyBase.name【"+ enemyBase.gameObject.name+"】 == null");
            return null;
        }

        GameObject monster = ResourceManagerPool.Instance.GetPoolObject(enemyBase.gameObject.name, ResourceType.ship);

        if (monster == null)
        {
            Debug.LogError("error ResourceManagerPool.Instance.GetPoolObject=" + enemyBase.gameObject.name);
            return null;

        }
        if (GunManager.Ships != null)
        {//默认子弹父类
            monster.transform.parent = GunManager.Ships.transform;
        }
        EnemyShipBase enemy = monster.GetComponent<EnemyShipBase>();
        float angel = Random.Range(45, 135);
        Vector3 borePos = MyUtils.findPlayeHeadrCircleHenPos(MyUtils.MaxEnemyDistance / 2, angel, false);
        Vector3 targetPos = OutSpaceCameraManager.Instance.Player.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);
        monster.transform.position = borePos;
        monster.name = monster.name;
        enemy.AddPathPos(new List<Vector3> { borePos, targetPos, targetPos - 2 * Vector3.forward });
        enemy.targetPos = targetPos;
        monsterList.Add(enemy);
        monster.SetActive(true);

        Debug.Log("raDar=" + raDar + " enemy=" + enemy);
        raDar.createEnemy(enemy);
        return monster;
    }
    private GameObject CreateAsteriod(GameObject MonsterClone)
    {
        if (MonsterClone == null)
        {
            return null;
        }

        GameObject monster = ResourceManagerPool.Instance.GetPoolObject(MonsterClone.name, ResourceType.ship);
        if (monster == null)
        {
            return null;
        }
        EnemyShipBase enemy = monster.GetComponent<EnemyShipBase>();
        float angel = Random.Range(45, 135);
        Vector3 borePos = MyUtils.findPlayeHeadrCircleHenPos(MyUtils.MaxEnemyDistance, angel, false);
        Vector3 targetPos = OutSpaceCameraManager.Instance.Player.position+new Vector3(Random.Range(-0.5f,0.5f),0,0);
        monster.transform.position = borePos;
        monster.name = MonsterClone.name;
        enemy.AddPathPos(new List<Vector3> { borePos, targetPos, targetPos - 2 * Vector3.forward });
        enemy.targetPos = targetPos;
        monsterList.Add(enemy);
        monster.SetActive(true);

        raDar.createEnemy(enemy);
        return monster;
    }
    //直接产生敌方
 

    //直接产生敌方
    public GameObject createrMonsterByPos(GameObject MonsterClone, Vector3 borePos, Vector3 targetPos)
    {
        if (MonsterClone == null)
        {
            return null;
        }

        GameObject monster = ResourceManagerPool.Instance.GetPoolObject(MonsterClone.name, ResourceType.ship);
        EnemyShipBase enemy = monster.GetComponent<EnemyShipBase>();
        monster.transform.position = borePos;
        monster.name = monster.name;
        enemy.AddPathPos(new List<Vector3> { borePos,targetPos, OutSpaceCameraManager.Instance.Player.position });
        enemy.targetPos = targetPos;
        enemy.life = 5;
        monsterList.Add(enemy);
        monster.SetActive(true);

        raDar.createEnemy(enemy);
        return monster;
    }

    //生成传送门
    public GameObject createrDoolMonster(GameObject MonsterClone)
    {
        if (MonsterClone == null)
        {
            return null;
        }

        GameObject monster = MyUtils.LoadModelPrefab(MonsterClone.name);
        //GameObject monster = GameObject.Instantiate(MonsterClone);
     
        monster.name = MonsterClone.name;
        EnemyShipBase enemy = monster.GetComponent<EnemyShipBase>();
        monsterList.Add(enemy);


        //生成门
        EnemyDool enemyDool = getEnemyDool();
        enemyDool.showDoll(enemy);
       // map.createEnemy(monster);
        raDar.createEnemy(enemy);
        return monster;
    }
  
    public void destoryAll()
    {
        int count = monsterList.Count - 1;
        for (int i = count; i >=0; i--)
        {
            if (monsterList[i] == null)
                continue;
            monsterList[i].Dead();
        }
        monsterList.Clear();
        NoticeManager.Instance.Dispatch(OutSpaceNotice.UpdateWaveMonsterCount);
    }

    public OutSpaceRadar raDar
    {
        get
        {
            if (_radar == null)
            {
                GameObject obj = GameObject.FindGameObjectWithTag(OutSpaceTags.Radar);
                Debug.Log(" raDar  obj=" + obj);
                if (obj != null)
                    _radar = obj.GetComponent<OutSpaceRadar>();
            }
            return _radar;
        }
    }
    public void addScore(int score)
    {
        //map.addScore(score);
    }
    public void destoy(GameObject obj)
    {
       // Logger.PrintColor("red", "GameObject=====begain destroy.count====" + monsterList.Count);
        EnemyShipBase shipBase= obj.GetComponent<EnemyShipBase>();
        if (shipBase == null)
        {
            Logger.PrintError(obj.gameObject.name+":错误 没有EnemyShipBase");
            return;
        }
        monsterList.Remove(shipBase);
        raDar.destroy(obj);
        NoticeManager.Instance.Dispatch(OutSpaceNotice.UpdateWaveMonsterCount);
    }
    public void destoy(EnemyShipBase obj)
    {
        monsterList.Remove(obj);
        raDar.destroy(obj.gameObject);
        NoticeManager.Instance.Dispatch(OutSpaceNotice.UpdateWaveMonsterCount);
    }

    public List<EnemyShipBase> getMonsterList
    {
        get
        {
            return monsterList;
        }
    }

    public EnemyShipBase SortDistance(DistanceComparer m_distanceComparer)
    {
      
        if (this.monsterList.Count > 0)
        {
            this.monsterList.Sort(m_distanceComparer);
            if (this.monsterList[0] == null)
            {
                return null;
            }
            if (!this.monsterList[0].gameObject.activeSelf)
            {
                Logger.PrintError("获取最近敌人为错误！");
            }
            return this.monsterList[0];
        }
        return null;
    }

    //从缓存的无用子弹中获取子弹
    private EnemyDool getEnemyDool()
    {
       GameObject obj= ResourceManagerPool.Instance.GetPoolObject("blackHole", ResourceType.effect, true);
        return obj.GetComponent<EnemyDool>();
    }
    //创建子弹
    private EnemyDool createEnemyDool()
    {

        GameObject cloneEnemyDool = MyUtils.LoadEffectPrefab("blackHole", false);
        cloneEnemyDool.name = cloneEnemyDool.name;
        EnemyDool obj = cloneEnemyDool.GetComponent<EnemyDool>();
        obj.transform.localScale = Vector3.one;
        obj.gameObject.SetActive(false);
        return obj;
    }

    public void Clear()
    {
        monsterList.Clear();
    }
} 

