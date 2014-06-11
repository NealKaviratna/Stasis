using UnityEngine;
using System.Collections;

public class Poison : MonoBehaviour {

	private float time;
	private float hurtTime;
	public GameObject end;
	public Sprite fixedT;

	// Use this for initialization
	void Start () {
		time = 0;
		hurtTime = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > hurtTime) {
			time = 0.0f;
			gameObject.GetComponent<Player>().hP--;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Teleporter") {
			Destroy(other.gameObject);
			end.GetComponent<SpriteRenderer>().sprite = fixedT;
			end.GetComponent<BoxCollider2D>().enabled = true;
		}
	}
}
