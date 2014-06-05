using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour
{
	//shop textures
	public Texture2D Background;
	public Texture2D Overlap;
	public Texture2D Item_bg;
	public Texture2D Button;
		
	//dinamic resolution
	private float wratio = 0.0f;
	private float hratio = 0.0f;

	//quick fix for initial select value print
	//bool select = false;

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

	//sound
	public AudioSource audio;
	public AudioClip clipSound;

	void Awake () {
		items [0] = "Basic Virus";
		items [1] = "Wall Breaker";
		items [2] = "Tank Virus";
		items [3] = "Resource Tower";
		items [4] = "Bomb Virus";
		
		itemsDescription [0] = "Cost: 5\n Basic virus that will head\n towards the corporation.";
		itemsDescription [1] = "Cost: 50\n Heads towards the corporation\n while destroying all obstacles\n in its path.";
		itemsDescription [2] = "Cost: 50\n Has more health than Basic\n Viruses but moves slower.";
		itemsDescription [3] = "Cost: 200\n Passively accumilate resources\n for you. Click anywhere on the\n map to place.";
		itemsDescription [4] = "Cost: 500\n When this virus is destroyed, it\n will destroy all turrents in its\n raduis.";
		
		//itemsDescription [0] = "Cost: 5. Basic virus that will head towards the corporation.";
	}

	// Use this for initialization
	void Start ()
	{
		scrolling_length = (witem_bg + between_items) * (items.Length);

        cashCount = 150.00;

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
		GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),Background,ScaleMode.StretchToFill);
		for (int i = 0; i<items.Length; i++) {
			GUI.DrawTexture (new Rect (xitem, yitem + (i * between_items * hratio), litem_bg * wratio, witem_bg * hratio), Item_bg, ScaleMode.StretchToFill);
			if (GUI.Button (new Rect (xitem, yitem + (i * between_items * hratio), litem_bg * wratio, witem_bg * hratio),new GUIContent( items [i], itemsDescription[i]), item_wording)){
				audio.PlayOneShot(clipSound);
				selected = i;
				Debug.Log(i + "CLICKED");
				VirusSpawner.spawn(i);
			}

		}
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), Overlap, ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (xbutton, ybutton, wscroll * wratio, hscroll * hratio), Button, ScaleMode.StretchToFill);

		//current selected

		GUI.Box (new Rect (560 * wratio+35, 350 * hratio+30, wratio, hratio), "Item: " + items [selected], item_wording);
		
		//draws cash
		GUI.Label(new Rect(560 * wratio - 40, 350 * hratio , 100, 20), "Cash : " + cashCount.ToString("##0.00"), item_wording);

		//draws the item description
		GUI.Label (new Rect (Screen.width-235,200 * hratio,200,200), GUI.tooltip, "box");
	}

}
