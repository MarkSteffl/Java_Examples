using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMeshTrigger : MonoBehaviour {

	int triggerInstance;

	private void OnTriggerEnter(){
		//Debug.Log ("Entered Disable Mesh Trigger: Foreground Element " + triggerInstance);
		foreach (GameObject foregroundElement in GameObject.FindGameObjectsWithTag("Foreground Element")) {
			foregroundElement.GetComponent<ForgroundMeshGenerator> ().deactivateMesh (triggerInstance);
		}
	}


	private void OnTriggerStay(){
		Debug.Log ("Entered Disable Mesh Trigger: Foreground Element " + triggerInstance);
		foreach (GameObject foregroundElement in GameObject.FindGameObjectsWithTag("Foreground Element")) {
			foregroundElement.GetComponent<ForgroundMeshGenerator> ().deactivateMesh (triggerInstance);
		}
	}


	public void setTriggerInstance(int instance){
		triggerInstance = instance;
	}
}

