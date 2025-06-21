using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class EnemyLevelWave2 
{
    [Header("敌人出现时间")]
    public float showTime;
    [Header("敌人列表")]
    public List<GameObject> enemyList;

}
