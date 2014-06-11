using UnityEngine;
using System.Collections;
using StasisElements;

public class Lava : Environment {	
	// Use this for initialization
	void Start () {
		isDead = false;
		Animator anim = GetComponent<Animator>();
		anim.SetBool("dead", false);
	}
	
	// Update is called once per frame
	void Update () {
		if(this.hP <= 0) {
			this.die();
		}
	}
	
	void die() {
		// Set the animator boolean dead to true
		// so the death animation plays

		if(!isDead) {
			GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().evil += 10;
			GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().score -= 300;
			GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().environsDestroyed++;
			GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().apq.add(tag);
		}

		Animator anim = GetComponent<Animator>();
		isDead = true;
		anim.SetBool("dead",true);
	}
}
