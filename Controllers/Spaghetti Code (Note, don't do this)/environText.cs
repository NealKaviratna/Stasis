using UnityEngine;
using System.Collections;

public class environText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.guiText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		Game game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
		if (game.levelBeaten) {
			this.guiText.enabled = true;
		}

		this.GetComponent<GUIText>().text = game.environsDestroyed.ToString();
	}
}
