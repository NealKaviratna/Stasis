using UnityEngine;
using System.Collections;

public class LevelLockUI : MonoBehaviour {
	
	public GameObject contr;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(contr.GetComponent<OverWorldController>().IsUnlocked(contr.GetComponent<OverWorldController>().location)) {
			if(contr.GetComponent<OverWorldController>().IsUnlocked(contr.GetComponent<OverWorldController>().location+1)) {
				this.guiText.text = "Complete";
			}
			else {
				this.guiText.text = "Incomplete";
			}
		}
		else {
			this.guiText.text = "Incomplete";
		}
	}
}
