using UnityEngine;
using System.Collections;
using Assets.Scripts.Creaturi.States;
using Assets.Scripts.Creaturi.States.PlayerStates;
using Assets.Scripts.Creaturi;

public class Player : Assets.Scripts.Creaturi.Creature
{
    public override bool invalid(float x, float y) {
        return false;
    }

    public Creature lastHitter;

	// Use this for initialization
	protected override void Start () {
        faction = 0;
        lastHitter = null;
        actM.acts.Add("walk", new PlayerWalk(this));
    }
	
	// Update is called once per frame
	protected override void Update () {
		
        if (level.phase == LevelMaster.TurnPhase.WAITING && steering.isMoving == false) {
            float dx = Input.GetAxisRaw("Horizontal");
            float dy = Input.GetAxisRaw("Vertical");
            float dz = Input.GetAxisRaw("Jump");
            if (!(dx == 0 && dy == 0)) {
                walkDir.x = dx;
                walkDir.y = dy;
                actM.set("walk");
                if (level.phase == LevelMaster.TurnPhase.WAITING /*&& steering.isMoving == false*/)
                    level.phase = LevelMaster.TurnPhase.START_PLAYER;
            } else if (dz != 0) {
                actM.set("idle");
                if (level.phase == LevelMaster.TurnPhase.WAITING /*&& steering.isMoving == false*/)
                    level.phase = LevelMaster.TurnPhase.START_PLAYER;
            }
        }
   }

	public void interractDoor(Vector2 pos){}
}
