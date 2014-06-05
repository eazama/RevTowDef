using UnityEngine;
using System.Collections;

public class turretScript : MonoBehaviour
{
	public GameObject myProjectile;
	public float reloadTime;
	public float turnSpeed;
	public float firePauseTime;
	public GameObject muzzleEffect;
	public float errorAmount;
	public Transform myTarget;
	public Transform turretBall;
	private float nextFireTime;
	private float nextMoveTime;
	private Quaternion desiredRotation;

	// Use this for initialization
	void Start ()
	{
  
	}
	
	// Update is called once per frame
	void Update ()
	{
		//GameObject goal = GameObject.FindGameObjectWithTag("Player");

		if (myTarget) {
			if (Time.time >= nextMoveTime) {
				desiredRotation = CalculateAimPosition (myTarget.position);
				//Debug.Log(desiredRotation.z);
				desiredRotation.x = 0;
				desiredRotation.y = 0;
				turretBall.rotation = Quaternion.Lerp (turretBall.rotation, desiredRotation, Time.deltaTime * turnSpeed);

			}

			if (Time.time >= nextFireTime) {
				FireProjectile ();
			}
		}
	}

	public void OnTriggerEnter (Collider other)
	{
		if (myTarget == null && other.gameObject.tag == "Player") {
			nextFireTime = (float)(Time.time + (reloadTime * .5));
			myTarget = other.gameObject.transform;
		}
	}

	public void OnTriggerExit (Collider other)
	{
		if (other.gameObject.transform == myTarget) {
			myTarget = null;
		}
	}

	public Quaternion CalculateAimPosition (Vector3 targetPos)
	{
		Vector3 aimPoint = new Vector3 (targetPos.x, targetPos.y, targetPos.z);
		Quaternion newRotation = Quaternion.LookRotation (aimPoint - turretBall.position, Vector3.back);
		return newRotation;
	}

	public float AimError ()
	{
		return Random.Range (-errorAmount, errorAmount);
	}

	public void FireProjectile ()
	{
		audio.Play();
		nextFireTime = Time.time + reloadTime;
		nextMoveTime = Time.time + firePauseTime;

		foreach (Transform theMuzzlePos in transform) {
			GameObject proj = Instantiate (myProjectile, theMuzzlePos.position, theMuzzlePos.rotation) as GameObject;
			Vector3 nRot = proj.transform.eulerAngles;
			nRot.z += AimError();
			proj.transform.eulerAngles = nRot;
			//Instantiate (muzzleEffect, theMuzzlePos.position, theMuzzlePos.rotation);
		}
	}

}