using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keeper : MonoBehaviour {
	public static keeper Instance;
	public int currentLevel;
	// Use this for initialization
	void Start () {
		currentLevel = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake ()   
	{

		//Debug.Log ("random scene: " + activeFirst);
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy (gameObject);
		}
	}

	public void nextLevel(){
		//
		currentLevel++;
	}
}
