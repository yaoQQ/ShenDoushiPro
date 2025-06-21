using System;
using UnityEngine;

[CreateAssetMenu(menuName = "State/StateBase/EnemyStateDead")]
    public class EnemyStateDead : EnemyState
    {
         public EnemyStateDead(Enemy enemy)
        : base(enemy) 
       {
           mStateName = ShipState.Dead.ToString();
       }
        public override void initTransition() {
            FSmTransition.RegisterTransition(ShipState.stop.ToString());
           //FSEvent fsEvent2 = fsmTransition.RegisterTransitionReturn<int>(TranslationEnum.ToMove.ToString(), StateID.MOVE.ToString(), testReturn);
       }
       public override bool OnEnter(string prevState) {
           Debug.Log("OnEnter EnemyStateDead  prevState=" + prevState);
           enemy.playerFSM.Trigger(ShipState.stop.ToString());
           enemy.Dead();
            return true;
           // public delegate TResult Func<T, TResult>(T arg1);
        }


        public override bool OnExit(string nextState) {
            Debug.Log("OnExit EnemyStateDead nextState=" + nextState);
            return true;
        }

        public override void OnUpdate() {
     
        }
    }

