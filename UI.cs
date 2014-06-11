using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {
	
	Texture fire, earth, lightning, water, plant, healthBar, pauseMenu, current;
	Texture2D button, buttonBack;
	Player player;
	Font f;

	int state;
	public int score;
	public string level;
	public int lives;
	
	private string scoreString;
	private bool negScore = false;

	public static bool paused;
	private bool instruct;
	
	// Use this for initialization
	void Start () {
		state = 1;
		paused = false;
		instruct = false;
		buttonBack = Resources.Load("UI/instructions/button_back") as Texture2D;
		current = Resources.Load("UI/instructions/pg1") as Texture;
		fire = Resources.Load("UI/fire_UI") as Texture;
		earth = Resources.Load("UI/earth_UI") as Texture;
		lightning = Resources.Load("UI/lightning_UI") as Texture;
		water = Resources.Load("UI/water_UI") as Texture;
		plant = Resources.Load("UI/plant_UI") as Texture;
		healthBar = Resources.Load("UI/healthBar") as Texture;
		player = GameObject.FindWithTag("Player").GetComponent<Player>();
		f = Resources.Load("UI/Dosis-Regular") as Font;
		pauseMenu = Resources.Load("UI/pause/pauseMenu") as Texture;
		button = Resources.Load("UI/pause/buttonHighlight") as Texture2D;
		lives = player.lives;
		negScore = false;

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape)) {
			paused = !paused;
		}

		if(paused || GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().levelBeaten) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}

		score = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().score;
		negScore = score < 0;
		if (negScore) {
			scoreString = string.Format("{0:000000000}",score).Substring(1);
		} else {
			scoreString = string.Format("{0:000000000}",score);
		}
	}
	
	void OnGUI() {
		GUI.skin.button.normal.background = null;
		GUI.skin.button.hover.background = button;
		GUI.skin.button.active.background = null;
		if(paused && !instruct) {
			Debug.Log("here");
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), pauseMenu);
			if(GUI.Button(new Rect(405, 175, button.width, button.height),"")) {
				paused = false;
			}
			if(GUI.Button(new Rect(405, 261, button.width, button.height),"")) {
				instruct = true;
				button = Resources.Load("UI/mainmenu/button") as Texture2D;
				GUI.skin.button.fontSize = 40;
			}
			if(GUI.Button(new Rect(405, 347, button.width, button.height),"")) {
				Debug.Log("and here");
				Application.LoadLevel("Overworld");
			}
		} else if(instruct){
			GUI.skin.button.normal.background = buttonBack;
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), current);
			if(state < 6){
				if(GUI.Button(new Rect(700, 500, button.width, button.height), "Next")){
					state++;
					changeState(state);
				}
			}
			else{
				if(GUI.Button(new Rect(700, 500, button.width, button.height), "Return")){
					instruct = false;
					button = Resources.Load("UI/pause/buttonHighlight") as Texture2D;
				}
				
			}
			if(state > 1){
				if(GUI.Button(new Rect(100, 500, button.width, button.height), "Prev")){
					state--;
					changeState(state);
				}
			}

		} else {
			lives = player.lives;
			GUI.skin.label.font = f;
			if(player.selected == StasisElements.Element.Fire)
				GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), fire);
			else if(player.selected == StasisElements.Element.Earth)
				GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), earth);
			else if(player.selected == StasisElements.Element.Plant)
				GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), plant);
			else if(player.selected == StasisElements.Element.Shock)
				GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), lightning);
			else if(player.selected == StasisElements.Element.Water)
				GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), water);

			GUI.BeginGroup(new Rect(0f, 0f, 360f - 355*(1 - player.hP/100f), Screen.height));
			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), healthBar);
			GUI.EndGroup();

			GUI.skin.label.fontSize = 43;
			GUI.skin.label.alignment = TextAnchor.UpperLeft;
			if (negScore) {
				GUI.skin.label.normal.textColor = Color.red;
			} else {
				GUI.skin.label.normal.textColor = Color.white;
			}
			GUI.Label(new Rect(3f, -8f, 1000f, 100f), scoreString);
			GUI.skin.label.normal.textColor = Color.white;
			GUI.skin.label.alignment = TextAnchor.UpperRight;
			GUI.skin.label.fontSize = 40;
			GUI.Label(new Rect(-3f, -8f, Screen.width, Screen.height), level);
			GUI.skin.label.alignment = TextAnchor.UpperLeft;
			GUI.skin.label.fontSize = 27;
			GUI.Label(new Rect(40f, Screen.height - 90f, 1000f, 100f), "x" + lives.ToString());
		}
	}

	void changeState(int state){
		switch(state){
		case 1:
			current = Resources.Load("UI/instructions/pg1") as Texture;
			break;
			
		case 2:
			current = Resources.Load("UI/instructions/pg2") as Texture;
			break;
			
		case 3:
			current = Resources.Load("UI/instructions/pg3") as Texture;
			break;
			
		case 4:
			current = Resources.Load("UI/instructions/pg4") as Texture;
			break;
			
		case 5:
			current = Resources.Load("UI/instructions/pg5") as Texture;
			break;

		case 6:
			current = Resources.Load("UI/instructions/pg6") as Texture;
			break;
		}
	}
}
