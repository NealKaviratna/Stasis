using UnityEngine;
using System.Collections;

public class SpeechBubbleController : MonoBehaviour {

	public float onTimer;
	public float offTimer;
	public float requiredEvil;
	public float maxEvil;

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().evil < maxEvil
		   && GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().evil > requiredEvil) {
			Camera mainCam = GameObject.FindWithTag ("MainCamera").GetComponent<Camera> ();
			if ((this.transform.position.x > mainCam.ScreenToWorldPoint (Vector3.zero).x &&
			     this.transform.position.x < mainCam.ScreenToWorldPoint (new Vector3 (Screen.width, 0, 0)).x &&
			     this.transform.position.y > mainCam.ScreenToWorldPoint (Vector3.zero).y &&
			     this.transform.position.y < mainCam.ScreenToWorldPoint (new Vector3 (0, Screen.height, 0)).y)) {
				onTimer -= Time.deltaTime;
				if(onTimer <0.0f) {
					GetComponent<MeshRenderer>().enabled = true;
					offTimer -= Time.deltaTime;
					if (offTimer < 0.0) {
						Destroy(gameObject);
					}
				}
			}
		}
		else {
			GetComponent<MeshRenderer>().enabled = false;
		}
	}
}
