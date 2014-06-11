using UnityEngine;
using System.Collections;

public class scoreText : MonoBehaviour {

	private int temp;
	private Texture2D button;
	private Texture2D buttonBack;
	// Use this for initialization
	void Start () {
		this.guiText.enabled = false;
		temp = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().score;

		button = Resources.Load("UI/pause/buttonHighlight") as Texture2D;
	
		buttonBack = Resources.Load("UI/instructions/button_back") as Texture2D;
	}
	
	// Update is called once per frame
	void Update () {
		Game game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
		if (game.levelBeaten) {
			this.guiText.enabled = true;

			if(temp + 1111< game.score)
				temp+= 1111;
			else {
				temp = game.score;
			}
		}
		this.GetComponent<GUIText>().text = temp.ToString();
	}

	void OnGUI() {
		GUI.skin.button.normal.background = buttonBack;
		GUI.skin.button.hover.background = button;
		GUI.skin.button.active.background = null;
		
		Game game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
		if (game.levelBeaten) {
			if(GUI.Button(new Rect(405, 400, button.width, button.height),"Next")) {
				Time.timeScale = 1;
				Application.LoadLevel("Overworld");
			}
		}
	}
}
