using System;
using UnityEngine;
using Assets.script.FSMFrame;

[CreateAssetMenu(menuName = "State/StateBase/EnemyStateStop")]
public class EnemyStateStop : EnemyState
    {
         public EnemyStateStop(Enemy enemy)
        : base(enemy) 
       {
           mStateName = ShipState.stop.ToString();
       }
        public override void initTransition() {
            FSmTransition.RegisterTransition(ShipState.Move.ToString());
            FSmTransition.RegisterTransition(ShipState.Dead.ToString());
            FSmTransition.RegisterTransition(ShipState.Attrack.ToString());
          // fsmTransition.RegisterTransition(TranslationEnum.ToAttrck.ToString(), StateID.ATTACK.ToString());
           //FSEvent fsEvent2 = fsmTransition.RegisterTransitionReturn<int>(TranslationEnum.ToMove.ToString(), StateID.MOVE.ToString(), testReturn);
       }
        protected override void initDecisions() {
            base.initDecisions();
            MoveDecision decision = new MoveDecision();
            addDecision(decision);
        }
       public override bool OnEnter(string prevState) {
          // Debug.Log("OnEnter EnemyStateStop  prevState=" + prevState);
           if (enemy.TailTransform != null) {
               enemy.TailTransform.gameObject.SetActive(false);
           }
            return true;
           // public delegate TResult Func<T, TResult>(T arg1);
        }


        public override bool OnExit(string nextState) {
          //  Debug.Log("OnExit EnemyStateStop nextState=" + nextState);
            return true;
        }

        public override void OnUpdate() {
            base.OnUpdate();
           // enemy.finiteStateMachine.Trigger(ShipState.Move.ToString());
            //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
            //setState = ShipState.Move;
        }
    }

