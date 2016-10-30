using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Creaturi;
using UnityEngine;

namespace Assets.Scripts.Creatures.States
{
    public class IdleAI : AIState {

        public IdleAI(Npc npc) : base(npc) {
        }

        public override void enterState() {
            
        }

        public override void exitState() {

        }

        public override void reason() {
            //float dist = Vector2.Distance(player.pos, npc.pos);
            //if (dist <= 5) {
            //    List<Vector2> path = Pathfind.pathJPS(npc, player.pos.x, player.pos.y);
            //    if (path.Count >= 2) {
            //        Vector2 n = path[1];
            //        npc.walkDir.x = Math.Sign(n.x - (int)npc.transform.position.x);
            //        npc.walkDir.y = Math.Sign(n.y - (int)npc.transform.position.y);
            //        npc.actM.set("walk");
            //    }
            //} else
            //    npc.actM.set("idle");

            Creature targ = npc.scanFov();
            if ((targ != null) && (npc.attrs.dumb || npc.strongerThan(targ))) {
                npc.target = targ;
                if (npc.hasRangeTo(targ))
                    npc.stateM.changeState("combat");
                else
                    npc.stateM.changeState("pursue");
                return;
            }

            if (npc.attrs.hp < npc.attrs.mhp()) {
                npc.stateM.changeState("rest");
                return;
            }

            act();
        }

        public override void act() {
            npc.stateM.changeState("rest");
        }
    }
}
