using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgroundLineGenerator : MonoBehaviour {

	//Declare ray elements
	Ray myRay;
	RaycastHit hit;

	public GameObject disabled;
	public GameObject borderPoint;
	public GameObject meshGenerator;

	public List<Vector3> vertices = new List<Vector3>();

	GameObject background;
	GameObject parentPoint;

	int numOfPoints = 0;
	int meshInstance = 0;

	LineRenderer foregroundLine;

	// Use this for initialization
	void Start () {
		
		parentPoint = GameObject.Find("parentPointForeground");

	}
	
	// Update is called once per frame
	void Update () {
		background = GameObject.Find ("background");

		myRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		foregroundLine = GetComponent (typeof(LineRenderer)) as LineRenderer;

		//If a ray hits a valid area (The Floor)
			if (Physics.Raycast (myRay, out hit) && disabled.activeInHierarchy == true) {
				if (Input.GetMouseButtonDown (0)) {
				

					//Create a point where the ray hit
					GameObject point = Instantiate (borderPoint, hit.point, Quaternion.identity) as GameObject;
					point.transform.parent = parentPoint.transform;

					//add new vertex to the LineRenderer
					foregroundLine.positionCount = numOfPoints + 1;

					//assign position to the vertex
					foregroundLine.SetPosition (numOfPoints, hit.point);
				
					vertices.Add (hit.point);

					//increment the number of points
					numOfPoints++;
				
				}
			}
	
	}

	public void createMesh(){
		vertices.RemoveAt (vertices.Count - 1);
		GameObject referencePoint = Instantiate (meshGenerator, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		referencePoint.transform.tag = "Foreground Element";
		referencePoint.GetComponent<ForgroundMeshGenerator> ().setMeshInstance (meshInstance);
		referencePoint.GetComponent<ForgroundMeshGenerator> ().GenerateMesh (vertices);
		vertices.Clear ();
		foregroundLine.positionCount--;
	}

	public void featureEnabled(){
		//Set up a LineRenderer component in the main camera (Where this script is located)
		foregroundLine = gameObject.AddComponent (typeof(LineRenderer)) as LineRenderer;
		foregroundLine.startColor = Color.blue;
		foregroundLine.startWidth = .01f;
		foregroundLine.positionCount = 0;
		foregroundLine.receiveShadows = false;

		background.transform.gameObject.layer = LayerMask.NameToLayer ("Default");
		foregroundLine.enabled = true;
		parentPoint.SetActive (true);
		foregroundLine.positionCount = 0;
		numOfPoints = 0;
	}

	public void featureDisabled(){
		background.transform.gameObject.layer = LayerMask.NameToLayer ("Ignore Raycast");
		foregroundLine.enabled = false;
		parentPoint.SetActive (false);
		foregroundLine.positionCount = 0;
		numOfPoints = 0;
		meshInstance++;

		Destroy (foregroundLine);
	}

}