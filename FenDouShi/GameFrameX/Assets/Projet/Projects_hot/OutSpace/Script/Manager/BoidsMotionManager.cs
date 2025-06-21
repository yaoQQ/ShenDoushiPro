using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

public class BoidsMotionManager :Singleton<BoidsMotionManager>
{
  
   // public BoidSettings settings;
    public Transform targetFllow;



    [Header("是否改变图形")]
    public bool isShow = false;

    public int CircleRadius = 2;

    [Header("图形最远距离")]
    public float motionMaxDistance = 8;
    public float MotionRadiues = 1;

    private Transform _normalEnemyParent;
    public Transform normalEnemyParent { get { return _normalEnemyParent; } }

    public void InitFun()
    {
        _normalEnemyParent = new GameObject("NormalEnemyContent").transform;

    }
  
   
    public void LoadMotionAssert(string AssertName, MotionType motionType,Action<BoidMotionType> loadCompeletFun=null)
    {
        OutSpaceResourceManager.Instance.GetBoidsMotionAssert(AssertName, (motionAssert) => {
            if (loadCompeletFun != null)
            {
                loadCompeletFun(motionAssert);
            }
        });
    }


    //private void showCircleMotion()
    //{
    //    //if (!currBoidsMotion.isShowTurnFractionMotion)
    //    //{
    //    //    return;
    //    //}
    //    //  Vector3 initPos = this.transform.position;
    //    Vector3 initPos = Vector3.zero;
    //    int numPoints = boids.Count;
    //    if (currBoidsMotion.isShowTurnFractionMotion)
    //    {
    //        Debug.Log("currBoidsMotion.turnFractionEnd=" + currBoidsMotion.turnFractionEnd);

    //        if (currBoidsMotion.turnFraction > currBoidsMotion.turnFractionEnd)
    //        {
    //            currBoidsMotion.turnFraction = currBoidsMotion.turnFractionEnd;
    //            currBoidsMotion.addOrReduce *= -1;//变相
    //        }
    //        else if (currBoidsMotion.turnFraction < currBoidsMotion.turnFractionStart)
    //        {
    //            currBoidsMotion.turnFraction = currBoidsMotion.turnFractionStart;
    //            currBoidsMotion.addOrReduce *= -1;//变相
    //        }
    //    }
    //    for (int i = 0; i < numPoints; i++)
    //    {

    //        float t = i / (numPoints - 1f);//0~1  //距离
    //        float inclination = Mathf.Acos(1 - 2 * t);
    //        float angel = 2 * Mathf.PI * currBoidsMotion.turnFraction * i;//角度 每次循环里它会得到圆分割后的度数


    //        float x = Mathf.Sin(inclination) * Mathf.Cos(angel);
    //        float y = Mathf.Sin(inclination) * Mathf.Sin(angel);
    //        float z = Mathf.Cos(inclination);//sin 大喇叭形状
    //        // PlotPoint(i, x, y, color);
    //        Vector3 fward = new Vector3(x, y, z) ;

    //        Vector3 pos = initPos + fward * MotionRadues;
    //        boids[i].transform.position = pos;
    //        boids[i].transform.forward = fward;
    //    }

    //    Boidtime += Time.deltaTime;
    //    if (Boidtime > currBoidsMotion.changeGraph)
    //    {
    //        currBoidsMotion.turnFraction += currBoidsMotion.addTurnFractionDelt * currBoidsMotion.addOrReduce;
    //        Boidtime = 0;
    //    }

    //}
    public List<Vector3> GetCircleMotionData(int dataCunt, float turnFraction)
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
            Vector3 fward = initPos + new Vector3(x, y, z) * CircleRadius;
            dataList.Add(fward);
            //  Vector3 pos = initPos + fward * currBoidsMotion.circleRadius;
            //boids[i].transform.position = pos;
            //boids[i].transform.forward = fward;
        }
        return dataList;
    }
    private List<Vector3> GetPowMotionData(int dataCunt, float turnFraction)
    {
        //  Vector3 initPos = this.transform.position;
        Vector3 initPos = new Vector3(0, 0, 1.5f);
        List<Vector3> dataList = new List<Vector3>();
        float pow = 1;
        //if (currBoidsMotion.isShowPowMotion)
        //{
        //    pow += currBoidsMotion.powDelt * currBoidsMotion.addOrReduce;
        //    if (pow > currBoidsMotion.powStart)
        //    {
        //        pow = currBoidsMotion.powStart;
        //        currBoidsMotion.addOrReduce *= -1;//变相
        //    }
        //    else if (pow < currBoidsMotion.powEnd)
        //    {
        //        pow = currBoidsMotion.powEnd;
        //        currBoidsMotion.addOrReduce *= -1;//变相
        //    }
        //}
        //int numPoints = boids.Count;

        for (int i = 0; i < dataCunt; i++)
        {
            if (i == 0)
            {
                continue;
            }
            float dst = Mathf.Pow(i / (dataCunt - 1f), pow);//0~1  //距离
            float angel = 2 * Mathf.PI * turnFraction * i;//角度 每次循环里它会得到圆分割后的度数

            // Debug.LogFormat("angel={0} dst={1}", angel, dst);
            float x = dst * Mathf.Cos(angel);
            float y = dst * Mathf.Sin(angel);
            float z = 0;
            // PlotPoint(i, x, y, color);
          //  float zPos = currBoidsMotion.isAddZ ? (i * 0.05f) : 0;
            //  Debug.LogFormat("x={0} y={1}" ,x,y);

            Vector3 fward = new Vector3(x, y, z);
            Vector3 targetPos = initPos + fward * CircleRadius;
            // boids[i].UpdateMotionType(initPos, fward, currBoidsMotion);
            dataList.Add(targetPos);
        }

        //if (!isShow)
        //{
        //    return;
        //}
        //Boidtime += Time.deltaTime;
        //if (Boidtime > currBoidsMotion.changeGraph)
        //{
        //    currBoidsMotion.turnFraction += currBoidsMotion.addTurnFractionDelt * currBoidsMotion.addOrReduce;
        //    Boidtime = 0;
        //}
        return dataList;
    }

    //Test GUI
    public void ShowMotionType(string motionTypeStr, MotionType motionType)
    {
        LoadMotionAssert(motionTypeStr, motionType, (motionAssert) =>
        {
         //   currBoidsMotion = motionAssert;
            Vector3 initPos = new Vector3(0, 0, 1.5f);
            SetMonstersPosByMotionAssert(motionAssert, initPos);
        });
    }
    //Test GUI
    //public void UpdateCurrMotion()
    //{
    //    currBoidsMotion.turnFraction += currBoidsMotion.addTurnFractionDelt;
    //    Vector3 initPos = new Vector3(0, 0, 1.5f);
    //    SetMonstersPosByMotionAssert(currBoidsMotion, initPos);
    //}

    /// <summary>
    /// 设置当前屏幕所有敌人布阵
    /// </summary>
    /// <param name="motionAssert">配置</param>
    /// <param name="initPos">初始位置</param>
    public void SetMonstersPosByMotionAssert(BoidMotionType motionAssert,Vector3 initPos)
    {
        if (motionAssert.motionType == MotionType.CircleMotion)
        {
            UpdateMotionByCircleAssert(motionAssert, initPos);
        }else if(motionAssert.motionType == MotionType.CircleMotion)
        {
            UpdateMotionByPowAssert(motionAssert, initPos);
        }
       
    }
    private void UpdateMotionByCircleAssert(BoidMotionType motionAssert, Vector3 initPos)
    {
        List<EnemyShipBase> enemyList = MonsterManager.Instance.getMonsterList;

        int dataCunt = enemyList.Count;
        for (int i = 0; i < dataCunt; i++)
        {
            if (enemyList[i] == null || !enemyList[i].gameObject.activeSelf)
            {
                continue;
            }
            float t = i / (dataCunt - 1f);//0~1  //距离
            float inclination = Mathf.Acos(1 - 2 * t);
            float angel = 2 * Mathf.PI * motionAssert.turnFraction * i;//角度 每次循环里它会得到圆分割后的度数


            float x = Mathf.Sin(inclination) * Mathf.Cos(angel);
            float y = Mathf.Sin(inclination) * Mathf.Sin(angel);
            float z = Mathf.Cos(inclination);//sin 大喇叭形状
            // PlotPoint(i, x, y, color);
            Vector3 fward = initPos + new Vector3(x, y, z) * MotionRadiues;
            enemyList[i].AddPathPos(fward);
        }
    }
    private void UpdateMotionByPowAssert(BoidMotionType motionAssert, Vector3 initPos)
    {
        List<EnemyShipBase> enemyList = MonsterManager.Instance.getMonsterList;
        int dataCunt = enemyList.Count;
        float pow = 1;
        for (int i = 0; i < dataCunt; i++)
        {
            if (i == 0)
            {
                continue;
            }
            float dst = Mathf.Pow(i / (dataCunt - 1f), pow);//0~1  //距离
            float angel = 2 * Mathf.PI * motionAssert.turnFraction * i;//角度 每次循环里它会得到圆分割后的度数
            float x = dst * Mathf.Cos(angel);
            float y = dst * Mathf.Sin(angel);
            float z = motionAssert.isAddZ ? (i * 0.05f) : 0;

            Vector3 fward = new Vector3(x, y, z);
            Vector3 targetPos = initPos + fward * CircleRadius;

            enemyList[i].AddPathPos(targetPos);
        }
    }
    //[Header("改变中心点")]
    //public float pow = 1;
    ////-0.18
    //private void showPow2()
    //{
    //    //  Vector3 initPos = this.transform.position;
    //    Vector3 initPos = Vector3.zero;
    //    if (currBoidsMotion.isShowPowMotion)
    //    {
    //        pow += currBoidsMotion.powDelt * currBoidsMotion.addOrReduce;
    //        if (pow > currBoidsMotion.powStart)
    //        {
    //            pow = currBoidsMotion.powStart;
    //            currBoidsMotion.addOrReduce *= -1;//变相
    //        }
    //           else if (pow < currBoidsMotion.powEnd)
    //        {
    //            pow = currBoidsMotion.powEnd;
    //            currBoidsMotion.addOrReduce *= -1;//变相
    //        }
    //    }
    //    int numPoints = boids.Count;

    //    for (int i = 0; i < numPoints; i++)
    //    {
    //        if (i == 0)
    //        {
    //            continue;
    //        }
    //        float dst = Mathf.Pow(i / (numPoints - 1f), pow);//0~1  //距离
    //        float angel = 2 * Mathf.PI * currBoidsMotion.turnFraction * i;//角度 每次循环里它会得到圆分割后的度数

    //        // Debug.LogFormat("angel={0} dst={1}", angel, dst);
    //        float x = dst * Mathf.Cos(angel);
    //        float y = dst * Mathf.Sin(angel);

    //        // PlotPoint(i, x, y, color);
    //        float zPos = currBoidsMotion.isAddZ ? (i * 0.05f) : 0;
    //        //  Debug.LogFormat("x={0} y={1}" ,x,y);

    //        Vector3 fward = new Vector3(x, y, zPos);
    //        Vector3 targetPos = initPos + fward;
    //        // boids[i].UpdateMotionType(initPos, fward, currBoidsMotion);
    //        boids[i].AddPathPos(targetPos);
    //    }

    //    if (!isShow)
    //    {
    //        return;
    //    }
    //    Boidtime += Time.deltaTime;
    //    if (Boidtime > currBoidsMotion.changeGraph)
    //    {
    //        currBoidsMotion.turnFraction += currBoidsMotion.addTurnFractionDelt * currBoidsMotion.addOrReduce;
    //        Boidtime = 0;
    //    }
    //}

    
}