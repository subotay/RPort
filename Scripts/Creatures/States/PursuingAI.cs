using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Creatures.States {
    public class PursuingAI : AIState {
        
        public PursuingAI(Npc npc) : base(npc) {
            
        }

        public override void act() {
            npc.steering.seek(npc.target); //if (loc.Equals(Vector2.down))
        }

        public override void enterState() {
            npc.steering.resetPath();
            act();
        }

        public override void exitState() {
            npc.steering.resetPath();
        }

        public override void reason() {
            if (!npc.hasTarget() || !npc.hasFovTo(npc.target)) {
                npc.stateM.changeState("rest");
                return;
            }
            if (npc.hasRangeTo(npc.target)) {
                npc.stateM.changeState("combat");
                return;
            }

            act();
        }
    }
}
