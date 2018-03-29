using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;

public class BallMaker2 : MonoBehaviour {

	public GameObject flower1;
	public GameObject island;
	public float createHeight;
	public GameObject beeCreator;
	private Vector3 diff;
	public int maxTaps;
	public int totalFlowers;
	//private int[] flowers;

	//private MaterialPropertyBlock props;
	//public GameObject myToggle;

	// Use this for initialization
	void Start () {
		//props = new MaterialPropertyBlock ();
		diff = new Vector3(.3f,0,0);
		totalFlowers = 6;
		maxTaps = 6;


	}

	void CreateBall(Vector3 atPosition, GameObject dingdong)
	{
		GameObject ballGO = Instantiate (dingdong, atPosition, Quaternion.identity);
		if (dingdong == flower1) {
			
			ballGO.tag = "flower";

		}

	}

	// Update is called once per frame
	void Update () {
		
		if (Input.touchCount > 0 )
		{
			var touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began)
			{
				var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
				ARPoint point = new ARPoint {
					x = screenPosition.x,
					y = screenPosition.y
				};
						
				List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, 
					ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent);
				if (hitResults.Count > 0) {
					foreach (var hitResult in hitResults) {
						Vector3 position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
						Vector3 placeLevel = new Vector3 (position.x, position.y + createHeight, position.z);
						levelator (placeLevel, 6, 36);

						//Level1(placeLevel);
						break;
					}
				}

			}
		}

	}
	/*
	void Level1(Vector3 pos){
		//
		Vector3 flowerPos= pos+ new Vector3(0,0.3f,0);
		CreateBall(pos, island);
		CreateBall(flowerPos, flower1);
		flowerPos = flowerPos + diff;
		CreateBall(flowerPos, flower1);;
		flowerPos = flowerPos + diff;
		beeCreator.GetComponent<ModeSwitcher2> ().placement (9);
	}
	*/

	void levelator(Vector3 pos, int totalFlowers, int totalBees){
		//
		Vector3 flowerPos= pos+ new Vector3(0,0.4f,0);
		CreateBall(pos, island);
		flowerPos = flowerPos - new Vector3 (.3f, 0, .3f);
		for (int i = 0; i < totalFlowers/3; i++) {
			for (int p=0; p<3; p++){
				CreateBall(flowerPos, flower1);;
				flowerPos = flowerPos + diff;
			}
			flowerPos = flowerPos-new Vector3(.9f, 0, .3f);
		}
		beeCreator.GetComponent<ModeSwitcher2> ().placement (totalBees-1);
		//flowers= GameObject.FindGameObjectsWithTag ("flower");
	}  

	void Level3(){
		//
	}
	void resetLevel(){
		//
	}

}

