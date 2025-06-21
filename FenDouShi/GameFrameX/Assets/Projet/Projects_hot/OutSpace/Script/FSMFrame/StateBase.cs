using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.script.FSMFrame
{
    public enum StateID
    {
        MOVE,
        STOP,
        DEAD,
        ATTACK,
        CAST,
        SUFFERING,
        //DODGE,
        WIN,
        CONTROL,
        COUNT
    }
  //   [CreateAssetMenu(menuName = "State/StateBase")]
    public abstract class StateBase : ScriptableObject,IState
    {
        protected string mStateName;                               // 状态名称
        private FSMTransition _fsmTransition;//保存状态转换
        public bool isLoop = false;//是否可以刷新进入相同状态
        [SerializeField]
        public List<ActionBase> _actions;//动作行为 巡逻，攻击，跟随等
        [SerializeField]
        public List<Decision> _decisions;//状态转换条件  是否移动 ，可攻击等    

        private ShipBase character;
        public string StateName {
            get {
                return mStateName;
            }
        }
        public FSMTransition FSmTransition {
            get {
                return _fsmTransition;
            }
        }
        public StateBase(ShipBase owner) {
            character = owner;
            _fsmTransition = new FSMTransition(owner.playerFSM, mStateName);
            initTransition();
            initDecisions();
            initActions();
        }

        //初始化条件事件
        protected virtual void initDecisions() {
            if(_decisions==null)
                return;
            for (int i = 0; i < _decisions.Count;i++ ) {
                _fsmTransition.addDecision(_decisions[i]);
            }
        }
        //初始化动作事件
        protected virtual void initActions() {

        }
        //添加条件事件
        protected void addDecision(Decision decision) {
            if (_decisions == null) {
                _decisions = new List<Decision>();
            }
            _decisions.Add(decision);
            _fsmTransition.addDecision(decision);
        }
        //添加动作事件
        protected void addActions(ActionBase action) {
            if (_actions == null) {
                _actions = new List<ActionBase>();
            }
            _actions.Add(action);

        }

        public virtual bool OnEnter(string prevState) {
            return false;
        }

        /// <summary>
        /// 注册转换事件
        /// </summary>
        public virtual void initTransition() {
          
        }

        /// <summary>
        /// 退出该状态时应执行一次该方法。
        /// </summary>
        /// <param name="nextState">该状态的下一个状态</param>
        public virtual bool OnExit(string nextState) {

            return false;
        }


        /// <summary>
        /// 当前状态为该状态时，每一帧执行一次该方法。
        /// </summary>
        public virtual void OnUpdate() {
            DoActions(character);
            CheckTransions(character);
        }

        //执行动作
        private void DoActions(ShipBase controller) {
            if (_actions == null)
                return;
            for (int i = 0; i < _actions.Count; i++) {
                _actions[i].Act(controller);
            }
        }
        private void CheckTransions(ShipBase controller) {
            if (_decisions == null)
                return;

            for (int i = 0; i < _decisions.Count; i++) {
                bool decisionSuccess = _decisions[i].dec(controller);
                if (decisionSuccess) {
                    _fsmTransition.Trigger(_decisions[i].getTrueState);
                }
                else {
                    _fsmTransition.Trigger(_decisions[i].getFalseState);
                }
            }
        }
    }
}
