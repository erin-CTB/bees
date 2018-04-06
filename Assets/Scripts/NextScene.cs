using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class NextScene : MonoBehaviour {
	//public GameObject DND;
	private int lvl;
	public GameObject parentFlower;
	public GameObject flower;
	private Rigidbody temp;
	//public Text alert;
	// Use this for initialization
	void Start () {
		//lvl = DontDestroyOnLoad.Instance.currentLevel;
		//lvl = DND.GetComponent<DontDestroyOnLoad>().currentLevel;
		lvl=keeper.Instance.currentLevel;
		//Rigidbody temp = currentFlower.GetComponent<Rigidbody>();
		//Vector3 rot = new Vector3 (0, 360,0);
		//temp.DORotate (rot, 1f).SetLoops (-1, LoopType.Incremental);
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

	public void collectFlower(){
		//
		Rigidbody temp = parentFlower.GetComponent<Rigidbody>();
		Rigidbody temp2 = flower.GetComponent<Rigidbody> ();
		temp2.DOMove(new Vector3(-7,-1,0),1,false);
		temp2.DORotate (new Vector3 (0, 360, 0), 5, RotateMode.FastBeyond360);
		//temp.DORotate (new Vector3 (0, 360, 0), 1, 0);
	}
}
