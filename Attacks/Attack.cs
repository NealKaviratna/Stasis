using UnityEngine;
using System.Collections;
using StasisElements;

public class Attack : MonoBehaviour {

	public Element type;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

//	void OnTriggerEnter2D(Collider2D col) {
//		if(col.tag=="Enemy" || col.tag=="Environ") {
//			col.gameObject.GetComponent<Damagable>().dmg(type);
//		}
//	}
}
