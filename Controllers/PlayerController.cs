using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Transform Respawn;
	public Transform defaultRespawn;
	public Transform checkPoint1;
	public Transform checkPoint2;
	public float maxSpeed = 10f;
	public bool facingRight = true;
	public float jumpForce = 700;
	private bool ghost = false; //able to move through platforms
	private double ghostTimer = 0.0;

	public bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	private int microForce = 300;

	Animator anim;

	void Start () {
		Respawn = defaultRespawn;
		anim = GetComponent<Animator>();
		anim.SetBool("shoot",false);
	}

	void Update() {
		// Handle jump input and shooting animations

		if(anim.GetBool("shoot"))
			anim.SetBool("shoot", false);
		if(grounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || 
		   Input.GetKeyDown(KeyCode.Space)) && !gameObject.GetComponent<Player>().isDead) {
			// Add jumping force
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}
		if((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && grounded 
		   && !gameObject.GetComponent<Player>().isDead) {
			// Add microforce to fall through platform
			rigidbody2D.AddForce(new Vector2(0, microForce));
		}
		// Handle movement through one way platforms
		Physics2D.IgnoreLayerCollision(9,12,ghost||rigidbody2D.velocity.y > -.1||Input.GetKey(KeyCode.S)
		                               || Input.GetKey(KeyCode.DownArrow));
		if(ghost) {
			ghostTimer += Time.deltaTime;
			if(ghostTimer > 0.1) {
				ghost = false;
				ghostTimer = 0.0;
			}
		}
	}

	public Transform getRespawn() {
		return Respawn;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Check if grounded through use of groundCheck transform in scene
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

		//Handle left/right movement
		if (!gameObject.GetComponent<Player>().isDead) {
			float move = Input.GetAxis("Horizontal");
			rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

			if(move > 0  && !facingRight)
				Flip();
			else if(move < 0 && facingRight)
				Flip();
		}
		else {
			
			rigidbody2D.velocity = new Vector2(0,0);;
		}
	}

	void Flip() {
		// Flip the  character to face the other direction

		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D(Collider2D other) {
		// Respawn and checkpoint functionality by Peilin Li
		if(other.tag == "checkPoint1") {
			Respawn.position = checkPoint1.position;
			Debug.Log("were here");
			other.gameObject.GetComponent<Animator>().SetBool("checkpoint", true);
		}
		if(other.tag == "checkPoint2") {
			Respawn.position = checkPoint2.position;
			other.gameObject.GetComponent<Animator>().SetBool("checkpoint", true);
		}

		if(other.tag == "platform") {
			if(rigidbody2D.velocity.y > -1 || !grounded) {
				ghost = true;
				ghostTimer = 0.0;
			}
		}
		if(other.tag == "Finish") {
			Time.timeScale = 0;
			GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().beatLevel();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.tag=="platform") {
			ghost = false;
		}
	}

	void OnCollisionEnter2D (Collision2D col) {
		if(col.collider.tag == "DeathPit" && !GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isDead) {
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().die();
		}
	}
}
