using UnityEngine;
using System.Collections;

	
public class StartScript : MonoBehaviour
{
	public GameObject gameController;
	//GameManager prefab to instantiate.
	public GameObject soundController;
	//SoundManager prefab to instantiate.
		
		
	void Awake ()
	{
		//Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
		if (GameController.inst == null)
				
				//Instantiate gameManager prefab
				Instantiate (gameController);
			
		//Check if a SoundManager has already been assigned to static variable GameManager.instance or if it's still null
		if (SoundController.inst == null)
				
				//Instantiate SoundManager prefab
				Instantiate (soundController);
	}
}
