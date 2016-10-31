using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Creaturi
{
    public abstract class Action
    {
        protected Creature actor;
        protected LevelMaster level;
        public int cost;

        public Action(Creature creature) {
            this.actor = creature;
            level = GameObject.Find("LevelMaster").GetComponent<LevelMaster>();
        }
        public abstract void enterAction();
        public abstract void exitAction();
        public abstract bool execute();
    }
}
