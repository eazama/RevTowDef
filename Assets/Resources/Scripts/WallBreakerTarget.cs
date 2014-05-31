using UnityEngine;
using System.Collections;

public class WallBreakerTarget : ClickDrag {

	void onStart(){
		this.thisHolding = true;
		ClickDrag.holding = true;
	}

	void doTheThing(){
		if(Pathfinder.grid[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)]){
			TestMovement obj = Instantiate(Resources.Load("Prefabs/WallBreakVirus")) as TestMovement;
			obj.getPath (this.gameObject);
			Destroy (this.gameObject);
		}
	}
}
