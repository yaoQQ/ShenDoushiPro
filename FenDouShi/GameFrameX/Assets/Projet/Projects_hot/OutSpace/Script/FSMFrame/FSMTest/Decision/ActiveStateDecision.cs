using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.script.FSMFrame
{
    public class ActiveStateDecision: Decision
    {
        public override bool dec(ShipBase controller) { 
            return isTargetActive(controller as Enemy);
        }
        private bool isTargetActive(Enemy con) {
            ShipBase controller = con as ShipBase;
            return controller.gameObject.activeSelf;
        }
    }
}
