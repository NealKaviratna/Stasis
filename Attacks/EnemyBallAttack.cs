using UnityEngine;
using System.Collections;
using StasisElements;

public class EnemyBallAttack : Attack {
	// Use this for initialization
	public float speedFactor;
	public bool hit;
	public float emissionRate;
	private float deathTimer;
	private bool safety;
	
	void Start () {
		//		renderer.sortingLayerName = "Attacks";
		//		SpriteRenderer[] s = this.GetComponentsInChildren<SpriteRenderer>();
		//		for(int i = 0; i < s.Length; i++){
		//			s[i].renderer.sortingLayerName = "Attacks";
		//		}
		emissionRate = 120;
		deathTimer = 0.0f;
		safety = false;
		speedFactor = .15f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!hit){
			transform.position += transform.TransformDirection(Vector3.up)*speedFactor;
		}
		else{
			transform.localEulerAngles = new Vector3(0,0,0);
			ParticleEmitter[] p = this.GetComponentsInChildren<ParticleEmitter>();
			for(int i = 0; i < p.Length; i++){
				p[i].minEmission = emissionRate;
				p[i].maxEmission = emissionRate;
				//Debug.Log (emissionRate);
			}
			if(emissionRate < -1000){
				Destroy(gameObject);
			}
			else{
				emissionRate -= 10;
			}
		}
		
		//Detects if the ball has flown off the screen.  If it has, begin the countdown to destroy it.
		Camera mainCam = GameObject.FindWithTag ("MainCamera").GetComponent<Camera> ();
		if ((this.transform.position.x < mainCam.ScreenToWorldPoint (Vector3.zero).x ||
		     this.transform.position.x > mainCam.ScreenToWorldPoint (new Vector3 (Screen.width, 0, 0)).x ||
		     this.transform.position.y < mainCam.ScreenToWorldPoint (Vector3.zero).y ||
		     this.transform.position.y > mainCam.ScreenToWorldPoint (new Vector3 (0, Screen.height, 0)).y) 
		    && deathTimer == 0) {
			deathTimer = 1.4f;
			safety = true;
		}
		
		if (deathTimer > 0) {
			deathTimer -= Time.deltaTime;
			if(deathTimer <= 0) {
				Destroy(gameObject);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag=="Player") {
			Destroy (gameObject.collider2D);
			hit = true;
			SpriteRenderer sprite = this.GetComponentInChildren<SpriteRenderer>();
			Destroy (sprite);
			col.gameObject.GetComponent<Player>().dmg(Element.Earth);
		}
		else {
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag=="Environ" && !safety) {
			other.gameObject.GetComponent<Environment>().dmg(type);
		}
	}
}
