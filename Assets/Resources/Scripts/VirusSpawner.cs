using UnityEngine;
using System.Collections;

public class VirusSpawner:MonoBehaviour{

	public static void spawn(int i){
		switch(i){
		case 0:
			Debug.Log("SPAWNING BASIC");
			Instantiate (Resources.Load("Prefabs/BasicVirus"));
			break;
		case 1:
			Debug.Log("SPAWNING Wallbreaker");
			Instantiate (Resources.Load("Prefabs/WallBreakVirus"));
			break;
		}
	}
}
