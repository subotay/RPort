using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Creaturi
{
    public abstract class Action
    {
        protected Creature creature;
        protected LevelMaster level;
        public int cost;

        public Action(Creature creature) {
            this.creature = creature;
            level = GameObject.Find("LevelMaster").GetComponent<LevelMaster>();
        }
        public abstract void enterAction();
        public abstract void exitAction();
        public abstract bool execute();
    }
}
