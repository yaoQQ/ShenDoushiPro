using System;
using UnityEngine;

namespace Assets.script.FSMFrame
{
   public class StateMove:PlayerState
    {
       public StateMove(ShipBase player, string name)
           : base(player, name) {

       }
       public override void initTransition() {
           FSmTransition.RegisterTransition(StateID.STOP.ToString());
          // FSEvent fsEvent2 = fsmTransition.RegisterTransitionReturn<int>(TranslationEnum.ToMove.ToString(), StateID.MOVE.ToString(), testReturn);
           FSmTransition.RegisterTransition(StateID.ATTACK.ToString());
         
       }
       public override bool OnEnter(string prevState) {
         //  playerCtr.renderObject.playAni("run", 0);
           Debug.Log("OnEnter StateMove prevState" + prevState);
           return true;
        }

        private void MoveToIdle(int a ) {
            Debug.Log("StateMove  MoveToIdle  a=" + a);
        }


        public override bool OnExit(string nextState) {
            Debug.Log("OnExit StateMove  nextState=" + nextState);
            return true;
        }

        public override void OnUpdate() {
            base.OnUpdate();
            //if (!playerCtr.renderObject.updateMove()) {
            //    playerCtr.playerFSM.Trigger(StateID.STOP.ToString());
            //}
        }
            
    }
}
