using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour {
	public static SoundController inst;

	public AudioSource music;
	public AudioSource sfx; //used by other components

	void Awake(){
		if (inst == null) {
			DontDestroyOnLoad (gameObject);
			inst = this;
		} else if (inst!= this)
			Destroy (gameObject);
	}


}
