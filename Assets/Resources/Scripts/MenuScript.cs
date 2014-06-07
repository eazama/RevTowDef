using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {
	public int x, y, sizeX, sizeY;
	public GameObject circle;
	public Texture2D xButton;
	public Texture2D creditList;
	public Texture2D instructions;
	private bool creditsPressed = false;
	private bool instructionsPressed = false;
	public AudioSource audio;
	public AudioClip clipSound;
	public GUISkin menuSkin;
	public GUISkin defaultSkin;
	int i = 0;
	// Use this for initialization
	void Start () {

		sizeX = 300;
		sizeY = 200;
	}
	
	// Update is called once per frame
	void Update () {
		i++;
		circle.transform.eulerAngles = new Vector3 (0, 0, -i/2);
	}

	IEnumerator waitSeconds (){
		yield return new WaitForSeconds(1.0f);
		Application.LoadLevel ("LevelScene");
	}

	void OnGUI () {
		//GUI.backgroundColor = new Color(0,0,0,0);
		if (!creditsPressed && !instructionsPressed) {
			GUI.skin = menuSkin;
			if (GUI.Button (new Rect (10, 5, sizeX, sizeY), "Play")) {
				audio.PlayOneShot(clipSound);
				StartCoroutine(waitSeconds());
			}

			if (GUI.Button (new Rect (10, sizeX / 2 + 30, sizeX, sizeY), "How To Play")) {
				instructionsPressed = true;
			}

			if (GUI.Button (new Rect (10, sizeX + 55, sizeX, sizeY), "Credits")) {
				creditsPressed = true;
			}
		}
		else if (instructionsPressed) {
			GUI.skin = defaultSkin;
			GUI.backgroundColor = new Color(0,0,0,0);
			GUI.Label(new Rect(Screen.width / 2 - instructions.width / 2, 75, instructions.width, instructions.height), instructions);
			if(GUI.Button(new Rect(Screen.width /2 + creditList.width / 2,140,50,50), xButton)){
				instructionsPressed = false;
			}
		}
		else if (creditsPressed) {
			GUI.backgroundColor = new Color(0,0,0,0);
			GUI.Label(new Rect(Screen.width / 2 - creditList.width / 2, 75, creditList.width, creditList.height), creditList);
			if(GUI.Button(new Rect(Screen.width /2 + creditList.width / 2 - 80,130,50,50), xButton)){
				creditsPressed = false;
			}
		}
	}
}
