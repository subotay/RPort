using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Creaturi.States
{
    class Idle : Action{
        
        public Idle(Creature creature) : base(creature){
            cost = 100;
        }

        public override void enterAction(){}

        public override void exitAction(){}

        public override bool execute(){
            if (actor.attrs.hp.Val < actor.attrs.hp.MaxVal){
                actor.attrs.accHp += actor.attrs.hpreg.Val;
                actor.attrs.hp.Val += (float)Math.Floor(actor.attrs.accHp);
                actor.attrs.accHp -= (float)Math.Floor(actor.attrs.accHp);
                if (actor.attrs.hp.Val >= actor.attrs.hp.MaxVal){
                    actor.attrs.hp.Val = actor.attrs.hp.MaxVal;
                    actor.attrs.accHp = 0;
                }
            }
            if (actor.attrs.stam.Val < actor.attrs.stam.MaxVal){
                actor.attrs.accStam += actor.attrs.stareg.Val;
                actor.attrs.stam.Val += (float)Math.Floor(actor.attrs.accStam);
                actor.attrs.accStam -= (float)Math.Floor(actor.attrs.accStam);
                if (actor.attrs.stam.Val >= actor.attrs.stam.MaxVal){
                    actor.attrs.stam.Val = actor.attrs.stam.MaxVal;
                    actor.attrs.accStam = 0;
                }
            }
            return true;
        }
    }
}
