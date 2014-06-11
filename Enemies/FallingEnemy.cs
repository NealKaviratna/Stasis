using UnityEngine;
using System.Collections;

public class FallingEnemy : Enemy {

	public Transform wayPoint1;
	public Transform wayPoint2;
	private bool hitLeft;
	public GameObject healthBar;
	private bool landed = false;

	private float movementSpeed = 0;

	// Use this for initialization
	void Start () {
		this.hP = 20;
		hitLeft = false;
	}

	public void setMovementSpeed(float ms) {
		movementSpeed = ms;
	}
	
	// Update is called once per frame
	void Update () {

		if(this.hP <= 0) {
			this.die();
		}
		healthBar.transform.localScale = new Vector3(this.hP*5.0f,1.0f,.1f);
		if(transform.position.x > wayPoint1.position.x && !hitLeft) {
			rigidbody2D.velocity = new Vector2(-movementSpeed,rigidbody2D.velocity.y);
		}
		else if(transform.position.x < wayPoint2.position.x) {
			if(!hitLeft) {
				Flip ();
			}
			rigidbody2D.velocity = new Vector2(movementSpeed,rigidbody2D.velocity.y);
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
		GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().score += 500;
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D col) {
		if ((col.collider.tag == "ground" || col.collider.tag == "platform") && !landed) {
			movementSpeed=7.0f;
			landed = true;
			GameObject play = GameObject.FindGameObjectWithTag("Player");
			float factor = play.GetComponent<Transform>().position.x - gameObject.transform.position.x;
			play.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000*factor,1000));
			play.GetComponent<Player>().dmg(type);
		}
		if(col.collider.tag=="Player") {
			// Calculate knockback and damage if collider is player

			//Vector2 knockback =col.transform.position - transform.position;
			//rigidbody2D.AddForce(knockback*100);
			col.collider.gameObject.GetComponent<Player>().dmg(type);
		}
	}
}
