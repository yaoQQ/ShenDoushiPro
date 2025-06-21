using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.script.FSMFrame
{
    public class patrolMveAction : ActionBase
    {
        public override void Act(ShipBase controller) {
            Patrol(controller as Enemy);
        }

        //3个点之间巡逻
        private void Patrol(Enemy contrller) {
         //   contrller.renderObject.updateMove();
            //contrller.navMeshAgent.destination = contrller.wayPointList[contrller.nextWaypoint].position;
            //contrller.nextWaypoint = (contrller.nextWaypoint + 1) % contrller.wayPointList.Count;
        }
    }
}
