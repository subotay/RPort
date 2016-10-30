using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Creaturi.States.PlayerStates
{
    public class PlayerWalk : PlayerAction
    {
        public PlayerWalk(Creature creature) : base(creature)
        {
            cost = 100;
        }

        public override void enterAction()
        {
            //Debug.Log("entered walk");
            //Debug.Log("     >>player energ:: " + player.attrs.energ);
        }

        public override void exitAction()
        {
            //Debug.Log("exited walk");
            //Debug.Log("     >>player energ:: " + player.attrs.energ);
        }

        public override bool execute() {
            player.dest.x = (int)player.transform.position.x + player.walkDir.x;
            player.dest.y = (int)player.transform.position.y + player.walkDir.y;
            //player.dest = player.pos + player.walkDir;
            if (player.dest.x >= player.level.worldw || player.dest.x < 0 || player.dest.y < 0 || player.dest.y >= level.worldh)
            {
                return true;
            }
            LevelMaster.CellFlag cell = level.cells[(int)player.dest.x, (int)player.dest.y];
            if ((cell & LevelMaster.CellFlag.BLOCKMV) != 0 ) {
                return true;
            }
            if ((cell & LevelMaster.CellFlag.DOOR) != 0) {
                player.interractDoor(player.dest);  //nu contine walk
                                                    //			player.steering.move (player.walkDir.x, player.walkDir.y);
                return true;
            }
            if ((cell & LevelMaster.CellFlag.NPC) !=0) {
                //Creatura target = actor.level.getCreaturAt(npos[0], npos[1]);
                ////                actor.target= target;
                //actor.removeTarget();
                //actor.setTarget(target);
                //actor.act = new GenAction.AtkMelee(actor, target, actor.getAtkCost());
                return true;
            }

            //if (cell.contains(NPC))
            //    actor.swapNPC((Npc)actor.level.getCreaturAt(npos[0], npos[1]));  //contine swap doar pt npc
            if ((cell & LevelMaster.CellFlag.TRAP) != 0) {
                //actor.interractTrap(npos[0], npos[1]);
                return true;
            }
                
            //nimic nu bloc.
            level.cells[(int)player.transform.position.x, (int)player.transform.position.y] &= ~LevelMaster.CellFlag.HERO;
            level.cells[(int)player.dest.x, (int)player.dest.y] |= LevelMaster.CellFlag.HERO;
            //player.pos = player.dest;
            player.steering.move(player.walkDir.x, player.walkDir.y);
            return true;
        }
    }
}
