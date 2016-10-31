using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Creatures.States {
    public class FollowAI : AIState {
        public FollowAI(Npc npc) : base(npc) {
        }

        public override void act() {
            }

        public override void enterState() {
            act();
        }

        public override void exitState() {
            
        }

        public override void reason() {
            
        }
    }
}
