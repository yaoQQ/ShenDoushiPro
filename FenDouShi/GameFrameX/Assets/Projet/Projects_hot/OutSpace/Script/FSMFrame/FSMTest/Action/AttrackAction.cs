using System;
using UnityEngine;

namespace Assets.script.FSMFrame
{
    public class AttrackAction : ActionBase
    {
        public override void Act(ShipBase controller) { 
            Attrack(controller as Enemy);
        }

        private void Attrack(Enemy con) {

          //  RaycastHit hit;
            //if (Physics.SphereCast(controller.worldPosition, controller.lookSphereCastRadius, controller.renderObject.transform.forward, out hit, 2)
            //   && hit.collider.CompareTag("Player")) {
            //    // controller.fire();
            //}
        }
    }
}
