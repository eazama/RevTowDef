using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class DirectPathfinder
{
	public static int width = 22;
	public static int length = 18;
	public static List<GridCoord> visited;
	public static Stack<GridCoord> path;
	public static List<GridCoord> toExpand;

	public static Stack<GridCoord> FindPath (GridCoord start, GridCoord finish)
	{
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
				if (!visited.Contains (next))
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

