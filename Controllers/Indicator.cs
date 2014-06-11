using UnityEngine;
using System.Collections;
using StasisElements;

public class Indicator : MonoBehaviour {

	public Element type;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// If this element's type is selected
		// cause this object's renderer to display

		SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer>();
		Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
		if(player.selected==type){
			rend.enabled = true;
		}
		else {
			rend.enabled = false;
		}
	}
}
