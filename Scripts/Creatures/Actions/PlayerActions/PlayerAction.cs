using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Creaturi.States.PlayerStates
{
    public abstract class PlayerAction : Action
    {
        protected Player player;
        public PlayerAction(Creature creature) : base(creature)
        {
            player = (Player)creature;
        }
    }
}
