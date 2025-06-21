using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.script.FSMFrame
{
    public class ChaseAction : ActionBase
    {
        public override void Act(ShipBase controller) {
            Chase(controller as Enemy);
        }
        private void Chase(Enemy controller) {
            //controller.navMeshAgent.destination = controller.ChaseTarget.position;
            //controller.navMeshAgent.Move(controller.ChaseTarget.position);
        }
    }
}
