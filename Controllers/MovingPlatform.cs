using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public Transform upperLimit;
	public Transform lowerLimit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x,transform.position.y+.1f,transform.position.z);
		if(transform.position.y > upperLimit.position.y) {
			transform.position = new Vector3(transform.position.x,lowerLimit.position.y,transform.position.z);
		}
	}
}
