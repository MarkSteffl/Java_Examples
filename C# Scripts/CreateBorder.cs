using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBorder : MonoBehaviour {

	//Declare ray elements
	Ray myRay;
	RaycastHit hit;

	//Declare borderPoint prefab
	public GameObject borderPoint;

	public GameObject disabled;

	GameObject floor;
	GameObject parentPoint;

	int numOfPoints = 0;
	Vector3 previousPoint;

	LineRenderer borderLine;
	MeshRenderer floorRenderer;

	// Use this for initialization
	void Start () {
		parentPoint = GameObject.Find("parentPointBorder");

	} //End of Start
		

	// Update is called once per frame
	void Update () {
		floor = GameObject.Find ("floor");
		floorRenderer = floor.GetComponent<MeshRenderer> ();

		myRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		LineRenderer borderLine = GetComponent (typeof(LineRenderer)) as LineRenderer;

		//If a ray hits a valid area (The Floor)
		if (Physics.Raycast(myRay, out hit) && disabled.activeInHierarchy == true) {
			if (Input.GetMouseButtonDown (0)) {


				
				//Create a point where the ray hit

				GameObject point = Instantiate (borderPoint, hit.point, Quaternion.identity) as GameObject;
				point.transform.parent = parentPoint.transform;

				//add new vertex to the LineRenderer
				borderLine.positionCount = numOfPoints + 1;

				//assign position to the vertex
				borderLine.SetPosition(numOfPoints, hit.point );

				//Add a BoxCollider onto the newly created LineRenderer segment
				if (numOfPoints >= 1) {
					AddColliderToLine (borderLine, previousPoint, hit.point); 
				}

				//Set the previous hit point as the current hit point
				previousPoint = hit.point;

				//increment the number of points
				numOfPoints++;

			}
		}

	} //End of Update


	// This function is used to add BoxColliders to LineRenderer segments
	private void AddColliderToLine(LineRenderer line, Vector3 startPoint,
								    Vector3 endPoint){

		//create a new BoxCollider object
		BoxCollider lineCollider = new GameObject ("Line Collider").AddComponent<BoxCollider> ();

		//Set the new BoxCollider to the "Ignore Raycast" layer
		//so new points can't be placed on other BoxColliders
		lineCollider.transform.gameObject.layer = LayerMask.NameToLayer ("Ignore Raycast");

		//Pair the BoxCollider to the LineRenderer segment 
		lineCollider.transform.parent = line.transform;

		//Set the width and length of the BoxCollider
		//TODO: Make height a variable so it doesn't have to be hardcoded in
		float lineWidth = line.endWidth;
		float lineLength = Vector3.Distance (startPoint, endPoint);
		lineCollider.size = new Vector3 (lineWidth, 2.4f, lineLength);

		//Center the BoxCollider over the LineRenderer segment
		Vector3 midPoint = (startPoint + endPoint) / 2;
		lineCollider.transform.position = midPoint;

		//Match the angle of the BoxCollider with the angle of the LineRenderer segment
		float angle = Mathf.Atan2 ((endPoint.z - startPoint.z), (endPoint.x - startPoint.x));
		angle *= Mathf.Rad2Deg;
		angle *= -1;
		lineCollider.transform.Rotate (0, angle + 90, 0);

	} // End of AddColliderToLine

	public void featureEnabled(){
		//Set up a LineRenderer component in the main camera (Where this script is located)
		borderLine = gameObject.AddComponent (typeof(LineRenderer)) as LineRenderer;
		borderLine.startColor = Color.yellow;
		borderLine.startWidth = .01f;
		borderLine.positionCount = 0;
		borderLine.receiveShadows = false;

		floorRenderer.enabled = true;
		floor.transform.gameObject.layer = LayerMask.NameToLayer ("Default");
		borderLine.enabled = true;
		parentPoint.SetActive (true);
		borderLine.positionCount = 0;
		numOfPoints = 0;
	}

	public void featureDisabled(){
		floorRenderer.enabled = false;
		floor.transform.gameObject.layer = LayerMask.NameToLayer ("Ignore Raycast");
		borderLine.enabled =  false;
		parentPoint.SetActive (false);
		borderLine.positionCount = 0;
		numOfPoints = 0;

		Destroy (borderLine);
	}

} // End of Class

