using UnityEngine;
using System.Collections;
using Assets.Scripts.Creaturi;

public abstract class AIState
{
    protected Npc npc;
    protected LevelMaster level;
    protected Player player;
    
    public AIState(Npc npc)
    {
        this.npc = npc;
        level = GameObject.Find("LevelMaster").GetComponent<LevelMaster>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    public abstract void enterState();
    public abstract void exitState();
    public abstract void reason();
    public abstract void act();
}
