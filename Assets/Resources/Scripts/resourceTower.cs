﻿using UnityEngine;
using System.Collections;

public class resourceTower : MonoBehaviour {

    public bool waitActive;
    public int health = 3;

	// Use this for initialization
	void Start () {
        waitActive = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (waitActive == false)
        {
            StartCoroutine(wait());
        }
	}

    IEnumerator wait()
    {
        waitActive = true;
        yield return new WaitForSeconds(15.0f);
        Shop.cashCount = Shop.cashCount + Random.Range(55, 123);
        audio.Play();
        Debug.Log("cash = " + Shop.cashCount);
        waitActive = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Destroy(other.gameObject);
            if (--health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
