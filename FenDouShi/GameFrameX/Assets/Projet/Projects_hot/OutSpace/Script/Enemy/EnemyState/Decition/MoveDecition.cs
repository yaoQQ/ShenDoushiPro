using System;
using UnityEngine;

 [CreateAssetMenu(menuName = "State/Decision/MoveDecision")]
    public class MoveDecision : Decision
    {
        public MoveDecision() {
            trueState = ShipState.Move; 
            falseState = ShipState.none;//保存原状态 
        }
        public override bool dec(ShipBase controller) {
            return decion(controller as Enemy);
        }

        private bool decion(Enemy controller) {
            Enemy enemy = controller as Enemy;
            float distance = Vector3.Distance(enemy.transform.position, enemy.targetPos);
           // float deletDis = enemy.speed * Time.deltaTime;
             if(enemy.pathList.Count<= 0)
            {
                return false;
            }
             if(enemy.shipAttrackType == EnemyAttrackShipType.earAttack)
            {
                return true;
             }
             if(distance > enemy.attrackDistance)
            {
                return true;
             }
    
            return false;
        }

     
        
    }

