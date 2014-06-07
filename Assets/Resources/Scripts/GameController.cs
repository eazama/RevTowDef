using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public bool startGame;
	public bool restartGame;
	public GUISkin messageBox;

	// Use this for initialization
	void Start () {
		startGame = false;
		restartGame = false;
	}

	void OnGUI () {
		//GUI.skin = null;
		if (!startGame) {
			GUI.skin = messageBox;
			GUI.Label (new Rect(Screen.width / 2 - 670 / 2, 100, 670, 390), "");
			GUI.skin = null;
			GUI.Label (new Rect(Screen.width / 2 - 630 / 2, 160, 630, 390), "Welcome!\n\n" +
			           "Your mission is to hack into the corporation's servers and take back resources that they have taken from your city to use for their own benefits..\n\n" +
			           "You can buy viruses or resource towers by clicking on your desired choice on the right and they will be immediately placed into the " +
			           "corporation’s maze of fire walls and security turrets. Each virus has their own attributes to help you get past the high security.\n\n" +
			           "To help you get started, we provided you with your first resource tower! Double click anywhere in the map to place it down.\n\n" +
			           "Press ENTER to begin.");
			if (Input.GetKeyUp (KeyCode.Return)) {
				startGame = true;
			}
		}

		if (startGame && restartGame){
			if (Input.GetKeyUp (KeyCode.Return)){
				startGame = false;
				restartGame = false;
				Application.LoadLevel("LevelScene");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
