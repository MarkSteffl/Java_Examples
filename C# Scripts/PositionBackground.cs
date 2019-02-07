using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionBackground : MonoBehaviour {

	//Declare ray elements
	Ray myRay;
	RaycastHit hit;

	//Declare GameObjects to be enabled/disabled for raycast
	GameObject background;

	//Declare GamObject (button)
	public GameObject enabled;
	public GameObject disabled;
	public GameObject controller;
	public GameObject enableDrawBorder;
	public GameObject enableDrawForeground;
	public GameObject enableSpawnCharacter;

	Parameters paramsScript;
	Vector3 corner;


	// Use this for initialization
	void Start () {
		paramsScript = controller.GetComponent<Parameters> ();
		corner = new Vector3 ((paramsScript.getAWidth () / 2) * -1, 0, paramsScript.getALength() / 2);

	}
	
	// Update is called once per frame
	void Update () {
		
		background = GameObject.Find ("background");
		myRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		//If a ray hits a valid area (The Background)
		if (Physics.Raycast(myRay, out hit) && disabled.activeInHierarchy == true) {
			if (Input.GetMouseButtonDown (0)) {
				GameObject reference = new GameObject ();
				reference.transform.position = hit.point;
				background.transform.SetParent (reference.transform);
				reference.transform.position = corner;
				background.transform.SetParent (null);
				Destroy (reference);
				cleanUp ();
			}
		}



	} //End of Update

	public void featureEnabled(){
		background.transform.gameObject.layer = LayerMask.NameToLayer ("Default");
	}

	void cleanUp(){
		disabled.SetActive(false);	
		enabled.SetActive(true);
		enableDrawBorder.SetActive (true);
		enableDrawForeground.SetActive (true);
		enableSpawnCharacter.SetActive (true);
		background.transform.gameObject.layer = LayerMask.NameToLayer ("Ignore Raycast");
	}
}
