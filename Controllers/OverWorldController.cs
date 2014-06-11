using UnityEngine;
using System.Collections;

public class OverWorldController : MonoBehaviour {

	
	public int location;	
	public Collider fire;
	public Collider water;
	public Collider shock;
	public Collider earth;
	public Collider plant;

	private GameObject[] waypoints;
	private Texture2D button;
	private Texture2D buttonBack;

	// Use this for initialization
	void Start () {
		waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
		location = 0;
		GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().levelBeaten = false;

		button = Resources.Load("UI/pause/buttonHighlight") as Texture2D;
		buttonBack = Resources.Load("UI/instructions/button_back") as Texture2D;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown(KeyCode.A)) {
			location--;
			if(location==-1) {
				location = 4;
			}
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.D)) {
			location++;
			if(location==5) {
				location = 0;
			}
		}
		if ((Input.GetKeyDown(KeyCode.KeypadEnter)||Input.GetKeyDown(KeyCode.Return)) && IsUnlocked(location)) {
			Application.LoadLevel(location+2);
		}
		transform.position = waypoints[location].transform.position;

		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			Vector3 pos = Input.mousePosition;
			pos.z = transform.position.z - Camera.main.transform.position.z;
			pos = Camera.main.ScreenToWorldPoint(pos);
			if (fire.bounds.Contains(pos)) {
				location = 1;
			} else if (water.bounds.Contains(pos)) {
				location = 2;
			} else if (shock.bounds.Contains(pos)) {
				location = 3;
			} else if (earth.bounds.Contains(pos)) {
				location = 4;
			} else if (plant.bounds.Contains(pos)) {
				location = 0;
			}
		}
	}

	public bool IsUnlocked(int loc) {
		Game game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
		return game.levelsUnlocked > loc;
	}

	void OnGUI () {
		GUI.skin.button.normal.background = buttonBack;
		GUI.skin.button.hover.background = button;
		GUI.skin.button.active.background = null;
		
		if (GUI.Button(new Rect(30, 500, button.width, button.height),"Menu")) {
			Application.LoadLevel(0);
		}
		if (IsUnlocked(location)) {
			if (GUI.Button(new Rect(994-button.width, 500, button.width, button.height),"Start Level"))
				Application.LoadLevel(location+2);
		}
		else {
			GUI.Button(new Rect(994-button.width, 500, button.width, button.height),"Level Locked");
		}
	}
}
