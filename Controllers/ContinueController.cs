using UnityEngine;
using System.Collections;

public class ContinueController : MonoBehaviour {
	
	private Texture2D button;
	private Texture2D buttonBack;
	public GameObject score;
	private int i;
	private Game game;
	
	// Use this for initialization
	void Start () {
		button = Resources.Load("UI/pause/buttonHighlight") as Texture2D;
		buttonBack = Resources.Load("UI/instructions/button_back") as Texture2D;
		i = 101;
		Game game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
		score.GetComponent<GUIText>().text = "Score: " + string.Format("{0:000000000}",GameObject.FindGameObjectWithTag("Game")
		                                                               .GetComponent<Game>().score.ToString());
		Debug.Log(game);
	}
	
	// Update is called once per frame
	void Update () {
		Game game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
		Debug.Log(game);
		score.GetComponent<GUIText>().text = "Score: " + string.Format("{0:000000000}",game.score.ToString());
		
		if (i < 100) {
			game.score -= 100;
			i++;
			if (i==99) {
				game.lives = 5;
				Application.LoadLevel(1);
			}
		}
	}
	
	void OnGUI () {
		GUI.skin.button.normal.background = buttonBack;
		GUI.skin.button.hover.background = button;
		GUI.skin.button.active.background = null;
		
		if(GUI.Button(new Rect(255, 300, button.width, button.height),"Yes")) {
			if(i > 100) {
				i = 0;
			}
		}
		
		if(GUI.Button(new Rect(555, 300, button.width, button.height),"No")) {
			Destroy(GameObject.FindGameObjectWithTag("Game"));
			Application.LoadLevel(0);
		}
	}
}
