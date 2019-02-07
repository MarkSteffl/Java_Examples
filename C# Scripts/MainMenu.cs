using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void Generate(){
		//Load the "Generate" scene
		SceneManager.LoadScene ("Generate");
	}//End of Generate

	public void QuitGame(){
		Debug.Log ("Quit");
		//Quit the game
		Application.Quit ();
	}//End of QuitGame

} //End of class
