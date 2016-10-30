using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Creatures.States;

public class StateManager : MonoBehaviour {

    public Npc npc;
    public AIState state;
    public string st;//debug
    public AIState prevState;
    public Dictionary<string, AIState> states;
	// Use this for initialization

	void Start () {
        npc = gameObject.GetComponent<Npc>();
        states = new Dictionary<string, AIState>();

        states.Add("rest", new RestAI(npc));
        states.Add("pursue", new PursuingAI(npc));
        states.Add("combat", new CombatAI(npc));
        states.Add("return", new ReturnAI(npc));

        state = states["rest"];
        prevState = states["rest"];
        //TODO add states from pool
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void updateState() {
        state.reason();
    }

    public void changeState(string newState)
    {
        state.exitState();
        prevState = state;
        st = newState;
        state = states[newState];
        state.enterState();
    }
}

