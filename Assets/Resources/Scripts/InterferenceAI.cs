using UnityEngine;
using System.Collections;

public class InterferenceAI : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (Place ());
	}

	public IEnumerator Place ()
	{
		while (true) {
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

			GridCoord[] path = Pathfinder.FindPath (new GridCoord (2, 2),
			                                        new GridCoord (Mathf.RoundToInt (transform.position.x),
			              										Mathf.RoundToInt (transform.position.y))).ToArray();


			if (path.Length > 0) {
				//get a random tile from that path
				GridCoord location = path [Mathf.FloorToInt (Random.value * path.Length)];
				//if the position is not on the same row or column
				if (location.x != transform.position.x && location.y != transform.position.y) {
					//place a barrier
					(Instantiate (Resources.Load ("Prefabs/Barrier"), new Vector3 (location.x, location.y, 1), new Quaternion ()) as GameObject).transform.eulerAngles = new Vector3 (0, 180, 0);
					Pathfinder.grid [location.x, location.y] = true;
					//and update the paths for every player object
					foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player")) {
						if(obj.GetComponent<BasicMovement>() != null){
							obj.GetComponent<BasicMovement> ().getPath (GameObject.FindGameObjectWithTag ("Goal"));
						}
					}
				}

			}
			
			//wait for 1 second before continuing
			yield return new WaitForSeconds (1);
		}
	}
}
