using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Creaturi.States
{
    class Idle : Action
    {
        
        public Idle(Creature creature) : base(creature)
        {
            cost = 100;
        }

        public override void enterAction()
        {
            
        }

        public override void exitAction()
        {
            
        }

        public override bool execute(){
            if (creature.attrs.hp < creature.attrs.mhp()){
                creature.attrs.accHp += creature.attrs.hpreg();
                creature.attrs.hp += (float)Math.Floor(creature.attrs.accHp);
                creature.attrs.accHp -= (float)Math.Floor(creature.attrs.accHp);
                if (creature.attrs.hp >= creature.attrs.mhp()){
                    creature.attrs.hp = creature.attrs.mhp();
                    creature.attrs.accHp = 0;
                }
            }
            if (creature.attrs.stam < creature.attrs.mstam()){
                creature.attrs.accStam += creature.attrs.stareg();
                creature.attrs.stam += (float)Math.Floor(creature.attrs.accStam);
                creature.attrs.accStam -= (float)Math.Floor(creature.attrs.accStam);
                if (creature.attrs.stam >= creature.attrs.mstam()){
                    creature.attrs.stam = creature.attrs.mstam();
                    creature.attrs.accStam = 0;
                }
            }
            return true;
        }
    }
}
