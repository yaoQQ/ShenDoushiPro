

using System.Collections.Generic;
namespace Assets.script.FSMFrame
{

    public interface IState
    {

        /// <summary>
        /// 进入该状态时应执行一次该方法。
        /// </summary>
        /// <param name="prevState">该状态的上一个状态</param>
        bool OnEnter(string prevState);


        //添加动作
     //   void AddAction();

        /// <summary>
        /// 注册转换事件
        /// </summary>
        void initTransition(); 

        /// <summary>
        /// 退出该状态时应执行一次该方法。
        /// </summary>
        /// <param name="nextState">该状态的下一个状态</param>
        bool OnExit(string nextState);


        /// <summary>
        /// 当前状态为该状态时，每一帧执行一次该方法。
        /// </summary>
        void OnUpdate();
    }
}
