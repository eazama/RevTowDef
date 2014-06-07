using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BomberMovement : AbstractMovement
{

	float speed = 3.5f;
	bool moving = false;
	public Stack<GridCoord> path;
	GridCoord target = new GridCoord (0, 0);
	public GridCoord dest = new GridCoord (0, 0);
	
	// Update is called once per frame
	void Update ()
	{
		//If the object is not mving and has a path remaining
		if (!moving && path.Count > 0) {
			//get the next position
			target = path.Pop ();
			//calculate the angle between the object and target
			float dY = target.y - transform.position.y;
			float dX = target.x - transform.position.x;
			float angle = Mathf.Atan2 (dY, dX) * 180 / Mathf.PI;
			//rotate the object to face the direction of movement
			transform.eulerAngles = new Vector3 (0, 0, angle - 90);
			//start the object moving
			StartCoroutine (Move (gameObject.transform.position, new Vector3 (target.x,
			                                                               target.y,
			                                                               gameObject.transform.position.z)));
		}
		//if the object is not moving and has no more positions on the path
		if (!moving && path.Count == 0) {
			//For each position around the virus
			foreach (int i in new int[] {-1,0,1}) {
				foreach (int j in new int[] {-1,0,1}) {
					//destroy any walls
					if (Pathfinder.grid [Mathf.RoundToInt (target.x + i), Mathf.RoundToInt (target.y + j)]) {
						Pathfinder.grid [Mathf.RoundToInt (target.x + i), Mathf.RoundToInt (target.y + j)] = false;
						foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Barrier")) {
							if (obj.transform.position.x == target.x + i && obj.transform.position.y == target.y + j) {
								Destroy (obj);
							}
						}
						//delete any turrets
						foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Turret")) {
							if (obj.transform.position.x == target.x + i && obj.transform.position.y == target.y + j) {
								Destroy (obj);
							}
						}
					}
				}
			}
			//destroy it
			Destroy (gameObject);
		}
	}

	public override void getPath (GameObject goal)
	{
		//starting grid coordinate (current x/y rounded)
		GridCoord start = new GridCoord (Mathf.RoundToInt (target.x),
		                                Mathf.RoundToInt (target.y));
		//ending grid coordinate (goal's x/y rounded)
		GridCoord finish = dest;
		//get a path from the pathfinder
		path = Pathfinder.FindPath (start, finish);
	}

	public IEnumerator Move (Vector3 from, Vector3 to)
	{
		//indicate the object is moving
		moving = true;
		//????
		if (from.Equals (to)) {
			moving = false;
			yield break;
		}
		//get the time the movement started
		float startTime = Time.time;
		//get the distance between the two points
		float dist = Vector3.Distance (from, to);
		//while the object has not arrived
		while (gameObject.rigidbody.position != to) {
			//calculate how far along the object has moved
			float timePassed = (Time.time - startTime) * speed;
			//set the position to the point between the start and end points
			gameObject.rigidbody.position = Vector3.Lerp (from, to, timePassed / dist);
			yield return null;
		}
		//indicate the object has stopped moving
		moving = false;
	}
}