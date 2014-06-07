using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MazeAI : MonoBehaviour
{
	//game controller
	public GameController gameController;
	List<GridCoord> maze = new List<GridCoord>();
	// Use this for initialization
	void Start ()
	{
		//gets gamecontroller for start bool
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		StreamReader sr = new StreamReader(Application.dataPath + "/" + "maze1.txt");
		string line = "";
		string[] sline;
		while((line = sr.ReadLine()) != null){
			sline = line.Split(" ".ToCharArray());
			maze.Add(new GridCoord(int.Parse(sline[0]), int.Parse(sline[1])));
		}
		Debug.Log(maze.Count);
		StartCoroutine (Place ());
	}

	public IEnumerator Place ()
	{
		while (true) {
			if (gameController.startGame){
				Debug.Log ("Place()");
				//random chance to place a turret
				if (Random.value < .05f) {
					//if there is at least one barrier
					GameObject[] objs = GameObject.FindGameObjectsWithTag ("Barrier");
					if (objs.Length > 0) {
						//get the position of a random one
						Vector3 bar = objs [Mathf.FloorToInt (Random.Range (0, objs.Length))].transform.position;
						//get the current turrets
						GameObject[] turrets = GameObject.FindGameObjectsWithTag ("Turret");
						Vector3 pos = new Vector3 (bar.x, bar.y, 0);
						bool doSpawn = true;
						foreach (GameObject obj in turrets) {
							//if there is already a turrent in that position
							if (obj.transform.position.Equals (pos)) {
								//don't spawn a new one
								doSpawn = false;
								break;
							}
						}
						//otherwise, spawn it
						if (doSpawn) {
							Instantiate (Resources.Load ("Prefabs/Turret"), new Vector3 (bar.x, bar.y, 0), new Quaternion ());
						}
					}
				}

				foreach(GridCoord coord in maze){
					if(Pathfinder.grid == null){
						break;
					}
					if(!Pathfinder.grid[coord.x, coord.y]){
						placeWall (coord);
						break;
					}
				}
			}
			//wait for 1 second before continuing
			yield return new WaitForSeconds (1);
		}
	}

	private void placeWall(GridCoord coord){
		(Instantiate (Resources.Load ("Prefabs/Barrier"), new Vector3 (coord.x, coord.y, 1), new Quaternion ()) as GameObject).transform.eulerAngles = new Vector3 (0, 180, 0);
		Pathfinder.grid [coord.x, coord.y] = true;
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Player")){
			AbstractMovement move = obj.GetComponent<AbstractMovement>();
			if(move != null){
				move.getPath(GameObject.FindGameObjectWithTag("Goal"));
			}
		}
	}
}
