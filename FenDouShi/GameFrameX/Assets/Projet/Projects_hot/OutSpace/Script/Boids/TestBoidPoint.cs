using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestBoidPoint : MonoBehaviour
{
    [SerializeField]
    [Header("点数量")]
    public int numPoints = 1;
    [Header("点的大小")]
    public float ballRadu = 0.1f;
    [Header("角度 每次循环里它会得到圆分割后的度数 黄金分割点1.618034")]
    public float turnFraction = 0;
    [Header("圆分割后的度数 自动改变量")]
    public float addTurnFractionDelt = 0.0001f;
    [Header("增加或者减少")]
    public int addOrReduce = 1;
    public bool isAddZ = false;

    public Color color = Color.green;

    [Header("是否改变图形")]
    public bool isShow = false;

    Transform catchTransform;

 
    float Boidtime = 0;
    [Header("是否改变图形 时间")]
    public float changeGraph = 2;

    public float deletTimeCount;

   
    [Header("获取点的位置")]
    public int hightLight = 5;
    [Header("获取点的位置 偏移")]
    public int hightOffset = 1;

    public Color defalutColor;
    public Color hightLightColor;
    [Header("改变中心点")]
    public float pow =1;
    [Header("改变中心点 速度增量")]
    public float powDelt = 0.01f;
    [Header("是否打开改变中心点 动画")]
    public bool isShowPowMotion = false;

    public BoidMotionType currBoidsMotion;
    void Start()
    {
        catchTransform = this.transform;
        deletTimeCount = 0;

    }

    private void OnDrawGizmos()
    {
        if (currBoidsMotion == null)
        {
            showCircle();
        }
        else
        {
            UpdateMotionAssert();
        }

           
    }
    private void UpdateMotionAssert()
    {

        //圆
        Vector3 initPos = this.transform.position;
        //deletTime += Time.deltaTime;
        //if (deletTime < refreshTime)
        //{
        //    return;
        //}
        // Debug.Log("numPoints=" + numPoints);
       // float test = (1 + Mathf.Sqrt(5)) / 2;//1.618034
                                             // Debug.Log("test=" + test);
     
        for (int i = 0; i < numPoints; i++)
        {

            float t = i / (numPoints - 1f);//0~1  //距离
            float inclination = Mathf.Acos(1 - 2 * t);
            float angel = 2 * Mathf.PI * currBoidsMotion.turnFraction * i;//角度 每次循环里它会得到圆分割后的度数


            float x = Mathf.Sin(inclination) * Mathf.Cos(angel);
            float y = Mathf.Sin(inclination) * Mathf.Sin(angel);
            float z = Mathf.Cos(inclination);//sin 大喇叭形状
            // PlotPoint(i, x, y, color);
            float zPos = z;
            Vector3 pos = initPos + new Vector3(x, y, zPos)* currBoidsMotion.circleRadius;
            if (i < 3)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(pos, new Vector3(0.05f, 0.05f, 0.05f));

            }
            else
            {
                if ((i + hightOffset) % hightLight == 0)
                {
                    Gizmos.color = hightLightColor;
                }
                else
                {
                    Gizmos.color = defalutColor;
                }
                Gizmos.DrawSphere(pos, ballRadu);
            }

            // Gizmos.DrawWireCube(pos, cubeSize);
            //   Gizmos.DrawWireSphere(pos, 0.1f);
        }
        deletTimeCount = 0;
        if (!isShow)
        {
            return;
        }
        Boidtime += Time.deltaTime;
        if (Boidtime > currBoidsMotion.changeGraph)
        {
            currBoidsMotion.turnFraction += currBoidsMotion.addTurnFractionDelt * currBoidsMotion.addOrReduce;
            Boidtime = 0;
        }
    }
    private void ShowTimeCost(Action fun)
    {
        if (fun != null)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            fun();
            TimeSpan ts2 = sw.Elapsed;
            Debug.LogFormat("Stopwatch总共花费{0}ms.", ts2.TotalMilliseconds);
        }
    }
    private void showCircle()
    {
        //圆
        Vector3 initPos = this.transform.position;
        //deletTime += Time.deltaTime;
        //if (deletTime < refreshTime)
        //{
        //    return;
        //}
        // Debug.Log("numPoints=" + numPoints);
        float test = (1 + Mathf.Sqrt(5)) / 2;//1.618034
                                             // Debug.Log("test=" + test);
        for (int i = 0; i < numPoints; i++)
        {

            float t = i / (numPoints - 1f);//0~1  //距离
            float inclination = Mathf.Acos(1 - 2 * t);
            float angel = 2 * Mathf.PI * turnFraction * i;//角度 每次循环里它会得到圆分割后的度数


            float x = Mathf.Sin(inclination) * Mathf.Cos(angel);
            float y = Mathf.Sin(inclination) * Mathf.Sin(angel);
            float z = Mathf.Cos(inclination);//sin 大喇叭形状
            // PlotPoint(i, x, y, color);
            float zPos = z;
            Vector3 pos = initPos + new Vector3(x, y, zPos);
            if (i < 3)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(pos, new Vector3(0.05f, 0.05f, 0.05f));

            }
            else
            {
                if ((i + hightOffset) % hightLight == 0)
                {
                    Gizmos.color = hightLightColor;
                }
                else
                {
                    Gizmos.color = defalutColor;
                }
                Gizmos.DrawSphere(pos, ballRadu);
            }

            // Gizmos.DrawWireCube(pos, cubeSize);
            //   Gizmos.DrawWireSphere(pos, 0.1f);
        }
        deletTimeCount = 0;
        if (!isShow)
        {
            return;
        }
        Boidtime += Time.deltaTime;
        if (Boidtime > changeGraph)
        {
            turnFraction += addTurnFractionDelt * addOrReduce;
            Boidtime = 0;
        }
    }
    private void showClow1()
    {
        Vector3 initPos = this.transform.position;
        //deletTime += Time.deltaTime;
        //if (deletTime < refreshTime)
        //{
        //    return;
        //}
        // Debug.Log("numPoints=" + numPoints);
        for (int i = 0; i < numPoints; i++)
        {

            float dst = i / (numPoints - 1f);//0~1  //距离
            float angel = 2 * Mathf.PI * turnFraction * i;//角度 每次循环里它会得到圆分割后的度数


            float x = dst * Mathf.Cos(angel);
            float y = dst * Mathf.Sin(angel);

            // PlotPoint(i, x, y, color);
            float zPos = isAddZ ? (i * 0.05f) : 0;
            Vector3 pos = initPos + new Vector3(x, y, zPos);
            if (i < 3)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(pos, new Vector3(0.05f, 0.05f, 0.05f));

            }
            else
            {
                if ((i + hightOffset) % hightLight == 0)
                {
                    Gizmos.color = hightLightColor;
                }
                else
                {
                    Gizmos.color = defalutColor;
                }
                Gizmos.DrawSphere(pos, ballRadu);
            }

            // Gizmos.DrawWireCube(pos, cubeSize);
            //   Gizmos.DrawWireSphere(pos, 0.1f);
        }
        deletTimeCount = 0;
        if (!isShow)
        {
            return;
        }
        Boidtime += Time.deltaTime;
        if (Boidtime > changeGraph)
        {
            turnFraction += addTurnFractionDelt * addOrReduce;
            Boidtime = 0;
        }
    }
    private void showPow2()
    {
        Vector3 initPos = this.transform.position;
        //deletTime += Time.deltaTime;
        //if (deletTime < refreshTime)
        //{
        //    return;
        //}
        // Debug.Log("numPoints=" + numPoints);
        if (isShowPowMotion)
        {
            pow += powDelt * addOrReduce;
        }
        for (int i = 0; i < numPoints; i++)
        {

            float dst = Mathf.Pow(i / (numPoints - 1f), pow);//0~1  //距离
            float angel = 2 * Mathf.PI * turnFraction * i;//角度 每次循环里它会得到圆分割后的度数


            float x = dst * Mathf.Cos(angel);
            float y = dst * Mathf.Sin(angel);

            // PlotPoint(i, x, y, color);
            float zPos = isAddZ ? (i * 0.05f) : 0;
            Vector3 pos = initPos + new Vector3(x, y, zPos);
            if (i < 3)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(pos, new Vector3(0.05f, 0.05f, 0.05f));

            }
            else
            {
                if ((i + hightOffset) % hightLight == 0)
                {
                    Gizmos.color = hightLightColor;
                }
                else
                {
                    Gizmos.color = defalutColor;
                }
                Gizmos.DrawSphere(pos, ballRadu);
            }

            // Gizmos.DrawWireCube(pos, cubeSize);
            //   Gizmos.DrawWireSphere(pos, 0.1f);
        }
        deletTimeCount = 0;
        if (!isShow)
        {
            return;
        }
        Boidtime += Time.deltaTime;
        if (Boidtime > changeGraph)
        {
            turnFraction += addTurnFractionDelt * addOrReduce;
            Boidtime = 0;
        }
    }
    //turnFraction=0.2061029
    //斐波那契数列 0，1，1，2，3，5，8，13，21，34，55，89，144，233，377，610，987，1597
    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect(0, 0, 100, 100), "TransformTest TimeCost"))
    //    {
    //        ShowTimeCost(TransformTest);
    //    }
    //    if (GUI.Button(new Rect(0, 100, 100, 100), "TransformThis TimeCost"))
    //    {
    //        ShowTimeCost(TransformThis);
    //    }
    //    GUI.Label(new Rect(100,0,100,100), "turnFraction="+ turnFraction);
       
    //}
    //timeCost 5.12
    private void TransformTest()
    {
        for(int i = 0; i < 100000; i++)
        {
            Transform test = this.transform;
        }
    }
    //timeCost 0.3281
    private void TransformThis()
    {
        for (int i = 0; i < 100000; i++)
        {
            Transform test = catchTransform;
        }
    }
}
