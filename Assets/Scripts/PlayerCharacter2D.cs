using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter2D : MonoBehaviour {

	public int speed;
	public float shotSpeed;
	public Transform positionLeft;
	public Transform positionRight;
	public GameObject bullet;
	public SpriteRenderer sr;
	public Animator anim;
	public float jumpForce;
	public Rigidbody2D rig;
	bool isGrounded;
	bool shotTimer;
	float shootTimer=0.2f;

	// Use this for initialization
	void Start () {
		isGrounded = true;
		shotTimer = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(shotTimer){
			shootTimer -= Time.deltaTime;
		}
		if(shootTimer<=0){
			shotTimer = false;
			anim.SetBool ("isShooting",false);
			shootTimer = 0.2f;
		}
		Movement ();
		if (isGrounded) {
			anim.SetBool ("isGrounded", true);
		} else {
			anim.SetBool ("isGrounded", false);
		}
		if (rig.velocity.y < 0) {
			rig.velocity += Vector2.up * Physics2D.gravity.y * 2 * Time.deltaTime;
		}
	}
	void Movement()
	{
		if(Input.GetMouseButtonDown(0)){
			if(!shotTimer){
				if (!sr.flipX) {
					GameObject newBullet = Instantiate (bullet, positionLeft.position, transform.rotation);
					newBullet.GetComponent<Rigidbody2D> ().velocity = Vector2.right * -shotSpeed;
				} else {
					GameObject newBullet = Instantiate (bullet, positionRight.position, transform.rotation);
					newBullet.GetComponent<Rigidbody2D> ().velocity = Vector2.right * shotSpeed;

				}
				anim.SetBool ("isShooting",true);
				shotTimer = true;
			}
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			if (isGrounded) {
				rig.AddForce (Vector2.up * jumpForce);
				anim.SetBool ("isShooting",false);
				isGrounded = false;
			}
		}
		if (Input.GetKey (KeyCode.D)) {//turn right
			anim.SetBool ("isShooting",false);
			anim.SetBool ("isWalking", true);
			transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
			sr.flipX = true;
		} else if (Input.GetKey (KeyCode.A)) {//turn left
			anim.SetBool ("isShooting",false);
			anim.SetBool ("isWalking", true);
			transform.Translate (new Vector3 (-speed * Time.deltaTime, 0, 0));
			sr.flipX = false;
		} else {
			anim.SetBool ("isWalking",false);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag=="Ground"){
			isGrounded = true;
			anim.SetBool ("isGrounded",true);
		}
	}
}
