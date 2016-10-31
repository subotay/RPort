using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Creaturi.States
{
    public class Walk : Action
    {
        public Walk(Creature creature) : base(creature) {
            cost = 100;
        }

        public override void enterAction() {}

        public override void exitAction() {}

        public override bool execute() {
            actor.dest.x = (int)actor.transform.position.x + actor.walkDir.x;//  actor.pos + actor.walkDir;
            actor.dest.y = (int)actor.transform.position.y + actor.walkDir.y;
            if (actor.dest.x >= level.worldw || actor.dest.x < 0 || actor.dest.y < 0 || actor.dest.y >= level.worldh)
            {
                return true;
            }
            
            LevelMaster.CellFlag cell = level.cells[(int)actor.dest.x, (int)actor.dest.y];
            if ((cell & LevelMaster.CellFlag.BLOCKMV) != 0) {
                return true;
            }

            if ((cell & LevelMaster.CellFlag.HERO) != 0) {
                //attck TODO
                //actor.act = new AtkMelee(actor, actor.level.erou, actor.atkcost);
                //return false;
                return true;
            }
            if ((cell & LevelMaster.CellFlag.NPC) != 0){
                //actor.path = null;
                //Creatura target = actor.level.getCreaturAt(nx, ny);
                //actor.target = target;
                //actor.act = new AtkMelee(actor, target, actor.atkcost);
                //return false;
                return true;
            }
            if ((cell & LevelMaster.CellFlag.MONST) != 0){
                //actor.path = null;
                //return true; //same as walking in wall
                return true;
            }

            if ((cell & LevelMaster.CellFlag.TRAP) != 0) {
                //actor.interractTrap(nx, ny);
            }

            //muta monstru+ update cells
            actor.steering.move(actor.walkDir.x, actor.walkDir.y);
            return true;
        }
    }
}
