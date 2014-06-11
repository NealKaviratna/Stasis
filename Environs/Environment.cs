using UnityEngine;
using System.Collections;
using StasisElements;

public class Environment : Targetable {

	public bool isDead;
	
	// Use this for initialization
	void Start () {
		this.hP = 10;
		isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(this.hP <= 0) {
			this.die();
		}
	}
	
	public new void dmg(Element atkType) {
		// If the atkType is one more or four less than, it is super effective (See StasisElements:Enum Elements)
		if(atkType-type == 1 || atkType-type == -4) {
			this.hP -= 10;
		}
		else if(atkType==type) {
			this.hP += 0;
		}
		else {
			this.hP -= 0;
		}
	}
	
	void die() {
		Debug.Log("I'm dead!");
		isDead = true;
	}
}
