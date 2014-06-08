using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour
{
    //match Results
    public int numberOfTowers;
    public GameObject[] towers;
    public int numberOfViruses;
    public GameObject[] virues;

	//shop textures
	public Texture2D Background;
	public Texture2D Overlap;
	public Texture2D Item_bg;
	public Texture2D Button;

	public Texture2D winScreen;
	public Texture2D loseScreen;
	
	//dinamic resolution
	private float wratio = 0.0f;
	private float hratio = 0.0f;
	
	//moving button
	private float xbutton = 0.0f;
	private float ybutton = 0.0f;
	private bool moving = false;
	private float old_button = 0;
	private float yold = 0;
	
	//moving items
	private float xitem = 0.0f;
	private float yitem = 0.0f;
	public string[] items;
	public string[] itemsDescription;
	public GUIStyle item_wording;
	public GUIStyle description_wording;
	public GUIStyle textLabels;
	public GUISkin mySkin;
	
	//selected button
	int selected;
	
	//scrollbutton variables
	int wscroll = 15;
	int hscroll = 30;
	
	//lock scrolling within fixed area
	int toplock = 26;
	int botlock = 160;
	
	//how far the scroll goes through the scrolling_length
	int scrolling = 263;
	//item length & width
	int litem_bg = 130;
	int witem_bg = 33;
	//spacing between items
	int between_items = 38;
	//starting items position
	int yitem_value = 28;
	int xitem_value = 530;
	//length of all items to scroll through
	int scrolling_length;
	
	//# of resources you have
	public static double cashCount;
	
	//enemy health
	public static float corpHealth;
	
	//sound
	public AudioSource audio;
	public AudioClip clipSound;

	//game controller
	public GameController gameController;
	
	void Awake () {
		items [0] = "Basic Virus";
		items [1] = "Wall Breaker";
		items [2] = "Tank Virus";
		items [3] = "Resource Tower";
		items [4] = "Bomb Virus";
		items [5] = "Juggernaut";
		
		itemsDescription [0] = "Cost: 5\nBasic virus that will head towards the corporation.";
		itemsDescription [1] = "Cost: 25\nHeads towards the corporation while destroying up to 3 obstacles in its path.";
		itemsDescription [2] = "Cost: 50\nHas more health than Basic Viruses but moves slower.";
		itemsDescription [3] = "Cost: 200\nPassively accumilate resources for you. Double-Click anywhere on the map to place.";
		itemsDescription [4] = "Cost: 500\nWhen this virus is destroyed, it will destroy all obstacles in its radius.";
		itemsDescription [5] = "Cost: 1000\nThis beast will go straight for the goal.";
	}
	
	// Use this for initialization
	void Start ()
	{
		//gets gamecontroller for start bool
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		//GameObject breakermovement = GameObject.Find("BreakerMovement").GetComponent<BreakerMovement>();
		scrolling_length = (witem_bg + between_items) * (items.Length);
		
		cashCount = 20.00;
		corpHealth = 100;
		
		//set resolution
		wratio = Screen.width;
		hratio = Screen.height;
		wratio = wratio / 700;
		hratio = hratio / 400;
		
		//starting scroll button position
		xbutton = 660 * wratio;
		ybutton = toplock * hratio;
		//starting items position
		xitem = xitem_value * wratio;
		yitem = yitem_value * hratio;
	}
	
	// Update is called once per frame
	void Update ()
	{
        //finds all of the towers and viruses on the scene
        towers = GameObject.FindGameObjectsWithTag("Tower");
        numberOfTowers = towers.Length;
        virues = GameObject.FindGameObjectsWithTag("Player");
        numberOfViruses = virues.Length;

        //Debug.Log("number of vtowers = " + numberOfTowers);
        //Debug.Log("number of viruses = " + numberOfViruses);

		ScrollButtonMove ();
		ScrollItems ();	
	}
	
	void ScrollButtonMove ()
	{
		//checks if click is in range of the button texture
		if (Input.GetMouseButtonDown (0)) {
			if (Input.mousePosition.x >= xbutton) {
				if (Input.mousePosition.x <= xbutton + wscroll * wratio) {
					if (Input.mousePosition.y <= Screen.height - ybutton) {
						if (Input.mousePosition.y >= Screen.height - ybutton - hscroll * wratio) {
							moving = true;
							yold = Input.mousePosition.y;
							old_button = ybutton;
						}
					}
				}
			}
		}
		if (Input.GetMouseButton (0) && moving) {
			ybutton = old_button + (yold - Input.mousePosition.y);
			//lock button
			if (ybutton >= botlock * hratio)
				ybutton = botlock * hratio;
			if (ybutton <= toplock * hratio)
				ybutton = toplock * hratio;
		}
		if (Input.GetMouseButtonUp (0) && moving)
			moving = false;
		
	}
	
	void ScrollItems ()
	{
		//scroll area
		float items_length = (items.Length) * witem_bg * hratio - (scrolling_length * hratio);
		//get scrolling length
		yitem = (yitem_value * hratio) + (items_length * ((ybutton - (toplock * hratio)) / ((scrolling - toplock) * hratio)));
	}
	
	void OnGUI ()
	{
		GUI.skin = mySkin;
		GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),Background,ScaleMode.StretchToFill);
		for (int i = 0; i<items.Length; i++) {
			GUI.DrawTexture (new Rect (xitem, yitem + (i * between_items * hratio), litem_bg * wratio, witem_bg * hratio), Item_bg, ScaleMode.StretchToFill);
			if (GUI.Button (new Rect (xitem, yitem + (i * between_items * hratio), litem_bg * wratio, witem_bg * hratio),new GUIContent( items [i], itemsDescription[i]), "button")){
				if (gameController.startGame){
					audio.PlayOneShot(clipSound);
					selected = i;
					Debug.Log(i + "CLICKED");
					VirusSpawner.spawn(i);
				}
			}
			
		}
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), Overlap, ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (xbutton, ybutton, wscroll * wratio, hscroll * hratio), Button, ScaleMode.StretchToFill);
		
		//current selected
		//GUI.Box (new Rect (560 * wratio+35, 350 * hratio+30, wratio, hratio), "Item: " + items [selected], item_wording);
		
		//draws cash
		GUI.Label(new Rect(560 * wratio - 25, 335 * hratio , 100, 20), "Cash : " + cashCount.ToString("##0.00"), item_wording);
		
		//draw corp health
		GUI.Label(new Rect(560 * wratio - 20, 355 * hratio , 100, 20), "Corp Health" , item_wording);
		GUI.Label(new Rect(560 * wratio - 10, 365 * hratio , 100, 20), corpHealth+" Bajillion $", item_wording);
		//draws the item description
		GUI.Label (new Rect (560*wratio-40,200 * hratio,130*hratio,130*hratio), GUI.tooltip, "box");

        if (Shop.corpHealth <= 0)
        {
            //GUI.Label(new Rect(Screen.width * 0.5f - 120, Screen.height * 0.5f - 40, 300, 50), "You Win!");
            //GUI.Label(new Rect(Screen.width * 0.5f - 120, Screen.height * 0.5f - 5, 300, 50), "The company has just gone bankrupt!");
            //Debug.Log("it works, you won");
			GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),winScreen,ScaleMode.StretchToFill);
			gameController.restartGame = true;
//            if (GUI.Button(new Rect(Screen.width / 2 - 85, Screen.height / 2 + 60, 170, 60), "Play Again?"))
//            {
//                Application.LoadLevel("LevelScene");
//            }

        }

        if (numberOfTowers <= 0 && numberOfViruses <= 0 && Shop.cashCount < 5 )
        {
			GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),loseScreen,ScaleMode.StretchToFill);
			gameController.restartGame = true;
//            GUI.Label(new Rect(Screen.width * 0.5f - 120, Screen.height * 0.5f - 40, 300, 50), "You can no longer generate money!");
//            GUI.Label(new Rect(Screen.width * 0.5f - 120, Screen.height * 0.5f - 5, 300, 50), "You Lose!");
//            Debug.Log("it works, you lost");
//            if (GUI.Button(new Rect(Screen.width / 2 - 85, Screen.height / 2 + 60, 170, 60), "Play Again?"))
//            {
//                Application.LoadLevel("LevelScene");
//            }
        }
    
	}
	
}
