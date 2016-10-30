using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Creaturi;

namespace Assets.Scripts.Creatures.States {
    public class RestAI : AIState {
        public RestAI(Npc npc) : base(npc) {
        }


        public override void enterState() {
            act();
        }

        public override void exitState() {
            
        }

        public override void reason() {
            Creature targ = npc.scanFov();
            if ((targ != null) && (npc.attrs.dumb || npc.strongerThan(targ))) {
                npc.target = targ;
                if (npc.hasRangeTo(targ))
                    npc.stateM.changeState("combat");
                else
                    npc.stateM.changeState("pursue");
                return;
            }

            if (npc.pos != npc.home) {
                npc.stateM.changeState("return");
                return;
            }

            act();
        }

        public override void act() {
            npc.actM.set("idle");
        }
    }
}
