using UnityEngine;
using System.Collections;

public class WallBreakerTarget : MonoBehaviour
{
	public static bool holding = false;
	public bool thisHolding = false;
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (thisHolding) {
			Vector3 pos_move = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position = new Vector3 (Mathf.Round (pos_move.x),
			                                 Mathf.Round (pos_move.y),
			                                 -5);
		}
		
	}
	
	protected void OnMouseUp ()
	{
		if (holding) {
			holding = false;
			thisHolding = false;
			doTheThing ();
		} else {
			holding = true;
			thisHolding = true;
		}
	}
	
	void doTheThing ()
	{
		GameObject obj = Instantiate (Resources.Load ("Prefabs/WallBreakVirus"), new Vector3(0,0,0), new Quaternion()) as GameObject;
		BreakerMovement move = obj.GetComponent<BreakerMovement> ();
		move.getPath (this.gameObject);
		Destroy (this.gameObject);
	}

}
