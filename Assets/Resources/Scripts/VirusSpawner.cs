using UnityEngine;
using System.Collections;

public class VirusSpawner:MonoBehaviour{

	public static void spawn(int i){
		GameObject obj;
		switch(i){
		case 0:
			Debug.Log("SPAWNING BASIC");
			obj = Instantiate (Resources.Load("Prefabs/BasicVirus")) as GameObject;
			TestMovement move = obj.GetComponent<TestMovement>();
			move.getPath (GameObject.FindGameObjectWithTag("Goal"));
			break;
		case 1:
			Debug.Log("SPAWNING Wallbreaker");
			obj = Instantiate (Resources.Load("Prefabs/WallBreakVirus") ) as GameObject;
			break;
		case 2:
			Debug.Log("SPAWNING Bomber");
			obj = Instantiate (Resources.Load ("Prefabs/BomberTarget")) as GameObject;
			BomberTarget tar = obj.GetComponent<BomberTarget>();
			tar.thisHolding = true;
			BomberTarget.holding = true;
			break;
		}
	}
}
