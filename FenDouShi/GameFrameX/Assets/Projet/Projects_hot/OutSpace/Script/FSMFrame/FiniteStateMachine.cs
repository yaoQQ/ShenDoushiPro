using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.script.FSMFrame
{
    /// <summary>
    /// 有限状态机类，对有限状态机进行封装。
    /// </summary>
  //  [CreateAssetMenu(menuName = "State/StateMachine/FiniteStateMachine")]
    public class FiniteStateMachine:ScriptableObject
    {

        public delegate void EnterState(string stateName);
        public delegate void PushState(string stateName, string lastStateName);
        public delegate void PopState();

        [SerializeField]
        protected Dictionary<string, StateBase> mStates;      // 存储了所有注册进来的状态。
        protected string mEntryPoint;                       // 该状态机的入口状态的名称。
        protected Stack<StateBase> mStateStack;               // 该状态栈用于状态切换，位于栈顶的状态是当前状态。

        public FiniteStateMachine() {
            mStates = new Dictionary<string, StateBase>();
            mStateStack = new Stack<StateBase>();
            mEntryPoint = null;
        }


        //游戏入口
        public StateBase Update() {
            if (CurrentState == null) {//开始游戏进入
                mStateStack.Push(mStates[mEntryPoint]);
                CurrentState.OnEnter(null);
            }
            CurrentState.OnUpdate();
            return CurrentState;
        }

        /// <summary>
        /// 注册一个状态
        /// </summary>
        /// <param name="stateName">状态名称</param>
        /// <param name="stateObject">状态对象</param>
        public void Register(string stateName, StateBase stateObject) {
            if (mStates.Count == 0) {
                // 第一个注册进来的状态是状态的入口。
                mEntryPoint = stateName;
            }
            //mStates.Add(stateName, new StateBase(stateObject, this, stateName, Enter, Push, Pop));
            mStates.Add(stateName,stateObject);
        }

        public StateBase State(string stateName) {
            return mStates[stateName];
        }

        public void SetEntryPoint(string startName) {
            mEntryPoint = startName;
        }

        public StateBase CurrentState {
            get {
                if (mStateStack.Count == 0)
                    return null;
                return mStateStack.Peek();//放出但不删除
            }
        }

        #region 状态切换方法

        /* 状态的切换有两种模式：
     * 1.记录上一个状态模式。
     *   进入新状态时直接压栈，那么旧状态就会被保留在栈的第二层，想回到旧状态就把栈顶Pop就可以。
     * 2.不记录上一个状态模式。
     *   进入新状态前先把当前状态Pop，再把新状态压入栈内。此时就回不到旧的状态了。
     */

        /// <summary>
        ///1.进入新状态，不记录上一个状态。
        /// </summary>
        /// <param name="stateName">新状态名称</param>
        public void Enter(string stateName) {
            //Push(stateName, Pop(stateName));
            string lastStateName = Pop(stateName);
            mStateStack.Push(mStates[stateName]);
            mStateStack.Peek().OnEnter(lastStateName);
        }


        //2.上一状态退出，不删除且进入新状态
        public void Push(string stateName, string lastStateName) {
            if (mStateStack.Peek().StateName == lastStateName) {
                mStateStack.Peek().OnExit(stateName);
            }
            mStateStack.Push(mStates[stateName]);
            mStateStack.Peek().OnEnter(lastStateName);
        }

        public void Pop() {
            Pop(null);
        }

        protected string Pop(string newName) {//推出下一个状态
            StateBase lastState = mStateStack.Peek();
            string newState = null;
            if (newName == null && mStateStack.Count > 1) {
                int index = 0;
                foreach (StateBase item in mStateStack) {
                    if (index++ == mStateStack.Count - 2) {//推出倒数第二状态
                        newState = item.StateName;
                    }
                }
            }
            else {
                newState = newName;
            }
            string lastStateName = null;
            if (lastState != null) {
                lastStateName = lastState.StateName;
                lastState.OnExit(newState);
            }
            mStateStack.Pop();
            return lastStateName;
        }

        #endregion

        #region 触发当前状态的事件

        public void Trigger(string eventName) {
            if (CurrentState == null)
                return;
            if (!CurrentState.isLoop && CurrentState.StateName.Equals(eventName)) 
                return;
            CurrentState.FSmTransition.Trigger(eventName);
        }

        public void Trigger(string eventName, object param1) {
            CurrentState.FSmTransition.Trigger(eventName, param1);
        }

        public void Trigger(string eventName, object param1, object param2) {
            CurrentState.FSmTransition.Trigger(eventName, param1, param2);
        }

        public void Trigger(string eventName, object param1, object param2, object param3) {
            CurrentState.FSmTransition.Trigger(eventName, param1, param2, param3);
        }

        #endregion

    }

}
