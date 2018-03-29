using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeSwitcher2 : MonoBehaviour {

	//public GameObject ballMake;
	//public GameObject ballMove;

	//private int appMode = 0;


	public string numberOfBees;
	//public GameObject controllerContainer;

	//public Terrain terrain;
	public int numberOfObjects; // number of objects to place
	private int currentObjects; // number of placed objects
	public GameObject objectToPlace; // GameObject to place
	private int terrainWidth; // terrain size (x)
	private int terrainLength; // terrain size (z)
	private int terrainPosX; // terrain position x
	private int terrainPosZ; // terrain position z
	public GameObject[] gems;
	public GameObject[] beesCreated;
	//private int totalBees = 0;
	public int num2; //array num
	//private bool move = false;
	private GameObject[] hives;
	private float centerZ = 0;
	private float centerX = 0;
	private Vector3 myPosition;

	public int currentBee = 0;
	public Stack<GameObject> beeStack = new Stack<GameObject> ();

	public float hSliderValue = 0.0F;
	private int grabValue = 0;
	private int totalBees = 0;

	private GameObject parentHolder;
	public Text caught;
	public Text left;



	// Use this for initialization
	void Start () {


		
		// terrain size x
		terrainWidth = 3;
		// terrain size z
		terrainLength = 3;

		parentHolder = new GameObject("BeeParent");
		//placement (numberOfObjects);




	}

	
	// Update is called once per frame
	void Update () {
		myPosition = Camera.main.gameObject.transform.position;

		grabValue = Mathf.RoundToInt(hSliderValue * 100);
		left.text = (beeStack.Count.ToString ()+" bees left");
		if (totalBees == 0) {
			caught.text = "0 bees caught";
		} else {
			caught.text = ((totalBees - beeStack.Count + 1) + " bees caught");
		}


	}








	//createBees
	public void placement(int boop){
		totalBees = boop;
		//add all bees with "caught" or "bees" tag into the gem array
		gems = GameObject.FindGameObjectsWithTag ("caught");
		//destroy all caught bees
		for (int i = 0; i < gems.Length; i++) {
			Destroy (gems [i]);
			Debug.Log ("ALERT: caught bees destroyed");
		}
		beesCreated = GameObject.FindGameObjectsWithTag ("bee");
		Debug.Log ("BEESCREATED LENGTH: "+beesCreated.Length);
		for (int i = 0; i < beesCreated.Length; i++) {
			Destroy(beesCreated[i]);
			Debug.Log ("ALERT: uncaught bees destroyed");
		}

		//reset number of bees caught in the scene to 0
		currentBee = 0;
		beeStack.Clear ();

		currentObjects = 0;

		hives = GameObject.FindGameObjectsWithTag ("flower");

		if (hives.Length == 0) {
			//float centerX = 0;
			//float centerZ = 0;
			Debug.Log("are there hives?"+hives.Length);
		} else {
			centerX = hives [0].gameObject.transform.position.x;
			centerZ = hives [0].gameObject.transform.position.z;
			Debug.Log("are there hives?"+hives.Length);
		}

			// generate objects
		while (currentObjects <= boop) {
			// generate random x position
			float posx = Random.Range (centerX-terrainWidth, centerX + terrainWidth);
				// generate random z position
			float posz = Random.Range (centerZ-terrainLength, centerZ + terrainLength);
				// create new gameObject on random position
			GameObject newObject = (GameObject)Instantiate (objectToPlace, new Vector3 (posx, 1, posz), Quaternion.identity);

			GameObject parental = (GameObject)Instantiate (parentHolder,new Vector3 (posx, 1, posz), Quaternion.identity);
			//Debug.Log ("bee created");
			//newObject.transform.parent = parentHolder.transform;
			newObject.transform.SetParent(parental.transform);
			//Debug.Log ("bee created");
			newObject.transform.RotateAround (newObject.transform.position, new Vector3 (0, 1, 0), Random.Range (0, 360));
			newObject.tag = "bee";
			newObject.layer = 9;

			beeStack.Push (newObject);
			currentObjects += 1;
				//beesCreated[beesCreated.Length+1] = newObject;
		}
		
		Debug.Log("All bees created!");

	}


	public void lineUp(int howMany)
	{
		Debug.Log ("group by: "+howMany);
		Debug.Log ("stack length: "+beeStack.Count);
		Debug.Log ("current bee: "+currentBee);
		if ((beeStack.Count+1)>howMany) {
			Debug.Log ("lining up " + howMany + " bees");
			//gems = GameObject.FindGameObjectsWithTag ("bee");

			for (int i = 0; i < howMany; i++) {
				//pop the top bee off of the bee stack and assign to temp variable
				GameObject temp = beeStack.Pop ();
				//if the current bee's x position is greater than 0, move it down to zero
				if (temp.gameObject.transform.position.x > 0) {
					while (temp.gameObject.transform.position.x >= 0) {
				
						temp.gameObject.transform.Translate (-1, 0, 0, Space.World);

					}
				} 
			//if the current bee's x position is less than 0, move it up to zero
			else if (temp.gameObject.transform.position.x < 0) {
				
					while (temp.gameObject.transform.position.x <= 0) {

						temp.gameObject.transform.Translate (1, 0, 0, Space.World);

					}
				}
				//remove all tags from current bee, and add "caught" tag
				temp.gameObject.tag = null;
				temp.gameObject.tag = "caught";
			}
			//update caught bee count
			currentBee = currentBee + howMany;
			Debug.Log (currentBee + " bees have been caught");
		}
		else {
			Debug.Log ("no more bees to grab");
		}
	}








}
