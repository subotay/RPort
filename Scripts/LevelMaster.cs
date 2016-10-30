using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using Tiled2Unity;
using System.Collections.Generic;

public class LevelMaster : MonoBehaviour {
	bool loading;
	[FlagsAttribute]
	public enum CellFlag {
		NONE =0, MONST =1, NPC =2, HERO =4, 
		DOOR =8, TRIGG =16, TRAP =32, LOOT =64, 
		BLOCKMV = 128, BLOCKV= 256
	}
	public CellFlag[,] cells;
	public GameObject[] doors;
	public GameObject[] tranzits;
	public Player player;
    public List<Npc> npcs;
    //enemies, npcs

    /**
 *0- waiting for input
 *1- can process player 
 *2- processing player 
 *3- can start processing others 
 *4- processing others
*/
    public enum TurnPhase { WAITING, START_PLAYER, PROC_PLAYER, START_OTHERS, PROC_OTHERS }
	public TurnPhase phase = TurnPhase.WAITING;
	int turn =0;
	public int TICKS = 20 ;
    public int COST = 100;

	GameObject level;
	TiledMap tiledMap;
	public int worldw, worldh, tilew, tileh;

	void Awake(){
		loading = true;
	}

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
        cells[(int)player.pos.x, (int)player.pos.y] |= CellFlag.HERO;
        npcs = new List<Npc>();
        GameObject[] npcarr = GameObject.FindGameObjectsWithTag("npc");
        foreach (GameObject npc in npcarr) {
            npcs.Add(npc.GetComponent<Npc>());
        }
    }
	
	// Update is called once per frame
	
	void Update () {
        turn = (turn + TICKS) % 100;
        if (loading || phase == TurnPhase.WAITING)
			return;
		if (phase == TurnPhase.START_PLAYER) 
			processPlayer ();
		else if (phase == TurnPhase.START_OTHERS)
			processOthers ();
	}

	void processPlayer(){
		phase = TurnPhase.PROC_PLAYER;
		Debug.Log(">>>>> turn :: " + turn);
		player.attrs.energ += TICKS;
		if (player.attrs.energ >= /*player.actM.cost*/  COST){
            Debug.Log("player energ:: " + player.attrs.energ);
            bool done = player.actM.processAct();
            while (!done)
                done = player.actM.processAct();
            player.attrs.energ -= /*player.actM.cost*/  COST;
            player.actM.idle = true;
        }
		//update AI target, lastHitter TODO
		phase = TurnPhase.START_OTHERS;
	}

	void processOthers(){
		phase = TurnPhase.PROC_OTHERS;
		foreach (Npc npc in npcs) {
            npc.attrs.energ += TICKS-5;
            if (npc.attrs.energ >= /*npc.actM.cost*/ COST)
            {
                Debug.Log("npc energ:: " + npc.attrs.energ);
                npc.stateM.updateState();
                bool done = false;
                while (!done)
                    done = npc.actM.processAct();
                npc.attrs.energ -= /*npc.actM.cost*/ COST;
                npc.actM.idle = true;
            }
        }
        phase = player.actM.idle ? TurnPhase.WAITING : TurnPhase.START_PLAYER;
	}

	public void randomGeneration(){
		//...
	}

	public void importLevel(string lvlName){
		Debug.Log ("Setting up level: " + lvlName);
		UnityEngine.Object levelPrefab = Resources.Load(lvlName);
		if (levelPrefab != null) {
			level = Instantiate (levelPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			tiledMap = level.GetComponent<TiledMap> ();
			level.transform.position = new Vector3 (0, tiledMap.NumTilesHigh, 0);
			initCells ();
			Camera.main.GetComponent<CameraFollow> ().enabled = true;
		} else
			Debug.Log ("Could not import level "+lvlName);
		loading = false;
	}

	void initCells(){
		worldw = tiledMap.NumTilesWide;
		worldh = tiledMap.NumTilesHigh;
		cells = new CellFlag[worldw, worldh];

		List<Vector2> wallsPositions = GameObject.FindGameObjectWithTag ("wall").GetComponent<ParsingUtil> ().positions;
		foreach (Vector2 pos in wallsPositions){
			cells [(int)pos.x, worldh +(int)pos.y] |=  CellFlag.BLOCKMV | CellFlag.BLOCKV;
		}
		List<Vector2> decorPositions = GameObject.FindGameObjectWithTag ("decor").GetComponent<ParsingUtil> ().positions;
		foreach (Vector2 pos in decorPositions){
			cells [(int)pos.x, worldh +(int)pos.y] |=  CellFlag.BLOCKMV;
		}
		doors= GameObject.FindGameObjectsWithTag ("door") ;
		foreach (GameObject door in doors){
			cells [(int)door.transform.position.x, (int)door.transform.position.y] |= CellFlag.DOOR;
		}
		tranzits = GameObject.FindGameObjectsWithTag ("tranzit");
		foreach (GameObject tranz in tranzits){
			cells [(int)tranz.transform.position.x, (int)tranz.transform.position.y] |= CellFlag.TRIGG;
		}
        destroyRedundantObjects ();
	}

	void destroyRedundantObjects(){
		//
	}


}
