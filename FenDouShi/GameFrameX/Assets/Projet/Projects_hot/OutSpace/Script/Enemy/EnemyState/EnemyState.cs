using System;
using UnityEngine;
using Assets.script.FSMFrame;

   public class EnemyState:StateBase
    {
       protected Enemy enemy;
       public EnemyState(Enemy enemy)
           : base(enemy) 
       {
           this.enemy = enemy;
       }
       // public override void initTransition() {
       //     fsmTransition.RegisterTransition<int>(TranslationEnum.ToMove.ToString(), StateID.MOVE.ToString(), test2);
       //    fsmTransition.RegisterTransition(TranslationEnum.ToAttrck.ToString(), StateID.ATTACK.ToString());
       //    //FSEvent fsEvent2 = fsmTransition.RegisterTransitionReturn<int>(TranslationEnum.ToMove.ToString(), StateID.MOVE.ToString(), testReturn);
       //}
       //public override bool OnEnter(string prevState) {

       //     return true;
       // }

       // private bool testReturn(int a) {

       //     return true;
       // }
       // private void test2(int a) {

       //   //  Debug.Log("StateIdle  test2 a=" + a);
       // }

       // public void Execute(string currentState) {
           
       // }

       // public override bool OnExit(string nextState) {
       //     Debug.Log("OnExit StateIdle nextState=" + nextState);
       //     return true;
       // }

       // public override void OnUpdate() {
       //     //AnimatorTransitionInfo info = playerCtr.renderObject.animator.GetAnimatorTransitionInfo(0);
       //     //AnimatorStateInfo stateInfo = playerCtr.renderObject.animator.GetCurrentAnimatorStateInfo(0);
       //     //NGUIDebug.Log("info.length=" + stateInfo.length);
       //     //NGUIDebug.Log("stateInfo.name=" + stateInfo.ToString() + " stateInfo.time=" + stateInfo.normalizedTime + "IsName=" + stateInfo.IsName("stand"));
       // }
    }

