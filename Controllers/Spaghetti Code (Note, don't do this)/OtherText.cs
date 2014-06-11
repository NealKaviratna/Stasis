using UnityEngine;
using System.Collections;

public class OtherText : MonoBehaviour {

	private int correctText;

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
	}
}
