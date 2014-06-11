using UnityEngine;
using System.Collections;
using StasisElements;

public class BallAttack : Attack {
	// Use this for initialization
	public Transform spawnPoint;
	public float speedFactor;
	public bool hit;
	public float emissionRate;
	private float deathTimer;
	private bool safety;

	void Start () {
		PlayerController player = GameObject.FindWithTag ("Player").GetComponent<PlayerController>();
//		renderer.sortingLayerName = "Attacks";
//		SpriteRenderer[] s = this.GetComponentsInChildren<SpriteRenderer>();
//		for(int i = 0; i < s.Length; i++){
//			s[i].renderer.sortingLayerName = "Attacks";
//		}
		emissionRate = 120;
		deathTimer = 0.0f;
		safety = false;

		// Calculate where to spawn the ball,
		// where to shoot the ball, and how to angle it

		if(player.facingRight) {
			spawnPoint = GameObject.FindWithTag("atkPoint").GetComponent<Transform>();
			this.transform.position.Set(spawnPoint.position.x,spawnPoint.position.y,-3);
			Vector3 pos = Input.mousePosition;
			pos.z = transform.position.z - Camera.main.transform.position.z;
			pos = Camera.main.ScreenToWorldPoint(pos);
			
			Quaternion q = Quaternion.FromToRotation(Vector3.up, pos - transform.position);
			this.rigidbody2D.transform.rotation = q;
		}
		else {
			spawnPoint = GameObject.FindWithTag("atkPoint").GetComponent<Transform>();
			this.transform.position.Set(spawnPoint.position.x,spawnPoint.position.y,-3);
			Vector3 pos = Input.mousePosition;
			pos.z = transform.position.z - Camera.main.transform.position.z;
			pos = Camera.main.ScreenToWorldPoint(pos);
			
			Quaternion q = Quaternion.FromToRotation(Vector3.down,transform.position - pos);
			this.rigidbody2D.transform.rotation = q;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		spawnPoint = GameObject.FindWithTag("atkPoint").GetComponent<Transform>();
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

	// If ball attack hits something damage it 
	// if it isn't a trigger (environ) delete the attack

	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag!="Fireball" && col.collider.tag!="Player") {
			hit = true;
			Destroy (gameObject.collider2D);
			SpriteRenderer sprite = this.GetComponentInChildren<SpriteRenderer>();
			Destroy (sprite);
			if(col.collider.tag=="Enemy" && !safety) {
				col.collider.gameObject.GetComponent<Damagable>().dmg(type);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag=="Environ" && !safety) {
			other.gameObject.GetComponent<Environment>().dmg(type);
		}
	}
}
