using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BallMover : MonoBehaviour {

	public GameObject collBallPrefab;
	private GameObject collBallGO;
	public Text label;
	public GameObject controller;
	public GameObject BallMaker;
	private int maxTaps;
	private int totalFlowers;
	public GameObject[] flowers;
	public int[] flowerTaps;
	private float[] height;
	private Vector3[] flowerLOC;

	// Use this for initialization
	void Start () {
		//maxTaps = BallMaker.GetComponent<BallMaker2> ().maxTaps;
		totalFlowers = BallMaker.GetComponent<BallMaker2> ().totalFlowers;
		flowers =  GameObject.FindGameObjectsWithTag ("flower");
		flowerTaps = new int[] { 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
		height = new float[100];
		height [0] = -.1f;
		for (int i=1; i<100;i++){
			//
			height[i] = height[i-1]+.05f;
			//Debug.Log ("heights "+height[i]);
		}
		flowerLOC= controller.GetComponent<ModeSwitcher2>().flowerLocations;

	}
	
	// Update is called once per frame
	void Update () {
		//maxTaps = BallMaker.GetComponent<BallMaker2> ().maxTaps;
		maxTaps = controller.GetComponent<ModeSwitcher2> ().maxTaps;
		//Debug.Log("max taps: "+ maxTaps);
		//var step = 2 * Time.deltaTime;


		if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
		{
			Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit raycastHit;

			if (Physics.Raycast(raycast, out raycastHit))
			{
				var hitPoint = raycastHit.point;
				Vector3 temp = raycastHit.transform.position;
				GameObject selectedObject = raycastHit.collider.gameObject;
				//label.text = "something hit ";

				if (raycastHit.collider.name == "flower")
				{

				}
				//OR with Tag

				if (raycastHit.collider.gameObject.CompareTag("flower")) {
					
					int flowerIndex = System.Array.IndexOf (flowers, selectedObject);
					label.text ="FLOWER TAPPED: "+flowerIndex+" maxTaps: "+maxTaps;
					Debug.Log ("FLOWER TAPS: "+flowerTaps [flowerIndex]);

					if (flowerTaps [flowerIndex] < maxTaps) {
						//get a bee 
						GameObject topBee = controller.GetComponent<ModeSwitcher2> ().beeStack.Pop ();
						//Vector3 tappedFlower = new Vector3 (selectedObject.transform.position.x, selectedObject.transform.position.y + .1f, selectedObject.transform.position.z);
						GameObject parental = topBee.transform.parent.gameObject;
					
						//GameObject parental = new GameObject();
						topBee.transform.parent = parental.transform;

						//scale bee down, parent bee to flower, set position to flower
						//Destroy (topBee.GetComponent<Rigidbody> ());
						parental.transform.localScale = new Vector3 (.2f, .2f, .2f);
						parental.transform.SetParent (selectedObject.transform);
						parental.transform.localPosition = new Vector3 (0, height[(flowerTaps[flowerIndex])], 0);

						Rigidbody beeRigid = parental.GetComponent<Rigidbody>();
						//Debug.Log ("tappedFlower location: "+selectedObject.transform.position.x +", "+ selectedObject.transform.position.y +", "+ selectedObject.transform.position.z);
						Debug.Log ("height: " + height [(flowerTaps [flowerIndex])]);
						//Debug.Log ("tappedFlower location: "+flowerPS.x +", "+ flowerPS.y +", "+ flowerPS.z);
						//Debug.Log ("tappedFlower location: "+flowerPS.x +", "+ flowerPS.y +", "+ flowerPS.z);
						//beeRigid.DOMove(new Vector3(0,height[(flowerTaps[flowerIndex])],0),3,false);
						//temp2.DORotate (new Vector3 (0, 360, 0), 5, RotateMode.FastBeyond360);

						flowerTaps [flowerIndex]++;

					} else {
						label.text ="FLOWER MAXED OUT TAPPED";

					}
				} 
				if (raycastHit.collider.CompareTag ("bee")) {


					//label.text = "bee clicked at "+ selectedObject.transform.position.x;

				}
				else {
					//label.text = raycastHit.collider.tag+" clicked at " +selectedObject.transform.position.x;
					label.text = "";
				}
			}
		}
	}
	void arrayPlacement(Vector3 rootPos){
		//

	}
}
