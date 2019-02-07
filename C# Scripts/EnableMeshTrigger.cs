using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMeshTrigger : MonoBehaviour {

	int triggerInstance;

	private void OnTriggerEnter(){
		//Debug.Log ("Entered Enable Mesh Trigger: Foreground Element " + triggerInstance);
		foreach (GameObject foregroundElement in GameObject.FindGameObjectsWithTag("Foreground Element")) {
			foregroundElement.GetComponent<ForgroundMeshGenerator> ().activateMesh (triggerInstance);
		}

	}


	private void OnTriggerStay(){
		Debug.Log ("Entered Enable Mesh Trigger: Foreground Element " + triggerInstance);
		foreach (GameObject foregroundElement in GameObject.FindGameObjectsWithTag("Foreground Element")) {
			foregroundElement.GetComponent<ForgroundMeshGenerator> ().activateMesh (triggerInstance);
		}

	}


	public void setTriggerInstance(int instance){
		triggerInstance = instance;
	}
}


