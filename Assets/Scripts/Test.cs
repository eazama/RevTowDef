using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("Hello World");
		GridCoord a = new GridCoord(0,0);

		GridCoord b = new GridCoord(0,0);

		GridCoord c = new GridCoord(0,1);
		GridCoord d = new GridCoord(0,1,b);

		Debug.Log(c.Equals(d));

		List<GridCoord> l = new List<GridCoord>();
		l.Add(a);
		Debug.Log(l.Contains(b));


		Stack<GridCoord> path = Pathfinder.FindPath(new GridCoord(0,0), new GridCoord(5,8), GameObject.FindGameObjectsWithTag("Barrier"));
		if(path == null){Debug.Log("no path");return;}
		foreach(GridCoord coord in path){
			Debug.Log(coord.x + " " + coord.y);
		}


	}
	
	// Update isDebug.Log("Hello World"); called once per frame
	void Update () {
	
	}
}
