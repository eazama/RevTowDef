using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public static class Pathfinder {

	public static bool[,] grid;
	public static int width = 12;
	public static int length = 10;

	public static List<GridCoord> visited;
	public static Stack<GridCoord> path;
	public static List<GridCoord> toExpand;

	public static Stack<GridCoord> FindPath(GridCoord start, GridCoord finish, GameObject[] barriers){
		visited = new List<GridCoord>();
		path = new Stack<GridCoord>();
		toExpand = new List<GridCoord>();
		grid = new bool[width,length];
		for(int i = 0; i < width; ++i){
			for(int j = 0; j < length; ++j){
				grid[i,j] = false;
			}
		}
		foreach(GameObject obj in barriers){
			try{
			grid[Mathf.FloorToInt (obj.transform.position.x), Mathf.FloorToInt (obj.transform.position.y)] = true;
			}catch(System.Exception ex){

			}
		}
		toExpand.Add (start);
		GridCoord cell = nextCell();
		while(cell != null && !cell.Equals ( finish)){
			Debug.Log("expanding " + nextCell ().x + " " + nextCell ().y);
			expandCoord (nextCell (), finish);
			cell = nextCell ();
		}
		while(cell != null){
			path.Push (cell);
			cell = cell.from;
		}
		return path;
	}

	private static void expandCoord(GridCoord coord, GridCoord finish){
		visited.Add (coord);
		toExpand.Remove(coord);
		GridCoord next1 = new GridCoord(Mathf.Min(coord.x+1, width-1), coord.y, coord);
		GridCoord next2 = new GridCoord(Mathf.Max(coord.x-1, 0), coord.y, coord);
		GridCoord next3 = new GridCoord(coord.x, Mathf.Min(coord.y+1, length-1), coord);
		GridCoord next4 = new GridCoord(coord.x, Mathf.Max(coord.y-1, 0), coord);
		next1.distance (finish);
		next2.distance (finish);
		next3.distance (finish);
		next4.distance (finish);
		if(!grid[next1.x, next1.y] && !visited.Contains(next1)) toExpand.Add (next1);
		if(!grid[next2.x, next2.y] && !visited.Contains(next2)) toExpand.Add (next2);
		if(!grid[next3.x, next3.y] && !visited.Contains(next3)) toExpand.Add (next3);
		if(!grid[next4.x, next4.y] && !visited.Contains(next4)) toExpand.Add (next4);
	}

	public static GridCoord nextCell(){
		if(toExpand.Count == 0) return null;
		GridCoord retVal = toExpand[0];
		foreach(GridCoord gc in toExpand){
			if(gc.dist < retVal.dist) retVal = gc;
		}
		return retVal;
	}
}



public class GridCoord{
	public int x;
	public int y;
	public float dist;
	public GridCoord from = null;
	public GridCoord(int x, int y){
		this.x = x;
		this.y = y;
	}
	public GridCoord(int x, int y, GridCoord from){
		this.x = x;
		this.y = y;
		this.from = from;
	}

	public bool Equals(GridCoord that){
		return this.x == that.x && this.y == that.y;
	}
	public override bool Equals(object obj){
		if(obj == null) return false;
		GridCoord objCoord = obj as GridCoord;
		if(objCoord == null) return false;
		else return Equals (objCoord);
	}
	public override int GetHashCode(){
		return x^y;
	}
	public float distance(GridCoord that){
		dist =  Mathf.Sqrt((this.x-that.x)*(this.x-that.x)+(this.y-that.y)*(this.y-that.y));
		return dist;
	}
}
