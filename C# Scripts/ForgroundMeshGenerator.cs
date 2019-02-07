using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgroundMeshGenerator : MonoBehaviour {

	Mesh myMesh; 

	public int meshInstance;

	public List<Vector3> vertices;
	private List<int> tris = new List<int>();
	public Material foregroundMaterial;

	public void GenerateMesh(List<Vector3> verts){
		myMesh = new Mesh ();

		Vector3 centerPoint = GenerateCenterPoint (verts);

		vertices.Add (centerPoint);
		vertices.AddRange (verts);

		for (int i = 1; i < vertices.Count -1; i++){

			tris.Add (0);
			tris.Add (i + 1);
			tris.Add (i);
		}

		tris.Add (0);
		tris.Add (1);
		tris.Add (vertices.Count - 1);

		myMesh.vertices = vertices.ToArray ();
		myMesh.triangles = tris.ToArray ();

		GetComponent<MeshFilter> ().mesh = myMesh;
		GetComponent<MeshRenderer> ().material = foregroundMaterial;

	}

	Vector3 GenerateCenterPoint(List<Vector3> clickPoints){

		Vector3 center = new Vector3 (0, 0, 0);
		for (int i = 0; i < clickPoints.Count; i++)
		{
			center += clickPoints[i];
		}

		center /= clickPoints.Count;
		return center;
	}

	public void deactivateMesh(int instance){
		if (instance == meshInstance) {
			Debug.Log ("disabling filter: Foreground Element " + meshInstance);
			gameObject.GetComponent<MeshRenderer> ().enabled = false;  
		}

	}

	public void activateMesh(int instance){
		if (instance == meshInstance) {
			Debug.Log ("enabling filter: Foreground Element " + meshInstance);
			gameObject.GetComponent<MeshRenderer> ().enabled = true;  
		}

	}

	public void setMeshInstance(int instance){
		meshInstance = instance;
	}

	void Start()
	{

	}
	
	// Update is called once per frame
	void Update () {

	}
}
