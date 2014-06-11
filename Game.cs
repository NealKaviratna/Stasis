using UnityEngine;
using System.Collections;
using Assets.Scripts.Data;


public class Game : MonoBehaviour {

	public int score;
	public int lives;
	public float evil;
	public int levelsUnlocked;
	public int enemiesKilled;
	public int environsDestroyed;
	public bool levelBeaten;
	public AdaptablePriorityQueue<string> apq;

	// Use this for initialization
	void Start () {
		Screen.SetResolution(1024, 576, false);
		DontDestroyOnLoad(transform.gameObject);
		lives = 5;
		score = 0;
		evil = 0;
		levelsUnlocked = 1;
		levelBeaten = false;
		apq = new AdaptablePriorityQueue<string>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			GameObject play = GameObject.FindGameObjectWithTag("Player");
			Debug.Log(play);
			if(play!=null) {
				lives = play.GetComponentInChildren<Player>().lives;
			}
		}
		if(levelsUnlocked==6) {
			levelsUnlocked = 7;
			Application.LoadLevel(8);
		}
		if(lives==0) {
			GameObject play = GameObject.FindGameObjectWithTag("Player");
			lives = play.GetComponentInChildren<Player>().lives;
			if(lives==0) {
				lives = 5;
				Application.LoadLevel(9);
			}
		}
	}

	public void beatLevel() {
		if(!levelBeaten) {
			score += 10000;
			levelsUnlocked++;
		}

		levelBeaten = true;
	}
}
