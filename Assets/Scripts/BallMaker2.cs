using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallMaker2 : MonoBehaviour {

	public GameObject flower1;
	public GameObject island;
	public float createHeight;
	public GameObject beeCreator;
	public Vector3 diff;
	public int maxTaps;
	public int totalFlowers;
	public Vector3 levelPosition;
	public GameObject mover;
	private int currentFlower;
	public 	GameObject[] tempArray;
	//public GameObject DND;
	private int currentLevel;
	private bool placed = false;
	public Text notifier;


	// Use this for initialization
	void Start () {
		diff = new Vector3(.1f,0,0);
		totalFlowers = 6;
		//currentLevel = DND.GetComponent<DontDestroyOnLoad> ().currentLevel;
		currentLevel = keeper.Instance.currentLevel;
	}

	public void CreateBall(Vector3 atPosition, GameObject dingdong)
	{
		GameObject ballGO = Instantiate (dingdong, atPosition, Quaternion.identity);
		if (dingdong == flower1) {
			
			ballGO.tag = "flower";
			ballGO.transform.RotateAround (ballGO.transform.position, new Vector3 (0, 1, 0), Random.Range (0, 360));
			//mover.GetComponent<BallMover> ().flowers [currentFlower] = ballGO;

		}
		else if (dingdong == island){
			//
			ballGO.tag = "island";
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
						levelPosition = placeLevel;
						//notifier.text = "Ground Tapped "+currentLevel;
						//beeCreator.GetComponent<ModeSwitcher2> ().levelator (placeLevel, 3, 6);
						if (currentLevel < 6) {
							beeCreator.GetComponent<ModeSwitcher2> ().setLevel (currentLevel);
						}
						else{
							//
							SceneManager.LoadScene("Reward");
						}
						break;
					}

				}
			}
		}
	}
}

