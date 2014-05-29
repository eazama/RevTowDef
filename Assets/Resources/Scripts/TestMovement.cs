using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestMovement : MonoBehaviour
{

	float speed = 3.5f;
	bool moving = false;
	public Stack<GridCoord> path;
	GridCoord target = new GridCoord(0,0);
	
	// Use this for initialization
	void Start()
	{
		getPath();
		foreach(GridCoord g in path) {
			Debug.Log(g.x + " " + g.y);
		}

	}
	
	// Update is called once per frame
	void Update()
	{
		if(!moving && path.Count > 0) {
			target = path.Pop();
			StartCoroutine(Move(gameObject.transform.position, new Vector3(target.x,
			                                                               target.y,
			                                                               gameObject.transform.position.z)));
		}
		if(!moving && path.Count == 0){
			Destroy(gameObject);
		}
	}

	public void getPath()
	{
		GridCoord start = new GridCoord(Mathf.RoundToInt(target.x),
		                                Mathf.RoundToInt(target.y));
		GameObject goal = GameObject.FindGameObjectWithTag("Goal");
		GridCoord finish = new GridCoord(Mathf.RoundToInt(goal.transform.position.x),
		                                 Mathf.RoundToInt(goal.transform.position.y));
		path = Pathfinder.FindPath(start, finish, GameObject.FindGameObjectsWithTag("Barrier"));
	}

	public IEnumerator Move(Vector3 from, Vector3 to)
	{
		Debug.Log("Moving");
		moving = true;
		if(from.Equals(to)) {
			moving = false;
			yield break;
		}
		float startTime = Time.time;
		float dist = Vector3.Distance(from, to);
		while(gameObject.rigidbody.position != to) {
			float timePassed = (Time.time - startTime) * speed;
			gameObject.rigidbody.position = Vector3.Lerp(from, to, timePassed / dist);
			yield return null;
		}
		moving = false;
	}
}
