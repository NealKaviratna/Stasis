using UnityEngine;
using System.Collections;
using StasisElements;

public class PlantCreature : Environment {	
	// Use this for initialization
	public Transform wayPoint1;
	public Transform wayPoint2;
	private bool hitLeft = false;

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
		if(!isDead){
			if(transform.position.x > wayPoint1.position.x && !hitLeft) {
				rigidbody2D.velocity = new Vector2(-2.0f,0.0f);
			}
			else if(transform.position.x < wayPoint2.position.x) {
				if(!hitLeft) {
					Flip ();
				}
				rigidbody2D.velocity = new Vector2(2.0f,0.0f);
				hitLeft = true;
			}
			else {
				Flip();
				hitLeft = false;
			}
		}

	}
	
	void die() {
		// Set the animator boolean dead to true
		// so the death animation plays
		TextMesh[] children = GetComponentsInChildren<TextMesh>();
		foreach(TextMesh child in children) {
			Destroy(child.gameObject);
		}

		if(!isDead) {
			GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().evil += 5;
			GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().score -= 500;
			GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().environsDestroyed++;
			GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().apq.add(tag);
		}
		
		Animator anim = GetComponent<Animator>();
		isDead = true;
		anim.SetBool("dead",true);
		rigidbody2D.velocity = new Vector2(0,0);
	}
	
	void Flip() {
		Vector3 theScale = transform.localScale;
		if (hitLeft) {
			transform.position = transform.position+new Vector3(1f,0,0);
		}
		else {
			transform.position = transform.position+new Vector3(-1f,0,0);
		}
		
		Transform[] objs = GetComponentsInChildren<Transform>();
		foreach(Transform tran in objs) {
			Vector3 subscale = tran.localScale;
			subscale.x *=-1;
			tran.localScale = subscale;
		}

		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
