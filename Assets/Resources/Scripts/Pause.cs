using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp(){
		if(Time.timeScale != 0){
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}

	}
}
