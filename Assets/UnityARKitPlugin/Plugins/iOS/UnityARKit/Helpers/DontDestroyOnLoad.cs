using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DontDestroyOnLoad: MonoBehaviour {
	public static DontDestroyOnLoad Instance;
	public int currentLevel;
	// Use this for initialization


	void Start () {
		currentLevel = 1;
        //DontDestroyOnLoad (gameObject);
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
	
	// Update is called once per frame
	void Update () {
		
	}



}
