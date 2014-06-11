using UnityEngine;
using System.Collections;

public class ShooterEnemy : Enemy {
	
	public Transform patrolPoint1;
	public Transform patrolPoint2;
	public Transform areaLimitLeft;
	public Transform areaLimitRight;
	public float range;
	public GameObject healthBar;
	public Transform spawnPoint;
	public GameObject arm;
	public bool frozen;
	
	private bool hitLeft;
	private bool facingRight;
	private states state;
	private float coolDown;
	
	private enum states {
		Patrol,
		Shooting
	}
	
	// Use this for initialization
	void Start () {
		this.hP = 20;
		hitLeft = false;
		coolDown = 1.0f;
		state = states.Patrol;
	}
	
	// Update is called once per frame
	void Update() {
		if(this.hP <= 0) {
			this.die();
		}
		healthBar.transform.localScale = new Vector3(this.hP*5.0f,1.0f,.1f);
		if(transform.localScale.x==-1) {
			facingRight = false;
		}
		else {
			facingRight = true;
		}

		switch(state) {
		case states.Patrol:
			state = Waypoint();
			break;
		case states.Shooting:
			state = shooting();
			break;
		}
	}
	
	
	states Waypoint () {
		if(!frozen) {
			if(transform.position.x > patrolPoint1.position.x && !hitLeft) {
				rigidbody2D.velocity = new Vector2(-7.0f,0.0f);
			}
			else if(transform.position.x < patrolPoint2.position.x) {
				if(!hitLeft) {
					Flip ();
				}
				rigidbody2D.velocity = new Vector2(7.0f,0.0f);
				hitLeft = true;
			}
			else {
				Flip();
				hitLeft = false;
			}
		}

		GameObject player = GameObject.FindWithTag ("Player");

		if (Mathf.Abs(player.transform.position.x - transform.position.x) < range) {
			return states.Shooting;
		}

		return states.Patrol;
	}
	
	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	states shooting() {
		GameObject player = GameObject.FindWithTag ("Player");

		if((transform.position.x > areaLimitLeft.position.x) && !frozen
		   && (player.transform.position.x - transform.position.x < -5)) {
			rigidbody2D.velocity = new Vector2(-3.0f,0.0f);
		}
		else if((transform.position.x < areaLimitRight.position.x) && !frozen
	        && (player.transform.position.x - transform.position.x > 5)) {
			rigidbody2D.velocity = new Vector2(3.0f,0.0f);
		}
		else {
			rigidbody2D.velocity = new Vector2(0.0f,0.0f);
		}

		if (player.transform.position.x - transform.position.x < 0 && facingRight) {
			facingRight = false;
			Flip ();
		}
		else if(player.transform.position.x - transform.position.x > 0 && !facingRight) {
			facingRight = true;
			Flip();
		}


		// Find the player and track him with arm transform.
		arm.transform.LookAt(player.transform.position);
		if (facingRight) {
			arm.transform.rotation = new Quaternion(arm.transform.rotation.x,
		                                        	arm.transform.rotation.y,
		                                        	0,
		                                        	0);
		}
		else {
			arm.transform.rotation = new Quaternion(arm.transform.rotation.x,
			                                        -1*arm.transform.rotation.y,
			                                        0,
			                                        0);
		}

		coolDown -= Time.deltaTime;
		if(coolDown < 0) {
			coolDown = 3.0f;
			shoot();
		}

		if (Mathf.Abs(player.transform.position.x - transform.position.x) > range+3) {
			return states.Patrol;
		}

		return states.Shooting;
	}

	void shoot() {
		switch(type) {
			case StasisElements.Element.Plant:
				if(facingRight) {
					GameObject plantball = Instantiate(Resources.Load("prefabs/attacks/enemyPlantBall"), 
					            new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity) as GameObject;
					plantball.transform.Rotate(Vector3.forward, (180 + 98.2f - arm.transform.eulerAngles.z));
				}
				else {
					GameObject plantball = Instantiate(Resources.Load("prefabs/attacks/enemyPlantBall"), 
					            new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity) as GameObject;
					plantball.transform.Rotate(Vector3.forward, (arm.transform.eulerAngles.z + 82.2f));
				}
				break;
			case StasisElements.Element.Fire:
				if(facingRight) {
					GameObject plantball = Instantiate(Resources.Load("prefabs/attacks/enemyFireBall"), 
					                                   new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity) as GameObject;
					plantball.transform.Rotate(Vector3.forward, (180 + 98.2f - arm.transform.eulerAngles.z));
				}
				else {
					GameObject plantball = Instantiate(Resources.Load("prefabs/attacks/enemyFireBall"), 
					                                   new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity) as GameObject;
					plantball.transform.Rotate(Vector3.forward, (arm.transform.eulerAngles.z + 82.2f));
				}
				break;
			case StasisElements.Element.Water:
				if(facingRight) {
					GameObject plantball = Instantiate(Resources.Load("prefabs/attacks/enemyWaterBall"), 
					                                   new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity) as GameObject;
					plantball.transform.Rotate(Vector3.forward, (180 + 98.2f - arm.transform.eulerAngles.z));
				}
				else {
					GameObject plantball = Instantiate(Resources.Load("prefabs/attacks/enemyWaterBall"), 
					                                   new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity) as GameObject;
					plantball.transform.Rotate(Vector3.forward, (arm.transform.eulerAngles.z + 82.2f));
				}
				break;
			case StasisElements.Element.Shock:
				if(facingRight) {
					GameObject plantball = Instantiate(Resources.Load("prefabs/attacks/enemyLightningBall"), 
					                                   new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity) as GameObject;
					plantball.transform.Rotate(Vector3.forward, (180 + 98.2f - arm.transform.eulerAngles.z));
				}
				else {
					GameObject plantball = Instantiate(Resources.Load("prefabs/attacks/enemyLightningBall"), 
					                                   new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity) as GameObject;
					plantball.transform.Rotate(Vector3.forward, (arm.transform.eulerAngles.z + 82.2f));
				}
				break;
			case StasisElements.Element.Earth:
				if(facingRight) {
					GameObject plantball = Instantiate(Resources.Load("prefabs/attacks/enemyEarthBall"), 
					                                   new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity) as GameObject;
					plantball.transform.Rotate(Vector3.forward, (180 + 98.2f - arm.transform.eulerAngles.z));
				}
				else {
					GameObject plantball = Instantiate(Resources.Load("prefabs/attacks/enemyEarthBall"), 
					                                   new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity) as GameObject;
					plantball.transform.Rotate(Vector3.forward, (arm.transform.eulerAngles.z + 82.2f));
				}
				break;
		}
	}
	
	public void die() {
		GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().score += 1000;
		GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().enemiesKilled++;
		GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().apq.add(tag);
		Destroy(gameObject);
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag=="Player") {
			// Calculate knockback and damage if collider is player
			
			//Vector2 knockback =col.transform.position - transform.position;
			//rigidbody2D.AddForce(knockback*100);
			col.collider.gameObject.GetComponent<Player>().dmg(type);
		}
	}
}
