using UnityEngine;
using System.Collections;
using StasisElements;

public class NSBerries : Environment {	
	// Use this for initialization
	private bool onGround;

	private bool deathCheck;

	void Start () {
		isDead = false;
		onGround = false;
		Animator anim = GetComponent<Animator>();
		anim.SetBool("dead", false);
	}
	
	// Update is called once per frame
	void Update () {
		if(this.hP <= 0 && !isDead) {
			this.die();
		}
	}

	void FixedUpdate(){
		if(isDead && !onGround){
			gameObject.rigidbody2D.gravityScale = 0.5f;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if((col.tag=="ground" || col.tag=="platform") && isDead) {
			gameObject.rigidbody2D.gravityScale = 0.0f;
			gameObject.rigidbody2D.velocity = new Vector2(0,0);
			this.onGround = true;
		}
	}
	
	void die() {
		// Set the animator boolean dead to true
		// so the death animation plays

		if(!isDead) {
			GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().evil += 2;
			GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().score -= 200;
			GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().environsDestroyed++;
			GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().apq.add(tag);
		}

		Animator anim = GetComponent<Animator>();
		isDead = true;
		anim.SetBool("dead",true);

		BoxCollider2D[] b = this.GetComponentsInChildren<BoxCollider2D>();
		for(int i = 0; i < b.Length; i++){
			if(b[i].size.x == 1.5)
				b[i].enabled = true;
			else
				b[i].enabled = false;
		}
	}
}
