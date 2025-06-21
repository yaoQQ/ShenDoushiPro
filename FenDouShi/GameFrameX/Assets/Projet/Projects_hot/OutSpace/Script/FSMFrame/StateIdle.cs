using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.script.FSMFrame
{
    public class StateIdle : PlayerState
    {

        public StateIdle(ShipBase player, string mStateName)
            : base(player, mStateName) 
       {
          
       }
        public override void initTransition() {
            FSmTransition.RegisterTransition(StateID.MOVE.ToString());
            FSmTransition.RegisterTransition(StateID.ATTACK.ToString());
           //FSEvent fsEvent2 = fsmTransition.RegisterTransitionReturn<int>(TranslationEnum.ToMove.ToString(), StateID.MOVE.ToString(), testReturn);
       }
       public override bool OnEnter(string prevState) {
           Debug.Log("OnEnter StateIdle  prevState=" + prevState);
          // playerCtr.renderObject.playAni("stand",2);
            return true;
           // public delegate TResult Func<T, TResult>(T arg1);
        }

        public void Execute(string currentState) {
           
        }

        public override bool OnExit(string nextState) {
            Debug.Log("OnExit StateIdle nextState=" + nextState);
            return true;
        }

        public override void OnUpdate() {
            base.OnUpdate();
            //if (playerCtr.renderObject.IsMoving) {
            //    playerCtr.playerFSM.Trigger(StateID.MOVE.ToString());
            //}
        }
    }
}
