using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "State/ActionBase/RotationAction")]
public class RotationAction : ActionBase
{
    public override void Act(ShipBase controller) {
        rotate(controller as Enemy);
    }
    private void rotate(Enemy enemy) {
        float angel = Quaternion.Angle(enemy.targetRotation, enemy.transform.rotation);
        if (angel <= 1f && angel >= -1f) {
            enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, enemy.targetRotation, enemy.RotateSpeed * Time.deltaTime);

        }
    }
}

