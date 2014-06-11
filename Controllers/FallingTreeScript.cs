using UnityEngine;
using System.Collections;

public class FallingTreeScript : MonoBehaviour {

	public Rigidbody2D tree;

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.tag.Equals("Fireball") && !GetComponentInChildren<Animator>().GetBool("dead")) {
			GetComponentInChildren<Animator>().SetBool("dead",true);
			tree.angularVelocity = 40f;
			GetComponentInChildren<Collider2D>().enabled = true;
		} 
		else if (coll.collider.tag.Equals("TreeBridge")) {
			gameObject.layer = 12;
			tree.angularVelocity = 0;
		}
	}
}
