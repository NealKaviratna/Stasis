using UnityEngine;
using System.Collections;
using StasisElements;

public class Damagable : MonoBehaviour {

	public Element type;
	public int hP;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(this.hP <= 0) {
			this.die();
		}
	}

	public virtual void dmg(Element atkType) {
		// If the atkType is one more or four less than, it is super effective (See StasisElements:Enum Elements)

		if(atkType-type == 1 || atkType-type == -4) {
			this.hP -= 10;
		}
		else if(atkType==type) {
			this.hP += 3;
		}
		else {
			this.hP -= 3;
		}
	}

	void die() {
		Debug.Log("I'm dead!");
	}
}
