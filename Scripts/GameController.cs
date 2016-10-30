using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GameController : MonoBehaviour {

	public static GameController inst;
	public string menuSceneName;
	public string worldSceneName;
	public string randomSceneName;

	private GameObject levelMaster;
	private string levelName;

	void Awake(){
		
		if (inst == null) {
			DontDestroyOnLoad (gameObject);
			inst = this;
		} else if (inst != this){
			Destroy (gameObject);
		}
	}

	public void play(){
		loadGame ();
		SceneManager.LoadScene ("Play");
	}

	public void quitGame(){
		Application.Quit ();
	}

	void OnLevelWasLoaded(int index){
		string loadedScene = SceneManager.GetActiveScene ().name;
		Debug.Log ("Loaded scene: "+ loadedScene);

		if (!loadedScene.Equals (menuSceneName)) {
			SoundController.inst.music.Stop ();
			levelMaster = GameObject.Find ("LevelMaster");

			if (loadedScene.Equals (randomSceneName)) {
				levelMaster.GetComponent<LevelMaster> ().randomGeneration ();
			} else {
				levelMaster.GetComponent<LevelMaster> ().importLevel (levelName);
			}
		}
	}

	public void saveGame(){
		if (!Directory.Exists ("ext_data"))
			Directory.CreateDirectory ("ext_data");
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream f = File.Create ("ext_data/sav.dat");
		bf.Serialize (f,"lev1");
		f.Close ();
	}

	void loadGame(){	
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream f = null;
		f = File.Open ("ext_data/sav.dat", FileMode.Open);
		levelName= (string)bf.Deserialize (f) ;
		f.Close ();
	}

}
