using System;
using UnityEngine;

namespace Assets.script.FSMFrame
{
    public class LooKDecision : Decision
    {
        //public override bool dec(FSMCharacter controller) {
        //    return Look(controller);
        //}

        //private bool Look(FSMCharacter controller) {
        //    RaycastHit hit;
        //    if (Physics.SphereCast(controller.worldPosition, controller.lookSphereCastRadius, controller.renderObject.transform.forward, out hit, 2)
        //       && hit.collider.CompareTag("Player")) {
        //        //controller.ChaseTarget = hit.transform;
        //        return true;
        //    }
        //    return false;
        //}
        public override bool dec(ShipBase controller) {
            throw new NotImplementedException();
        }
    }
}
