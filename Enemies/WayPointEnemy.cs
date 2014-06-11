using UnityEngine;
using System.Collections;

public class WayPointEnemy : Enemy {

	public Transform wayPoint1;
	public Transform wayPoint2;
	private bool hitLeft;

	// Use this for initialization
	void Start () {
		this.hP = 20;
		hitLeft = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(this.hP <= 0) {
			this.die();
		}
		if(transform.position.x > wayPoint1.position.x && !hitLeft) {
			rigidbody2D.velocity = new Vector2(-7.0f,0.0f);
		}
		else if(transform.position.x < wayPoint2.position.x) {
			if(!hitLeft) {
				Flip ();
			}
			rigidbody2D.velocity = new Vector2(7.0f,0.0f);
			hitLeft = true;
		}
		else {
			Flip();
			hitLeft = false;
		}
	}

	void Flip() {
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void die() {
		Debug.Log("I'm dead!");
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag=="Player") {
			// Calculate knockback and damage if collider is player

			//Vector2 knockback =col.transform.position - transform.position;
			//rigidbody2D.AddForce(knockback*100);
			col.collider.gameObject.GetComponent<Player>().dmg(type);
		}
	}
}
