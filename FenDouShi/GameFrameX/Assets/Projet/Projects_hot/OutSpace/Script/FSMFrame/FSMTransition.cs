using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.script.FSMFrame
{
  
    public class FSMTransition
    {
        //string 状态明  FSEvent 状态回调和触发事件
        protected Dictionary<string, FSEvent> mTranslationEvents;  // 状态切换事件
        protected FiniteStateMachine OwerFiniteStateMachine;       // 该状态所属的状态机。
        private string _mStateName;
        public FSMTransition(FiniteStateMachine currStateMachine, string mStateName) {

            OwerFiniteStateMachine = currStateMachine;
            _mStateName = mStateName;
            mTranslationEvents = new Dictionary<string, FSEvent>();
        }
        #region 该状态的事件触发器
        
        public void Trigger(string name) {
            if (String.IsNullOrEmpty(name))
                return;
            if (!mTranslationEvents.ContainsKey(name)) {
                Debug.LogError("State[" + _mStateName+"] did not have transition state="+name);
                return;
            }
            mTranslationEvents[name].Execute(null, null, null);
        }

        public void Trigger(string StateName, object param1) {
            if (!mTranslationEvents.ContainsKey(StateName)) {
                return;
            }
            mTranslationEvents[StateName].Execute(param1, null, null);
        }

        public void Trigger(string StateName, object param1, object param2) {
            if (!mTranslationEvents.ContainsKey(StateName)) {
                return;
            }
            mTranslationEvents[StateName].Execute(param1, param2, null);
        }

        public void Trigger(string StateName, object param1, object param2, object param3) {
            if (!mTranslationEvents.ContainsKey(StateName)) {
                return;
            }
            mTranslationEvents[StateName].Execute(param1, param2, param3);
        }

        #endregion


        /// <summary>
        /// 添加和注册转换对象
        /// </summary>
        /// <param name="trasition"></param>
        public void addDecision(Decision decision) {
            //注册条件true和false 时的 状态的事件
            RegisterTransition(decision.getTrueState);
            RegisterTransition(decision.getFalseState);
        }

        #region 为该状态添加状态切换事件

        private void addEvent(string StateName, FSEvent evt) {

            if (mTranslationEvents.ContainsKey(StateName)) {
                mTranslationEvents[StateName] = evt;
                Debug.Log("error! addEvent  already have " + StateName + "evt=" + evt);
                return;
            }
            mTranslationEvents.Add(StateName, evt);
        }
        /// <summary>
        /// 注册事件
        /// （mEnterDelegate，mPushDelegate，mPopDelegate） FiniteStateMachine状态机统一注册的函数调用
        /// </summary>
        /// <param name="eventName">注册的事件名</param>
        /// <returns></returns>
        public FSEvent RegisterTransition(string targetStateID) {
    
            if (mTranslationEvents.ContainsKey(targetStateID)) {
                return mTranslationEvents[targetStateID];
            }
            FSEvent newEvent = new FSEvent(targetStateID, OwerFiniteStateMachine);
            addEvent(targetStateID, newEvent);
            return newEvent;
        }

        public FSEvent RegisterTransitionReturn<T>(string targetStateID, Func<T, bool> action) {
            FSEvent newEvent = new FSEvent(targetStateID, OwerFiniteStateMachine);
            newEvent.mAction = delegate(object o1, object o2, object o3) {
                T param1;
                try {
                    param1 = (T)o1;
                }
                catch {
                    param1 = default(T);
                }
                action(param1);
                return true;
            };
            addEvent(targetStateID, newEvent);
            return newEvent;
        }

        public FSEvent RegisterTransition<T>(string targetStateID, Action<T> action) {
            FSEvent newEvent = new FSEvent( targetStateID, OwerFiniteStateMachine);
            newEvent.mAction = delegate(object o1, object o2, object o3) {
                T param1;
                try {
                    param1 = (T)o1;
                }
                catch {
                    param1 = default(T);
                }
                action(param1);
                return true;
            };
            addEvent(targetStateID, newEvent);
            return newEvent;
        }

        public FSEvent RegisterTransitionReturn<T1, T2>(string targetStateID, Func<T1, T2, bool> action) {
            FSEvent newEvent = new FSEvent(targetStateID, OwerFiniteStateMachine);
            newEvent.mAction = delegate(object o1, object o2, object o3) {
                T1 param1;
                T2 param2;
                try {
                    param1 = (T1)o1;
                }
                catch {
                    param1 = default(T1);
                }
                try {
                    param2 = (T2)o2;
                }
                catch {
                    param2 = default(T2);
                }
                action(param1, param2);
                return true;
            };
            addEvent(targetStateID, newEvent);
            return newEvent;
        }

        public FSEvent RegisterTransition<T1, T2>(string targetStateID, Action<T1, T2> action) {
            FSEvent newEvent = new FSEvent(targetStateID, OwerFiniteStateMachine);
            newEvent.mAction = delegate(object o1, object o2, object o3) {
                T1 param1;
                T2 param2;
                try {
                    param1 = (T1)o1;
                }
                catch {
                    param1 = default(T1);
                }
                try {
                    param2 = (T2)o2;
                }
                catch {
                    param2 = default(T2);
                }
                action(param1, param2);
                return true;
            };
            addEvent(targetStateID, newEvent);
            return newEvent;
        }

        public FSEvent RegisterTransitionReturn<T1, T2, T3>(string targetStateID, Func<T1, T2, T3, bool> action) {
            FSEvent newEvent = new FSEvent(targetStateID, OwerFiniteStateMachine);
            newEvent.mAction = delegate(object o1, object o2, object o3) {
                T1 param1;
                T2 param2;
                T3 param3;
                try {
                    param1 = (T1)o1;
                }
                catch {
                    param1 = default(T1);
                }
                try {
                    param2 = (T2)o2;
                }
                catch {
                    param2 = default(T2);
                }
                try {
                    param3 = (T3)o3;
                }
                catch {
                    param3 = default(T3);
                }
                action(param1, param2, param3);
                return true;
            };
            addEvent(targetStateID, newEvent);
            return newEvent;
        }

        public FSEvent RegisterTransition<T1, T2, T3>(string targetStateID, Action<T1, T2, T3> action) {
            FSEvent newEvent = new FSEvent(targetStateID, OwerFiniteStateMachine);
            newEvent.mAction = delegate(object o1, object o2, object o3) {
                T1 param1;
                T2 param2;
                T3 param3;
                try {
                    param1 = (T1)o1;
                }
                catch {
                    param1 = default(T1);
                }
                try {
                    param2 = (T2)o2;
                }
                catch {
                    param2 = default(T2);
                }
                try {
                    param3 = (T3)o3;
                }
                catch {
                    param3 = default(T3);
                }
                action(param1, param2, param3);
                return true;
            };
            addEvent(targetStateID, newEvent);
            return newEvent;
        }

        #endregion
       
    }
}
