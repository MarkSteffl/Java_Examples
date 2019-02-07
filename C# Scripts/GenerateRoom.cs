using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateRoom : MonoBehaviour {

	//Material that we will assign the background image texture to
	public Material dynamicMaterial;
	public Material floorMaterial;
	//Object that holds static parameters passed from the main menu
	public GameObject controller;
	//Camera object that holds the default camera in the scene
	public Camera Camera1;
	//Object that holds a Parameters script
	Parameters paramsScript;

	//Declare variables used for setting up the room in the scene
	Vector3 floorPosition;
	Vector3 floorScale;
	Quaternion floorRotation;
	Vector3 backgroundPosition;
	Vector3 backgroundScale;
	Quaternion backgroundRotation;
	Vector3 cameraPosition;
	Quaternion cameraRotation;


	// Use this for initialization
	void Start() {
		//Istantiate the Parameters script attached to the Controller object
		paramsScript = controller.GetComponent<Parameters> ();

		//Initialize variables used to set up the floor in the level
		floorPosition = new Vector3 (0, 0, 0);

		floorRotation = Quaternion.Euler(new Vector3(90.0f, 0, 0));

		floorScale = new Vector3 (paramsScript.getAWidth(),
									paramsScript.getALength(), 1);

		//Initialize variables used to set up the background in the scene
		backgroundPosition = new Vector3 ((((paramsScript.getAWidth())/2) * -1),
											0, (paramsScript.getALength()/2));
		
		backgroundRotation = Quaternion.Euler(new Vector3(paramsScript.getCamAngleUD(),
												paramsScript.getCamAngleLR (), 0));
		
		backgroundScale = calculateBackgroundScale (paramsScript.getAWidth(), paramsScript.getALength(),
														paramsScript.getCamHeight());

		//Initialize variables used to set up the main camera in the scene
		cameraPosition = new Vector3(paramsScript.getAWidth()/2, paramsScript.getCamHeight(),
											(paramsScript.getALength()/2) * -1);
		
		cameraRotation = Quaternion.Euler(new Vector3(paramsScript.getCamAngleUD(),
												paramsScript.getCamAngleLR (), 0));

		//Load a new Quad object into the scene for use as the floor
		GameObject floor = GameObject.CreatePrimitive (PrimitiveType.Quad);
		floor.name = "floor";
		MeshRenderer floorRenderer = floor.GetComponent<MeshRenderer> ();
		floorRenderer.material = floorMaterial;
		floorRenderer.enabled = false;
		floor.transform.SetPositionAndRotation (floorPosition, floorRotation);
		floor.transform.localScale = floorScale;
		floor.transform.gameObject.layer = LayerMask.NameToLayer ("Ignore Raycast");

		//Load a new Quad into the scene for use as the background
		GameObject background = GameObject.CreatePrimitive (PrimitiveType.Quad);
		background.name = "background";
		MeshRenderer backgroundRenderer = background.GetComponent<MeshRenderer> ();
		backgroundRenderer.receiveShadows = false;
		background.transform.SetPositionAndRotation (backgroundPosition, backgroundRotation);
		background.transform.localScale = backgroundScale;
		background.transform.gameObject.layer = LayerMask.NameToLayer ("Ignore Raycast");

		//If a valid filepath to a PNG image was saved in the controller object...
		if (paramsScript.getImage () != null) {
			//...set the path and assign it to a material...
			string path = paramsScript.getImage();
			WWW image = new WWW ("file:///" + path);
			dynamicMaterial.mainTexture = image.texture;
			//...then assign the material to the MeshRenderer attached to the "background" quad
			Renderer rend = background.GetComponent<MeshRenderer> ();
			rend.material = dynamicMaterial;
		}//End of if

		//Set the default camera's position and rotation
		Camera1.transform.SetPositionAndRotation(cameraPosition, cameraRotation);
		Camera1.fieldOfView = 45.5f;

	}//End of Start

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.O)) {
			Camera1.fieldOfView += 3f;

		}

		if (Input.GetKeyDown (KeyCode.P)) {
			Camera1.fieldOfView -= 3f;
		
		}

	}//End of Update

	//Function used for calculating the correct scale of the background quad in relation to the rest of the level
	Vector3 calculateBackgroundScale(float areaWidth, float areaLength, float cameraHeight){
				
		float crossSection = Mathf.Sqrt ((Mathf.Pow (areaWidth, 2) + Mathf.Pow (areaLength, 2)));
		
		float distanceFromCamera = Mathf.Sqrt ((Mathf.Pow (crossSection, 2) + Mathf.Pow (cameraHeight, 2)));

		//the number 2.2 was determined to be the closest 2-digit number through extensive trial-and-error
		//It stands for the distance (in meters) the camera is from a 3 meter x 2 meter image, that distance...
		//...being near-correct to a real-world scale of an image taken with an 18mm lens.
		float scalingFactor = distanceFromCamera / 2.3f;

		Vector3 scale = new Vector3 ((3 * scalingFactor), (((3 * scalingFactor) / 3) * 2), 1);

		return scale;
	}//End of calculateBackgroundScale
		
		
}
