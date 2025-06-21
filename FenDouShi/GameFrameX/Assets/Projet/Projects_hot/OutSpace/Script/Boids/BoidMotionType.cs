using UnityEngine;
using System.Collections;


[CreateAssetMenu(menuName = "Boids/BoidMotionType")]
public class BoidMotionType:ScriptableObject
{
    public MotionType motionType;
    [Header("0.0001~1角度 每次循环里它会得到圆分割后的度数 黄金分割点1.618034")]
    public float turnFraction = 0;
    [Header("改变图形 时间")]
    public float changeGraph = 0.2f;
    [Header("增加或者减少")]
    public int addOrReduce = 1;
    [Header("3D")]
    public bool isAddZ = false;
    [Header("最大距离")]
    public float maxDistance = 8;

    [HideInInspector]
    [Header("是否开启TurnFractionMotion")]
    public bool isShowTurnFractionMotion;
    [HideInInspector]
    [Header("turnFractionMotion开始值")]
    public float turnFractionStart = 0;
    [HideInInspector]
    [Header("turnFractionMotion结束值")]
    public float turnFractionEnd = 0;
    [HideInInspector]
    [Header("圆分割后的度数 自动改变量")]
    public float addTurnFractionDelt = 0.0001f;
    [Header("球半径大小")]
    public int circleRadius = 1;




    [HideInInspector]
    [Header("是否打开改变中心点 动画")]
    public bool isShowPowMotion = false;
    [HideInInspector]
    [Header("pow开始值")]
    public float powStart = 0;
    [HideInInspector]
    [Header("pow结束值")]
    public float powEnd = 0;
    [HideInInspector]
    [Header("改变中心点 速度增量")]
    public float powDelt = 0.01f;



}
