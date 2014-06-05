using UnityEngine;
using System.Collections;

public class bombExplosion : MonoBehaviour {
    public float radius = 1.0f;
    public Transform explosion;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        Collider[] nearObjects = Physics.OverlapSphere(transform.position, radius);
        //Return an array of all the colliders within a certain radius of some obj.

        Debug.Log("inside explosion");

        foreach (Collider objects in nearObjects)
        {
            //Iterate through the array

            if (objects.tag == "Barrier" || objects.tag == "Turret") //Does the object have a certain tag.
                Destroy(objects.gameObject);
            //If yes, then Destroy the gameObject the collider is attached to
        }


    }

}
