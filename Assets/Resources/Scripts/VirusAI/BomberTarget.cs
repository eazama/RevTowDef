using UnityEngine;
using System.Collections;

public class BomberTarget : MonoBehaviour
{
	public static bool holding = false;
	public bool thisHolding = false;
	
	// Update is called once per frame
	void Update ()
	{
		//If this object is being held
		if (thisHolding) {
			//Move it to the mouse position
			Vector3 pos_move = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//Round off the position
			transform.position = new Vector3 (Mathf.Round (pos_move.x), Mathf.Round (pos_move.y), 5);
		}	
	}
	
	protected void OnMouseUp ()
	{
		//if object is being held
		if (holding) {
			//release it
			holding = false;
			thisHolding = false;
			//do whatever the thing is
			doTheThing ();
		} else {
			//otherwise grab it
			holding = true;
			thisHolding = true;
		}
	}
	
	void doTheThing ()
	{
		//if the object is not over a wall
		if (!Pathfinder.grid [Mathf.RoundToInt (transform.position.x), Mathf.RoundToInt (transform.position.y)]) {
			//create a new Bomber virus
			GameObject obj = Instantiate (Resources.Load ("Prefabs/BomberVirus"), new Vector3 (0, 0, 0), new Quaternion ()) as GameObject;
			//Set it's destination to the location of this object
			BomberMovement move = obj.GetComponent<BomberMovement> ();
			move.dest = new GridCoord (Mathf.RoundToInt (transform.position.x), Mathf.RoundToInt (transform.position.y));
			//get the path needed
			move.getPath (this.gameObject);
		}
		//destroy the target
		Destroy (this.gameObject);
	}

}
