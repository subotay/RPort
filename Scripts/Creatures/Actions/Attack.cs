using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Creaturi.States {
    public class Attack : Action {
        public Attack(Creature creature) : base(creature) {
        }

        public override void enterAction() {
            
        }

        public override bool execute() {
            if (actor.attrs.hit.Val <= actor.target.attrs.eva.Val)
                return true;
            SoundController.inst.sfx.Play();
            float efdmg = Mathf.Clamp((actor.attrs.dmg.Val - actor.target.attrs.armor.Val), 0, 10000);
            actor.target.attrs.hp.Val -= efdmg;
            if (actor.target.attrs.hp.Val <= 0) {
                actor.target.onDeath();
                actor.target = null;
            }
            return true;
        }

        public override void exitAction() {
            
        }
    }
}
