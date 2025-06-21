using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.script.FSMFrame
{
    public class PlayerState:StateBase
    {
        protected ShipBase playerCtr;
        public PlayerState(ShipBase player, string mStateName)
            : base(player) {
            playerCtr = player;
        }
    }
}
