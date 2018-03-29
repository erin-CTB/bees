using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;


public class BallMover : MonoBehaviour {

	public GameObject collBallPrefab;
	private GameObject collBallGO;
	public Text label;
	public GameObject controller;
	public GameObject BallMaker;
	private int maxTaps;
	private int totalFlowers;
	private GameObject[] flowers;
	private int[] flowerTaps;

	// Use this for initialization
	void Start () {
		maxTaps = BallMaker.GetComponent<BallMaker2> ().maxTaps;
		totalFlowers = BallMaker.GetComponent<BallMaker2> ().totalFlowers;
		flowers =  GameObject.FindGameObjectsWithTag ("flower");
		flowerTaps = new int[] { 0, 0, 0, 0, 0, 0 };
		//Debug.Log("max taps: "+ maxTaps);
	}
	
	// Update is called once per frame
	void Update () {
		var step = 2 * Time.deltaTime;
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
					flowerTaps [flowerIndex]++;

					if (flowerTaps [flowerIndex] < maxTaps) {
						//get a bee 
						GameObject topBee = controller.GetComponent<ModeSwitcher2> ().beeStack.Pop ();
						Vector3 tappedFlower = new Vector3 (selectedObject.transform.position.x, selectedObject.transform.position.y + .1f, selectedObject.transform.position.z);
						GameObject parental = topBee.transform.parent.gameObject;

						//scale bee down, parent bee to flower, set position to flower
						parental.transform.localScale = new Vector3 (.3f, .3f, .3f);
						parental.transform.SetParent (selectedObject.transform);
						parental.transform.localPosition = new Vector3 (0, .1f, 0);

						Destroy (topBee.GetComponent<Rigidbody> ());
					} else {
						
					}
					label.text ="FLOWER TAPPED";
				} 
				if (raycastHit.collider.CompareTag ("bee")) {


					label.text = "bee clicked at "+ selectedObject.transform.position.x;

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
