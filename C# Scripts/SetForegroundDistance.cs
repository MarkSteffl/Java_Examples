using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetForegroundDistance : MonoBehaviour {
	Ray myRay;
	RaycastHit hit;

	//Declare GamObject (button)
	public GameObject enabled;
	public GameObject disabled;
	public GameObject enableDrawBorder;
	public GameObject enablePositionBackground;
	public GameObject enableSpawnCharacter;


	public GameObject borderPoint;

	GameObject floor;
	GameObject parentPoint;
	GameObject enableTrigger;
	GameObject disableTrigger;

	public int numOfPoints = 0;
	int triggerInstance = 0;
	Vector3 previousPoint;

	MeshRenderer floorRenderer;
	LineRenderer distanceLine;

	// Use this for initialization
	void Start () {
		parentPoint = GameObject.Find("parentPointDistance");
	}

	// Update is called once per frame
	void Update () {
		floor = GameObject.Find ("floor");
		floorRenderer = floor.GetComponent<MeshRenderer> ();

		myRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		distanceLine = GetComponent (typeof(LineRenderer)) as LineRenderer;

		//If a ray hits a valid area (The Floor)
		if (Physics.Raycast (myRay, out hit) && disabled.activeInHierarchy == true) {
			if (Input.GetMouseButtonDown (0)) {


				//Create a point where the ray hit
				GameObject point = Instantiate (borderPoint, hit.point, Quaternion.identity) as GameObject;
				point.transform.parent = parentPoint.transform;

				//add new vertex to the LineRenderer
				distanceLine.positionCount = numOfPoints + 1;

				//assign position to the vertex
				distanceLine.SetPosition (numOfPoints, hit.point);

				if (numOfPoints >= 1) {
					generateTriggerOnLine(distanceLine, previousPoint, hit.point); 
				}

				previousPoint = hit.point;

				//increment the number of points
				numOfPoints++;


			}
		}

	}

	private void generateTriggerOnLine(LineRenderer line, Vector3 startPoint,
		Vector3 endPoint){

		float lineWidth = 10f;
		float lineLength = 10f;
		Vector3 midPoint = (startPoint + endPoint) / 2;
		float angle = Mathf.Atan2 ((endPoint.z - startPoint.z), (endPoint.x - startPoint.x));
		angle *= Mathf.Rad2Deg;
		angle *= -1;

		enableTrigger = new GameObject ("enableTrigger");
		enableTrigger.AddComponent<BoxCollider> ();
		enableTrigger.GetComponent<BoxCollider> ().isTrigger = true;
		enableTrigger.AddComponent < EnableMeshTrigger> ();
		enableTrigger.GetComponent<EnableMeshTrigger> ().setTriggerInstance (triggerInstance);
		enableTrigger.transform.gameObject.layer = LayerMask.NameToLayer ("Ignore Raycast");
		enableTrigger.transform.parent = line.transform;
		enableTrigger.GetComponent<BoxCollider>().size = new Vector3 (lineWidth, 5f, lineLength);
		enableTrigger.transform.position = midPoint;
		enableTrigger.transform.Rotate (0, angle + 90, 0);
		enableTrigger.transform.SetParent (null);
		enableTrigger.transform.position += (enableTrigger.transform.right * ((lineWidth / 2) +.15f));

	//----------------------------------------------------------------------------------------------------------------------------------------------

		disableTrigger = new GameObject ("disableTrigger");
		disableTrigger.AddComponent<BoxCollider> ();
		disableTrigger.GetComponent<BoxCollider> ().isTrigger = true;
		disableTrigger.AddComponent<DisableMeshTrigger> ();
		disableTrigger.GetComponent<DisableMeshTrigger> ().setTriggerInstance (triggerInstance);
		disableTrigger.transform.gameObject.layer = LayerMask.NameToLayer ("Ignore Raycast");
		disableTrigger.transform.parent = line.transform;
		disableTrigger.GetComponent<BoxCollider>().size = new Vector3 (lineWidth, 5f, lineLength);
		disableTrigger.transform.position = midPoint;
		disableTrigger.transform.Rotate (0, angle + 90, 0);
		disableTrigger.transform.SetParent (null);
		disableTrigger.transform.position += (disableTrigger.transform.right * -((lineWidth / 2)+ .15f));


		cleanUp ();
	} // End of generateTrigger

	public void featureEnabled(){
		floorRenderer.enabled = true;
		parentPoint.SetActive (true);
		numOfPoints = 0;
		floor.transform.gameObject.layer = LayerMask.NameToLayer ("Default");
		//Set up a LineRenderer component in the main camera (Where this script is located)
		distanceLine = gameObject.AddComponent (typeof(LineRenderer)) as LineRenderer;
		distanceLine.startColor = Color.red;
		distanceLine.startWidth = .01f;
		distanceLine.positionCount = 0;
		distanceLine.receiveShadows = false;


		distanceLine.enabled = true;

		distanceLine.positionCount = 0;

	}

	public void cleanUp(){
		Destroy (distanceLine);
		floorRenderer.enabled = false;
		floor.transform.gameObject.layer = LayerMask.NameToLayer ("Ignore Raycast");
		distanceLine.enabled = false;
		parentPoint.SetActive (false);
		distanceLine.positionCount = 0;
		numOfPoints = 0;
		triggerInstance++;

		disabled.SetActive(false);	
		enabled.SetActive(true);
		enableDrawBorder.SetActive (true);
		enablePositionBackground.SetActive (true);
		enableSpawnCharacter.SetActive (true);
	}
}
