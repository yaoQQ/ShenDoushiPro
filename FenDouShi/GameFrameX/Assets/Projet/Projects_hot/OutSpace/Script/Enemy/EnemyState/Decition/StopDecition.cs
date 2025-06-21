using System;
using UnityEngine;

 [CreateAssetMenu(menuName = "State/Decision/MoveDecision")]
    public class StopDecision:Decision
    {
        public StopDecision() {
            trueState = ShipState.stop;
            falseState = ShipState.none;//保存原状态
        }
        public override bool dec(ShipBase controller) {
            return decition(controller as Enemy);
        }

        private bool decition(Enemy enemy) {
            float distance = Vector3.Distance(enemy.transform.position, enemy.targetPos);
            // float deletDis = enemy.speed * Time.deltaTime;
            if (enemy.pathList.Count <= 0 && distance <= 0) {
                return true;
            }
    
            return false;
        }

     
        
    }


