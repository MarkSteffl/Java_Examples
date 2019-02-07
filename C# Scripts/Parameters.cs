using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Parameters : MonoBehaviour{

	//Static variables used to hold data in Controller gameobject prefabs between scenes
	private static float aWidth;
	private static float aLength;
	private static float aHeight;
	private static float camAngleUD;
	private static float camAngleLR;
	private static float camHeight;
	private static string image;

	//GameObjects that will hold inputfields from the main menu 
	public GameObject areaWidth;
	public GameObject areaLength;
	public GameObject areaHeight;
	public GameObject cameraAngleUD;
	public GameObject cameraAngleLR;
	public GameObject cameraHeight;
	public GameObject imagePath;


	// Update is called once per frame
	void Update() {
		//Get the user input from the main menu inputfields and convert the strings to floats
		aWidth = float.Parse(areaWidth.GetComponent<TMP_InputField>().textComponent.text);
		aLength = float.Parse(areaLength.GetComponent<TMP_InputField>().textComponent.text);
		aHeight = float.Parse(areaWidth.GetComponent<TMP_InputField>().textComponent.text);
		camAngleUD = float.Parse(cameraAngleUD.GetComponent<TMP_InputField>().textComponent.text);
		camAngleLR = float.Parse(cameraAngleLR.GetComponent<TMP_InputField>().textComponent.text);
		camHeight = float.Parse(cameraHeight.GetComponent<TMP_InputField>().textComponent.text);
		//Get the user input from the main menu inputfield
		image = imagePath.GetComponent<InputField>().text;
		Debug.Log (aWidth + " " + aLength + " " + aHeight);
		Debug.Log (camAngleUD + " " + camAngleLR + " " + camHeight);
		Debug.Log (image);
	}//End of Update

	//Getters
	public float getAWidth(){
		return aWidth;
	}

	public float getALength(){
		return aLength;
	}

	public float getAHeight(){
		return aHeight;
	}

	public float getCamAngleUD(){
		return camAngleUD;
	}

	public float getCamAngleLR(){
		return camAngleLR;
	}

	public float getCamHeight(){
		return camHeight;
	}

	public string getImage(){
		return image;
	}
	//End of Getters
}

