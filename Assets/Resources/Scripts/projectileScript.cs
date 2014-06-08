﻿using UnityEngine;
using System.Collections;

public class projectileScript : MonoBehaviour {

    public float mySpeed = 20;
    public float myRange = 10;
    public float myDist;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * Time.deltaTime * mySpeed);
        myDist += Time.deltaTime * mySpeed;
        if (myDist >= myRange)
        {
            Destroy(gameObject);
        }
	}
}
