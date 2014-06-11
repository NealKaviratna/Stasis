using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	
	public Transform target;
	private float trackSpeed;
	public Collider NoTrackZone;
	
	
	// Set target
	public void SetTarget(Transform t) {
		target = t;
		trackSpeed = 14;
	}
	
	// Track target
	void LateUpdate() {
		// Move towards the coordinates of the 
		// target transform position at trackspeed
		trackSpeed = 10;
		Vector3 pos = Input.mousePosition;
		pos.z = transform.position.z - Camera.main.transform.position.z;
		pos = Camera.main.ScreenToWorldPoint(pos);

		Vector3 midpoint = transform.position;
		if (!NoTrackZone.bounds.Contains(pos)) {
			Debug.Log(NoTrackZone.gameObject.transform.localScale);
			NoTrackZone.gameObject.transform.localScale = new Vector3 (14.36587f, 6.63505f, 159.2349f);
			Debug.Log(NoTrackZone.gameObject.transform.localScale);
			midpoint.Set(pos.x + (target.position.x - pos.x) / 4,
			                      pos.y + (target.position.y - pos.y) / 4,
			                      transform.position.z);

			if ( midpoint.y < 5f) {
				midpoint.Set(midpoint.x,
				             5,
				             transform.position.z);
			}

			if ( midpoint.x - target.position.x > 6) {
				midpoint.Set(target.position.x + 6,
				             midpoint.y,
				             transform.position.z);
			} else if ( midpoint.x - target.position.x < -6) {
				midpoint.Set(target.position.x - 6,
				             midpoint.y,
				             transform.position.z);
			}
			if ( midpoint.y - target.position.y > 5) {
				midpoint.Set(midpoint.x,
				             target.position.y + 5,
				             transform.position.z);
			}
			if ( midpoint.y - target.position.y < -5) {
				midpoint.Set(midpoint.x,
				             target.position.y - 5,
				             transform.position.z);
			}
		} else {
			NoTrackZone.gameObject.transform.localScale = new Vector3 (16.36587f, 8.63505f, 159.2349f);
			midpoint.Set(target.position.x,
			             target.position.y,
			             transform.position.z);
		}

		Player play = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		if (play.isDead) {
			trackSpeed = 150;
			if (transform.position.x==midpoint.x) {
				play.isDead = false;
				play.hP = 120;
			}
		}
		if (play.rigidbody2D.velocity.y < -5) {
			trackSpeed = 20;
		}
		if(play.transform.position.y > 5) {
			transform.position = Vector3.MoveTowards (transform.position, midpoint, trackSpeed * Time.deltaTime);
		}
	}
}