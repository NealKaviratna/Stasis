using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {

	public int health;

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.collider2D.gameObject.tag.Equals ("Player")) {
			Debug.Log("Adding Health!");
			coll.gameObject.SendMessage("addHealth", health);
			GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().score += 100;
			Destroy (gameObject);
		}
	}
}
