using UnityEngine;
using System.Collections;
using StasisElements;

public class FireballEnemy : MonoBehaviour {
	Vector3 startPos, holdVel;
	public float force;
	float timer, flashTimer;

	Animator anim;
	// Use this for initialization
	void Start () {
		startPos = transform.position;
		rigidbody2D.velocity = new Vector3(0, force, 0);
		anim = gameObject.GetComponent<Animator>();
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y <= startPos.y && rigidbody2D.velocity.y <= 0)
			rigidbody2D.velocity = new Vector3(0, force, 0);
		/*if(rigidbody2D.velocity.y <= 0){
			transform.localScale = new Vector3(1.5f, -1.5f, 1);
		}
		else{
			transform.localScale = new Vector3(1.5f, 1.5f, 1);
		}*/

		if(timer > 0){
			timer-= Time.deltaTime;
			if(timer <=1.5){
				flashTimer -= Time.deltaTime;
				if(flashTimer <= 0){
					flashTimer = 0.15f;
					anim.SetBool ("Hardened", !anim.GetBool ("Hardened"));
				}
			}
			if(timer <=0){
				timer = 0;
				collider2D.isTrigger = true;
				anim.SetBool ("Hardened", false);
				rigidbody2D.isKinematic = false;
				rigidbody2D.velocity = holdVel;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.name == "waterball(Clone)"){
			collider2D.isTrigger = false;
			anim.SetBool ("Hardened", true);
			holdVel = rigidbody2D.velocity;
			rigidbody2D.isKinematic = true;
			rigidbody2D.velocity = new Vector3(0, 0, 0);
			timer = 5;
			flashTimer = 0.15f;
		}

		else if(other.gameObject.name == "player") {
			other.gameObject.GetComponent<Player>().dmg(Element.Earth);
		}
	}
}
