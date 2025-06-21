using UnityEngine;
using System.Collections.Generic;
using System;
public enum MotionType
{
    CircleMotion,
    Pow2,
    AutoRotate,
    None
}
[Serializable]
public class EnemyLevelWave 
{
    //[Header("是否随机选取生成")]
    //public bool isRandom = false;

    //开始时等待多少秒后开始产生敌人
    [Header("是否生成")]
    public bool isCreate=false;
    [Header("当前队列标志")]
    private int _index;
    public int index { get { return _index; }set { _index = value; } }
    [Header("队列图形模式")]
    public MotionType motionType= MotionType.None;
    [Header("等待出现时间")]
    public float showTime;
    [Header("生成敌人列表的总批次")]
    public int waveTotalCreate = 1;
    [Header("一次生成敌人多少批数")]
    public int createListNum=1;
    [Header("生成每一敌人的间隔")]
    public float everyEnemyDelayTime = 0;
    [Header("一批敌人列表")]
    public List<GameObject> enemyList;

    //[Header("结合群阵列类")]
    //public EnemyBoidsMotionBehaviour boidsMotionBehaviour;
}
