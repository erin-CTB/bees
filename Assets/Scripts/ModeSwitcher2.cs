using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.iOS;
using UnityEngine.SceneManagement;

public class ModeSwitcher2 : MonoBehaviour {

	//public GameObject ballMake;
	//public GameObject ballMove;

	//private int appMode = 0;


	public string numberOfBees;
	//public GameObject controllerContainer;

	//public Terrain terrain;
	public int numberOfObjects; // number of objects to place
	private int currentObjects; // number of placed objects
	public GameObject objectToPlace; // GameObject to place - bee
	public GameObject island;
	public GameObject flower;
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

	private int level = 1;
	public bool createBeesCalled;

	public int currentLevel;

	public GameObject creator;
	public GameObject mover;
	private Vector3 localLevelPosition;

	public int maxTaps;
	public GameObject DND;
	public Text levelText;



	// Use this for initialization
	void Start () {
		//set make or break mode
		setMode (true);
		
		// terrain size x
		terrainWidth = 3;
		// terrain size z
		terrainLength = 3;

		parentHolder = new GameObject("BeeParent");
		parentHolder.tag = "bee";
		//placement (numberOfObjects);
		createBeesCalled = false;
	


	}

	
	// Update is called once per frame
	void Update () {
		myPosition = Camera.main.gameObject.transform.position;
		localLevelPosition = creator.GetComponent<BallMaker2> ().levelPosition;
		grabValue = Mathf.RoundToInt(hSliderValue * 100);
		left.text = ((beeStack.Count).ToString ()+" bees left");
		//Debug.Log ("ALERT: "+createBeesCalled);

		if (beeStack.Count==0 && createBeesCalled==true){
			//level completed... go to next level!
			reset();
		}

		else if (totalBees == 0) {
			caught.text = "0 bees caught";
			//nextLevel.gameObject.SetActive (true);
		} else {
			caught.text = ((totalBees - beeStack.Count) + " bees caught");
		}
		levelText.text = keeper.Instance.currentLevel.ToString();

	}


	public void ResetScene() {
		ARKitWorldTrackingSessionConfiguration sessionConfig = new ARKitWorldTrackingSessionConfiguration ( UnityARAlignment.UnityARAlignmentGravity, UnityARPlaneDetection.Horizontal);
		UnityARSessionNativeInterface.GetARSessionNativeInterface().RunWithConfigAndOptions(sessionConfig, UnityARSessionRunOption.ARSessionRunOptionRemoveExistingAnchors | UnityARSessionRunOption.ARSessionRunOptionResetTracking);
	}





	//createBees
	public void placement(int boop){
		
		for ( int i = 0; i < mover.GetComponent<BallMover> ().flowerTaps.Length; i++)
		{
			mover.GetComponent<BallMover> ().flowerTaps[i] = 0;
		}

		createBeesCalled = true;
		totalBees = boop;
		//add all bees with "caught" or "bees" tag into the gem array
		/*
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
*/
		hives = GameObject.FindGameObjectsWithTag ("island");

		if (hives.Length == 0) {
			//float centerX = 0;
			//float centerZ = 0;
			Debug.Log("are there hives?"+hives.Length);
		} else {
			centerX = hives [0].gameObject.transform.position.x;
			centerZ = hives [0].gameObject.transform.position.z;
			Debug.Log("are there hives?"+hives.Length);
		}

		for (int i =0; i<boop; i++){
			//
			float posx = Random.Range (centerX-terrainWidth, centerX + terrainWidth);
			float posz = Random.Range (centerZ-terrainLength, centerZ + terrainLength);
			GameObject newObject = (GameObject)Instantiate (objectToPlace, new Vector3 (posx, 1, posz), Quaternion.identity);

			GameObject parental = (GameObject)Instantiate (parentHolder,new Vector3 (posx, 1, posz), Quaternion.identity);
			Debug.Log("PUSH!");
			newObject.transform.SetParent(parental.transform);
			newObject.transform.RotateAround (newObject.transform.position, new Vector3 (0, 1, 0), Random.Range (0, 360));
			parental.tag = "beeParent";
			newObject.tag = "bee";
			beeStack.Push (newObject);

		}
		//mover.GetComponent<BallMover> ().flowers = new GameObject[0];
		//System.Array.Clear(mover.GetComponent<BallMover> ().flowers,0, mover.GetComponent<BallMover> ().flowers.Length);

		Debug.Log("All bees created!");

	}

	public void setLevel(int x){
		//
		//reset();
		currentLevel = x;
		//currentLevel = DND.GetComponent<DontDestroyOnLoad> ().currentLevel;
		//Debug.Log("DND Current Level: "+DND.GetComponent<DontDestroyOnLoad> ().currentLevel);
		//currentLevel = DND.GetComponent<DontDestroyOnLoad> ().currentLevel;
		setLevels ();

	}

	void setLevels(){
		//destroy all islands, bees, flowers and reset which flowers are in the field, and which bees are in the stack
		Debug.Log ("LEVEL" + currentLevel);
		DestroyGameObjectsWithTag ("island");
		DestroyGameObjectsWithTag ("beeParent");
		DestroyGameObjectsWithTag ("flower");
		beeStack.Clear ();

		switch(currentLevel)
		{

		//
		case 0: break;
		case 1:
			//creator.GetComponent<BallMaker2> ().levelator (localLevelPosition, 3, 6);
			levelator (localLevelPosition, 3, 6);
			break;
		case 2:
			//creator.GetComponent<BallMaker2> ().levelator (localLevelPosition,6, 12);
			levelator (localLevelPosition,6, 12);
			break;
		case 3:
			//creator.GetComponent<BallMaker2> ().levelator (localLevelPosition, 9, 18);
			levelator (localLevelPosition, 9, 18);
			break;
		case 4:
			//creator.GetComponent<BallMaker2> ().levelator (localLevelPosition,12, 24);
			levelator (localLevelPosition,12, 24);
			break;
		case 5:
			//creator.GetComponent<BallMaker2> ().levelator (localLevelPosition,15, 30);
			levelator (localLevelPosition,15, 30);
			break;
		default:
			break;
		}

		Debug.Log ("level set and total bees: " + totalBees);
		Debug.Log ("beestack count: " + beeStack.Count);

	}

	public static void DestroyGameObjectsWithTag(string tag)
	{
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
		foreach (GameObject target in gameObjects) {
			GameObject.Destroy(target);
		}
	}

	public void levelator(Vector3 pos, int totalFlowers, int totalBees){

		maxTaps = totalBees/totalFlowers;
		Debug.Log ("max taps: "+maxTaps);
		Vector3 flowerPos= pos+ new Vector3(0,0.1f,0);
		creator.GetComponent<BallMaker2> ().CreateBall(pos, creator.GetComponent<BallMaker2> ().island);
		flowerPos = flowerPos - new Vector3 (.1f, 0, -.1f);

		for (int i = 0; i < totalFlowers / 3; i++) {
			for (int p = 0; p < 3; p++) {
				creator.GetComponent<BallMaker2> ().CreateBall (flowerPos, creator.GetComponent<BallMaker2> ().flower1);
				flowerPos = flowerPos + creator.GetComponent<BallMaker2> ().diff;
			}
			flowerPos = pos- new Vector3 (.1f,-.1f, .12f*(i));
		}
		//GameObject[] sizeofDaBeez = GameObject.FindGameObjectsWithTag ("flower");
		//System.Array.Resize<int>( ref mover.GetComponent<BallMover>().flowers, sizeofDaBeez.Length );
		Debug.Log ("total bees to be created: " + totalBees);
		setMode (false);
		placement (totalBees);
	} 

	public void reset(){
		//
		Debug.Log("lvl: "+keeper.Instance.currentLevel);
		ResetScene ();
		if (keeper.Instance.currentLevel < 5) {
			keeper.Instance.nextLevel();
			SceneManager.LoadScene ("youGotEm");
		}
		else{
			//
			SceneManager.LoadScene("Reward");
		}
	}

	public void setMode(bool enable){
		//
		creator.SetActive (enable);
		mover.SetActive (!enable);
	}
		

}
