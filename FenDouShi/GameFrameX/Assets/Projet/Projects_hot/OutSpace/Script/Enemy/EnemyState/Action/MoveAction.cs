using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    [CreateAssetMenu(menuName = "State/ActionBase/MoveAction")]
   public class MoveAction:ActionBase
    {
        public override void Act(ShipBase enemy) {
            move(enemy as Enemy);
        }
        private void move(Enemy enemy) {
            Vector3 targetPos = enemy.targetPos;
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPos, enemy.speed * Time.deltaTime);
            Vector3 currPos = enemy.transform.position;
            float distance = Vector3.Distance(currPos, targetPos);
            if (distance < enemy.speed * Time.deltaTime) {
                enemy.getNextPos();
            }    
        }
    }

