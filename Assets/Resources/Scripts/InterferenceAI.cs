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
			if (Random.value < .05f) {
				GameObject[] objs = GameObject.FindGameObjectsWithTag ("Barrier");
				if (objs.Length > 0) {
					Vector3 bar = objs [Mathf.FloorToInt (Random.Range (0, objs.Length))].transform.position;
					GameObject[] turrets = GameObject.FindGameObjectsWithTag ("Turret");
					Vector3 pos = new Vector3 (bar.x, bar.y, 0);
					bool doSpawn = true;
					foreach (GameObject obj in turrets) {
						if (obj.transform.position.Equals (pos)) {
							doSpawn = false;
							break;
						}
					}
					if (doSpawn) {
						Instantiate (Resources.Load ("Prefabs/Turret"), new Vector3 (bar.x, bar.y, 0), new Quaternion ());
					}
				}
			}



			//Get an object tagged with "Player
			GameObject closest = GameObject.FindGameObjectWithTag ("Player");
			//As long as there is one "Player" object
			if (closest != null) {
				//find the closest one
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player")) {
					if (Vector3.Distance (this.transform.position, obj.transform.position) < Vector3.Distance (this.transform.position, closest.transform.position)) {
						closest = obj;
					}
				}
				//If the player object has a path
				if (closest.GetComponent<BasicMovement> ().path != null) {
					GridCoord[] path = closest.GetComponent<BasicMovement> ().path.ToArray ();
					//that is not empty
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
								obj.GetComponent<BasicMovement> ().getPath (GameObject.FindGameObjectWithTag ("Goal"));

							}
						}
					}
				}
			}
			//wait for 1 second before continuing
			yield return new WaitForSeconds (1);
		}
	}
}
