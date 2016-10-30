using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	private SpriteRenderer rend;
	[HideInInspector]public bool opened ;

	public Sprite openedSprite;
	public Sprite closedSprite;



	// Use this for initialization
	void Awake(){
		opened = false;
		rend = gameObject.GetComponent<SpriteRenderer> ();
	}

	public void open(bool val){
		opened = val;
		rend.sprite = val ? openedSprite : closedSprite;
	}

	
}