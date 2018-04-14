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
	public Text levelText;
	public Text tapText;
	public RawImage checkBox;
	//public Text alert;
	// Use this for initialization
	void Start () {
		
		lvl=keeper.Instance.currentLevel;
		int createdBees = keeper.Instance.lvlBees [keeper.Instance.currentLevel];
		tapText.text = tapText.text + " "+keeper.Instance.numTotalTaps+" "+createdBees;

		if (keeper.Instance.numTotalTaps == createdBees) {
			//
			checkBox.gameObject.SetActive (true);
		} else {
			checkBox.gameObject.SetActive (false);
		}
		Debug.Log ("current level"+ keeper.Instance.currentLevel);
		//Rigidbody temp = currentFlower.GetComponent<Rigidbody>();
		//Vector3 rot = new Vector3 (0, 360,0);
		//temp.DORotate (rot, 1f).SetLoops (-1, LoopType.Incremental);
		//flower.GetComponent<Rigidbody>().DORotate (new Vector3 (0, 360, 0), 5, RotateMode.FastBeyond360);
		if (keeper.Instance.currentLevel%3==0){
			//
			createFlower ();
			levelText.text = keeper.Instance.currentFl;
		}
		else{
			//
			//levelText.text = "Current Level: "+ keeper.Instance.currentLevel.ToString();
		}

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
		//Rigidbody temp = parentFlower.GetComponent<Rigidbody>();
		Rigidbody temp2 = flower.GetComponent<Rigidbody> ();
		temp2.DOMove(new Vector3(-7,-1,0),1,false);
		StartCoroutine(wait());
		SceneManager.LoadScene ("Beez");
		//temp.DORotate (new Vector3 (0, 360, 0), 1, 0);
	}

	IEnumerator wait()
	{
		print(Time.time);
		yield return new WaitForSeconds(20);
		print(Time.time);
	}

	public void createFlower(){
		//keeper.Instance.currentFlower
		Vector3 atPosition = new Vector3 (0.153f,0.93f,-9.663f);
		GameObject currentFlower = Instantiate (keeper.Instance.currentFlower, atPosition, Quaternion.identity);
		currentFlower.GetComponent<Rigidbody>().DORotate (new Vector3 (0, 360, 0), 5, RotateMode.FastBeyond360);
		levelText.text = keeper.Instance.currentFl;
	}
}
