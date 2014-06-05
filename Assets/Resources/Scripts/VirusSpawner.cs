using UnityEngine;
using System.Collections;

public class VirusSpawner:MonoBehaviour{

	public static void spawn(int i){
		GameObject obj;
		if (Shop.cashCount > 0) {
			switch (i) {
			case 0:
				if (Shop.cashCount >= 5) {
					Debug.Log ("SPAWNING BASIC");
					obj = Instantiate (Resources.Load ("Prefabs/BasicVirus")) as GameObject;
					BasicMovement move = obj.GetComponent<BasicMovement> ();
					move.getPath (GameObject.FindGameObjectWithTag ("Goal"));
					Shop.cashCount = Shop.cashCount - 5;
				}
				break;
			case 1:
				if (Shop.cashCount >= 25) {
					Debug.Log ("SPAWNING Wallbreaker");
					obj = Instantiate (Resources.Load ("Prefabs/WallBreakVirus")) as GameObject;
					Shop.cashCount = Shop.cashCount - 25;
				}
				break;
			case 2:
				if (Shop.cashCount >= 50) {
					Debug.Log ("SPAWNING Bomber");
					obj = Instantiate (Resources.Load ("Prefabs/BomberTarget")) as GameObject;
					BomberTarget tar = obj.GetComponent<BomberTarget> ();
					tar.thisHolding = true;
					BomberTarget.holding = true;
					Shop.cashCount = Shop.cashCount - 50;
				}
				break;
			case 3:
				if (Shop.cashCount >= 60) {
					Debug.Log("SPAWNING Resource Tower");
					Instantiate (Resources.Load("Prefabs/ResourceTower"));
					Shop.cashCount = Shop.cashCount - 60;
				}
				break;
			}
		}
	}
}
