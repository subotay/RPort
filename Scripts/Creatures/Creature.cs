﻿using Assets.Scripts.Creaturi.States;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Creaturi
{
    public abstract class Creature : UnityEngine.MonoBehaviour
    {
        public Attributes attrs;

        public Steering steering;
        public Vector2 walkDir;
        public Vector2 home, dest;

        private Vector2 _pos = new Vector2();
        public Vector2 pos {
            get {
                _pos.x = (int)transform.position.x;
                _pos.y = (int)transform.position.y;
                return _pos;
            }
            set { }
        }
        
        [HideInInspector]public ActionMachine actM;
        public LevelMaster level;
        public Creature target;
        public bool dead;

        public int faction;
        public List<int> friends;
        public List<int> enemies;

        void Awake()
        {
            walkDir = UnityEngine.Vector2.zero;
            steering = gameObject.GetComponent<Steering>();
            attrs = gameObject.GetComponent<Attributes>();
            actM = gameObject.GetComponent<ActionMachine>();
            level = GameObject.Find("LevelMaster").GetComponent<LevelMaster>();
            target = null;
            dest = new Vector2(transform.position.x, transform.position.y);
            actM.idle = true;
        }

        protected abstract void Update();

        protected abstract void Start();

        public abstract bool invalid(float x, float y);

        public bool isFriend(Creature c) { return friends.Contains(c.faction); }

        public bool hasRangeTo(Creature c) {
            return Pathfind.cebDist(pos.x, pos.y, c.pos.x, c.pos.y) <= attrs.range;
        }

        public bool hasFovTo(Creature c) {
            return Pathfind.cebDist(pos.x, pos.y, c.pos.x, c.pos.y) <= 5;       //TODO Fov
        }

        public Creature scanFov() {     //TODO fov
            if (this.hasFovTo(level.player) && !isFriend(level.player) && !level.player.dead)
                return level.player;
            else
                return null;
        }

        public bool strongerThan(Creature c) {
            return attrs.hp.Val >=  c.attrs.hp.Val;
        }

        public bool hasTarget() {
            return (target != null && target.dead == false);
        }

        public virtual void onDeath() {
            attrs.hp.Val = 0;
            dead = true;
            level.cells[(int)pos.x, (int)pos.y] &= ~LevelMaster.CellFlag.NPC;
            level.npcs.Remove((Npc)this);
        }
    }
}
