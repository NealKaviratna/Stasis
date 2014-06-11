using UnityEngine;
using System.Collections;

public class CreditsController : MonoBehaviour {

	private Texture2D button;
	private Texture2D buttonBack;
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		button = Resources.Load("UI/pause/buttonHighlight") as Texture2D;
		buttonBack = Resources.Load("UI/instructions/button_back") as Texture2D;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		GUI.skin.button.normal.background = buttonBack;
		GUI.skin.button.hover.background = button;
		GUI.skin.button.active.background = null;
		
		if(GUI.Button(new Rect(405, 500, button.width, button.height),"Menu")) {
			Application.LoadLevel(0);
		}
	}
}
