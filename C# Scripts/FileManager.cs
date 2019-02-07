using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileManager : MonoBehaviour {

	public string path;
	public RawImage image;
	public InputField imagePath;

	FileBrowser fb = new FileBrowser(); //all defaults are kept

	public void launchBrowser(){
		OnGUI ();
	}

	void OnGUI(){
		if(fb.draw()){
			if(fb.outputFile == null){
				Debug.Log("Cancel hit");
				gameObject.GetComponent<FileManager> ().enabled = false;
			}else{
				//Debug.Log("Ouput File = \"""+fb.outputFile.ToString()+"\"");
				path = fb.outputFile.ToString();
				/*the outputFile variable is of type FileInfo from the .NET library "http://msdn.microsoft.com/en-us/library/system.io.fileinfo.aspx"*/
				UpdatePath();
				gameObject.GetComponent<FileManager> ().enabled = false;
				}
		}
	}


	void UpdatePath(){
		imagePath.text = path;
	}
}
