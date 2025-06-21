using System;
using UnityEngine;
using Assets.script.FSMFrame;

[CreateAssetMenu(menuName = "State/StateBase/EnemyStateAttrack")]
    public class EnemyStateAttrack:EnemyState
    {
        public EnemyStateAttrack(Enemy enemy)
        : base(enemy) 
       {
           mStateName = ShipState.Attrack.ToString();
       }
        public override void initTransition() {
            FSmTransition.RegisterTransition(ShipState.Move.ToString());
            FSmTransition.RegisterTransition(ShipState.stop.ToString());
            FSmTransition.RegisterTransition(ShipState.Dead.ToString());
          // fsmTransition.RegisterTransition(TranslationEnum.ToAttrck.ToString(), StateID.ATTACK.ToString());
           //FSEvent fsEvent2 = fsmTransition.RegisterTransitionReturn<int>(TranslationEnum.ToMove.ToString(), StateID.MOVE.ToString(), testReturn);
       }
        protected override void initDecisions() {    
            MoveDecision decision = new MoveDecision();
            addDecision(decision);
        }

        protected override void initActions() {

        }
       public override bool OnEnter(string prevState) {
        //   Debug.Log("OnEnter EnemyStateAttrack  prevState=" + prevState);
          
            return true;
           // public delegate TResult Func<T, TResult>(T arg1);
        }


        public override bool OnExit(string nextState) {
        //    Debug.Log("OnExit EnemyStateAttrack nextState=" + nextState);
            return true;
        }

        public override void OnUpdate() {
            base.OnUpdate();
            enemy.attackPlayer();
        }
    }

