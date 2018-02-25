using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonController : MonoBehaviour {

	public void onClickLoad(string levelname){
		SceneManager.LoadScene(levelname);
	}

	public void onClickQuit(){
		Debug.Log("Quit request");
		Application.Quit();
	}
}
