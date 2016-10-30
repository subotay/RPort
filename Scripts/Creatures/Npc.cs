using UnityEngine;
using System.Collections;
using Assets.Scripts.Creaturi.States;


public class Npc : Assets.Scripts.Creaturi.Creature
{
    //TODO group - tag

    public StateManager stateM;

    
    // Use this for initialization
    protected override void Start () {
        faction = 1;
        stateM = gameObject.GetComponent<StateManager>();
        actM.acts.Add("walk", new Walk(this));
    }

    // Update is called once per frame
    protected override void Update () {
        //if (idle) stateM.proceesState();
	}

    public override bool invalid(float x, float y) {
        if (!(x >= 0 && x < level.worldw && y >= 0 && y < level.worldh))
            return true;
        LevelMaster.CellFlag cell = level.cells[(int)x, (int)y];
        return ((cell & LevelMaster.CellFlag.BLOCKMV) != 0);
    }
}
