using UnityEngine;
using System.Collections;

public class turretScript : MonoBehaviour {
    public GameObject myProjectile;
    public float reloadTime;
    public float turnSpeed;
    public float firePauseTime;
    public GameObject muzzleEffect;
    public float errorAmount;
    public Transform myTarget;
    public Transform[] muzzlePositions;
    public Transform turretBall;

    private float nextFireTime;
    private float nextMoveTime;
    private Quaternion desiredRotation;
    float aimError;

	// Use this for initialization
	void Start () {
  
	}
	
	// Update is called once per frame
	void Update () {
		//GameObject goal = GameObject.FindGameObjectWithTag("Player");

        if (myTarget)
        {
            if (Time.time >= nextMoveTime)
            {
                desiredRotation = CalculateAimPosition(myTarget.position);
                desiredRotation.x = 0;
                desiredRotation.y = 0;
                turretBall.rotation = Quaternion.Lerp(turretBall.rotation, desiredRotation, Time.deltaTime*turnSpeed);
            }

            if (Time.time >= nextFireTime)
            {
                FireProjectile();
            }
        }
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            nextFireTime = (float) (Time.time + (reloadTime*.5));
            myTarget = other.gameObject.transform;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.transform == myTarget ){
            myTarget = null;
        }
    }

    public Quaternion CalculateAimPosition(Vector3 targetPos)
    {
        Vector3 aimPoint = new Vector3(targetPos.x, targetPos.y, targetPos.z);
        Quaternion newRotation = Quaternion.LookRotation(aimPoint - turretBall.position);
        return newRotation;
    }

public void CalculateAimError(){
    aimError = Random.Range(-errorAmount, errorAmount);
    }


public void FireProjectile(){
    audio.Play();
    nextFireTime = Time.time + reloadTime;
    nextMoveTime = Time.time + firePauseTime;
    CalculateAimError();

    foreach(Transform theMuzzlePos in muzzlePositions){
        Instantiate(myProjectile, theMuzzlePos.position, theMuzzlePos.rotation);
        Instantiate(muzzleEffect, theMuzzlePos.position, theMuzzlePos.rotation);
         }
    }

}