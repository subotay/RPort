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

        public override void enterAction() {
            //Debug.Log("entered walk");
            //Debug.Log("     >>creature energ:: " + creature.attrs.energ);
            //creature.attrs.energ-= creature.actM.prevAction.cost;
        }

        public override void exitAction() {
            //Debug.Log("exited walk");
            //Debug.Log("     >>creature energ:: " + creature.attrs.energ);
        }

        public override bool execute() {
            creature.dest.x = (int)creature.transform.position.x + creature.walkDir.x;//  creature.pos + creature.walkDir;
            creature.dest.y = (int)creature.transform.position.y + creature.walkDir.y;
            if (creature.dest.x >= level.worldw || creature.dest.x < 0 || creature.dest.y < 0 || creature.dest.y >= level.worldh)
            {
                return true;
            }
            
            LevelMaster.CellFlag cell = level.cells[(int)creature.dest.x, (int)creature.dest.y];
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
            level.cells[(int)creature.transform.position.x, (int)creature.transform.position.y] &= ~LevelMaster.CellFlag.MONST;
            level.cells[(int)creature.dest.x, (int)creature.dest.y] |= LevelMaster.CellFlag.MONST;
            creature.pos = creature.dest;
            creature.steering.move(creature.walkDir.x, creature.walkDir.y);
            return true;
        }
    }
}
