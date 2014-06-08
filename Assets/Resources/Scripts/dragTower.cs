using UnityEngine;
using System.Collections;

public class dragTower : MonoBehaviour {
    public static bool holding = true;
    public bool thisHolding = true;

	//game controller
	public GameController gameController;

    // Use this for initialization
    void Start()
    {
		//gets gamecontroller for start bool
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
    }

    // Update is called once per frame
    void Update()
    {
		if (gameController.startGame){
	        if (thisHolding)
	        {
	            Vector3 pos_move = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	            transform.position = new Vector3(Mathf.Round(pos_move.x),
	                                             Mathf.Round(pos_move.y),
	                                             gameObject.transform.position.z);
	        }
		}

    }

    void OnMouseUp()
    {
        if (holding)
        {
            holding = false;
            thisHolding = false;
			Destroy (this);
           // doTheThing();
        }
        else
        {
            holding = true;
            thisHolding = true;
        }
    }

   /* void doTheThing()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            obj.GetComponent<BasicMovement>().getPath(GameObject.FindGameObjectWithTag("Goal"));
        }
    } */
}
