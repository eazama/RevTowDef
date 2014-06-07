using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractMovement : MonoBehaviour
{
	public abstract void getPath(GameObject goal);
}

