using System;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/DeadDecition")]
public class DeadDecition : Decision
{
    public DeadDecition() {
        trueState = ShipState.Dead;
        falseState = ShipState.none;//保存原状态 
    }
    public override bool dec(ShipBase enemy) {
        return decion(enemy as Enemy);
    }

    private bool decion(Enemy enemy) {

        float distance = Vector3.Distance(enemy.transform.position, enemy.targetPos);
        if (distance <= enemy.attrackDistance) {
            return true;
        }

        return false;
    }



}

