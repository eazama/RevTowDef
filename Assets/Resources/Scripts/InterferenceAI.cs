using UnityEngine;
using System.Collections;

public class InterferenceAI : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (Place ());
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public IEnumerator Place ()
	{
		while (true) {
			Debug.Log ("Place()");
			GameObject closest = GameObject.FindGameObjectWithTag ("Player");
			if (closest != null) {
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player")) {
					if (Vector3.Distance (this.transform.position, obj.transform.position) < Vector3.Distance (this.transform.position, closest.transform.position)) {
						closest = obj;
					}
				}
			
				if (closest.GetComponent<TestMovement> ().path != null) {
					GridCoord[] path = closest.GetComponent<TestMovement> ().path.ToArray ();
					if(path.Length>0){
					GridCoord location = path [Mathf.FloorToInt (Random.value * path.Length)];
					if (location.x != transform.position.x && location.y != transform.position.y) {
							(Instantiate (Resources.Load ("Prefabs/Barrier"), new Vector3 (location.x, location.y, this.transform.position.y), new Quaternion ()) as GameObject).transform.eulerAngles = new Vector3(0,180,0);
						foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player")) {
							obj.GetComponent<TestMovement> ().getPath ();
						}
					}
					}
				}
			}
			yield return new WaitForSeconds (1);
		}
	}
}
