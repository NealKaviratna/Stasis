using UnityEngine;
using System.Collections.Generic;

public class Instructions : MonoBehaviour {

	Texture ring1, ring2, current;
	Texture2D button, buttonBack;
	Font f;

	int state = 1;

	//float rotateVal = 0;

	public MainMenuScript menu;
	void Start () {
		//ring1 = Resources.Load("UI/instructions/ring") as Texture;
		//ring2 = Resources.Load("UI/instructions/ring") as Texture;
		current = Resources.Load("UI/instructions/pg1") as Texture;
		button = Resources.Load("UI/instructions/button") as Texture2D;
		buttonBack = Resources.Load("UI/instructions/button_back") as Texture2D;
		f = Resources.Load("UI/Dosis-Regular") as Font;

		menu.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		//rotateVal += Time.deltaTime * 2;
	}

	void OnGUI() {
		
		GUI.skin.button.normal.background = (Texture2D)buttonBack;
		GUI.skin.button.hover.background = button;
		GUI.skin.button.active.background = null;
		GUI.skin.button.font = f;
		GUI.skin.button.fontSize = 40;
		GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), current);

		if(state < 6){
			if(GUI.Button(new Rect(700, 500, button.width, button.height), "Next")){
				state++;
				changeState(state);
			}
		}
		else{
			if(GUI.Button(new Rect(700, 500, button.width, button.height), "Play")){
				Application.LoadLevel(1);
			}

		}
		if(state > 1){
			if(GUI.Button(new Rect(100, 500, button.width, button.height), "Prev")){
				state--;
				changeState(state);
			}
		}

		if(GUI.Button(new Rect(408, 515, button.width, button.height), "Main Menu"))
			Application.LoadLevel(0);
		
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
