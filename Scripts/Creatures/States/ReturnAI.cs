using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Creatures.States {
    public class ReturnAI : AIState {
        public ReturnAI(Npc npc) : base(npc) {
        }

        public override void act() {
            npc.steering.walkTo(npc.home);
        }

        public override void enterState() {
            npc.steering.resetPath();
            act();
        }

        public override void exitState() {
            npc.steering.resetPath();
        }

        public override void reason() {
            if (npc.pos==npc.home) {
                npc.stateM.changeState("rest");
                return;
            }
            act();
        }
    }
}
