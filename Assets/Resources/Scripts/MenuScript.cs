using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {
	public int x, y, sizeX, sizeY;
	public Texture playButton;
	public Texture creditsButton;
	public Texture howToPlayButton;
	public GameObject circle;
	int i = 0;
	// Use this for initialization
	void Start () {

		sizeX = playButton.width;
		sizeY = playButton.height;
	}
	
	// Update is called once per frame
	void Update () {
		i++;
		circle.transform.eulerAngles = new Vector3 (0, 0, -i/2);
	}

	void OnGUI () {
		GUI.backgroundColor = new Color(0,0,0,0);
		if (GUI.Button (new Rect (10, 5, sizeX, sizeY), playButton)) 
		{
			Application.LoadLevel ("LevelScene");
			//print ("reset!!");
		}

		if (GUI.Button (new Rect (10, sizeX/2+30, sizeX, sizeY), howToPlayButton)) 
		{
			//Application.LoadLevel (Application.loadedLevel + 1);
			//print ("reset!!");
		}

		if (GUI.Button (new Rect (10, sizeX+55, sizeX, sizeY), creditsButton)) 
		{
			//Application.LoadLevel (Application.loadedLevel + 1);
			//print ("reset!!");
		}
	}
}
