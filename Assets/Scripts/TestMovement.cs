using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestMovement : MonoBehaviour {

	float speed = 3.5f;
	bool moving = false;
	Stack<GridCoord> path;

	// Use this for initialization
	void Start () {
		getPath();
		foreach(GridCoord g in path){
			Debug.Log(g.x + " " + g.y);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(!moving && path.Count >0){
			GridCoord next = path.Pop();
			StartCoroutine(Move(gameObject.transform.position, new Vector3(next.x, next.y, gameObject.transform.position.z)));
		}
	}

	public void getPath(){
		GridCoord start = new GridCoord(Mathf.RoundToInt(gameObject.transform.position.x), Mathf.RoundToInt(gameObject.transform.position.y));
		GameObject target = GameObject.FindGameObjectWithTag("Goal");
		GridCoord finish = new GridCoord(Mathf.RoundToInt(target.transform.position.x), Mathf.RoundToInt(target.transform.position.y));
		path = Pathfinder.FindPath (start, finish, GameObject.FindGameObjectsWithTag("Barrier"));
	}

	public IEnumerator Move(Vector3 from, Vector3 to){
		Debug.Log("Moving");
		moving = true;
		if (from.Equals (to)) {
			moving = false;
			yield break;
		}
		float startTime = Time.time;
		float dist = Vector3.Distance(from, to);
		while(gameObject.rigidbody.position != to){
			float timePassed = (Time.time - startTime)*speed;
			gameObject.rigidbody.position = Vector3.Lerp (from, to, timePassed/dist);
			yield return null;
		}
		moving = false;
	}
}
