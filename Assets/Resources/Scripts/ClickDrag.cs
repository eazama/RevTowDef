using UnityEngine;
using System.Collections;

public class ClickDrag : MonoBehaviour {

	static bool holding = false;
	bool thisHolding = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(thisHolding){
			Vector3 pos_move = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = new Vector3(Mathf.Round(pos_move.x),
			                                 Mathf.Round(pos_move.y),
			                                 gameObject.transform.position.z);
		}
	
	}

	void OnMouseUp(){
		if(holding){
			holding = false;
			thisHolding = false;
			foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Player")){
				obj.GetComponent<TestMovement>().getPath ();
			}
		} else {
			holding = true;
			thisHolding = true;

		}

	}
}
