using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gardenController : MonoBehaviour {
	public RawImage popUp;
	public InputField numberOfFlowers;
	public int gardenFlowers;
	public int gardenBees;
	public int level;

	public GameObject maker;
	public GameObject mover;
	// Use this for initialization
	void Start () {
		level = keeper.Instance.currentGardenLevel;
		gardenBees = keeper.Instance.gardenBees [level];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void plusButton(){
		//
		Debug.Log("plus button clicked");
		popUp.gameObject.SetActive(true);
	}

	public void createHowMany(){
		//

		int temp;
		int.TryParse( numberOfFlowers.text, out temp);
		gardenFlowers = gardenFlowers + temp;
		Debug.Log("Total Flowers: "+gardenFlowers);
		popUp.gameObject.SetActive (false);
	}
}
