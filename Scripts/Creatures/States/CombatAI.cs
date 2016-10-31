using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Creatures.States {
    public class CombatAI : AIState {
        public CombatAI(Npc npc) : base(npc) {
        }

        public override void act() {
            npc.actM.set("atk");
        }

        public override void enterState() {
            act();
        }

        public override void exitState() {
            
        }

        public override void reason() {
            if (!npc.hasTarget() || !npc.hasFovTo(npc.target)) {
                npc.stateM.changeState("rest");
                return;
            }
            if (!npc.hasRangeTo(npc.target)) {
                npc.stateM.changeState("pursue");
                return;
            }
            act();
        }
    }
}
