using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour {

	public GameObject thirdPersonCharacter;
	Vector3 startPosition;
	Quaternion startRotation;
	GameObject floor;
	GameObject character;

	// Use this for initialization
	void Start () {
		startPosition = new Vector3 (0, 0.1f, 0);
		startRotation = Quaternion.Euler (new Vector3 (0, 0, 0));
		floor = GameObject.Find ("floor");
	}

	// Update is called once per frame
	void Update () {

	}

	public void spawnCharacter(){
		floor.transform.gameObject.layer = LayerMask.NameToLayer ("Default");
		character = Instantiate (thirdPersonCharacter, startPosition, startRotation) as GameObject; 
		character.transform.gameObject.layer = LayerMask.NameToLayer ("Ignore Raycast");
		character.transform.tag = "Player";

	}

	public void removeCharacter(){
		floor.transform.gameObject.layer = LayerMask.NameToLayer ("Ignore Raycast");
		Destroy (character);
	}
}
