using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.script.FSMFrame
{
   public class StateAttrack:PlayerState
    {
       public StateAttrack(ShipBase player,  string name)
           : base(player, name) {

       }
       public override void initTransition() {
          FSmTransition.RegisterTransition(StateID.MOVE.ToString());
          FSmTransition.RegisterTransition(StateID.STOP.ToString());
         //  fsmTransition.addTranstion(
          // FSEvent fsEvent2 = fsmTransition.RegisterTransitionReturn<int>(TranslationEnum.ToMove.ToString(), StateID.MOVE.ToString(), testReturn);

           //  mTranslationEvents.Add(TranslationEnum.ToMove.ToString(), new FSEvent(TranslationEnum.ToMove.ToString(), StateID.MOVE.ToString(), OwerFiniteStateMachine,FSEvent.EventType.ENTER));
       }
       //private Transition initTransition() {
       //    Transition transion = new Transition();
       //    return transion;
       //}
       protected override void initActions() { 
           patrolMveAction moveAction = new patrolMveAction();//攻击移动动作
           addActions(moveAction);
       }
       public override bool OnEnter(string prevState) {
          // playerCtr.renderObject.playAni("attack01", 0);
           Debug.Log("OnEnter StateAttrack prevState=" + prevState);
           return true;
        }


        public override bool OnExit(string nextState) {
            Debug.Log("OnExit StateAttrack  nextState=" + nextState);
            return true;
        }

        public override void OnUpdate() {
            base.OnUpdate();
           //AnimatorStateInfo stateInfo = playerCtr.renderObject.animatorStateInfo;
           // if (stateInfo.IsName("attack01") && stateInfo.normalizedTime >= 1f) {
           //     if (playerCtr.renderObject.IsMoving) {
           //         playerCtr.playerFSM.Trigger(StateID.MOVE.ToString());
           //     }
           //     else {
           //         playerCtr.playerFSM.Trigger(StateID.STOP.ToString());
           //     }
           // }
        }
            
    }
}
