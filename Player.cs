using UnityEngine;
using System.Collections;
using StasisElements;

public class Player : Damagable {

	public Element selected;
	public float knockbackFactor;
	public bool isDead;
	public int lives;
	public AudioSource noise;

	// Use this for initialization
	void Start () {
		this.hP = 101;
		this.lives = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().lives;
	}
	
	// Update is called once per frame
	void Update () {
		if(this.hP <= 0 && !isDead) {
			this.die();
		}
		else if(this.hP > 101) {
			this.hP = 101;
		}
		Game game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();

		if (!UI.paused) {
			if(Input.GetKeyDown(KeyCode.Mouse0) && !game.levelBeaten) {
				atk();
			}
			if(Input.GetKeyDown(KeyCode.Mouse1) && !game.levelBeaten) {
				change();
			}
			if (Input.GetAxis("Mouse ScrollWheel") > 0) {
				selected++;
				if ((int)selected==5)
					selected = Element.Plant;
			}
			else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
				selected--;
				if ((int)selected==-1)
					selected = Element.Earth;
			}

			if (Input.GetKeyDown(KeyCode.Alpha1))
				selected = Element.Fire;
			if (Input.GetKeyDown(KeyCode.Alpha2))
				selected = Element.Water;
			if (Input.GetKeyDown(KeyCode.Alpha3))
				selected = Element.Shock;
			if (Input.GetKeyDown(KeyCode.Alpha4))
				selected = Element.Earth;
			if (Input.GetKeyDown(KeyCode.Alpha5))
				selected = Element.Plant;
		}
	}

	void atk() {
		// Fire a different attack method based on the
		// currently selected element

		// Check if the target is in front of the arm-gun

		noise.Play();
		Vector3 mousPos = Input.mousePosition;
		mousPos.z = transform.position.z - Camera.main.transform.position.z;
		mousPos = Camera.main.ScreenToWorldPoint(mousPos);
		if(!GetComponentInChildren<SphereCollider>().bounds.Contains(mousPos)) {
			switch(selected) {
			case Element.Plant:
				plantBall();
				break;
			case Element.Fire:
				fireBall();
				break;
			case Element.Water:
				waterBall();
				break;
			case Element.Shock:
				shockBall();
				break;
			case Element.Earth:
				earthBall();
				break;
			}
		}
	}

	void change(){
		// Switch to the next element

		switch(selected) {
		case Element.Plant:
			selected=Element.Fire;
			break;
		case Element.Fire:
			selected=Element.Water;
			break;
		case Element.Water:
			selected=Element.Shock;
			break;
		case Element.Shock:
			selected=Element.Earth;
			break;
		case Element.Earth:
			selected=Element.Plant;
			break;
		}
	}


	// The following Five methods all instantiate
	// different attacks in the correct place on screen
	
	void fireBall() {
		PlayerController controller = GameObject.FindWithTag ("Player").GetComponent<PlayerController>();
		Transform spawnPoint = GameObject.FindWithTag("atkPoint").GetComponent<Transform>();

		if(controller.facingRight) {
			Instantiate(Resources.Load("prefabs/attacks/fireball"), 
			new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity);
		}
		else {
			Instantiate(Resources.Load("prefabs/attacks/fireball"), 
			new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity);
		}
	}

	void waterBall() {
		PlayerController controller = GameObject.FindWithTag ("Player").GetComponent<PlayerController>();
		Transform spawnPoint = GameObject.FindWithTag("atkPoint").GetComponent<Transform>();
		
		if(controller.facingRight) {
			Instantiate(Resources.Load("prefabs/attacks/waterball"), 
				new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity);
		}
		else {
			Instantiate(Resources.Load("prefabs/attacks/waterball"), 
				new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity);
		}
	}

	void shockBall() {
		PlayerController controller = GameObject.FindWithTag ("Player").GetComponent<PlayerController>();
		Transform spawnPoint = GameObject.FindWithTag("atkPoint").GetComponent<Transform>();
		
		if(controller.facingRight) {
			Instantiate(Resources.Load("prefabs/attacks/lightningBall"), 
				new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity);
		}
		else {
			Instantiate(Resources.Load("prefabs/attacks/lightningBall"), 
				new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity);
		}
	}

	void earthBall() {
		PlayerController controller = GameObject.FindWithTag ("Player").GetComponent<PlayerController>();
		Transform spawnPoint = GameObject.FindWithTag("atkPoint").GetComponent<Transform>();
		
		if(controller.facingRight) {
			Instantiate(Resources.Load("prefabs/attacks/earthBall"), 
			    new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity);
		}
		else {
			Instantiate(Resources.Load("prefabs/attacks/earthBall"), 
				new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity);
		}
	}

	void plantBall() {
		PlayerController controller = GameObject.FindWithTag ("Player").GetComponent<PlayerController>();
		Transform spawnPoint = GameObject.FindWithTag("atkPoint").GetComponent<Transform>();
		
		if(controller.facingRight) {
			Instantiate(Resources.Load("prefabs/attacks/plantBall"), 
				new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity);
		}
		else {
			Instantiate(Resources.Load("prefabs/attacks/plantBall"), 
				new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity);
		}
	}

	//Adds health to the player. Called by HealthPickup script.
	public void addHealth(int amount) {
		this.hP += amount;
	}

	public void addLife(int amount) {
		this.lives += amount;
	}

	public new void dmg(Element type) {
		// Add knockback and reduce health upon taking damage

		//rigidbody2D.AddForce(new Vector2(0,200));
		//rigidbody2D.velocity = knockback*knockbackFactor;
		hP -= 15;
	}
	
	public void die() {
		//Switch to death animation/physics
		
		GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().score -= 1000;
		GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().lives--;
		GameObject play = GameObject.FindGameObjectWithTag("Player");
		play.transform.position = play.GetComponent<PlayerController>().getRespawn().position;
		this.lives--;
//		rigidbody2D.AddForce(new Vector2(0, 1000));
//		CircleCollider2D collider1 = GameObject.FindWithTag ("Player").GetComponent<CircleCollider2D>();
//		BoxCollider2D collider2 = GameObject.FindWithTag ("Player").GetComponent<BoxCollider2D>();
//		Rigidbody2D rigid = GameObject.FindWithTag ("Player").GetComponent<Rigidbody2D>();
//
//		collider1.isTrigger = false;
//		collider2.isTrigger = false;
//		rigid.isKinematic = true;
		isDead = true;
	}
}
