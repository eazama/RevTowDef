﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Pathfinder
{

	public static bool[,] grid;
	public static int width = 22;
	public static int length = 18;
	public static List<GridCoord> visited;
	public static Stack<GridCoord> path;
	public static List<GridCoord> toExpand;

	private static void buildGrid(){
		grid = new bool[width, length];
		for (int i = 0; i < width; ++i) {
			for (int j = 0; j < length; ++j) {
				grid [i, j] = false;
			}
		}
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Barrier")) {
			try {
				grid [Mathf.FloorToInt (obj.transform.position.x), Mathf.FloorToInt (obj.transform.position.y)] = true;
			} catch (System.Exception ex) {
				Debug.Log (ex.StackTrace);
			}
		}
	}

	public static Stack<GridCoord> FindPath (GridCoord start, GridCoord finish)
	{
		if(grid == null) {
			buildGrid();
		}
		visited = new List<GridCoord> ();
		path = new Stack<GridCoord> ();
		toExpand = new List<GridCoord> ();

		toExpand.Add (start);
		GridCoord cell = nextCell ();
		while (cell != null && !cell.Equals ( finish)) {
			expandCoord (nextCell (), finish);
			cell = nextCell ();
		}
		while (cell != null) {
			path.Push (cell);
			cell = cell.from;
		}
		return path;
	}

	private static void expandCoord (GridCoord coord, GridCoord finish)
	{
		visited.Add (coord);
		toExpand.Remove (coord);
		GridCoord next;
		foreach (int i in new int[]{-1, 0, 1}) {
			foreach (int j in new int[]{-1, 0, 1}) {
				next = new GridCoord (Mathf.Clamp (coord.x + i, 0, width - 1), Mathf.Clamp (coord.y + j, 0, length - 1), coord);
				next.distance (finish);
				if (!grid [next.x, next.y] && !visited.Contains (next) && !toExpand.Contains(next))
					toExpand.Add (next);
			}
		}
	}

	public static GridCoord nextCell ()
	{
		if (toExpand.Count == 0)
			return null;
		GridCoord retVal = toExpand [0];
		foreach (GridCoord gc in toExpand) {
			if (gc.dist < retVal.dist)
				retVal = gc;
		}
		return retVal;
	}
}