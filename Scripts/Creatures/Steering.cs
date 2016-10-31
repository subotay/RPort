using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Creaturi;

public class Steering : MonoBehaviour {
	public float speed = 7f;
    public float targetDelta = 1f;
    public bool isMoving = false;

    LevelMaster level;
    Creature creature;
    Vector2 direction;
    Transform sprite;
    public List<Vector2> path;
    public int step;
    public Vector2 lastTargPos;

    void Start(){
        level = GameObject.Find("LevelMaster").GetComponent<LevelMaster>();
        creature = gameObject.GetComponent<Creature>();
        sprite = gameObject.transform.FindChild("Sprite");
        step = 0;
        //lastTargPos = Vector2.zero;
    }

	public void move(float dx,float dy){
		if ((dx == 0 && dy == 0) || isMoving)
			return;
		direction.x = dx;
		direction.y = dy;

        //transform.position = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, 0);
        StartCoroutine(moving());
    }

	private IEnumerator moving(){
        isMoving = true;
        LevelMaster.CellFlag type = (creature.GetType() == typeof(Player)) ? LevelMaster.CellFlag.HERO : LevelMaster.CellFlag.NPC;
        level.cells[(int)creature.transform.position.x, (int)creature.transform.position.y] &= ~type;
        level.cells[(int)creature.dest.x, (int)creature.dest.y] |= type;

		Vector3 startPos = transform.position;
		Vector3 endPos = new Vector3 (startPos.x + direction.x, startPos.y + direction.y, 0);
        //float t = 0;
        //while (t < 1f) {
        //    t += Time.deltaTime * speed;
        //    transform.position = Vector3.Lerp(startPos, endPos, t);
        //    yield return null;
        //}
        float dist = (endPos - startPos).sqrMagnitude;
		while(dist>float.Epsilon){
			transform.position = Vector3.MoveTowards (transform.position, endPos, Time.deltaTime * speed);
			dist = (endPos - transform.position).sqrMagnitude;
			yield return null;
		}

        //sprite.parent = creature.transform;
		isMoving = false;
		yield return null;
	}


    
    public void seek(Creature target) {
        if ((path == null) 
            || (Pathfind.cebDist(lastTargPos.x, lastTargPos.y, target.pos.x, target.pos.x) > targetDelta)) {
            Vector2 loc = target.pos;// + target.walkDir;
            path = Pathfind.pathJPS(creature,  loc.x, loc.y);
            step = 1;
        }
        lastTargPos = target.pos;
        processPath();
    }

    public void walkTo(Vector2 dest) {
        if (path == null) {
            path = Pathfind.pathJPS(creature, dest.x, dest.y);
            step = 1;
        }
        processPath();
    }

    private void processPath() {
        if (isMoving)
            return;
        if (step == path.Count) {
            resetPath();
            return;
        }
        Vector2 next = path[step];
        if (Pathfind.cebDist((int)creature.transform.position.x, (int)creature.transform.position.y, next.x, next.y) == 1) {
            step++;
            creature.walkDir = next - creature.pos;
            creature.actM.set("walk");
        } else {
            var x = Mathf.Clamp((next.x - (int)creature.transform.position.x), -1, 1);
            var y = Mathf.Clamp((next.y - (int)creature.transform.position.y), -1, 1);
            creature.walkDir.x = x;
            creature.walkDir.y = y;
            creature.actM.set("walk");
        }
    }

    public void resetPath() {
        path = null;
        step = 0;
        creature.walkDir = Vector2.zero;
    }

}
