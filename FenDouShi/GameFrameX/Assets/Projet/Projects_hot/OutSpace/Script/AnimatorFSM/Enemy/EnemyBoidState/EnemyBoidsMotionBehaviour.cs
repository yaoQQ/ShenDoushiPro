using UnityEngine;
using System.Collections.Generic;
using System.Collections;


/// <summary>
/// 群组类 操作群体行为
/// </summary>
public class EnemyBoidsMotionBehaviour : MonoBehaviour
{
    public static readonly int hashIdle = Animator.StringToHash("Idle");
    public static readonly int hashCircleMotion = Animator.StringToHash("CircleMotion");
    public static readonly int hashPowMotion = Animator.StringToHash("PowMotion");
    public static readonly int hashRotateByPos = Animator.StringToHash("AutoRotate");

    public MotionType motionType = MotionType.None;
    public float MotionRadiues = 1;
    private float pow = 1;
    public BoidMotionType currBoidsMotion;
    private bool _isMotionComplete;//是否正在在动画中
    public bool isMotionComplete { get { return _isMotionComplete; } set { _isMotionComplete = value; } }
    public Transform motionParent;//群组容器
    private Transform _target;//领头对象
    public Transform target { get { return _target; } }
    private Animator _animator;

    public int currEnemyNum = 0;
    public List<EnemyShipBase> boidsList;//当前群组的对象集合
    private Vector3 initPos = new Vector3(0, 0, 1.5f);//集群初始位置

    public float delayTime = 2;

    [Header("是否初始化完成")]
    public bool isInitComplete = false;
    public void Awake()
    {
        _animator = this.gameObject.GetComponent<Animator>();


    }
    private void OnEnable()
    {

        //   Active();
        if (_animator)
        {
            SceneLinkedSMB<EnemyBoidsMotionBehaviour>.Initialise(_animator, this);
        }
    }
    //初始化animator的参数
    public void initAnimator()
    {


    }
    public EnemyLevelWave currEnemyLevelWave;
    private System.Action<EnemyLevelWave> completeCallBackFun;
    public void Init(EnemyLevelWave enemyWave,System.Action<EnemyLevelWave> completeCallBack)
    {
        completeCallBackFun = completeCallBack;
        isMotionComplete = false;
        motionParent = this.transform;
        currEnemyLevelWave = enemyWave;
        motionType = enemyWave.motionType;
        int index = Random.Range(1, 20);
        if (index >= 10)
        {
            string AssertName = "RandomType_" + index;
            this.currBoidsMotion = null;
            StartCoroutine(CreateEnemy());
            this.gameObject.name = this.gameObject.name + "%" + AssertName;
        }
        else if (motionType == MotionType.CircleMotion)
        {
            string AssertName = "CircleMotionType" + index;
            ShowMotionType(AssertName, MotionType.CircleMotion);
            this.gameObject.name = this.gameObject.name + "@" + AssertName;
        }
        else if (motionType == MotionType.Pow2)
        {
            string AssertName = "PowMotionType2D" + index;
            ShowMotionType(AssertName, MotionType.Pow2);
            this.gameObject.name = this.gameObject.name + "@" + AssertName;
        }
        else if(motionType== MotionType.AutoRotate)
        {
            string AssertName = "CircleMotionType" + index;
            ShowMotionType(AssertName, MotionType.CircleMotion);
            this.gameObject.name = this.gameObject.name + "@AutoRotate@" + AssertName;
        }
    }
    /// <summary>
    /// 是否动画完成
    /// </summary>
    /// <param name="exceptNum">排除的计算数量</param>
    /// <returns></returns>
    public bool IsMonsterCompeletMotion(int exceptNum)
    {
        if (boidsList == null)
        {
            //  Logger.PrintDebug(" IsMonsterCompeletMotion()" + " boidsList == null" );
            return false;
        }
       
        int dataCunt = boidsList.Count - 1;
        int completeNum = 0;
       
        for (int i = dataCunt; i >= 0; i--)
        {
            if (boidsList[i].shipState == AIState.Idle)
            {
                completeNum++;
            }
            else if (boidsList[i].shipState == AIState.Dead)
            {
                boidsList.RemoveAt(i);
                completeNum++;
            }


        }
        CheckDestroy();
        Logger.PrintDebug(" IsMonsterCompeletMotion()" + "dataCunt=" + dataCunt + " completeNum =" + completeNum);
        if (completeNum >= dataCunt - exceptNum)
        {
            return true;
        }
        return false;
    }
    //结束销毁
    public void CheckDestroy()
    {
        currEnemyNum = boidsList.Count;
        if (boidsList.Count <= 0&&isInitComplete)
        {
            if (completeCallBackFun != null)
            {
                completeCallBackFun(currEnemyLevelWave);
            }
            GameObject.Destroy(this.gameObject);
        }
    }

    public void RotateBoids()
    {
        motionParent.Rotate(Vector3.forward, 1f);

    }
    public void TrigetRotateCircleByPosSMF()
    {
        _animator.SetTrigger(hashRotateByPos);
    }
    public void ToAttrick(int num)
    {
        int createNum = 0;
        int boidsNum = boidsList.Count-1;
        Transform enemyContent = MonsterManager.Instance.enemyParent;
        for (int i = boidsNum; i >=0; i--)
        {

            EnemyShipBase shipBase = boidsList[i];
            if (shipBase.shipState != AIState.Dead)
            {
                shipBase.transform.parent = enemyContent;
                shipBase.AttrackState();//转为攻击状态
                boidsList.Remove(shipBase);
                createNum++;
            }
            if (createNum >= num)
            {
                break;
            }

        }
        currEnemyNum = boidsList.Count;

    }

    private void AddBoids(EnemyShipBase enemy)
    {
        boidsList.Add(enemy);
        currEnemyNum = boidsList.Count;
    }
  
    public void ShowMotionType(string motionTypeStr, MotionType motionType)
    {
        BoidsMotionManager.Instance.LoadMotionAssert(motionTypeStr, motionType, (motionAssert) =>
        {
            this.currBoidsMotion = motionAssert;
            StartCoroutine(CreateEnemy());
        });
    }
    IEnumerator CreateEnemy()
    {
        List<GameObject> enemyList = currEnemyLevelWave.enemyList;
        int createNum = currEnemyLevelWave.createListNum;//一次生成敌人多少批数
        int enemyNum = enemyList.Count;
        List<Vector3> motionDataList = GetCurrBoidsData(createNum * enemyNum);
        Vector3 borePos = MyUtils.findPlayeHeadrCircleHenPos(MyUtils.MaxEnemyDistance, 90, false);
        int dataIndex = 0;
        boidsList = new List<EnemyShipBase>();

        int halfInit = createNum * enemyNum / 2;
        for (int j = 0; j < createNum; j++)
        {
            for (int i = 0; i < enemyNum; i++)
            {
                yield return new WaitForSeconds(currEnemyLevelWave.everyEnemyDelayTime);//刷新小波等待时间

                EnemyShipBase enemy = MonsterManager.Instance.ShowMotionMonsterByPos(enemyList[i], borePos, motionDataList[dataIndex], motionParent);
                //  enemyWave.boidsMotionBehaviour.InitBoids
                AddBoids(enemy);
                dataIndex++;
            }
           
        }
        isInitComplete = true;
    }

    /// <summary>
    /// 获得当前动画类型 位置数据集合
    /// </summary>
    /// <param name="enemyCount">数据数量</param>
    /// <returns></returns>
    private List<Vector3> GetCurrBoidsData(int enemyCount)
    {
        BoidMotionType motionAssert = currBoidsMotion;
        List<Vector3> dataList = null;
        if (currBoidsMotion == null)
        {
            // 0.0001
             float turnFraction= Random.Range(0.0001f, 0.1001f);
            dataList = getCircleDataListByTurnFraction(turnFraction, enemyCount);
        }
        else if (motionAssert.motionType == MotionType.CircleMotion)
        {
            dataList = UpdateMotionByCircleAssert(motionAssert, enemyCount);
        }
        else if (motionAssert.motionType == MotionType.Pow2)
        {
            dataList = UpdateMotionByPowAssert(motionAssert, enemyCount);
        }
        return dataList;
        //enemyList[i].GetComponent<EnemyShipBase>().AddPathPos(targetPos);
    }
    /// <summary>
    /// 获得圆的数据集合
    /// </summary>
    /// <param name="motionAssert">圆的配置数据</param>
    /// <param name="enemyCount">数据数量</param>
    /// <returns></returns>
    private List<Vector3> UpdateMotionByCircleAssert(BoidMotionType motionAssert, int enemyCount)
    {
        List<Vector3> dataList = getCircleDataListByTurnFraction(motionAssert.turnFraction, enemyCount);
        return dataList;
    }
    private List<Vector3> getCircleDataListByTurnFraction(float turnFraction, int enemyCount)
    {
        List<Vector3> dataList = new List<Vector3>();
        for (int i = 0; i < enemyCount; i++)
        {
            float t = i / (enemyCount - 1f);//0~1  //距离
            float inclination = Mathf.Acos(1 - 2 * t);
            float angel = 2 * Mathf.PI * turnFraction * i;//角度 每次循环里它会得到圆分割后的度数
            float x = Mathf.Sin(inclination) * Mathf.Cos(angel);
            float y = Mathf.Sin(inclination) * Mathf.Sin(angel);
            float z = Mathf.Cos(inclination);//sin 大喇叭形状
                                             // PlotPoint(i, x, y, color);
            Vector3 fward = new Vector3(x, y, z);
            Vector3 targetPos = this.initPos + fward * MotionRadiues;
            dataList.Add(targetPos);
        }
        return dataList;
    }
    /// <summary>
    /// 获得Pow的数据集合
    /// </summary>
    /// <param name="motionAssert">Pow的配置数据</param>
    /// <param name="enemyCount">数据数量</param>
    /// <returns></returns>
    private List<Vector3> UpdateMotionByPowAssert(BoidMotionType motionAssert, int enemyCount)
    {
        List<Vector3> dataList = new List<Vector3>();
        float pow = this.pow;
        float turnFraction = motionAssert.turnFraction;
        bool isAddZ = motionAssert.isAddZ;
        for (int i = 0; i < enemyCount; i++)
        {
            float dst = Mathf.Pow(i / (enemyCount - 1f), pow);//0~1  //距离
            float angel = 2 * Mathf.PI * turnFraction * i;//角度 每次循环里它会得到圆分割后的度数
            float x = dst * Mathf.Cos(angel);
            float y = dst * Mathf.Sin(angel);
            float z = isAddZ ? (i * 0.05f) : 0;
            Vector3 fward = new Vector3(x, y, z);
            Vector3 targetPos = this.initPos + fward * MotionRadiues;
            dataList.Add(targetPos);
        }
        return dataList;
    }

    public List<Vector3> GetMotionData(int dataCunt, float turnFraction)
    {
        Vector3 initPos = new Vector3(0, 0, 1.5f);
        List<Vector3> dataList = new List<Vector3>();
        // float turnFraction = 0.9801924f;
        for (int i = 0; i < dataCunt; i++)
        {

            float t = i / (dataCunt - 1f);//0~1  //距离
            float inclination = Mathf.Acos(1 - 2 * t);
            float angel = 2 * Mathf.PI * turnFraction * i;//角度 每次循环里它会得到圆分割后的度数


            float x = Mathf.Sin(inclination) * Mathf.Cos(angel);
            float y = Mathf.Sin(inclination) * Mathf.Sin(angel);
            float z = Mathf.Cos(inclination);//sin 大喇叭形状
            // PlotPoint(i, x, y, color);
            Vector3 fward = initPos + new Vector3(x, y, z) * MotionRadiues;
            dataList.Add(fward);
            //  Vector3 pos = initPos + fward * currBoidsMotion.circleRadius;
            //boids[i].transform.position = pos;
            //boids[i].transform.forward = fward;
        }
        return dataList;
    }

    public void TrigetShowIdleSMF()
    {
        _animator.SetTrigger(hashIdle);
    }
    public void TrigetShowCircleSMF()
    {

        _animator.SetTrigger(hashCircleMotion);
    }
    public void TrigetShowPowSMF()
    {
        _animator.SetTrigger(hashPowMotion);
    }
 

    public void OnDestroy()
    {
        currBoidsMotion = null;
        boidsList = null;
        currEnemyLevelWave = null;
        completeCallBackFun = null;
    }
}