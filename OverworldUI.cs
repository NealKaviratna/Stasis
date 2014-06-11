using UnityEngine;
using System.Collections;

public class OverworldUI : MonoBehaviour {

	public GameObject contr;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch(contr.GetComponent<OverWorldController>().location) {
		case 0:
			this.guiText.text = "If a Tree Falls?";
			break;
		case 1:
			this.guiText.text = "The Roof! The Roof!";
			break;
		case 2:
			this.guiText.text = "Water Water\nEverywhere...";
			break;
		case 3:
			this.guiText.text = "...Strikes Twice";
			break;
		case 4:
			this.guiText.text = "Heaven and...";
			break;
		default:
			this.guiText.text = "Stasis";
			break;
		}
	}
}
