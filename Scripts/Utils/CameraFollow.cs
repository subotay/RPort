using UnityEngine;
using System.Collections;
using Tiled2Unity;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float damping = 0.25f;
	public float lookAheadFactor = 1.25f;
	public float lookAheadReturnSpeed = 0.5f;
	public float lookAheadMoveThreshold = 0.1f;


	private float m_OffsetZ;
	private Vector3 m_LastTargetPosition;
	private Vector3 m_CurrentVelocity;
	private Vector3 m_LookAheadPos;


	private float viewh, vieww;
	private float worldh, worldw;


	// Use this for initialization
	private void Start()
	{	
		transform.position = new Vector3 (target.position.x, target.position.y, -10);
		viewh = Camera.main.orthographicSize;
		vieww = viewh * Camera.main.aspect;
		TiledMap world = GameObject.FindGameObjectWithTag ("world").GetComponent<TiledMap> ();
		worldw = world.NumTilesWide;
		worldh = world.NumTilesHigh;
		//transform.position = target.position;
		m_LastTargetPosition = target.position;
		m_OffsetZ = (transform.position - target.position).z;
		transform.parent = null;
	}

	Vector3 checkWorldBounds(Vector3 pos){
		Vector3 newpos = pos;
		if (pos.x < vieww) {
			newpos.x = vieww; 
		}
		if (pos.x > worldw - vieww){
			newpos.x=  worldw - vieww;
		}
		if (pos.y < viewh){
			newpos.y = viewh;
		}
		if (pos.y > worldh-viewh){
			newpos.y = worldh - viewh;
		}
		return newpos;
	}

	// Update is called once per frame
	private void Update()
	{
		

				
		// only update lookahead pos if accelerating or changed direction
		float ymove = (target.position - m_LastTargetPosition).y;
		float xmove  = (target.position - m_LastTargetPosition).x;
		bool updateLookAheadTarget_y = Mathf.Abs (ymove) > lookAheadMoveThreshold;
		bool updateLookAheadTarget_x =  Mathf.Abs(xmove) > lookAheadMoveThreshold ;

		if (updateLookAheadTarget_y)
		{
			m_LookAheadPos = lookAheadFactor * Vector3.up * Mathf.Sign (ymove);
		} else if (updateLookAheadTarget_x){
			m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign (xmove);
		}
		else
		{
			m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
		}

		Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
		Vector3 boundedPos = checkWorldBounds (aheadTargetPos);
		if (boundedPos.Equals (aheadTargetPos)) {
			Vector3 newPos = Vector3.SmoothDamp (transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);
			transform.position = newPos;
		} else {
			Vector3 newPos = Vector3.SmoothDamp (transform.position, boundedPos, ref m_CurrentVelocity, damping);
			transform.position = newPos;
		}

		m_LastTargetPosition = target.position;
	}
}
