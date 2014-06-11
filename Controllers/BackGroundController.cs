using UnityEngine;
using System.Collections;

public class BackGroundController : MonoBehaviour {

	public Transform cameraPos;
	public float distanceFactor;
	private Vector3 cameraLast;

	// Use this for initialization
	void Start () {
		cameraLast = cameraPos.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x - (cameraPos.position.x - cameraLast.x)*distanceFactor,
		                       transform.position.y - (cameraPos.position.y -cameraLast.y)*distanceFactor,
								transform.position.z
		);
		cameraLast = cameraPos.position;
	}
}
