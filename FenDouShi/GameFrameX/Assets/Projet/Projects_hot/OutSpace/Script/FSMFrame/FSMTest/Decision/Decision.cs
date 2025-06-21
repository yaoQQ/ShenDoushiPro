using System;
using UnityEngine;


    /// <summary>
    /// 判断决策基类
    /// </summary>
   // [CreateAssetMenu(menuName = "State/Decision/Transition")]
    public abstract class Decision : ScriptableObject
    {
        //ShipState 对应State状态
        public ShipState trueState;//true时转换的目标状态 
        public ShipState falseState;//false时转换的目标状态
        public abstract bool dec(ShipBase controller);

        public string getTrueState {
            get {

                return (trueState != ShipState.none) ? trueState.ToString() : ""; 
            }
        }
        public string getFalseState {
            get {

                return (falseState != ShipState.none) ? falseState.ToString() : "";
            }
        }
    }

