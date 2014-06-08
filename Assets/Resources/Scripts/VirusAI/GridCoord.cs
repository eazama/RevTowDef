using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridCoord
{
	public int x;
	public int y;
	public float dist;
	public GridCoord from = null;
	
	public GridCoord (int x, int y)
	{
		this.x = x;
		this.y = y;
	}
	
	public GridCoord (int x, int y, GridCoord from)
	{
		this.x = x;
		this.y = y;
		this.from = from;
	}
	
	public bool Equals (GridCoord that)
	{
		return this.x == that.x && this.y == that.y;
	}
	
	public override bool Equals (object obj)
	{
		if (obj == null)
			return false;
		GridCoord objCoord = obj as GridCoord;
		if (objCoord == null)
			return false;
		else
			return Equals (objCoord);
	}
	
	public override int GetHashCode ()
	{
		return x ^ y;
	}
	
	public float distance (GridCoord that)
	{
		dist = Mathf.Sqrt ((this.x - that.x) * (this.x - that.x) + (this.y - that.y) * (this.y - that.y));
		return dist;
	}
}