using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.script.FSMFrame
{

    /// <summary>
    /// 状态切换事件类
    /// </summary>
    public class FSEvent
    {
        /// <summary>
        /// 事件类别
        /// </summary>
        public enum EventType
        {
            NONE,   // 空类别。
            ENTER,  // 进入新状态，不记录上一个状态。
            PUSH,   // 进入新状态，记录上一个状态。
            POP     // 弹出当前状态。
        }
     //   protected string mEventName;                                // 事件名称
        protected StateBase mStateOwner;                              // 事件所属状态
        protected string mTargetState;                             // 事件名称（同等 事件的目标状态）
        protected FiniteStateMachine mOwner;                        // 所属状态机
        protected EventType eType;                                  // 事件类别
        public Func<object, object, object, bool> mAction = null;   // 事件方法

        /// <summary>
        /// 注册界面状态机事件
        /// </summary>
        /// <param name="name">事件名</param>
        /// <param name="targetName"></param>
        /// <param name="oweState">界面容器包含FSState类</param>
        /// <param name="owner">状态机管理类对象</param>
        /// <param name="e">界面FSState enter回调</param>
        /// <param name="pu">界面FSState   push回掉</param>
        /// <param name="po">界面FSState   pop回调</param>
        public FSEvent(string targetStateID, FiniteStateMachine owner,EventType evt = EventType.ENTER) {
            mTargetState = targetStateID;
            mOwner = owner;
            eType = evt;

        }


        public void Execute(object o1, object o2, object o3) {
            switch (eType) {
                case EventType.ENTER:
                    mOwner.Enter(mTargetState);
                    break;
                case EventType.PUSH:
                   // mOwner.p(mTargetState, mOwner.CurrentState.StateName);
                    mOwner.Push(mTargetState, mOwner.CurrentState.StateName);
                    break;
                case EventType.POP:
                    mOwner.Pop();
                    break;
                case EventType.NONE://Decition中调用  falseState = ShipState.none;//保存原状态 此为保留原状态效果
                default:
                    break;
            }
            if (mAction != null) {
                mAction(o1, o2, o3);
            }
        }

    }
}