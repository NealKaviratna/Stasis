using UnityEngine;
using System.Collections;

public class ArmController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Game game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
		if(!UI.paused && !game.levelBeaten) {
			// Find the player controller and 
			// the mouse position on the screen in pixels

			PlayerController player = GameObject.FindWithTag ("Player").GetComponent<PlayerController>();
			Vector3 pos = Input.mousePosition;

			// Math to get the vector from the 
			// shoulder pivot to the mouse, 
			// depending on if you are facing right or left

			Quaternion q;
			if(player.facingRight) {
				pos.z = transform.position.z - Camera.main.transform.position.z;
				pos = Camera.main.ScreenToWorldPoint(pos);
				q = Quaternion.FromToRotation(Vector3.down, pos - transform.position);
			}
			else {
				pos.z = transform.position.z - Camera.main.transform.position.z;
				pos = Camera.main.ScreenToWorldPoint(pos);
				q = Quaternion.FromToRotation(Vector3.down,pos - transform.position);
				q = Quaternion.Inverse(q);
			}
			this.transform.rotation = q;
		}
	}
}
