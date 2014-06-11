using UnityEngine;
using System.Collections;
using StasisElements;

public class BombAttack : Attack {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.rigidbody2D.velocity = new Vector2(0,-10);
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.collider.tag=="Player") {
			Destroy (gameObject);
			// Deal double-damage
			col.gameObject.GetComponent<Player>().dmg(Element.Earth);
		}
		else if (col.collider.tag=="Enemy") {
			// Do nothing
		}
		else {
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag=="Environ") {
			other.gameObject.GetComponent<Environment>().dmg(type);
		}
	}
}
