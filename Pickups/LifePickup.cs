using UnityEngine;
using System.Collections;

public class LifePickup : MonoBehaviour {
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag.Equals ("Player")) {
			coll.gameObject.SendMessage("addLife", 1);
			GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().score += 100;
			Destroy (gameObject);
		}
	}
}
