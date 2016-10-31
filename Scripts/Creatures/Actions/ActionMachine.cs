using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Creaturi.States
{
    public class ActionMachine : UnityEngine.MonoBehaviour
    {
        [HideInInspector]public Creature creature;
        public bool idle;
        public Action action;
        public string act; //debug
        public Action prevAction;
        public int cost
        {
            get { return action.cost; }
        }

        public Dictionary<string, Action> acts;

        public void Awake()
        {
            acts = new Dictionary<string, Action>();
            creature = gameObject.GetComponent<Creature>();
            acts.Add("idle", new Idle(creature));
            //acts.Add("walk", new Walk(creature)); //playerwalk vs walk
            acts.Add("atk", new Attack(creature));
            action = acts["idle"];
            prevAction = acts["idle"];
        }

        public void set(string newACtion)
        {
            action.exitAction();
            prevAction = action;
            idle = false;
            act = newACtion;
            action = acts[newACtion];
            action.enterAction();
        }
        

        public bool processAct()
        {
            return action.execute();
        }

        
    }
}
