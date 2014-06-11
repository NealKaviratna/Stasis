using UnityEngine;
using System.Collections.Generic;

public class MainMenuScript : MonoBehaviour {
	
	Texture ring, title, options;
	Texture2D button, buttonBack;
	Font f;

	public GameObject fireBG;
	public GameObject plantBG;
	public GameObject lightningBG;
	public GameObject earthBG;
	public GameObject waterBG;
	
	float rotateVal = 0;
	
	public Instructions instructions;
	private bool showOptions = false;
	
	// Use this for initialization
	void Start () {
		ring = Resources.Load("UI/mainmenu/ring") as Texture;
		title = Resources.Load("UI/mainmenu/title") as Texture;
		options = Resources.Load("UI/mainmenu/options") as Texture;
		button = Resources.Load("UI/mainmenu/button") as Texture2D;
		buttonBack = Resources.Load("UI/instructions/button_back") as Texture2D;
		f = Resources.Load("UI/Dosis-Regular") as Font;
		
		instructions.enabled = false;
		
		float gen = Random.value;
		
		if (gen < 0.2) {
			fireBG.SetActive(enabled);
		} else if(gen < .4){
			plantBG.SetActive(enabled);
		} else if(gen < .6){
			lightningBG.SetActive(enabled);
		} else if(gen < .8){
			earthBG.SetActive(enabled);
		} else {
			waterBG.SetActive(enabled);
		}
	}
	
	// Update is called once per frame
	void Update () {
		rotateVal += Time.deltaTime * 2;
		transform.Translate(new Vector3(0.02f, 0f));
	}
	
	void OnGUI() {
		GUI.skin.button.normal.background = null;
		GUI.skin.button.hover.background = button;
		GUI.skin.button.active.background = null;
		GUI.skin.button.font = f;
		GUI.skin.button.fontSize = 30;
		if(!showOptions){
			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), title);
			
			if(GUI.Button(new Rect(408, 240, button.width, button.height), "Play"))
				Application.LoadLevel(1);
			if(GUI.Button(new Rect(408, 301, button.width, button.height), "Help"))
				instructions.enabled = true;;
			if(GUI.Button(new Rect(408, 362, button.width, button.height), "Options"))
				showOptions = true;
			if(GUI.Button(new Rect(408, 423, button.width, button.height), "Credits"))
				Application.LoadLevel(7);
			
			GUI.BeginGroup(new Rect(0f, 0f, Screen.width, Screen.height));
			GUIUtility.RotateAroundPivot(rotateVal, new Vector2(Screen.width/2, Screen.height/2));
			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), ring);
			GUIUtility.RotateAroundPivot(-rotateVal, new Vector2(Screen.width/2, Screen.height/2));
			GUI.EndGroup();
		}
		else{
			GUI.skin.button.normal.background = (Texture2D)buttonBack;
			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), options);
			if(GUI.Button(new Rect(408, 500, button.width, button.height), "Back"))
				showOptions = false;
		}
	}
}
