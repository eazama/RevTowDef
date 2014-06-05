using UnityEngine;
using System.Collections;

public class VirusBulletDetection : MonoBehaviour {

	public int health;

	public void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Projectile") {
			Destroy(other.gameObject);
			if(--health == 0){
				Destroy(gameObject);
			}
		}
	}
}
