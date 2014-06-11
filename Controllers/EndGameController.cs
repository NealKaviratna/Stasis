using UnityEngine;
using System.Collections;

public class EndGameController : MonoBehaviour {

	public Sprite win;
	private Texture2D button;
	private Texture2D buttonBack;

	// Use this for initialization
	void Start () {
		Game game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
		if(game.evil < 250) {
			gameObject.GetComponent<SpriteRenderer>().sprite = win;
		}
		
		button = Resources.Load("UI/pause/buttonHighlight") as Texture2D;
		buttonBack = Resources.Load("UI/instructions/button_back") as Texture2D;
	}
	
	void OnGUI () {
		GUI.skin.button.normal.background = buttonBack;
		GUI.skin.button.hover.background = button;
		GUI.skin.button.active.background = null;
		Debug.Log("hey");
		if(GUI.Button(new Rect(750, 500, button.width, button.height),"Done")) {
			Application.LoadLevel(7);
		}
	}
}
