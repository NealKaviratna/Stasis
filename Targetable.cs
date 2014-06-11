using UnityEngine;
using System.Collections;
using StasisElements;

public class Targetable : Damagable {

	// Use this for initialization
	void Start () {
		this.hP = 20;
	}
	
	// Update is called once per frame
	void Update () {
		if(this.hP <= 0) {
			this.die();
		}
	}

	
	void die() {
		Debug.Log("I'm dead!");
	}
}
