using System;
using UnityEngine;

    /// <summary>
    /// 行为基类
    /// </summary>
    public abstract class ActionBase : ScriptableObject
    {
        public abstract void Act(ShipBase controller);
    }
