using UnityEngine;
using System.Collections;

public class BombEnemy : Enemy {
	
	public Transform patrolPoint1;
	public Transform patrolPoint2;
	public Transform areaLimitLeft;
	public Transform areaLimitRight;
	public Transform spawnPoint;
	public float range;
	public GameObject healthBar;

	public bool facingRight;
	private bool hitLeft;
	private States state;
	private float coolDown;

	private enum States {
		Patrol,
		Bombing
	}
	
	// Use this for initialization
	void Start () {
		this.hP = 20;
		hitLeft = false;
	}
	
	// Update is called once per frame
	void Update() {
		if(this.hP <= 0) {
			this.die();
		}

		healthBar.transform.localScale = new Vector3(this.hP*5.0f,1.0f,.1f);

		if(transform.localScale.x==1) {
			facingRight = false;
		}
		else {
			facingRight = true;
		}

		switch(state) {
			case States.Patrol:
				state = Waypoint();
				break;
			case States.Bombing:
			state = Bombing();
				break;
		}
	}


	States Waypoint () {
		if(transform.position.x > patrolPoint1.position.x && !hitLeft) {
			rigidbody2D.velocity = new Vector2(-7.0f,0.0f);
		}
		else if(transform.position.x < patrolPoint2.position.x) {
			rigidbody2D.velocity = new Vector2(7.0f,0.0f);
			hitLeft = true;
		}
		else {
			hitLeft = false;
		}

		GameObject player = GameObject.FindWithTag ("Player");

		if (rigidbody2D.velocity.Equals(new Vector2(7.0f,0.0f)) && !facingRight) {
			facingRight = true;
			Flip ();
		}
		else if(rigidbody2D.velocity.Equals(new Vector2(-7.0f,0.0f)) && facingRight) {
			facingRight = false;
			Flip();
		}

		if (Mathf.Abs(player.transform.position.x - transform.position.x) < range
		    && player.transform.position.y < transform.position.y) {
			return States.Bombing;
		}

		return States.Patrol;
	}
	
	void Flip() {
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	States Bombing() {
		GameObject player = GameObject.FindWithTag ("Player");
		
		if((transform.position.x > areaLimitLeft.position.x)
		   && (player.transform.position.x - transform.position.x < -.5)) {
			rigidbody2D.velocity = new Vector2(-7.0f,0.0f);
		}
		else if((transform.position.x < areaLimitRight.position.x)
	        && (player.transform.position.x - transform.position.x > .5)) {
			rigidbody2D.velocity = new Vector2(7.0f,0.0f);
		}
		else {
			rigidbody2D.velocity = new Vector2(0.0f,0.0f);
		}
		
		if (rigidbody2D.velocity.Equals(new Vector2(7.0f,0.0f)) && !facingRight) {
			facingRight = true;
			Flip ();
		}
		else if(rigidbody2D.velocity.Equals(new Vector2(-7.0f,0.0f)) && facingRight) {
			facingRight = false;
			Flip();
		}
		
		coolDown -= Time.deltaTime;
		if(coolDown < 0 && Mathf.Abs(player.transform.position.x - transform.position.x) < .5) {
			coolDown = 3.0f;
			Bomb ();
		}
		
		if (Mathf.Abs(player.transform.position.x - transform.position.x) > range+3
		    || player.transform.position.y > transform.position.y) {
			return States.Patrol;
		}
		
		return States.Bombing;
	}

	void Bomb() {
		switch(type) {
		case StasisElements.Element.Plant:
			Instantiate(Resources.Load("prefabs/attacks/plantBomb"), 
            new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity);
			break;
		case StasisElements.Element.Fire:
			Instantiate(Resources.Load("prefabs/attacks/fireBomb"), 
            new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity);
			break;
		case StasisElements.Element.Water:
			Instantiate(Resources.Load("prefabs/attacks/WaterBomb"), 
            new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity);
			break;
		case StasisElements.Element.Shock:
			Instantiate(Resources.Load("prefabs/attacks/LightningBomb"), 
            new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity);
			break;
		case StasisElements.Element.Earth:
			Instantiate(Resources.Load("prefabs/attacks/EarthBomb"), 
            new Vector3(spawnPoint.position.x,spawnPoint.position.y,0.0f), Quaternion.identity);
			break;
		}
	}
	
	public void die() {
		GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().enemiesKilled++;
		GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().score += 5000;
		GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().apq.add(tag);
		Destroy(gameObject);
	}
}
