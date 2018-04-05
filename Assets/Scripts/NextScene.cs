using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour {
	//public GameObject DND;
	private int lvl;
	//public Text alert;
	// Use this for initialization
	void Start () {
		//lvl = DontDestroyOnLoad.Instance.currentLevel;
		//lvl = DND.GetComponent<DontDestroyOnLoad>().currentLevel;
		lvl=keeper.Instance.currentLevel;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void goNext(){
		string currentScene = SceneManager.GetActiveScene ().name;
		//
		if (currentScene == "youGotEm") {
			//lvl++;
			Debug.Log ("LEVEL: " + lvl);
			//alert.text = "Beez-" + lvl;
			//keeper.Instance.nextLevel ();
			//DND.GetComponent<DontDestroyOnLoad> ().currentLevel = lvl;
			//SceneManager.LoadScene ("Beez-" + lvl);
			SceneManager.LoadScene ("Beez");
		}
		if (currentScene == "Reward") {

			//alert.text = "YOU WIN!";


		}
	}
}
